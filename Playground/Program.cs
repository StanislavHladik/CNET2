// See https://aka.ms/new-console-template for more information
Console.WriteLine("String ToUpper");

var numbers = new[] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
var strings = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };


var results = strings.Select(x => x.ToUpper());

/*
IEnumerable<string> results = from s in strings
                            select s.ToUpper();
*/

PrintList(results.ToList());

static void PrintList(List<string> listToPrint)
{
    foreach (var item in listToPrint)
    {
        Console.WriteLine(item);
    }
}
