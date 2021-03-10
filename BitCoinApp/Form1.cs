using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BitCoinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetRates_Click(object sender, EventArgs e)
        {
            if (currencyCombo.SelectedItem.ToString() == "EUR")
            {
                resultLabel.Visible = true;
                resultTextBox.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result = Int32.Parse(amountTextBox.Text) * bitcoin.bpi.EUR.rate_float;
                resultTextBox.Text = $"{result.ToString()} {bitcoin.bpi.EUR.code}";
            }

            if (currencyCombo.SelectedItem.ToString() == "USD")
            {
                resultLabel.Visible = true;
                resultTextBox.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result = Int32.Parse(amountTextBox.Text) * bitcoin.bpi.USD.rate_float;
                resultTextBox.Text = $"{result.ToString()} {bitcoin.bpi.USD.code}";
            }
        }

        public static BitCoinRates GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitCoinRates bitcoin;
            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRates>(response);
            }

            return bitcoin;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
