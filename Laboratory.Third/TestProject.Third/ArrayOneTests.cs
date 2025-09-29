using ClassLib.Third;
using NUnit.Framework;

namespace TestProject.Third;

[TestFixture]
public class ArrayOneTests
{
    [Test]
    public void IndexerGetAndSetWorksCorrectly()
    {
        var arr = new ArrayOne([1, 2, 3]);
        Assert.That(arr.Length, Is.EqualTo(3));

        Assert.That(arr[0], Is.EqualTo(1));
        arr[0] = 10;
        Assert.That(arr[0], Is.EqualTo(10));
    }

    [Test]
    public void OperatorMultiplicationArrayByNumberReturnsNewArray()
    {
        var arr = new ArrayOne([1, -2, 3]);
        var result = arr * 2;

        Assert.That(result.ToArray(), Is.EqualTo(new[] { 2, -4, 6 }));
        Assert.That(arr.ToArray(), Is.EqualTo(new[] { 1, -2, 3 })); // оригинал не изменился
    }

    [Test]
    public void OperatorMultiplicationNumberByArrayReturnsNewArray()
    {
        var arr = new ArrayOne([-1, 2]);
        var result = 3 * arr;

        Assert.That(result.ToArray(), Is.EqualTo(new[] { -3, 6 }));
    }

    [Test]
    public void OperatorUnaryMinusReturnsNewArray()
    {
        var arr = new ArrayOne([1, -2, 3]);
        var result = -arr;

        Assert.That(result.ToArray(), Is.EqualTo(new[] { -1, 2, -3 }));
        Assert.That(arr.ToArray(), Is.EqualTo(new[] { 1, -2, 3 })); // оригинал не изменился
    }

    [Test]
    public void SumOnlyNegativeMultipleArraysWorksCorrectly()
    {
        var arr1 = new ArrayOne([1, -2, 3]);
        var arr2 = new ArrayOne([-5, 6, -1]);

        var sum = ArrayOne.SumOnlyNegative(arr1, arr2);
        Assert.That(sum, Is.EqualTo(-8)); // -2 + -5 + -1
    }

    [Test]
    public void SumNegativesSingleArrayWorksCorrectly()
    {
        var arr = new ArrayOne([1, -2, -3, 4]);
        Assert.That(arr.SumNegatives(), Is.EqualTo(-5));
    }

    [Test]
    public void ReplaceNegativesWithSumReplacesOnlyRepeatedNegatives()
    {
        var arr = new ArrayOne([-2, 1, -2, -3, -3, 4]);
        int sum = arr.SumNegatives(); // -2 -2 -3 -3 = -10
        arr.ReplaceNegativesWithSum(sum);

        Assert.That(arr.ToArray(), Is.EqualTo(new[] { -2, 1, -10, -3, -10, 4 }));
    }
}