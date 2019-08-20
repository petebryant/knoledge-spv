using NBitcoin.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knoledge_spv
{
    public partial class FormNodes : Form
    {
        //KnoledgeNodesGroup _group;
        NodesGroup _group;
        ConnectedNode _node;
        
        //public FormNodes(KnoledgeNodesGroup group)
        //{
        //    InitializeComponent(); 
        //    _group = group;
        //}

        public FormNodes(NodesGroup group)
        {
            InitializeComponent();
            _group = group;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormNodes_Load(object sender, EventArgs e)
        {

        }

        private void FormNodes_Shown(object sender, EventArgs e)
        {
            if (_group == null) return;

            foreach (Node node in _group.ConnectedNodes)
            {
                ListViewItem item = new ListViewItem();
                ConnectedNode connected = new ConnectedNode(node);
                item.Text = connected.Name;
                item.Tag = connected;
                listView.Items.Add(item);
            }

            listView.Items[0].Selected = true;
            listView.Select();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                _node = listView.SelectedItems[0].Tag as ConnectedNode;

                if (_node != null)
                {
                    textBoxAt.Text = _node.ConnectedAt.ToString();
                    textBoxHeight.Text = _node.StartHeight.ToString();
                    textBoxLatency.Text = _node.Latency.ToString();
                    textBoxSeen.Text = _node.LastSeen.ToString();
                    textBoxPerf.Text = _node.PerfCounter.ToString();
                    textBoxVersion.Text = _node.Version.ToString();
                }
                else
                {
                    textBoxAt.Text = "";
                    textBoxHeight.Text = "";
                    textBoxLatency.Text = "";
                    textBoxSeen.Text = "";
                    textBoxPerf.Text = "";
                    textBoxVersion.Text = "";
                }
            }

        }
    }
}
