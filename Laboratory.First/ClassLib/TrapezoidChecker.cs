namespace ClassLib;

public class TrapezoidChecker
{
    public bool isTrapezoid(Figure figure)
    {
        if (figure.Points.Length != 4) return false;

        var points = figure.Points;
        return AreSidesParallel(points, (0, 1), (2, 3)) ||
            AreSidesParallel(points, (0, 2), (1, 3)) ||
            AreSidesParallel(points, (0, 3), (1, 2));
    }  

    private bool AreSidesParallel(Point[] points, (int First, int Second) firstSide, (int First, int Second) secondSide)
    {
        var firstVector = GetVector(points[firstSide.First], points[firstSide.Second]);
        var secondVector = GetVector(points[secondSide.First], points[secondSide.Second]);

        return AreVectorsParallel(firstVector, secondVector);
    }   

    private (float X, float Y) GetVector(Point first, Point second)
    {
        return (first.X - second.X, first.Y - second.Y);
    }

    private static bool AreVectorsParallel((float X, float Y) v1, (float X, float Y) v2)
    {
        float crossProduct = v1.X * v2.Y - v1.Y * v2.X;
        return Math.Abs(crossProduct) < 1e-10;
    }
}
