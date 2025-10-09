using System.Text.Json;
using ClassLib.Fourth;

var result = WordAnalyzer.FindWordsWithSameStartAndEnd(Console.ReadLine() ?? "");
Console.WriteLine(JsonSerializer.Serialize(result));
