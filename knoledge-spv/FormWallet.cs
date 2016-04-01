using NBitcoin;
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
    public partial class FormWallet : Form
    {
        Network _network;
        KnoledgeWallet _wallet;

        public FormWallet(Network network, KnoledgeWallet wallet)
        {
            InitializeComponent();

            _network = network;
            _wallet = wallet;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            using (new HourGlass())
            {
                WalletKey walletKey = new WalletKey(_network, _wallet);
                walletKey.Generate();

                listBoxPublicKeys.Items.Add(walletKey);

                //have limited the Keys to just one as this is an example wallet
                buttonGenerate.Enabled = false;
                buttonOk.Enabled = true;
            }
        }

        private void FormWallet_Load(object sender, EventArgs e)
        {
            textBoxName.Text = _wallet.Name;
        }

        private void FormWallet_FormClosing(object sender, FormClosingEventArgs e)
        {
            _wallet.Name = textBoxName.Text;
            _wallet.IsP2SH = checkBoxP2SH.Checked;

            int sigs = 1;
            if (int.TryParse(textBoxSigs.Text, out sigs))
                _wallet.SigRequired = sigs;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }
    }
}
