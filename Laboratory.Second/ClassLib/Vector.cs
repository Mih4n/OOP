namespace ClassLib.Second;

/// <summary>
/// 3 dimensioned vector
/// </summary>
/// <param name="X"></param>
/// <param name="Y"></param>
/// <param name="Z"></param>
public record struct Vector(double X, double Y, double Z)
{
    /// <summary>
    /// vector subtraction first minus second
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static Vector operator -(Vector first, Vector second) 
    {
        var (fx, fy, fz) = first;
        var (sx, sy, sz) = second;

        return new Vector(fx - sx, fy - sy, fz - sz);
    }

    /// <summary>
    /// vector addition 
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static Vector operator +(Vector first, Vector second) 
    {
        var (fx, fy, fz) = first;
        var (sx, sy, sz) = second;

        return new Vector(fx + sx, fy + sy, fz + sz);
    }

    /// <summary>
    /// vector multiplication
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public static Vector operator *(Vector first, Vector second) 
    {
        var (fx, fy, fz) = first;
        var (sx, sy, sz) = second;

        return new Vector(fx * sx, fy * sy, fz * sz);
    }
}