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
        private double CalMedia() {
            double media = 0;
            media = (double.Parse(TBcn.Text) + double.Parse(TBch.Text) + double.Parse(TBlc.Text) + double.Parse(TBmath.Text) + double.Parse(TBredacao.Text)) / 5;
            return media;

        }
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
                Redacao = double.Parse(TBredacao.Text),
                Media = CalMedia(),
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
                Redacao = double.Parse(TBredacao.Text),
                Media = CalMedia(),
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
            LV1.ItemsSource = obj.ToList().OrderBy(x => x.Nome);
        }

        private async void btnPesquisarUF_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("API/GetEscUF/" + TbUFpesq.Text);
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Escola> obj = JsonConvert.DeserializeObject<List<Models.Escola>>(str);
            LV2.ItemsSource = obj.ToList().OrderBy(x => x.Nome);
        }

        private async void btnListarPorArea_Click(object sender, RoutedEventArgs e)
        {
            var comboBoxItem = CBarea.Items[CBarea.SelectedIndex] as ComboBoxItem;
            string selectedcmb = string.Empty;
            if (comboBoxItem != null)
            {
               selectedcmb = comboBoxItem.Content.ToString();
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/api/escola");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Escola> obj = JsonConvert.DeserializeObject<List<Models.Escola>>(str);
            if (selectedcmb == "Ciências Humanas")
            {
                LV3.ItemsSource = obj.ToList().OrderBy(x => x.CH);
            }
            if (selectedcmb == "Ciências da Natureza")
            {
                LV3.ItemsSource = obj.ToList().OrderBy(x => x.CN);
            }
            if (selectedcmb == "Linguagens")
            {
                LV3.ItemsSource = obj.ToList().OrderBy(x => x.LC);
            }
            if (selectedcmb == "Matemática")
            {
                LV3.ItemsSource = obj.ToList().OrderBy(x => x.Matematica);
            }
            if (selectedcmb == "Redação")
            {
                LV3.ItemsSource = obj.ToList().OrderBy(x => x.Redacao);
            }
            if (selectedcmb == "Média Geral")
            {
                LV3.ItemsSource = obj.ToList().OrderByDescending(x => x.Media);
            }
        }
    }
}
