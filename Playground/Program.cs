// See https://aka.ms/new-console-template for more information
// Console.WriteLine("String ToUpper");

using Playground;
using System.Text.RegularExpressions;

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