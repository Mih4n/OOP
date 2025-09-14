namespace ClassLib;

using System;

/// <summary>
/// Представляет плоскую фигуру — трапецию на плоскости.
/// </summary>
public class Trapezoid
{
    /// <summary>
    /// Координаты вершин трапеции (A, B, C, D) по часовой стрелке.
    /// </summary>
    public Point VertexA { get; set; }
    public Point VertexB { get; set; }
    public Point VertexC { get; set; }
    public Point VertexD { get; set; }

    /// <summary>
    /// Создает трапецию с указанными координатами вершин.
    /// </summary>
    public Trapezoid(Point a, Point b, Point c, Point d)
    {
        VertexA = a;
        VertexB = b;
        VertexC = c;
        VertexD = d;
    }

    /// <summary>
    /// Проверяет, может ли существовать трапеция с данными вершинами.
    /// Трапеция существует, если есть хотя бы одна пара параллельных сторон.
    /// </summary>
    public bool Exists()
    {
        var AB = (X: VertexB.X - VertexA.X, Y: VertexB.Y - VertexA.Y);
        var CD = (X: VertexD.X - VertexC.X, Y: VertexD.Y - VertexC.Y);
        var BC = (X: VertexC.X - VertexB.X, Y: VertexC.Y - VertexB.Y);
        var DA = (X: VertexA.X - VertexD.X, Y: VertexA.Y - VertexD.Y);

        return AreVectorsParallel(AB, CD) || AreVectorsParallel(BC, DA);
    }

    /// <summary>
    /// Возвращает массив длин всех сторон трапеции.
    /// </summary>
    public double[] GetSidesLengths()
    {
        return
        [
            CalculateDistance(VertexA, VertexB),
            CalculateDistance(VertexB, VertexC),
            CalculateDistance(VertexC, VertexD),
            CalculateDistance(VertexD, VertexA)
        ];
    }

    /// <summary>
    /// Вычисляет периметр трапеции.
    /// </summary>
    public double GetPerimeter()
    {
        double[] sides = GetSidesLengths();
        return sides[0] + sides[1] + sides[2] + sides[3];
    }

    /// <summary>
    /// Вычисляет площадь трапеции по формуле: S = ((a + b)/2) * h
    /// где a и b — основания, h — высота.
    /// Основания определяются автоматически.
    /// </summary>
    public double GetArea()
    {
        var AB = (X: VertexB.X - VertexA.X, Y: VertexB.Y - VertexA.Y);
        var BC = (X: VertexC.X - VertexB.X, Y: VertexC.Y - VertexB.Y);
        var CD = (X: VertexD.X - VertexC.X, Y: VertexD.Y - VertexC.Y);
        var DA = (X: VertexA.X - VertexD.X, Y: VertexA.Y - VertexD.Y);

        Point base1Start, base1End, base2Start, base2End;

        if (AreVectorsParallel(AB, CD))
        {
            base1Start = VertexA; base1End = VertexB;
            base2Start = VertexC; base2End = VertexD;
        }
        else if (AreVectorsParallel(BC, DA))
        {
            base1Start = VertexB; base1End = VertexC;
            base2Start = VertexD; base2End = VertexA;
        }
        else
        {
            throw new InvalidOperationException("Трапеция не имеет параллельных оснований");
        }

        double a = CalculateDistance(base1Start, base1End);
        double b = CalculateDistance(base2Start, base2End);

        // Высота через расстояние от точки до прямой
        double dx = base2End.X - base2Start.X;
        double dy = base2End.Y - base2Start.Y;
        double h = Math.Abs((dy * base1Start.X - dx * base1Start.Y + base2End.X * base2Start.Y - base2End.Y * base2Start.X)
                            / Math.Sqrt(dx * dx + dy * dy));

        return (a + b) / 2 * h;
    }

    /// <summary>
    /// Проверяет, принадлежит ли точка границе трапеции.
    /// </summary>
    public bool IsPointOnBorder(Point point)
    {
        return IsPointOnLine(VertexA, VertexB, point) ||
               IsPointOnLine(VertexB, VertexC, point) ||
               IsPointOnLine(VertexC, VertexD, point) ||
               IsPointOnLine(VertexD, VertexA, point);
    }

    /// <summary>
    /// Проверяет, принадлежит ли точка трапеции (внутри или на границе).
    /// </summary>
    public bool IsPointInside(Point point)
    {
        Point[] vertices = { VertexA, VertexB, VertexC, VertexD };
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            var a = vertices[i];
            var b = vertices[(i + 1) % 4];

            if ((a.Y > point.Y) != (b.Y > point.Y) &&
                point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y + 1e-10) + a.X)
            {
                count++;
            }
        }

        return count % 2 == 1;
    }

    /// <summary>
    /// Проверяет, лежит ли точка на линии между двумя точками.
    /// </summary>
    private bool IsPointOnLine(Point firstPoint, Point secondPoint, Point point)
    {
        double cross = (point.Y - firstPoint.Y) * (secondPoint.X - firstPoint.X) - (point.X - firstPoint.X) * (secondPoint.Y - firstPoint.Y);
        if (cross != 0) return false;

        double dot = (point.X - firstPoint.X) * (secondPoint.X - firstPoint.X) + (point.Y - firstPoint.Y) * (secondPoint.Y - firstPoint.Y);
        if (dot < 0) return false;

        double lenSq = (secondPoint.X - firstPoint.X) * (secondPoint.X - firstPoint.X) + (secondPoint.Y - firstPoint.Y) * (secondPoint.Y - firstPoint.Y);
        return dot <= lenSq;
    }

    /// <summary>
    /// Проверяет вектора на параллельность
    /// </summary>
    private bool AreVectorsParallel((double X, double Y) firstVector, (double X, double Y) secondVector)
    {
        return (firstVector.X * secondVector.Y - firstVector.Y * secondVector.X) == 0;
    }

    /// <summary>
    /// Вычисляет длину стороны между двумя точками.
    /// </summary>
    private double CalculateDistance(Point firstPoint, Point secondPoint)
    {
        return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
    }
}