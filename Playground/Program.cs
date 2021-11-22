// See https://aka.ms/new-console-template for more information
Console.WriteLine("String ToUpper");

var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
var strings = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };


var results1 = strings.Select(x => x.ToUpper());

IEnumerable<string> results2 = from s in strings
                            select s.ToUpper();


foreach(string r in results1)
{
    Console.WriteLine(r);
}

foreach (string r in results2)
{
    Console.WriteLine(r);
}
