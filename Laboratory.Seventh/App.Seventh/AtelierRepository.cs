using ClassLib.Seventh.Contracts;

namespace App.Seventh;


public class AtelierRepository
{
    private readonly List<object> storage = new();

    public void Add(object product)
    {
        if (product == null) return;
        storage.Add(product);
        Console.WriteLine($"[Repository] Добавлен объект типа {product.GetType().Name}");
    }

    public void AddRange(IEnumerable<object> products)
    {
        foreach (var p in products) Add(p);
    }
    
    public IEnumerable<object> GetAll() => storage;

    public IEnumerable<T> Find<T>(Func<T, bool> predicate)
    {
        return storage.OfType<T>().Where(predicate);
    }

    public void Remove(object product)
    {
        if (storage.Remove(product))
        {
            Console.WriteLine($"[Repository] Объект удален.");
        }
        else
        {
            Console.WriteLine($"[Repository] Объект не найден.");
        }
    }
}