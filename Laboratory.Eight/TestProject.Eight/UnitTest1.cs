using ClassLib.Eight;

namespace TestProject.Eight;

public class Tests
{
    /// <summary>
    /// Тест проверяет корректность вычисления площади квадрата.
    /// </summary>
    [Test]
    public void SquareAreaCalculationCorrect()
    {
        double[] coords = { 0, 0, 2, 0, 2, 2, 0, 2 };
        Square sq = new Square(coords);
        
        Assert.That(sq.Area, Is.EqualTo(4).Within(0.001));
    }

    /// <summary>
    /// Тест проверяет корректность вычисления длины окружности.
    /// </summary>
    [Test]
    public void CircleCircumferenceCalculationCorrect()
    {
        Circle c = new Circle(0, 0, 10, "Red");
        double expected = 2 * System.Math.PI * 10;
        
        Assert.That(c.CalculateCircumference(), Is.EqualTo(expected).Within(0.001));
    }

    /// <summary>
    /// Тест проверяет, корректно ли определяется, что квадрат расположен более чем в одной четверти.
    /// </summary>
    [Test]
    public void SquareMultipleQuadrantsCheck()
    {
        double[] coords = { -1, -1, 1, -1, 1, 1, -1, 1 };
        Square sq = new Square(coords);
        
        Assert.That(sq.SpansMultipleQuadrants());
    }

    /// <summary>
    /// Тест проверяет, корректно ли определяется, что квадрат находится в одной четверти.
    /// </summary>
    [Test]
    public void SquareSingleQuadrantCheck()
    {
        double[] coords = { 1, 1, 3, 1, 3, 3, 1, 3 };
        Square sq = new Square(coords);
        
        Assert.That(!sq.SpansMultipleQuadrants());
    }
}
