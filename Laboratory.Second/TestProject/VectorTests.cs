using ClassLib.Second;

namespace TestProject;

public class VectorTests
{
    [Test]
    public void Subtraction()
    {
        var first = new Vector(1, 1, 1);
        var second = new Vector(1, 1, 1);

        Assert.That(first - second == new Vector(0, 0, 0));
    }

    [Test]
    public void Addition()
    {
        var first = new Vector(1, 1, 1);
        var second = new Vector(1, 1, 1);

        Assert.That(first + second == new Vector(2, 2, 2));
    }

    [Test]
    public void Multiplication()
    {
        var first = new Vector(1, 1, 1);
        var second = new Vector(3, 3, 3);

        Assert.That(first * second == new Vector(3, 3, 3));
    }
}
