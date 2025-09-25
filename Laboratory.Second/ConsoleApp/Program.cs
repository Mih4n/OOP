using System.Diagnostics;
using ClassLib;

var firstVector = ReadVector("First Vector");
var secondVector = ReadVector("Second Vector");

Console.WriteLine($"Vector subtraction: { firstVector - secondVector }");
Console.WriteLine($"Vector addition: { firstVector + secondVector }");
Console.WriteLine($"Vector multiplication: { firstVector * secondVector }");

var stopwatch = Stopwatch.StartNew();
        
var type = firstVector.GetType();

stopwatch.Stop();
Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Execution time: {stopwatch.ElapsedTicks} ticks");
Console.WriteLine($"Execution time: {stopwatch.Elapsed}");

stopwatch = Stopwatch.StartNew();
        
Console.WriteLine("Hello world");

stopwatch.Stop();
Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Execution time: {stopwatch.ElapsedTicks} ticks");
Console.WriteLine($"Execution time: {stopwatch.Elapsed}");


Vector ReadVector(string name)
{
    Console.Write($"{name} X: ");
    double x = double.Parse(Console.ReadLine()!);
    Console.Write($"{name} Y: ");
    double y = double.Parse(Console.ReadLine()!);
    Console.Write($"{name} Z: ");
    double z = double.Parse(Console.ReadLine()!);
    return new Vector(x, y, z);
}