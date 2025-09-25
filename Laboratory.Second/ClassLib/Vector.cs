namespace ClassLib;

public record struct Vector(double X, double Y, double Z)
{
    public static Vector operator -(Vector first, Vector second) 
    {
        var (fx, fy, fz) = first;
        var (sx, sy, sz) = second;

        return new Vector(fx - sx, fy - sy, fz - sy);
    }

    public static Vector operator +(Vector first, Vector second) 
    {
        var (fx, fy, fz) = first;
        var (sx, sy, sz) = second;

        return new Vector(fx + sx, fy + sy, fz + sy);
    }

    public static Vector operator *(Vector first, Vector second) 
    {
        var (fx, fy, fz) = first;
        var (sx, sy, sz) = second;

        return new Vector(fx * sx, fy * sy, fz * sy);
    }
}