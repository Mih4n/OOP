using System.Text;
using ClassLib.Third;

var A = ReadArray("A");
var B = ReadArray("B");
var C = ReadArray("C");

WriteArray(A, "A");
WriteArray(B, "B");
WriteArray(C, "C");

Console.WriteLine($"Sum is: {SumOnlyNegative(5 * A, C)}");
Console.WriteLine($"Sum is: {SumOnlyNegative(2 * B, -A, C * 4)}");

static ArrayOne ReadArray(string name)
{
    Console.WriteLine($"write down an array: {name}");
    var input = Console.ReadLine();
    var array = input?.Split(" ").Select(int.Parse);

    return new ArrayOne(array?.ToArray() ?? []);
}

static void WriteArray(ArrayOne array, string name)
{
    var stringBuilder = new StringBuilder();
    stringBuilder.Append("[ ");
    foreach (var element in array)
        stringBuilder.AppendFormat("{0}, ", element);
    stringBuilder.Append("]");

    Console.WriteLine(stringBuilder.ToString());
}

static int SumOnlyNegative(params ArrayOne[] arrays)
{
    return arrays
        .Sum(a => a.Where(e => e < 0).Sum());
}