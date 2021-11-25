using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using WPFTextGUI.Model;
using WPFTextGUI.Views;
using System.Net.Http.Json;

namespace WPFTextGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string bigfilesdir = @"C:\Users\Student\Documents\BigFiles";

        static IEnumerable<string> GetBigFiles()
        {
            return Directory.EnumerateFiles(bigfilesdir, "*.txt");
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void LoadBooks()
        {
            //var bookdir = @"C:\Users\Student\source\repos\CNET2\Books";

            //foreach (var file in GetFilesFromDir(bookdir))
            //{
            //    var dict = await TextTools.TextTools.FreqAnalysisAsync(file);
            //    var top10 = TextTools.TextTools.GetTopWords(10, dict);
            //    var fi = new FileInfo(file);

            //    txbInfo.Text += fi.Name + Environment.NewLine;
            //    foreach (var kv in top10)
            //    {
            //        txbInfo.Text += $"{kv.Key}: {kv.Value} {Environment.NewLine}";
            //    }
            //    txbInfo.Text += Environment.NewLine;
            //}
        }

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = txbDebugInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var files = GetBigFiles();

            foreach (var file in files)
            {
                var wordsstats = await TextTools.TextTools.FreqAnalysisFromFileAsync(file, Environment.NewLine);
                var top10 = TextTools.TextTools.GetTopWords(10, wordsstats);

                var fi = new FileInfo(file);
                txbInfo.Text += fi.Name + Environment.NewLine;
                foreach (var kv in top10)
                {
                    txbInfo.Text += $"{kv.Key}: {kv.Value} {Environment.NewLine}";
                }
                txbInfo.Text += Environment.NewLine;
                txbDebugInfo.Text += stopwatch.ElapsedMilliseconds + Environment.NewLine;

                Data.Data.Results.Add(new StatsResult() { Source = file, Top10Words = top10 });

                progress1.Value += 100.0 / files.Count();
            }

            stopwatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopwatch.ElapsedMilliseconds;
            Mouse.OverrideCursor = null;
        }

        private void btnStatsAll_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = new();
            stopwatch.Start();
            txbInfo.Text = txbDebugInfo.Text = "";

            var files = GetBigFiles();

            var allwords =
                string.Join(Environment.NewLine,
                files.Select(f => File.ReadAllText(f)));

            var dict = TextTools.TextTools.FreqAnalysisFromString(allwords, Environment.NewLine);
            var top10 = TextTools.TextTools.GetTopWords(10, dict);

            foreach (var kv in top10)
            {
                txbInfo.Text += $"{kv.Key}: {kv.Value} {Environment.NewLine}";
            }

            stopwatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopwatch.ElapsedMilliseconds;
            Mouse.OverrideCursor = null;
        }

        private void btnStatsAllParallel_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = txbDebugInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = new();
            stopwatch.Start();

            ConcurrentDictionary<string, int> dict = new();

            var files = GetBigFiles();

            Parallel.ForEach(files, file =>
            {
                foreach (var word in File.ReadAllLines(file))
                {
                    dict.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                }
            });

            foreach (var kv in dict.OrderByDescending(x => x.Value).Take(10))
            {
                txbInfo.Text += $"{kv.Key}: {kv.Value} {Environment.NewLine}";
            }

            stopwatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopwatch.ElapsedMilliseconds;
            Mouse.OverrideCursor = null;
        }

        private void btnStatsAllParallelLock_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = txbDebugInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = new();
            stopwatch.Start();

            object locker = new object();
            Dictionary<string, int> dict = new();

            var files = GetBigFiles();

            Parallel.ForEach(files, file =>
            {
                foreach (var word in File.ReadAllLines(file))
                {
                    lock (locker)
                    {
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else
                            dict.Add(word, 1);
                    }
                }
            });

            foreach (var kv in dict.OrderByDescending(x => x.Value).Take(10))
            {
                txbInfo.Text += $"{kv.Key}: {kv.Value} {Environment.NewLine}";
            }

            stopwatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopwatch.ElapsedMilliseconds;
            Mouse.OverrideCursor = null;
        }

        private async void btnShowAnalysisDetail_Click(object sender, RoutedEventArgs e)
        {
            var url = txtInputUrl.Text;

            var d = DateTime.Now;

            var dict = await TextTools.TextTools.FreqAnalysisfromUrlAsync(url);
            var top10 = TextTools.TextTools.GetTopWords(10, dict);

            var elapsed = (int)(DateTime.Now - d).TotalMilliseconds;

            Model.StatsResult result = new();
            result.Top10Words = top10;
            result.Source = url;
            result.ElapsedMilliseconds = elapsed;
            result.SubmitedBy = "Stanislav Hladík";

            StatsResultWindow rw = new StatsResultWindow(result);
            rw.Show();
        }

        private async void btnReadAllFromAPI_Click(object sender, RoutedEventArgs e)
        {
            var url = Data.Data.APIUrl;

            using var httpClient = new HttpClient();

            var result = await httpClient.GetFromJsonAsync<IEnumerable<StatsResult>>(url + "/stats/all");

            foreach(var item in result)
            {
                txbInfo.Text += item.Source + Environment.NewLine;
                txbInfo.Text += item.SubmitedBy + Environment.NewLine;
            }
            
        }

        //private async void btnUpload_Click(object sender, RoutedEventArgs e)
        //{
        //    var client = new HttpClient();

        //    Stopwatch stopwatch = new();
        //    stopwatch.Start();

        //    var file1all = TextTools.TextTools.FreqAnalysisFromFile(@"holmes.txt");
        //    var top10 = TextTools.TextTools.GetTopWords(10, file1all);

        //    stopwatch.Stop();
        //    txbDebugInfo.Text = "elapsed ms: " + stopwatch.ElapsedMilliseconds;

        //    StatsResult sr = new StatsResult()
        //    {
        //        ElapsedMilliseconds = (int)stopwatch.ElapsedMilliseconds,
        //        Top10Words = top10,
        //        Name = "The Adventures of Sherlock Holmes, by Arthur Conan Doyle",
        //        Source = "holmes.txt",
        //        SubmitedBy = "Lukas Kubicek"
        //    };

        //    //var api = "http://localhost:5237";
        //    var api = "http://demo.vakutech.cz";
        //    var res = await client.PostAsJsonAsync<StatsResult>(api + "/stats", sr);

        //}
    }

}
