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

        public bool Found { get; set; }

        public string LastState { get; set; }

        public string LastError { get; set; }

        private HttpClient httpClient = new HttpClient();

        public void Start()
        {
            Task.Run(() => Checking());
        }
        private void Checking()
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
                }
                catch (Exception e)
                {
                    LastError = LastState = $"{DateTime.Now.ToString()} - ERROR - {e.Message}";
                }

                Task.Delay(5000).Wait();
            }
        }
    }
}
