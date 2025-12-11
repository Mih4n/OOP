namespace ClassLib.Eight.Contracts;

/// <summary>
/// Интерфейс для геометрических фигур.
/// </summary>
public interface IGeometricShape
{
    /// <summary>
    /// Площадь фигуры.
    /// </summary>
    double Area { get; }

    /// <summary>
    /// Метод получения информации о фигуре.
    /// </summary>
    /// <returns>Строка с описанием.</returns>
    string GetInfo();

    /// <summary>
    /// Индексатор для доступа к параметрам фигуры.
    /// </summary>
    /// <param name="index">Индекс параметра.</param>
    /// <returns>Значение параметра.</returns>
    double this[int index] { get; }
}
