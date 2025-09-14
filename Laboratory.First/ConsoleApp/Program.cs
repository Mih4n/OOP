using ClassLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите координаты трапеции по часовой стрелке (A, B, C, D):");

        var a = ReadPoint("A");
        var b = ReadPoint("B");
        var c = ReadPoint("C");
        var d = ReadPoint("D");

        var trapezoid = new Trapezoid(a, b, c, d);

        if (!trapezoid.Exists())
        {
            Console.WriteLine("Трапеция с такими координатами не существует.");
            return;
        }

        Console.WriteLine($"Периметр: {trapezoid.GetPerimeter():F2}");
        Console.WriteLine($"Площадь: {trapezoid.GetArea():F2}");

        Console.WriteLine("Введите точку для проверки:");
        var p = ReadPoint("P");
        Console.WriteLine(
            trapezoid.IsPointOnBorder(p)
            ? "Точка на границе трапеции."
            : trapezoid.IsPointInside(p)
                ? "Точка внутри трапеции."
                : "Точка снаружи трапеции."
        );
    }

    static Point ReadPoint(string name)
    {
        Console.Write($"{name} X: ");
        double x = double.Parse(Console.ReadLine()!);
        Console.Write($"{name} Y: ");
        double y = double.Parse(Console.ReadLine()!);
        return new Point(x, y);
    }
}
