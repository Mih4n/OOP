using ClassLib.Eight.Contracts;

namespace ClassLib.Eight;

public class Square : IGeometricShape
{
    /// <summary>
    /// Массив координат вершин (x1, y1, x2, y2, x3, y3, x4, y4).
    /// </summary>
    private double[] vertices;

    /// <summary>
    /// Конструктор квадрата.
    /// </summary>
    /// <param name="coords">Массив из 8 чисел (координаты 4 вершин).</param>
    public Square(double[] coords)
    {
        if (coords.Length != 8)
            throw new ArgumentException("Для квадрата необходимо 4 пары координат (8 чисел).");
        
        vertices = coords;
    }

    /// <summary>
    /// Вычисление длины стороны (расстояние между первой и второй точкой).
    /// </summary>
    private double SideLength => Math.Sqrt(Math.Pow(vertices[2] - vertices[0], 2) + Math.Pow(vertices[3] - vertices[1], 2));

    /// <summary>
    /// Площадь квадрата.
    /// </summary>
    public double Area => Math.Pow(SideLength, 2);

    /// <summary>
    /// Индексатор для доступа к координатам.
    /// </summary>
    public double this[int index]
    {
        get
        {
            if (index < 0 || index >= vertices.Length)
                throw new IndexOutOfRangeException();
            return vertices[index];
        }
    }

    /// <summary>
    /// Метод вычисления периметра.
    /// </summary>
    public double CalculatePerimeter()
    {
        return 4 * SideLength;
    }

    public string GetInfo()
    {
        return $"Квадрат: Сторона = {SideLength:F2}, Площадь = {Area:F2}, Периметр = {CalculatePerimeter():F2}";
    }

    /// <summary>
    /// Проверка, находится ли квадрат более чем в одной четверти.
    /// </summary>
    public bool SpansMultipleQuadrants()
    {
        // Простая логика: если знаки координат всех точек одинаковы, они в одной четверти.
        // Если есть разные знаки X или Y, то фигура пересекает оси.
        bool hasPositiveX = false, hasNegativeX = false;
        bool hasPositiveY = false, hasNegativeY = false;

        for (int i = 0; i < vertices.Length; i += 2)
        {
            double x = vertices[i];
            double y = vertices[i + 1];

            if (x > 0) hasPositiveX = true;
            if (x < 0) hasNegativeX = true;
            if (y > 0) hasPositiveY = true;
            if (y < 0) hasNegativeY = true;
        }

        // Если есть и плюсы и минусы по любой оси, значит пересекает четверти
        return (hasPositiveX && hasNegativeX) || (hasPositiveY && hasNegativeY);
    }

    public string ToDataString()
    {
        return string.Join(" ", vertices.Select(v => v.ToString()));
    }
}