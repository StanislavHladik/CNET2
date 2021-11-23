using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTextGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var bookdir = @"C:\Users\S1244598\source\repos\CNET2\Books";

            //foreach (var file in GetFilesFromDir(bookdir))
            //{
            //    var dict = TextTools.TextTools.FreqAnalysis(file);
            //    var top10 = TextTools.TextTools.GetTopWords(10, dict);
            //    var fi = new FileInfo(file);

            //    txbInfo.Text += fi.Name + Environment.NewLine;

            //    foreach (var kv in top10)
            //    {
            //        txbInfo.Text += $"{kv.Key}: {kv.Key} {Environment.NewLine}";
            //    }

            //    txbInfo.Text += Environment.NewLine;

            //}

        }

        static IEnumerable<string> GetFilesFromDir(string dir)
        {
            return Directory.EnumerateFiles(dir);
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var bigFilesDir = @"C:\Users\S1244598\source\repos\CNET2\bigfiles";
            var filePath = "words01.txt";

            var files = Directory.EnumerateFiles(bigFilesDir, "*.txt");

            //var file = System.IO.Path.Combine(bigFilesDir, filePath);

            foreach (var file in files)
            {
                var wordStats = TextTools.TextTools.FreqAnalysis(file, Environment.NewLine);
                var top10 = TextTools.TextTools.GetTopWords(10, wordStats);
                var fi = new FileInfo(file);

                txbInfo.Text += fi.Name + Environment.NewLine;

                foreach (var kv in top10)
                {
                    txbInfo.Text += $"{kv.Key}: {kv.Key} {Environment.NewLine}";
                }

                txbInfo.Text += Environment.NewLine;
            }

        }
    }
}
