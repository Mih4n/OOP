namespace Second.ConsoleApp.Nine;

public interface IElectricComponent
{
    string Name { get; }
    void Shutdown();
    void SwitchToGenerator();
}

public class Machine : IElectricComponent
{
    public string Name { get; }
    public Machine(string name) => Name = name;

    public void Shutdown() => Console.WriteLine($"  [!] Станок '{Name}' остановлен.");
    public void SwitchToGenerator() => Console.WriteLine($"  [+] Станок '{Name}' переведен на питание от генератора.");
}

public class ElectricNode : IElectricComponent
{
    public string Name { get; }
    private readonly List<IElectricComponent> _children = new List<IElectricComponent>();

    public ElectricNode(string name) => Name = name;

    public void Add(IElectricComponent component) => _children.Add(component);

    public void Shutdown()
    {
        Console.WriteLine($"\n>>> Отключение узла: {Name}");
        foreach (var child in _children)
        {
            child.Shutdown();
        }
    }

    public void SwitchToGenerator()
    {
        Console.WriteLine($"\n>>> Перевод узла {Name} на генераторы:");
        foreach (var child in _children)
        {
            child.SwitchToGenerator();
        }
    }
}

public class PowerSystemFacade
{
    private readonly ElectricNode _mainSubstation;

    public PowerSystemFacade()
    {
        _mainSubstation = new ElectricNode("Главная Подстанция Завода");

        var shopA = new ElectricNode("Цех Сборки (Щит №1)");
        shopA.Add(new Machine("Токарный станок 1A"));
        shopA.Add(new Machine("Робот-манипулятор 2A"));

        var shopB = new ElectricNode("Цех Покраски (Щит №2)");
        shopB.Add(new Machine("Компрессор 1B"));
        shopB.Add(new Machine("Сушильная камера 2B"));

        _mainSubstation.Add(shopA);
        _mainSubstation.Add(shopB);
    }

    public void ActivateEmergencyMode()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("АКТИВАЦИЯ АВАРИЙНОГО РЕЖИМА!");
        Console.WriteLine("========================================");
        
        _mainSubstation.Shutdown();
        
        Console.WriteLine("\n--- Все системы обесточены. Пауза 2 сек... ---");
        
        _mainSubstation.SwitchToGenerator();
        
        Console.WriteLine("\n========================================");
        Console.WriteLine("Завод работает на резервном питании.");
        Console.WriteLine("========================================");
    }
}

class Program
{
    static void Main()
    {
        PowerSystemFacade factoryPowerControl = new PowerSystemFacade();

        Console.WriteLine("Система готова. Нажмите Enter для имитации аварии.");
        Console.ReadLine();

        factoryPowerControl.ActivateEmergencyMode();

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}