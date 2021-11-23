using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Playground
{
    public class _TextTools
    {
        public static Dictionary<string, int> FreqAnalysis(string file)
        {
            var content = File.ReadAllText(file);
            var words = content.Split(' ');

            Dictionary<string, int> dict = new();

            foreach(var word in words)
            {
                if (dict.ContainsKey(word))
                {
                    dict[word]++;
                }
                else
                {
                    dict[word] = 1;
                }
            }

            return dict;
        }


        public static List<string> GetWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select OdebraniMezer(m.Value);

            return words.ToList();
        }

        public static string OdebraniMezer(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }

        public static Dictionary<string, int> WordFreg(List<string> input)
        {
            var tuples = input.GroupBy(x => x)
                              .Select(g => (Letter: g.Key, Count: g.Count()))
                              .OrderByDescending(x => x.Count);


            Dictionary<string, int> dict = new Dictionary<string, int>();

            int count = 0;
            foreach (var tuple in tuples)
            {
                if (count < 10)
                {
                    dict.Add(tuple.Letter, tuple.Count);
                }
                count = count + 1;
            }

            return dict;

        }


    }
}
