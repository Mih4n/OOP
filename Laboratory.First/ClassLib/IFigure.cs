namespace ClassLib;

public interface IFigure
{
    /// <summary>
    /// первая точка
    /// </summary>
    public Vector VertexA { get; set; }
    /// <summary>
    /// вторая точка
    /// </summary>
    public Vector VertexB { get; set; }
    /// <summary>
    /// третья точка
    /// </summary>
    public Vector VertexC { get; set; }
    /// <summary>
    /// четвёртая точка
    /// </summary>
    public Vector VertexD { get; set; }

    /// <summary>
    /// проверяет может ли существовать фигура
    /// </summary>
    public bool Exists();
    /// <summary>
    /// проверят стоит ли точка внутри фигуры
    /// </summary>
    /// <param name="point">точка для проверки</param>
    /// <returns></returns>
    public bool IsPointInside(Vector point);
    /// <summary>
    /// проверяет стоит ли точка на границе фигуры
    /// </summary>
    /// <param name="point">точка для проверки</param>
    /// <returns></returns>
    public bool IsPointOnBorder(Vector point);
    /// <summary>
    /// возвращает площадь фигуры 
    /// </summary>
    public double GetArea();
    /// <summary>
    /// возвращает периметр фигуры
    /// </summary>
    public double GetPerimeter();
    /// <summary>
    /// возвращает длинны всех сторон фигуры
    /// </summary>
    public double[] GetSidesLengths();
}
