using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTextGUI.Model;

namespace WPFTextGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static string bigFilesDir = @"C:\Users\S1244598\source\repos\CNET2\bigfiles";

        static IEnumerable<string> GetBigFiles()
        {
            return Directory.EnumerateFiles(bigFilesDir, "*.txt");
        }

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

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            txbInfo.Text = txbDebugInfo.Text = "";

            var filePath = "words01.txt";
            var files = GetBigFiles();


            //var file = System.IO.Path.Combine(bigFilesDir, filePath);

            foreach (var file in files)
            {
                var wordStats = await TextTools.TextTools.FreqAnalysisFromFileAsync(file, Environment.NewLine);
                var top10 = TextTools.TextTools.GetTopWords(10, wordStats);
                var fi = new FileInfo(file);

                txbInfo.Text += fi.Name + Environment.NewLine;

                foreach (var kv in top10)
                {
                    txbInfo.Text += $"{kv.Key}: {kv.Key} {Environment.NewLine}";
                }

                txbInfo.Text += Environment.NewLine;
                txbDebugInfo.Text += stopWatch.ElapsedMilliseconds + Environment.NewLine;

                Data.Data.Results.Add(new StatsResult() { Source = file, Top10Words = top10 });

                pgbBar1.Value += 100 / files.Count();
            }

            stopWatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopWatch.ElapsedMilliseconds.ToString();

            Mouse.OverrideCursor = null;

            pgbBar1.Value = 100;
        }

        private void btnStatsAll_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            txbInfo.Text = txbDebugInfo.Text = "";

            var files = GetBigFiles();
            var allWords = string.Join
                           (
                           Environment.NewLine,
                           files.Select(f => File.ReadAllText(f))
                           );

            var dict = TextTools.TextTools.FreqAnalysisFromString(allWords, Environment.NewLine);
            var top10 = TextTools.TextTools.GetTopWords(10, dict);

            foreach (var kv in top10)
            {
                txbInfo.Text += $"{kv.Key}: {kv.Key} {Environment.NewLine}";
            }

            stopWatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopWatch.ElapsedMilliseconds.ToString();

            Mouse.OverrideCursor = null;
        }

        private void btnStatsAllParallel_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            txbInfo.Text = txbDebugInfo.Text = "";

            ConcurrentDictionary<string, int> dict = new();

            var files = GetBigFiles();

            Parallel.ForEach
            (
                files, file =>
                {
                    foreach(var word in File.ReadAllLines(file))
                    {
                        dict.AddOrUpdate(word, 1, (key, oldvalue) => oldvalue + 1);
                    }
                }
            );

            foreach (var kv in dict.OrderByDescending(x => x.Value).Take(10))
            {
                txbInfo.Text += $"{kv.Key}: {kv.Value} {Environment.NewLine}";
            }

            stopWatch.Stop();
            txbDebugInfo.Text = "elapsed ms: " + stopWatch.ElapsedMilliseconds.ToString();

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

    }
}
