namespace ClassLib;

public record struct Point(double X, double Y)
{
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y); 
};