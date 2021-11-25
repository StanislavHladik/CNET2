using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTextGUI.Model;

namespace WPFTextGUI.Data
{
    public class Data
    {
        public static List<StatsResult> Results { get; set; } = new List<StatsResult>();

        public static string APIUrl = "https://localhost:7045/";
    }
}
