using ClassLib.Second;

var firstVector = ReadVector("First Vector");
var secondVector = ReadVector("Second Vector");

Console.WriteLine($"Vector subtraction: { firstVector - secondVector }");
Console.WriteLine($"Vector addition: { firstVector + secondVector }");
Console.WriteLine($"Vector multiplication: { firstVector * secondVector }");

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