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
    public partial class FormConfirm : Form
    {
        public string Amount { get; set; }
        public string Fee { get; set; }
        public string Total { get; set; }
        public string Address { get; set; }


        public FormConfirm()
        {
            InitializeComponent();
        }

        private void FormConfirm_Load(object sender, EventArgs e)
        {
            labelFee.Text = Fee;
            labelSend.Text = Amount;
            labelTotal.Text = Total;
            labelAddress.Text = Address;
        }
    }
}
