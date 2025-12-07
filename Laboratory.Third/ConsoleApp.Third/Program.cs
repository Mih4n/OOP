using ClassLib.Third;
using ConsoleApp.Third;

var A = ArrayOneExtensions.Input("A");
var B = ArrayOneExtensions.Input("B");
var C = ArrayOneExtensions.Input("C");

A.Print("A");
B.Print("B");
C.Print("C");

Console.WriteLine($"Sum is: {ArrayOne.SumOnlyNegative(5 * A, C)}");
Console.WriteLine($"Sum is: {ArrayOne.SumOnlyNegative(2 * B, -A, C * 4)}");

int sumA = A.SumNegatives();
int sumB = B.SumNegatives();

if (sumA > sumB)
{
    Console.WriteLine("Sum of negatives in A is greater than in B -> replacing repeated negatives...");
    A.ReplaceNegativesWithSum(sumA);
    A.Print("A (updated)");
}
