using NBitcoin;
using NBitcoin.Protocol;
using NBitcoin.Protocol.Behaviors;
using NBitcoin.SPV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knoledge_spv
{
    public partial class FormMain : Form
    {
        bool _isSaving = false;
        bool _isClosing = false;
        bool _disposed = false;
        bool _updatingUI = false;
        object _padlock = new object();
        long _transactions = 0;
        NodesGroup _group;
        NodeConnectionParameters _connectionParameters;
        KnoledgeWallet _wallet;
        IList<KnoledgeWallet> _wallets = new List<KnoledgeWallet>();

        public FormMain()
        {
            InitializeComponent();

            LoadWallets();
        }

        private void LoadWallets()
        {
            var wallets = Common.LoadWallets(Network);

            foreach (var wallet in wallets)
            {
                wallet.Update();
                _wallets.Add(wallet);
            }

            _wallet = _wallets.FirstOrDefault();
            UpdateWalletComboBox();
        }

        private void UpdateWalletComboBox()
        {
            if (comboBoxWallet.Items.Count > 0) comboBoxWallet.Items.Clear();

            comboBoxWallet.Items.AddRange(_wallets.ToArray());
            comboBoxWallet.SelectedItem = _wallet;
        }

        private Network Network
        {
            get { return Network.TestNet; }
        }

        private string AddrmanFile
        {
            get { return Path.Combine(Common.AppDir, "addrman.dat"); }
        }

        private string ChainFile
        {
            get { return Path.Combine(Common.AppDir, "chain.dat"); }
        }

        private string TrackerFile
        {
            get { return Path.Combine(Common.AppDir, "tracker.dat"); }
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

        private void UpdateBalanceLabel(string text)
        {
            if (_isClosing) return;

            MethodInvoker method = delegate
            {
                labelBalance.Text = text;
            };

            if (labelBalance.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void UpdateFeeLabel(string text)
        {
            if (_isClosing) return;

            MethodInvoker method = delegate
            {
                labelFee.Text = text;
            };

            if (labelFee.InvokeRequired)
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

        private async void PeriodicKick()
        {
            while (!_disposed)
            {
                await Task.Delay(TimeSpan.FromMinutes(10));
                _group.Purge("For privacy concerns, will renew bloom filters on fresh nodes");
            }
        }

        private async void PeriodicSave()
        {
            while (!_disposed)
            {
                await Task.Delay(100000);
                SaveAsync();
            }
        }

        private async void PeriodicUiUpdate()
        {
            while (!_disposed)
            {
                await Task.Delay(1000);
                UpdateUI();
            }
        }

        private async void UpdateUI()
        {
            if (_isClosing)
                return;

            if (_updatingUI)
                return;

            await Task.Factory.StartNew(() =>
            {
                _updatingUI = true;
                int nodes = 0;

                if (!IsNetworkAvailable())
                {
                    UpdateStatusLabel("No network connnection.");
                }
                else
                {
                    if (_group != null && !CanConnect())
                        nodes = _group.ConnectedNodes.Count;

                    if (nodes == 1)
                        UpdateStatusButton(string.Format("Connected to {0} node on {1}", nodes, Network.ToString()), nodes);
                    else
                        UpdateStatusButton(string.Format("Connected to {0} nodes on {1}", nodes, Network.ToString()), nodes);
                }


                ConcurrentChain chain = GetChain();
                int height = chain.Tip == null ? 0 : chain.Height;
                int localHeight = 0;
                bool outOfDate = true;

                if (chain != null && chain.Tip != null)
                {
                    DateTimeOffset date = chain.Tip.Header.BlockTime.ToLocalTime();
                    DateTimeOffset now = DateTimeOffset.Now;
                    TimeSpan difference = now - date;

                    outOfDate = difference.TotalMinutes > 45;
                    localHeight = chain.Tip == null ? 0 : chain.Height;
                }

                string chainStatus = string.Format("Local chain height = {0}", localHeight);

                if (localHeight != 0)
                {
                    chainStatus = string.Format("{0}{1} Latest Block Date = {2}", chainStatus, Environment.NewLine, chain.Tip.Header.BlockTime.ToLocalTime().ToString("R"));
                }

                if (nodes > 0)
                {
                    if (height == 0 || height > localHeight)
                    {
                        outOfDate = true;
                        UpdateStatusLabel("Synchronising...");
                    }
                }

                if (outOfDate)
                    UpdateInfoButton(ChainStatus.OutofDate, chainStatus);
                else
                    UpdateInfoButton(ChainStatus.UptoDate, chainStatus);

                if (_wallet != null)
                {
                    _wallet.Update();

                    int txCount = _wallet.Transactions.Count;

                    if (_transactions != txCount)
                    {
                        UpdateListView(_wallet.Transactions);
                        UpdateBalanceLabel(_wallet.GetBalanceString());
                        _transactions = txCount;
                    }
                }

                if (_isSaving) UpdateStatusLabel("Saving...");

                _updatingUI = false;
            });
        }        

        public bool CanConnect()
        {
            if (_isClosing)
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

        private Tracker GetTracker()
        {
            if (_connectionParameters != null)
            {
                TrackerBehavior behaviour = _connectionParameters.TemplateBehaviors.Find<TrackerBehavior>();

                if (behaviour != null)
                    return behaviour.Tracker;

            }

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

        private ConcurrentChain GetChain()
        {
            if (_connectionParameters != null)
            {
                return _connectionParameters.TemplateBehaviors.Find<ChainBehavior>().Chain;
            }

            var chain = new ConcurrentChain(Network);

            try
            {
                lock (_padlock)
                {
                    chain.Load(File.ReadAllBytes(ChainFile));
                }
            }
            catch
            {
            }
            return chain;
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

        public async void StartConnecting()
        {
            await Task.Factory.StartNew(() =>
            {
                var parameters = new NodeConnectionParameters();
                parameters.TemplateBehaviors.Add(new AddressManagerBehavior(GetAddressManager())); //So we find nodes faster
                parameters.TemplateBehaviors.Add(new ChainBehavior(GetChain())); //So we don't have to load the chain each time we start
                parameters.TemplateBehaviors.Add(new TrackerBehavior(GetTracker())); //Tracker knows which scriptPubKey and outpoints to track, it monitors all your wallets at the same
                
                if (!_disposed)
                {
                    _group = new NodesGroup(Network, parameters, new NodeRequirement()
                    {
                        RequiredServices = NodeServices.Network //Needed for SPV
                    });

                    _group.Connect();
                    _connectionParameters = _group.NodeConnectionParameters;
                }
            });

            PeriodicSave();
            PeriodicUiUpdate();
            PeriodicKick();

            foreach (var wallet in _wallets)
                wallet.Connect(_connectionParameters);
        }

        private bool IsNetworkAvailable()
        {
            int description;
            return NativeCalls.InternetGetConnectedState(out description, 0);
        }

        private void SaveAsync()
        {
            if (_isClosing)
                return; 

            var unused = Task.Factory.StartNew(() =>
            {
                lock (_padlock)
                {
                    _isSaving = true;

                    GetAddressManager().SavePeerFile(AddrmanFile, Network);

                    using (var fs = File.Open(ChainFile, FileMode.Create))
                    {
                        GetChain().WriteTo(fs);
                    }

                    using (var fs = File.Open(TrackerFile, FileMode.Create))
                    {
                        GetTracker().Save(fs);
                    }

                    foreach (var wallet in _wallets)
                        wallet.Save();

                    _isSaving = false;
                }
            });
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isSaving)
            {
                MessageBox.Show(this, "Save in progress, try closing again in a moment...", "Warning" ,MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            _isClosing = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            StartConnecting();
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            if (!CanConnect())
            {
                using (FormNodes form = new FormNodes(_group))
                {
                    form.ShowDialog();
                }
            }
        }

        #region IDisposable Members

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                if (_group != null)
                {
                    _group.Disconnect();
                    _group.Dispose();
                }
            }

            base.Dispose(disposing);

            _disposed = true;
        }

        #endregion

        private void addWalletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnoledgeWallet wallet = new KnoledgeWallet(Network);
            wallet.Name = "Wallet" + _wallets.Count;

            using (FormWallet form = new FormWallet(Network, wallet))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (wallet.IsValid)
                    {
                        using (new HourGlass())
                        {
                            UpdateStatusLabel("Creating a new wallet, please wait...");
                            CreateWallet(wallet);
                            UpdateWalletComboBox();
                        }
                    }
                }
            }
        }

        internal void CreateWallet(KnoledgeWallet knoledgeWallet)
        {
            WalletCreation creation = knoledgeWallet.CreateWalletCreation();
            Wallet wallet = new Wallet(creation);
            knoledgeWallet.Set(wallet);

            _wallets.Add(knoledgeWallet);

            if (_wallet == null)
                _wallet = knoledgeWallet;

            knoledgeWallet.Save();

            if (_connectionParameters != null)
                wallet.Connect(_connectionParameters);
        }

        private void comboBoxWallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWallet.SelectedItem != null)
            {
                _wallet = (KnoledgeWallet)comboBoxWallet.SelectedItem;
                _wallet.Update();

                UpdateAddressComboBox();

                comboBoxAddress.Text = "";
                comboBoxAddress.SelectedItem = _wallet.CurrentAddress;

                UpdateListView(_wallet.Transactions);
            }
        }

        private void comboBoxAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            _wallet.CurrentAddress = (BitcoinAddress)comboBoxAddress.SelectedItem;
        }

        private void UpdateListView(List<KnoledgeTransaction> list)
        {
            MethodInvoker method = delegate
            {
                if (listView.Items.Count > 0) listView.Items.Clear();

                foreach (KnoledgeTransaction item in list)
                {
                    string[] subItems = { item.TransactionId, item.BlockId, item.Confirmations };

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = item.GetBalanceString();
                    lvi.Tag = item;
                    lvi.SubItems.AddRange(subItems);

                    listView.Items.Add(lvi);
                }
            };

            if (listView.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void createNewAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_wallet != null && (_wallet.State != WalletState.Created))
            {
                using (new HourGlass())
                {
                    UpdateStatusLabel("Creating a new address, please wait...");
                    _wallet.GetNextScriptPubKey();
                    comboBoxWallet_SelectedIndexChanged(this, new EventArgs());
                }
            }
        }

        private void UpdateAddressComboBox()
        {
            if (comboBoxAddress.Items.Count > 0) comboBoxAddress.Items.Clear();

            comboBoxAddress.Items.AddRange(_wallet.Addresses.ToArray());
        }

        private void copyAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBoxAddress.SelectedItem != null)
            {
                Clipboard.SetText(comboBoxAddress.SelectedItem.ToString());
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tx = listView.SelectedItems[0].Tag as KnoledgeTransaction;

            if (tx != null)
            {
                using (FormTxDetails form = new FormTxDetails(tx))
                {
                    form.ShowDialog();
                }
            }
        }

        private void buttonPastePayTo_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                textBoxPayTo.Text = (string)Clipboard.GetData(DataFormats.Text);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxPayTo.Text = string.Empty;
            textBoxLabel.Text = string.Empty;
            numericUpDownAmount.Value = 0;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPayTo.Text)) return;
            if (numericUpDownAmount.Value == 0) return;

            try
            {
                var payToaddress = new BitcoinAddress(textBoxPayTo.Text);
                var amount = Money.Coins(numericUpDownAmount.Value);
                var coins = _wallet.GetCoinSource();

                if (coins == null)
                {
                    MessageBox.Show(this, "Could not find any unspent coins in the selected Wallet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var txBuilder = CreateTransactionBuilder(payToaddress, amount, coins, textBoxLabel.Text);
                var transaction = txBuilder.BuildTransaction(false);
                var estimatedFees = CalcFees(txBuilder, transaction);

                txBuilder.SendFees(estimatedFees);
                var signed = SignTransaction(txBuilder);

                if (txBuilder.Verify(signed, estimatedFees))
                {
                    using (FormConfirm frm = new FormConfirm())
                    {
                        frm.Address = _wallet.CurrentAddress.ToString();
                        frm.Amount = string.Format("{0} BTC", amount.ToUnit(MoneyUnit.BTC));
                        frm.Fee = string.Format("{0} BTC", estimatedFees.ToUnit(MoneyUnit.BTC));
                        frm.Total = string.Format("Total amount {0} BTC", (amount + estimatedFees).ToUnit(MoneyUnit.BTC));

                        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (_group != null && _group.ConnectedNodes.Any())
                            {
                                var node = _group.ConnectedNodes.First();
                                // say hello
                                node.VersionHandshake();
                                // advertise the transaction, just send the hash
                                node.SendMessage(new InvPayload(NBitcoin.Protocol.InventoryType.MSG_TX, signed.GetHash()));
                                //send the transaction
                                node.SendMessage(new TxPayload(signed));
                                //wait for a bit
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "The Transaction could not be Verified for the selected Wallet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(this, "The [Pay To] address has an invalid format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NBitcoin.NotEnoughFundsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Transaction SignTransaction(TransactionBuilder txBuilder)
        {
            var signed = txBuilder.BuildTransaction(true);
            return signed;
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPayTo.Text)) return;            
            if (numericUpDownAmount.Value == 0) return;

            try
            {                
                var payToaddress = new BitcoinAddress(textBoxPayTo.Text);
                var amount = Money.Coins(numericUpDownAmount.Value);
                var coins = _wallet.GetCoinSource();

                if (coins == null)
                {
                    MessageBox.Show(this, "Could not find any unspent coins in the selected Wallet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                var txBuilder = CreateTransactionBuilder(payToaddress, amount, coins, textBoxLabel.Text);
                var transaction = txBuilder.BuildTransaction(false);
                var estimatedFees = CalcFees(txBuilder, transaction);

                txBuilder.SendFees(estimatedFees);
                var signed = SignTransaction(txBuilder);

                if (txBuilder.Verify(signed, estimatedFees))
                {
                    MessageBox.Show(signed.ToString());
                }
                else
                {
                    MessageBox.Show(this, "The Transaction could not be Verified for the selected Wallet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(this, "The [Pay To] address has an invalid format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NBitcoin.NotEnoughFundsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Money CalcFees(TransactionBuilder txBuilder, Transaction transaction)
        {
            // the fees seem a bit high...
            var estimatedFees = txBuilder.EstimateFees(transaction);
            UpdateFeeLabel(estimatedFees.ToUnit(MoneyUnit.BTC) + " BTC/kb");

            return estimatedFees;
        }

        private TransactionBuilder CreateTransactionBuilder(BitcoinAddress address, Money amount, ICoin[] coins, string message = "")
        {
            TransactionBuilder txBuilder = new TransactionBuilder();
            var keys = _wallet.GetKeysForCoins(coins);

            if (string.IsNullOrEmpty(message))
            {
                txBuilder
                    .AddCoins(coins)
                    .AddKeys(keys)
                    .Send(address.ScriptPubKey, amount)
                    .SetChange(_wallet.CurrentAddressScriptPubKey);
            }
            else 
            {
                var bytes = Encoding.UTF8.GetBytes(message);

                if (bytes.Length <= 40)
                {
                    txBuilder
                        .AddCoins(coins)
                        .AddKeys(keys)
                        .Send(address.ScriptPubKey, amount)
                        .Send(TxNullDataTemplate.Instance.GenerateScriptPubKey(bytes), Money.Zero)
                        .SetChange(_wallet.CurrentAddressScriptPubKey);
                }
            }



            return txBuilder;
        }

        private void contextMenuStripTx_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listView.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
            }
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewItem item = listView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    item.Selected = true;
                    contextMenuStripTx.Show(listView, e.Location);
                }
            }
        }
    }
}
