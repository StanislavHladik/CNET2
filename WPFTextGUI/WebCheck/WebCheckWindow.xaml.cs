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
    /// Interaction logic for WebCheckWindow.xaml
    /// </summary>
    public partial class WebCheckWindow : Window
    {
        private WebCheck webcheck;
        public WebCheckWindow(WebCheck _web)
        {
            InitializeComponent();

            webcheck = _web;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtWebCheckInfo.Text = $"spouštím hledání textu {webcheck.Term} v {webcheck.Url}...{Environment.NewLine}";

            IProgress<string> progress = new Progress<string>(message =>
            {
                txtWebCheckInfo.Text += message;
            });

            webcheck.Start(progress);

        }
    }
}
