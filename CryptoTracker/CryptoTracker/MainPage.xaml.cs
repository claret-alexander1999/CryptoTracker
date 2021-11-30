using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using CryptoTracker.Model;


namespace CryptoTracker
{
    public partial class MainPage : ContentPage
    {
        private string baseImageUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_64/";
        


        public MainPage()
        {
            InitializeComponent();
            coinListView.ItemsSource = GetCoins();




        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            coinListView.ItemsSource = GetCoins();
        }

        private List<Coin> GetCoins()
        {
            List<Coin> coins;
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicy) => { return true; };
            var client = new RestClient("https://rest.coinapi.io/v1/assets?filter_asset_id=BTC;ETH;ADA;XMR;LTC");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-CoinAPI-Key", "E9B6FE65-61B2-44C7-92E9-110238A2F935");

            var response = client.Execute(request);

            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach (var c in coins)
            {
                if (!String.IsNullOrEmpty(c.id_icon))
                {
                    c.icon_url = baseImageUrl + c.id_icon.Replace("-", "") + ".png";
                }
                else
                {
                    c.icon_url = "";
                }
            }

            return coins;
        }
    }




}
