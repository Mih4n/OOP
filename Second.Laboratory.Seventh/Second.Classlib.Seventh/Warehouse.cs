namespace Second.Classlib.Seventh;

public sealed class Warehouse
{
    public static Warehouse Instance => lazy.Value;
    private static readonly Lazy<Warehouse> lazy = new Lazy<Warehouse>(() => new Warehouse());

    private readonly object @lock = new Lock();
    private readonly Dictionary<string, int> stock = new();

    private Warehouse() { }

    public void AddGoods(string item, int quantity)
    {
        lock (@lock)
        {
            stock[item] = stock.GetValueOrDefault(item, 0) + quantity;
            Log($"[Склад] Добавлено: {quantity} × {item}. Остаток: {stock[item]}");
        }
    }

    public bool RemoveGoods(string item, int quantity)
    {
        lock (@lock)
        {
            if (stock.TryGetValue(item, out int current) && current >= quantity)
            {
                stock[item] -= quantity;
                Log($"[Склад] Отгружено: {quantity} × {item}. Остаток: {stock[item]}");
                return true;
            }
            Log($"[Склад] Ошибка: недостаточно товара '{item}' на складе!");
            return false;
        }
    }

    public void ShowStock()
    {
        lock (@lock)
        {
            Log("\n=== Текущее состояние склада ===");
            foreach (var item in stock)
                Log($"{item.Key}: {item.Value} ед.");
            Log("===============================\n");
        }
    }

    public static Action<string> Log { get; set; } = Console.WriteLine;
}
