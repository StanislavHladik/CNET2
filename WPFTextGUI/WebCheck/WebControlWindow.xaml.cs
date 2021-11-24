using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPFTextGUI.Webcheck
{
    /// <summary>
    /// Interaction logic for WebControlWindow.xaml
    /// </summary>
    public partial class WebControlWindow : Window
    {
        public WebControlWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var url = txtUrl.Text;
            var term = txtTerm.Text;

            if(Webs.WebsToCheck.TryAdd(url, true))
            {
                WebCheck wc = new WebCheck(url, term);
                WebCheckWindow wcw = new WebCheckWindow(wc);
                wcw.Show();
            }

            txtUrl.Text = txtTerm.Text = "";
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            var url = txtUrl.Text;

            var success = Webs.WebsToCheck.TryUpdate(url, false, true);

            if (!success)
            {
                MessageBox.Show("failed to stop " + url, "Error", MessageBoxButton.OK);
            }

        }
    }
}
