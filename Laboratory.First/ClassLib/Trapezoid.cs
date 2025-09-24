namespace ClassLib;

/// <summary>
/// Представляет плоскую фигуру — трапецию на плоскости.
/// </summary>
public class Trapezoid : IFigure
{
    /// <inheritdoc/>
    public Vector VertexA { get; set; }
    /// <inheritdoc/>
    public Vector VertexB { get; set; }
    /// <inheritdoc/>
    public Vector VertexC { get; set; }
    /// <inheritdoc/>
    public Vector VertexD { get; set; }

    /// <summary>
    /// Создает трапецию с указанными координатами вершин.
    /// </summary>
    public Trapezoid(Vector a, Vector b, Vector c, Vector d)
    {
        VertexA = a;
        VertexB = b;
        VertexC = c;
        VertexD = d;
    }

    /// <inheritdoc/>
    public bool Exists()
    {
        var AB = VertexB - VertexA;
        var BC = VertexC - VertexB;
        var CD = VertexD - VertexC;
        var DA = VertexA - VertexD;

        return AreVectorsParallel(AB, CD) || AreVectorsParallel(BC, DA);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public double GetPerimeter()
    {
        double[] sides = GetSidesLengths();
        return sides[0] + sides[1] + sides[2] + sides[3];
    }

    /// <inheritdoc/>
    public double GetArea()
    {
        var AB = VertexB - VertexA;
        var BC = VertexC - VertexB;
        var CD = VertexD - VertexC;
        var DA = VertexA - VertexD;

        Vector baseFirstStart, baseFirstEnd, baseSecondStart, baseSecondEnd;

        if (AreVectorsParallel(AB, CD))
        {
            baseFirstStart = VertexA; baseFirstEnd = VertexB;
            baseSecondStart = VertexC; baseSecondEnd = VertexD;
        }
        else if (AreVectorsParallel(BC, DA))
        {
            baseFirstStart = VertexB; baseFirstEnd = VertexC;
            baseSecondStart = VertexD; baseSecondEnd = VertexA;
        }
        else
        {
            throw new InvalidOperationException("Трапеция не имеет параллельных оснований");
        }

        double a = CalculateDistance(baseFirstStart, baseFirstEnd);
        double b = CalculateDistance(baseSecondStart, baseSecondEnd);

        // Высота через расстояние от точки до прямой
        double dx = baseSecondEnd.X - baseSecondStart.X;
        double dy = baseSecondEnd.Y - baseSecondStart.Y;
        double h = Math.Abs((dy * baseFirstStart.X - dx * baseFirstStart.Y + baseSecondEnd.X * baseSecondStart.Y - baseSecondEnd.Y * baseSecondStart.X) / Math.Sqrt(dx * dx + dy * dy));

        return (a + b) / 2 * h;
    }

    /// <inheritdoc/>
    public bool IsPointOnBorder(Vector point)
    {
        return IsPointOnLine(VertexA, VertexB, point) ||
               IsPointOnLine(VertexB, VertexC, point) ||
               IsPointOnLine(VertexC, VertexD, point) ||
               IsPointOnLine(VertexD, VertexA, point);
    }

    /// <inheritdoc/>
    public bool IsPointInside(Vector point)
    {
        Vector[] vertices = { VertexA, VertexB, VertexC, VertexD };
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            var a = vertices[i];
            var b = vertices[(i + 1) % 4];

            if ((a.Y > point.Y) != (b.Y > point.Y) && point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y + 1e-10) + a.X)
            {
                count++;
            }
        }

        return count % 2 == 1;
    }

    /// <inheritdoc/>
    private bool IsPointOnLine(Vector firstPoint, Vector secondPoint, Vector point)
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
    private bool AreVectorsParallel(Vector firstVector, Vector secondVector)
    {
        return (firstVector.X * secondVector.Y - firstVector.Y * secondVector.X) == 0;
    }

    /// <summary>
    /// Вычисляет длину стороны между двумя точками.
    /// </summary>
    private double CalculateDistance(Vector firstPoint, Vector secondPoint)
    {
        return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
    }
}