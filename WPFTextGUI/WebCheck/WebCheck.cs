using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPFTextGUI.Webcheck
{
    public class WebCheck
    {
        public WebCheck(string _url, string _term)
        {
            Url = _url;
            Term = _term;
        }
        public string Url { get; set; }

        public string Term { get; set; }

        volatile public bool Found;

        public string LastState { get; set; }

        public string LastError { get; set; }

        private HttpClient httpClient = new HttpClient();

        public void Start(IProgress<string> progress)
        {
            Task.Run(() => Checking(progress));
        }
        private void Checking(IProgress<string> progress)
        {
            if (!(Webs.WebsToCheck.ContainsKey(Url) && Webs.WebsToCheck[Url] == true))
                return;

            while (true)
            {
                if (Webs.WebsToCheck[Url] == false)
                    break;

                try
                {
                    string content = httpClient.GetStringAsync(Url).Result;

                    if (content.Contains(Term, StringComparison.OrdinalIgnoreCase) == true)
                        Found = true;

                    progress.Report($"{DateTime.Now.ToString()} {Found} {Environment.NewLine}");
                }
                catch (Exception e)
                {
                    LastError = LastState = $"{DateTime.Now.ToString()} - ERROR - {e.Message}";

                    progress.Report(LastError + Environment.NewLine);
                }

                Task.Delay(2000).Wait();
            }
        }
    }
}
