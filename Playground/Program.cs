// See https://aka.ms/new-console-template for more information
// Console.WriteLine("String ToUpper");

Console.WriteLine("Hello World!");


HttpClient httpClient = new HttpClient();
var res1 = await httpClient.GetAsync("https://www.gutenberg.org/cache/epub/2036/pg2036.txt");
var res2 = await httpClient.GetAsync("https://www.gutenberg.org/files/16749/16749-0.txt");
var res3 = await httpClient.GetAsync("https://www.gutenberg.org/cache/epub/19694/pg19694.txt");

if (res1.IsSuccessStatusCode && res2.IsSuccessStatusCode && res3.IsSuccessStatusCode)
{
    string content1 = await res1.Content.ReadAsStringAsync();

    var task1 = Task.Run(() =>
    {
        var dict = TextTools.TextTools.FreqAnalysisFromString(content1);
        var top10 = TextTools.TextTools.GetTopWords(10, dict);

        foreach (var kv in top10)
        {
            Console.WriteLine($"{kv.Key}: {kv.Key} {Environment.NewLine}");
        }

        Console.WriteLine("Task finished 1");
    }
    );

    string content2 = await res1.Content.ReadAsStringAsync();

    var task2 = Task.Run(() =>
    {
        var dict = TextTools.TextTools.FreqAnalysisFromString(content2);
        var top10 = TextTools.TextTools.GetTopWords(10, dict);

        foreach (var kv in top10)
        {
            Console.WriteLine($"{kv.Key}: {kv.Key} {Environment.NewLine}");
        }

        Console.WriteLine("Task finished 2");
    }
    );

    string content3 = await res1.Content.ReadAsStringAsync();

    var task3 = Task.Run(() =>
    {
        var dict = TextTools.TextTools.FreqAnalysisFromString(content3);
        var top10 = TextTools.TextTools.GetTopWords(10, dict);

        foreach (var kv in top10)
        {
            Console.WriteLine($"{kv.Key}: {kv.Key} {Environment.NewLine}");
        }

        Console.WriteLine("Task finished 3");
    }
    );

    Task.WaitAll(task1, task2, task3);
}



Console.WriteLine("Program finished");

/*
HttpClient httpClient = new HttpClient();
var res = await httpClient.GetAsync("https://google.com");
if(res.IsSuccessStatusCode)
{
    string content = await res.Content.ReadAsStringAsync();
}
*/



/*
var task1 = Task.Run(() =>
                {
                    TextTools.TextTools.FreqAnalysisfromFile(@"C:\Users\S1244598\source\repos\CNET2\bigfiles\words01.txt", Environment.NewLine);
                    Console.WriteLine("Task finished 1");
                }
                );

var task2 = Task.Run(() =>
                {
                    TextTools.TextTools.FreqAnalysisfromFile(@"C:\Users\S1244598\source\repos\CNET2\bigfiles\words09.txt", Environment.NewLine);
                    Console.WriteLine("Task finished 2");
                }
                );

var task3 = Task<Dictionary<string, int>>.Run(() =>
                {
                    TextTools.TextTools.FreqAnalysisfromFile(@"C:\Users\S1244598\source\repos\CNET2\bigfiles\words09.txt", Environment.NewLine);
                    Console.WriteLine("Task finished 3");
                }
                );
*/


//Task.WaitAny(task1, task2);
//Task.WaitAll(task1, task2);    
//await Task.WhenAll(task1, task2);
//await Task.WhenAny(task1, task2);

//Console.WriteLine("Program finished");

static void PrintList(List<string> listToPrint)
{
    foreach (var item in listToPrint)
    {
        Console.WriteLine(item);
    }
}

static void PrintItems<T>(IEnumerable<T> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static Dictionary<char, int> CharFreg(string input)
{
    var tuples = input.GroupBy(x => x)
                      .Select(g => (Letter: g.Key, Count: g.Count()))
                      .OrderBy(x => x.Count)
                      .ThenByDescending(x => x.Letter);

    Dictionary<char, int> dict = new Dictionary<char, int>();

    foreach (var tuple in tuples)
    {
        dict.Add(tuple.Letter, tuple.Count);
    }

    return dict;

}

static void LINQ()
{
    var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    var strings = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    // var results = strings.Select(x => x.ToUpper());

    /*
    IEnumerable<string> results = from s in strings
                                select s.ToUpper();
    */

    // PrintList(results.ToList());

    /*
    var count = numbers.Where(x => x % 2 != 0). Count();
    if(count > 0)
    {
        //false
    }
    */

    /*
    bool isOnlyEvenNumbers = numbers.All(x => x % 2 == 0);
    global::System.Console.WriteLine($"Jsou všechna čísla sudá: {isOnlyEvenNumbers}");
    */

    /*
    Array.Sort(numbers);
    foreach(var s in numbers)
    {
        Console.WriteLine(strings[s]);
    }
    */

    // var result = numbers.Select(x => strings[x]);

    // var<int> numbersString = numbers.OrderByDescending(x); 

    /*
    var sumLetters = strings.Select(x => x.Length).Sum();
    Console.WriteLine($"Dohromadey je {sumLetters} písmen");
    */

    /*
    var result = strings.Select(slovo => new UpperLowerString(slovo))
                        .Select(x => $"upper: {x.UpperCase} lower:{x.LowerCase}");

    PrintList(result.ToList());
    */

    /*
    var result = strings.Select(slovo => (slovo.ToUpper(), slovo.ToLower()));
    PrintItems<(string, string)>(result);
    */

    // Spojení slov do jednoho řetězce
    //var aggregated = string.Join("", strings);

    // Pracuje se stringem jako kolekcí znaků
    /*
    var result = aggregated
                        // Seskupení podle písmenek (char v kolekci string)
                        .GroupBy(x => x)
                        // Vytvoření tuple obsahující klíč a počet znaků
                        .Select(g => (Letter: g.Key,Count: g.Count())) 
                        .OrderByDescending(x => x.Count)
                        .ThenBy(x => x.Letter);

    PrintItems(result);
    */
    /*
    foreach(var s in strings)
    {
        foreach(var ch in s )

        foreach(var ch in s)
        {
            int freq = s.Where(x => (x == ch)).Count();
            Console.WriteLine(freq);
        }

    }
    */


    // Dictionary
    string alice = File.ReadAllText("alice.txt");
    string holmes = File.ReadAllText("holmes.txt");
    string rur = File.ReadAllText("rur.txt");

    /*
    Console.WriteLine("");
    Console.WriteLine("Alice");
    foreach (var a in TextTools.WordFreg(TextTools.GetWords(alice)))
    {
        Console.WriteLine(a);
    }
    Console.WriteLine("");
    Console.WriteLine("Holmes");
    foreach (var a in TextTools.WordFreg(TextTools.GetWords(holmes)))
    {
        Console.WriteLine(a);
    }
    Console.WriteLine("");
    Console.WriteLine("RUR");
    foreach (var a in TextTools.WordFreg(TextTools.GetWords(rur)))
    {
        Console.WriteLine(a);
    }
    */
}
