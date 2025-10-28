using System.Text;
using ClassLib.Third;

namespace ConsoleApp.Third;

/// <summary>
/// array one extensions for console input and output
/// </summary>
public static class ArrayOneExtensions
{
    /// <summary>
    /// input array
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ArrayOne Input(string name)
    {
        Console.WriteLine($"write down an array: {name}");
        var input = Console.ReadLine();
        var arr = input?.Split(" ").Select(int.Parse).ToArray() ?? [];
        return new ArrayOne(arr);
    }

    /// <summary>
    /// print array
    /// </summary>
    /// <param name="name"></param>
    public static void Print(this ArrayOne array, string name)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("[ ");
        foreach (var element in array)
            stringBuilder.AppendFormat("{0}, ", element);
        stringBuilder.Append("]");
        Console.WriteLine($"{name} = {stringBuilder}");
    }
}
