using System.Drawing;
using ClassLib.Eight.Contracts;

namespace ClassLib.Eight;

public class Circle : IGeometricShape
{
    private double x;
    private double y;
    private double radius;
    
    /// <summary>
    /// Цвет фигуры.
    /// </summary>
    public Color ShapeColor { get; set; }

    public Circle(double x, double y, double radius, string colorName)
    {
        this.x = x;
        this.y = y;
        this.radius = radius;
        try
        {
            ShapeColor = Color.FromName(colorName);
        }
        catch
        {
            ShapeColor = Color.Black; // Цвет по умолчанию
        }
    }

    public double Area => Math.PI * Math.Pow(radius, 2);

    /// <summary>
    /// Индексатор: 0 - X, 1 - Y, 2 - Radius.
    /// </summary>
    public double this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return radius;
                default: throw new IndexOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Длина окружности (границы круга).
    /// </summary>
    public double CalculateCircumference()
    {
        return 2 * Math.PI * radius;
    }

    public string GetInfo()
    {
        return $"Круг: Центр({x},{y}), Радиус={radius}, Цвет={ShapeColor.Name}, Площадь={Area:F2}";
    }

    public string ToDataString()
    {
        return $"{x} {y} {radius} {ShapeColor.Name}";
    }
}