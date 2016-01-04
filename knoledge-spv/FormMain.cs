using NBitcoin;
using NBitcoin.Protocol;
using NBitcoin.Protocol.Behaviors;
using NBitcoin.SPV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knoledge_spv
{
    public partial class FormMain : Form
    {

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        bool _isClosing = false;
        bool _connecting = false;
        bool _disposed = false;
        object _padlock = new object();
        KnoledgeNodesGroup _group;
        Node _node;
        NodeConnectionParameters _connectionParameters;
        ConcurrentChain _chain;
        ConcurrentChain _localChain;
        CancellationTokenSource _cts = new CancellationTokenSource();
        IPAddress _localIPAddress = null;
        IPAddress _oldIPAddress = null;
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public FormMain()
        {
            InitializeComponent();
        }

        private Network Network
        {
            get { return Network.TestNet; }
        }

        private bool LocalConnection
        {
            get { return localToolStripMenuItem.Checked; }
        }

        private string AppDir
        {
            get { return Directory.GetParent(this.GetType().Assembly.Location).FullName; }
        }

        private string AddrmanFile
        {
            get { return Path.Combine(AppDir, "addrman.dat"); }
        }

        private string TrackerFile
        {
            get {return Path.Combine(AppDir, "tracker.dat");}
        }

        private string ChainFile
        {
            get { return Path.Combine(AppDir, "chain.dat"); }
        }

        private void CheckLocalToolStripMenu(bool check)
        {
            if (_isClosing)
                return;

            MethodInvoker method = delegate
            {
                localToolStripMenuItem.Checked = check;
            };

            if (this.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void UpdateInfoButton(ChainStatus status, string text)
        {
            if (_isClosing) return;

            MethodInvoker method = delegate
            {
                switch (status)
                {
                    case ChainStatus.OutofDate:
                        buttonInfo.ToolTipText = text;
                        buttonInfo.Image = Properties.Resources.dialog_warning_4;
                        break;
                    case ChainStatus.UptoDate:
                        buttonInfo.ToolTipText = text;
                        buttonInfo.Image = Properties.Resources.dialog_clean;
                        break;
                    default:
                        buttonInfo.ToolTipText = text;
                        buttonInfo.Image = Properties.Resources.dialog_question;
                        break;
                }

            };

            if (statusStrip.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void UpdateStatusLabel(string text)
        {
            if (_isClosing) return;

            MethodInvoker method = delegate
            {
                labelStatus.Text = text;
            };

            if (statusStrip.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void UpdateStatusButton(string text, int count)
        {
            if (_isClosing) return;

            MethodInvoker method = delegate
            {
                buttonStatus.ToolTipText = text;

                if (count > 4) count = 4;

                switch (count)
                {
                    case 4:
                        buttonStatus.Image = Properties.Resources.network_wireless_full;
                        break;
                    case 3:
                        buttonStatus.Image = Properties.Resources.network_wireless_high;
                        break;
                    case 2:
                        buttonStatus.Image = Properties.Resources.network_wireless_medium;
                        break;
                    case 1:
                        buttonStatus.Image = Properties.Resources.network_wireless_low;
                        break;
                    default:
                        buttonStatus.Image = Properties.Resources.network_wireless_none;
                        break;
                }
            };

            if (statusStrip.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void EnableMenus(bool local, bool connect, bool disconnect)
        {
            MethodInvoker method = delegate
            {
                localToolStripMenuItem.Enabled = local;
                connectToolStripMenuItem.Enabled = connect;
                disconnectToolStripMenuItem.Enabled = disconnect;
            };

            if (menuStrip.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private IPAddress GetLocalIP()
        {
            IPAddress address = null;

            try
            {
                UdpClient u = new UdpClient("bitcoin.org", 1);
                address = ((IPEndPoint)u.Client.LocalEndPoint).Address;
            }
            catch { }

            return address;
        }

        private void UpdateUIAsNotConnected()
        {
            CheckLocalToolStripMenu(true);
            EnableMenus(false, true, false);

            UpdateUIAsync();
        }

        private Task UpdateUIAsync()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                UpdateUI();
            });

            return t;
        }

        private void UpdateUI()
        {
            int nodes = 0;

            if (_group != null && !CanConnect())
                nodes = _group.ConnectedNodes.Count;

            if (nodes == 1)
                UpdateStatusButton(string.Format("Connected to {0} node on {1}", nodes, Network.ToString()), nodes);
            else
                UpdateStatusButton(string.Format("Connected to {0} nodes on {1}", nodes, Network.ToString()), nodes);

            int height = (_chain == null || _chain.Tip == null) ? 0 : _chain.Height;
            int localHeight = (_localChain == null || _localChain.Tip == null) ? 0 : _localChain.Height;
            string chainStatus = string.Format("Local chain height = {0}", localHeight);

            if (localHeight != 0)
            {
                chainStatus = string.Format("{0}{1} Latest Block Time = {2}", chainStatus, Environment.NewLine, _localChain.Tip.Header.BlockTime.ToLocalTime().ToString("R"));
            }

            if (nodes > 0)
            {
                if (height == 0 || height > localHeight)
                {
                    UpdateStatusLabel("Synchronising...");
                    UpdateInfoButton(ChainStatus.OutofDate, chainStatus);
                }
                else if (height == localHeight)
                {
                    UpdateStatusLabel(string.Empty);
                    UpdateInfoButton(ChainStatus.UptoDate, chainStatus);
                }
            }
            else
            {
                if (_connecting)
                    UpdateStatusLabel(string.Format("Trying to connect to {0}...", Network.ToString()));
                else
                    UpdateStatusLabel("Not connected...");

                UpdateInfoButton(ChainStatus.OutofDate, chainStatus);
            }
        }

        public bool CanConnect()
        {
            if (_isClosing || _connecting)
                return false;

            if (_group != null && _group.ConnectedNodes.Count >= 1)
            {
                Node node = _group.ConnectedNodes.FirstOrDefault(n => n.State == NodeState.Connected ||
                                                                    n.State == NodeState.HandShaked);
                if (node != null)
                {
                    return false;
                }
            }

            return true;
        }

        private void SaveChainToDisk()
        {
            int locaHeight = _localChain.Tip == null ? 0 : _localChain.Height;
            int height = _chain.Tip == null ? 0 : _chain.Height;

            if (locaHeight < height)
            {
                using (var fs = File.Open(ChainFile, FileMode.Create))
                {
                    _chain.WriteTo(fs);
                    _localChain = _chain.Clone();
                }
            }
        }

        private Task<ConcurrentChain> GetChain()
        {
            Task<ConcurrentChain> t = Task<ConcurrentChain>.Factory.StartNew(() =>
            {
                ConcurrentChain chain = new ConcurrentChain();
                try
                {
                    lock (_padlock)
                    {
                        chain.Load(File.ReadAllBytes(ChainFile));
                    }
                }
                catch
                {
                    chain = new ConcurrentChain();
                }

                return chain;
            }, _cts.Token);

            return t;
        }

        public async void Connect()
        {
            EnableMenus(false, false, true);

            await ConnectAsync();
            await UpdateUIAsync();
        }

        private Tracker GetTracker()
        {
            if (_connectionParameters != null)
                return _connectionParameters.TemplateBehaviors.Find<TrackerBehavior>().Tracker;

            try
            {
                lock (_padlock)
                {
                    using (var fs = File.OpenRead(TrackerFile))
                    {
                        return Tracker.Load(fs);
                    }
                }
            }
            catch
            {
            }
            return new Tracker();
        }

        private ChainBehavior GetChainBehaviour()
        {
            if (_connectionParameters != null)
                return _connectionParameters.TemplateBehaviors.Find<ChainBehavior>();

            return new ChainBehavior(_chain);
        }

        private AddressManager GetAddressManager()
        {
            if (_connectionParameters != null)
            {
                AddressManagerBehavior behaviour = _connectionParameters.TemplateBehaviors.Find<AddressManagerBehavior>();

                if (behaviour != null)
                    return behaviour.AddressManager;

            }
            try
            {
                lock (_padlock)
                {
                    return AddressManager.LoadPeerFile(AddrmanFile);
                }
            }
            catch
            {
                return new AddressManager();
            }
        }

        public Task ConnectAsync()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                try
                {
                    lock (_padlock)
                    {
                        _connecting = true;
                    }

                    var parameters = new NodeConnectionParameters();
                    ChainBehavior chainBehave = GetChainBehaviour();
                    parameters.TemplateBehaviors.Add(chainBehave);

                    if (LocalConnection)
                    {
                        _group = GetNodesGroup(parameters);
                        _node = Node.ConnectToLocal(Network);

                        if (_chain.Tip != null && _node.Behaviors.Find<ChainBehavior>() != null)
                            _node.Behaviors.Add(chainBehave);


                        _group.ConnectedNodes.Add(_node);
                        _connectionParameters = _group.NodeConnectionParameters;
                    }
                    else
                    {
                        if (parameters.TemplateBehaviors.Find<AddressManagerBehavior>() == null)
                        {
                            AddressManagerBehavior addMan = new AddressManagerBehavior(GetAddressManager());
                            parameters.TemplateBehaviors.Add(addMan);
                        }

                        if (parameters.TemplateBehaviors.Find<TrackerBehavior>() == null)
                        {
                            TrackerBehavior tracker = new TrackerBehavior(GetTracker());
                            parameters.TemplateBehaviors.Add(tracker); 
                        }

                        _group = GetNodesGroup(parameters);
                        _group.Connect();
                        _connectionParameters = _group.NodeConnectionParameters;
                    }
                }
                catch
                {
                    EnableMenus(true, true, false);

                    if (_localIPAddress == null)
                        UpdateUIAsNotConnected();
                }
                finally
                {
                    lock (_padlock)
                    {
                        _connecting = false;
                    }
                }
            }, _cts.Token);

            return t;
        }

        private KnoledgeNodesGroup GetNodesGroup(NodeConnectionParameters parameters)
        {
            KnoledgeNodesGroup group = new KnoledgeNodesGroup(Network, parameters, new NodeRequirement()
            {
                RequiredServices = NodeServices.Network
            });

            group.StateChanged = Node_StateChanged;
            group.MessageReceived = Node_MessageReceived;
            group.Disconnected = Node_Disconnected;

            return group;
        }

        private bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }

        private void Disconnect()
        {
            EnableMenus(true, true, false);

            if (_group != null)
                _group.Disconnect();

            if (_node != null && _node.IsConnected)
                _node.Disconnect();

            if (_localIPAddress == null)
                UpdateUIAsNotConnected();

            UpdateUIAsync();
        }

        private void Node_MessageReceived(Node node, IncomingMessage message)
        {
        }

        private void Node_Disconnected(Node node)
        {
            if (CanConnect())
                Disconnect();
        }

        private void Node_StateChanged(Node node, NodeState oldState)
        {
            if (node.State == NodeState.Connected)
            {
                node.VersionHandshake();
            }
            else if (node.State == NodeState.HandShaked)
            {
                bool noChain = _chain == null || _chain.Tip == null || _localChain == null;

                if (noChain || node.PeerVersion.StartHeight > _localChain.Height)
                {
                    Task.Factory.StartNew(() =>
                    {
                        _chain = node.GetChain();
                        SaveChainToDisk();
                    }, _cts.Token);

                }
            }
        }

        private Task SaveAsync()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                lock (_padlock)
                {
                    Save();
                }
            });

            return t;
        }

        public bool Save()
        {
            bool saved = false;

            try
            {
                AddressManager addr = GetAddressManager();

                if (addr != null)
                    addr.SavePeerFile(AddrmanFile, Network);

                SaveChainToDisk();

                saved = true;
            }
            catch { }

            return saved;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            _cts.Cancel();
            Disconnect();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            NetworkChange_NetworkAddressChanged(this, new EventArgs());
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (_isClosing)
                return;

            await UpdateUIAsync();
            await SaveAsync();
        }

        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            if (IsInternetAvailable())
                _localIPAddress = GetLocalIP();
            else
                _localIPAddress = null;

            if ((_oldIPAddress == null && _localIPAddress == null) ||
                _oldIPAddress != null && _localIPAddress != null &&
                _oldIPAddress.Equals(_localIPAddress))
                return;

            if (_localIPAddress == null)
                Disconnect();
            else
                EnableMenus(true, true, false);

            _oldIPAddress = _localIPAddress;
        }

        private async void FormMain_Shown(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            _chain = await GetChain();
            _localChain = _chain.Clone();

            _timer.Interval = 5000;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            EnableMenus(true, true, false);

            if (_localIPAddress == null)
                UpdateUIAsNotConnected();
        }

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
                _cts.Cancel();

                if (_group != null)
                {
                    _group.Disconnect();
                    _group.Dispose();
                }

                if (_node != null && _node.IsConnected)
                {
                    _node.Disconnect();
                    _node.Dispose();
                }
            }

            base.Dispose(disposing);

            _disposed = true;
        }

        #endregion

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            if (!CanConnect())
            {
                MessageBox.Show("hello");
            }
        }
    }
}
