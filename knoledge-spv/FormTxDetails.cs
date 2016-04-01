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
    public partial class FormTxDetails : Form
    {
        KnoledgeTransaction _transaction;
        public FormTxDetails(KnoledgeTransaction Transaction = null)
        {
            InitializeComponent();

            _transaction = Transaction;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormTxDetails_Load(object sender, EventArgs e)
        {
            labelBlockId.Text = _transaction.BlockId;
            labelTxID.Text = _transaction.TransactionId;
            labelStatus.Text = _transaction.Confirmations + " confirmations";
            labelAmount.Text = _transaction.GetBalanceString();
            labelDate.Text = _transaction.Date;

            KeyId hashOut = (KeyId)_transaction.Transaction.Transaction.Outputs[0].ScriptPubKey.GetDestination();

            if (hashOut == null)
            {
                labelTo.Text = "unknown";
            }
            else
            {
                BitcoinAddress addressTo = new BitcoinAddress(hashOut, Network.TestNet);
                labelTo.Text = addressTo.ToString();
            }


            KeyId hashIn = (KeyId)_transaction.Transaction.Transaction.Outputs[1].ScriptPubKey.GetDestination();

            if (hashIn == null)
            {
                labelFrom.Text = "unknown";
            }
            else
            {
                BitcoinAddress addressFrom = new BitcoinAddress(hashIn, Network.TestNet);
                labelFrom.Text = addressFrom.ToString();
            }
        }
    }
}
