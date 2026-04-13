using Microsoft.Extensions.Configuration;
using Second.Classlib.Sixth.Models;
using Second.Classlib.Sixth.Repositories;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string not found.");

var supplierRepo = new SuppliersRepository(connectionString);
var materialRepo = new MaterialsRepository(connectionString);
var deliveryRepo = new DeliveriesRepository(connectionString);

Console.WriteLine("=== СКЛАД ===");
Console.WriteLine("Команды:");
Console.WriteLine("  ls-s, add-s <n>, upd-s <id> <n>, rm-s <id>");
Console.WriteLine("  ls-m, add-m <n>, upd-m <id> <n>, rm-m <id>");
Console.WriteLine("  ls-d, add-d <sid> <mid>, upd-d <id> <sid> <mid>, rm-d <id>");

while (true)
{
    Console.Write("\n> ");
    var input = Console.ReadLine()?.Split(' ');
    if (input == null || input[0] == "exit") break;

    try
    {
        switch (input[0])
        {
            // --- Suppliers ---
            case "ls-s":
                foreach (var s in supplierRepo.GetAll()) Console.WriteLine($"[{s.Id}] {s.Name}");
                break;
            case "add-s":
                supplierRepo.Create(new Supplier { Name = input[1] });
                break;
            case "upd-s":
                supplierRepo.Update(new Supplier { Id = int.Parse(input[1]), Name = input[2] });
                break;
            case "rm-s":
                supplierRepo.Delete(int.Parse(input[1]));
                break;

            // --- Materials ---
            case "ls-m":
                foreach (var m in materialRepo.GetAll()) Console.WriteLine($"[{m.Id}] {m.Name}");
                break;
            case "add-m":
                materialRepo.Create(new Material { Name = input[1] });
                break;
            case "upd-m":
                materialRepo.Update(new Material { Id = int.Parse(input[1]), Name = input[2] });
                break;
            case "rm-m":
                materialRepo.Delete(int.Parse(input[1]));
                break;

            // --- Deliveries ---
            case "ls-d":
                foreach (var d in deliveryRepo.GetAll()) 
                    Console.WriteLine($"ID:{d.Id} | S_ID:{d.SupplierId} | M_ID:{d.MaterialId} | {d.DeliveryDate}");
                break;
            case "add-d":
                deliveryRepo.Create(new Delivery { SupplierId = int.Parse(input[1]), MaterialId = int.Parse(input[2]), DeliveryDate = DateTime.Now });
                break;
            case "upd-d":
                deliveryRepo.Update(new Delivery { Id = int.Parse(input[1]), SupplierId = int.Parse(input[2]), MaterialId = int.Parse(input[3]) });
                break;
            case "rm-d":
                deliveryRepo.Delete(int.Parse(input[1]));
                break;

            default: Console.WriteLine("Unknown command."); break;
        }
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
}