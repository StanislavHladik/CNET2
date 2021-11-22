using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public class UpperLowerString
    {
        public UpperLowerString(string s)
        {
            UpperCase = s.ToUpper();
            LowerCase = s.ToLower();
        }

        public string UpperCase { get; set; }
        public string LowerCase { get; set; }
    }
}
