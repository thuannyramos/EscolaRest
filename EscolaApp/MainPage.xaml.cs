using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EscolaApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private string ip = "http://localhost:61563/";
        private async void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Escola f = new Models.Escola
            {

                Id = int.Parse(TBid.Text),
                Nome = TBnome.Text,
                UF = TBuf.Text,
                CN = double.Parse(TBcn.Text),
                CH = double.Parse(TBch.Text),
                LC = double.Parse(TBlc.Text),
                Matematica = double.Parse(TBmath.Text),
                Redacao = double.Parse(TBredacao.Text)
            };
            List<Models.Escola> fl = new List<Models.Escola>();
            fl.Add(f);
            string s = "=" + JsonConvert.SerializeObject(fl);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/api/escola/", content);
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Escola f = new Models.Escola
            {

                Id = int.Parse(TBid.Text),
                Nome = TBnome.Text,
                UF = TBuf.Text,
                CN = double.Parse(TBcn.Text),
                CH = double.Parse(TBch.Text),
                LC = double.Parse(TBlc.Text),
                Matematica = double.Parse(TBmath.Text),
                Redacao = double.Parse(TBredacao.Text)
            };
            string s = "=" + JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("/api/escola/" + f.Id, content);
        }

        private async void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/api/escola/" + TBid.Text);
        }

        private async void btnListar_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/api/escola");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Escola> obj = JsonConvert.DeserializeObject<List<Models.Escola>>(str);
            LV1.ItemsSource = obj.ToList();
        }
    }
}
