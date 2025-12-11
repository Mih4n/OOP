using ClassLib.Eight.Contracts;

namespace ClassLib.Eight.Services;

public class AreaComparer : IComparer<IGeometricShape>
{
    public int Compare(IGeometricShape? x, IGeometricShape? y)
    {
        if (x == null || y == null) return 0;
        return x.Area.CompareTo(y.Area);
    }
}