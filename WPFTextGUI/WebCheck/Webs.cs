using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTextGUI.Webcheck
{
    public class Webs
    {
        public static ConcurrentDictionary<string, bool> WebsToCheck { get; set; }
                                                = new ConcurrentDictionary<string, bool>();
    }
}
