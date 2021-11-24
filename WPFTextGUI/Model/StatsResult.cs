using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTextGUI.Model
{
    /// <summary>
    /// Result of frequential analysis from given source    
    /// </summary>

    public class StatsResult
    {
        /// <summary>
        ///     Source of text for analysis
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        ///    10 most common words in source
        /// </summary>
        public Dictionary<string, int> Top10Words { get; set; }

    }
}
