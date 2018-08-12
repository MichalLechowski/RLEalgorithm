using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RLE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCheckLetters_Click(object sender, EventArgs e)
        {
            txtInfo.Text = (RLECoder.ContainsOnlyLetters(txtInputString.Text)).ToString();
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            txtEncoded.Text = (RLECoder.RLEEncode(txtInputString.Text)).ToString();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            txtDecoded.Text = (RLECoder.RLEDecode(txtEncoded.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtInfo.Text = (txtDecoded.Text.Equals(txtInputString.Text)).ToString();
        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInputString_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOutputString_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
