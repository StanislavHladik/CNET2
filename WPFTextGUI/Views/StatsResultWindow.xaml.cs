using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFTextGUI.Model;

namespace WPFTextGUI.Views
{
    /// <summary>
    /// Interaction logic for StatsResultWindow.xaml
    /// </summary>
    public partial class StatsResultWindow : Window
    {
        public StatsResultWindow(Model.StatsResult result)
        {
            InitializeComponent();

            DataContext = result;
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
           StatsResult result = (StatsResult)DataContext;

            var apiUrl = "https://localhost:7045/";

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(apiUrl);

            var res = await httpClient.PostAsJsonAsync<StatsResult>("/stats", result);

            if (res.IsSuccessStatusCode)
                this.Close();
            else MessageBox.Show("Chyba");
        }
    }
}
