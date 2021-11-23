namespace TextTools
{
    public class TextTools
    {
        public static async Task<Dictionary<string, int>> FreqAnalysisFromFileAsync(string file, string splitBy = " ")
        {
            var content = await File.ReadAllTextAsync(file);             
            return FreqAnalysisFromString(content, splitBy);
        }
        public static Dictionary<string, int> FreqAnalysisfromFile(string file, string splitBy = " ")
        {
            var content = File.ReadAllText(file);
            return FreqAnalysisFromString(content, splitBy);
        }
        public static Dictionary<string, int> FreqAnalysisFromString(string content, string splitBy = " ")
        {
        
            var words = content.Split(splitBy);

            Dictionary<string, int> dict = new();

            foreach (var word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (dict.ContainsKey(word))
                {
                    dict[word] = dict[word] + 1;
                }
                else
                {
                    dict.Add(word, 1);
                }
            }

            return dict;
        }
        public static Dictionary<string, int> GetTopWords(int takeTop, Dictionary<string, int> dict)
        {
            return dict
                .OrderByDescending(x => x.Value).Take(takeTop)
                .ToDictionary(x => x.Key, y => y.Value);
            ;
        }
    }
}