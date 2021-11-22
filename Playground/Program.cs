// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
var strings = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };


IEnumerable<string> score = from s in strings
                            select s.ToUpper();

foreach(string s in score)
{
    Console.WriteLine(s);
}