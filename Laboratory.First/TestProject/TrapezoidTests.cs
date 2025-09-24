using ClassLib;

namespace TestProject;

[TestFixture]
public class TrapezoidTests
{
    [Test]
    public void ExistsWhenTrapezoidIsValidReturnsTrue()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        Assert.That(trapezoid.Exists(), Is.True);
    }

    [Test]
    public void ExistsWhenNotTrapezoidReturnsFalse()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(2, 1),
            new Vector(3, 3),
            new Vector(1, 4)
        );

        Assert.That(trapezoid.Exists(), Is.False);
    }

    [Test]
    public void GetSidesLengthsReturnsCorrectValues()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        var sides = trapezoid.GetSidesLengths();

        Assert.That(sides[0], Is.EqualTo(4).Within(1e-6)); // AB
        Assert.That(sides[2], Is.EqualTo(2).Within(1e-6)); // CD
    }

    [Test]
    public void GetPerimeterReturnsCorrectValue()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        double perimeter = trapezoid.GetPerimeter();

        Assert.That(perimeter, Is.EqualTo(10.472135).Within(1e-6));
    }

    [Test]
    public void GetAreaReturnsCorrectValue()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        double area = trapezoid.GetArea();

        Assert.That(area, Is.EqualTo(6).Within(1e-6));
    }

    [Test]
    public void IsPointInsideWhenPointInsideReturnsTrue()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        var point = new Vector(2, 1);

        Assert.That(trapezoid.IsPointInside(point), Is.True);
    }

    [Test]
    public void IsPointInsideWhenPointOutsideReturnsFalse()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        var point = new Vector(5, 5);

        Assert.That(trapezoid.IsPointInside(point), Is.False);
    }

    [Test]
    public void IsPointOnBorderWhenPointOnSideReturnsTrue()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        var point = new Vector(2, 0); // лежит на AB

        Assert.That(trapezoid.IsPointOnBorder(point), Is.True);
    }

    [Test]
    public void IsPointOnBorderWhenPointNotOnBorderReturnsFalse()
    {
        var trapezoid = new Trapezoid(
            new Vector(0, 0),
            new Vector(4, 0),
            new Vector(3, 2),
            new Vector(1, 2)
        );

        var point = new Vector(2, 1);

        Assert.That(trapezoid.IsPointOnBorder(point), Is.False);
    }
}

