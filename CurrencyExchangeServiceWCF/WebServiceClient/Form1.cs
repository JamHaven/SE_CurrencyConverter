using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServiceClient
{
    public partial class Form1 : Form
    {
        ServiceReference1.Service1Client SoapClient = new ServiceReference1.Service1Client();

        public Form1()
        {
            InitializeComponent();


        }

        public void FillComboBox(string[] array, ComboBox box)
        {
            foreach (string x in array)
            {
                box.Items.Add(x);
            }
        }
        private void btn_GO_Click(object sender, EventArgs e)
        {
            lbl_Result.Text = SoapClient.ConvertCurrency(com_Currency.Text, tbx_Value.Text);

        }

        private void btn_Initialize_Click(object sender, EventArgs e)
        {
            FillComboBox(SoapClient.GetCurrencyCodes(), com_Currency);
            
        }
    }
}
