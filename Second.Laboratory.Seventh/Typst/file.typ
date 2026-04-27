#import "@mih4n/ghost:1.0.0": *
#show: standard-styles

#(pages.title)(
  course: "Программирование на C#",
  supervisor: "Асенчик О.Д",
  supervisorTitle: "доцент",
  number: 7,
  theme: "Многопоточность (TPL) и паттерны проектирования Command, Singleton."
)

*Цель работы:* Научиться разрабатывать многопоточные приложения с использованием библиотеки Task Parallel Library (TPL). Освоить паттерн Singleton для управления разделяемыми ресурсами и паттерн Command для инкапсуляции действий.

= Теоретические сведения

Многопоточность позволяет выполнять несколько частей программы одновременно, повышая отзывчивость интерфейса и эффективность использования процессора.

*Task Parallel Library (TPL)* предоставляет высокий уровень абстракции над потоками через класс `Task`, `async/await` и `Parallel`.

*Паттерн Singleton* гарантирует существование только одного экземпляра класса и предоставляет глобальную точку доступа к нему (потокобезопасная реализация с `Lazy<T>`).

*Паттерн Command* инкапсулирует запрос как объект, позволяя ставить команды в очередь, выполнять их асинхронно и поддерживать отмену операций.

= Задание (Вариант 9)

Система управления складом. Роботы-погрузчики (потоки) выполняют команды перемещения (Command) из общей очереди.

*Требования:*
- Реализовать приложение на *Windows Forms*.
- Левая колонка — кнопки для отправки команд.
- Правая колонка — область вывода результатов и логов работы роботов.
- Использовать *TPL* для параллельной работы роботов-погрузчиков.
- Реализовать *потокобезопасный* паттерн *Singleton* для склада.
- Реализовать паттерн *Command* для команд перемещения товаров.
- Обеспечить потокобезопасный вывод информации в интерфейс.

= Реализация приложения

== 1. Паттерн Singleton — Класс Warehouse

```csharp
public sealed class Warehouse
{
    private static readonly Lazy<Warehouse> _lazy = new Lazy<Warehouse>(() => new Warehouse());
    public static Warehouse Instance => _lazy.Value;

    private readonly Dictionary<string, int> _stock = new();
    private readonly object _lockObject = new object();

    private Warehouse() { }

    public void AddGoods(string item, int quantity)
    {
        lock (_lockObject)
        {
            _stock[item] = _stock.GetValueOrDefault(item, 0) + quantity;
            Log($"[Склад] Добавлено: {quantity} × {item}. Остаток: {_stock[item]}");
        }
    }

    public bool RemoveGoods(string item, int quantity)
    {
        lock (_lockObject)
        {
            if (_stock.TryGetValue(item, out int current) && current >= quantity)
            {
                _stock[item] -= quantity;
                Log($"[Склад] Отгружено: {quantity} × {item}. Остаток: {_stock[item]}");
                return true;
            }
            Log($"[Склад] Ошибка: недостаточно товара '{item}' на складе!");
            return false;
        }
    }

    public void ShowStock()
    {
        lock (_lockObject)
        {
            Log("\n=== Текущее состояние склада ===");
            foreach (var item in _stock)
                Log($"{item.Key}: {item.Value} ед.");
            Log("===============================\n");
        }
    }

    // Метод для безопасного логирования в UI (будет переопределён в форме)
    public static Action<string> Log { get; set; } = Console.WriteLine;
}
```

== 2. Паттерн Command

```csharp
public interface ICommand
{
    void Execute();
    void Undo();
}

public class MoveGoodsCommand : ICommand
{
    private readonly string _item;
    private readonly int _quantity;
    private readonly Warehouse _warehouse = Warehouse.Instance;

    public MoveGoodsCommand(string item, int quantity)
    {
        _item = item;
        _quantity = quantity;
    }

    public void Execute()
    {
        _warehouse.RemoveGoods(_item, _quantity);
    }

    public void Undo()
    {
        _warehouse.AddGoods(_item, _quantity);
    }
}
```

== 3. Класс управления роботами (Invoker)

```csharp
public class RobotManager
{
    private readonly ConcurrentQueue<ICommand> _queue = new();
    private readonly List<Task> _robots = new();
    private volatile bool _isRunning = true;

    public void EnqueueCommand(ICommand command)
    {
        _queue.Enqueue(command);
    }

    public void StartRobots(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            int robotId = i;
            _robots.Add(Task.Run(() => RobotWorker(robotId)));
        }
    }

    private async Task RobotWorker(int robotId)
    {
        while (_isRunning || !_queue.IsEmpty)
        {
            if (_queue.TryDequeue(out ICommand command))
            {
                Warehouse.Log($"Робот #{robotId} → выполняет команду...");
                command.Execute();
                await Task.Delay(700); // имитация времени работы
            }
            else
            {
                await Task.Delay(200);
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
        Task.WaitAll(_robots.ToArray());
        Warehouse.Log("Все роботы завершены.");
    }
}
```

== 4. Главная форма (Form1.cs) — WinForms интерфейс

```csharp
public partial class Form1 : Form
{
    private readonly RobotManager _robotManager = new RobotManager();

    public Form1()
    {
        InitializeComponent();
        Warehouse.Log = AppendLog;           // перенаправляем логи в RichTextBox
        SetupUI();
    }

    private void SetupUI()
    {
        // Левая панель — кнопки
        var leftPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Left,
            Width = 280,
            Padding = new Padding(10)
        };

        leftPanel.Controls.Add(new Label { Text = "Команды перемещения", Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true }, 0, 0);

        AddCommandButton(leftPanel, "Переместить 10 Ноутбуков", () => _robotManager.EnqueueCommand(new MoveGoodsCommand("Ноутбук", 10)));
        AddCommandButton(leftPanel, "Переместить 8 Мониторов", () => _robotManager.EnqueueCommand(new MoveGoodsCommand("Монитор", 8)));
        AddCommandButton(leftPanel, "Переместить 20 Клавиатур", () => _robotManager.EnqueueCommand(new MoveGoodsCommand("Клавиатура", 20)));
        AddCommandButton(leftPanel, "Переместить 15 Мышей", () => _robotManager.EnqueueCommand(new MoveGoodsCommand("Мышь", 15)));
        AddCommandButton(leftPanel, "Переместить 5 Ноутбуков", () => _robotManager.EnqueueCommand(new MoveGoodsCommand("Ноутбук", 5)));

        var btnStart = new Button { Text = "Запустить роботов (4 шт.)", Height = 40, Dock = DockStyle.Top };
        btnStart.Click += (s, e) => { _robotManager.StartRobots(4); AppendLog("Роботы-погрузчики запущены (4 шт.)"); };
        
        var btnStop = new Button { Text = "Остановить всех роботов", Height = 40, Dock = DockStyle.Top };
        btnStop.Click += (s, e) => _robotManager.Stop();

        var btnStock = new Button { Text = "Показать состояние склада", Height = 40, Dock = DockStyle.Top };
        btnStock.Click += (s, e) => Warehouse.Instance.ShowStock();

        leftPanel.Controls.Add(btnStart, 0, 6);
        leftPanel.Controls.Add(btnStop, 0, 7);
        leftPanel.Controls.Add(btnStock, 0, 8);

        // Правая панель — лог
        var rightPanel = new Panel { Dock = DockStyle.Fill };
        richTextBox1 = new RichTextBox
        {
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 10),
            BackColor = Color.Black,
            ForeColor = Color.Lime,
            ReadOnly = true
        };
        rightPanel.Controls.Add(richTextBox1);

        // Разделение формы
        var splitter = new SplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterWidth = 8,
            SplitterDistance = 290
        };

        splitter.Panel1.Controls.Add(leftPanel);
        splitter.Panel2.Controls.Add(rightPanel);

        Controls.Add(splitter);
        Text = "Система управления складом — Вариант 9";
        Size = new Size(950, 650);
        StartPosition = FormStartPosition.CenterScreen;
    }

    private void AddCommandButton(TableLayoutPanel panel, string text, Action action)
    {
        var btn = new Button 
        { 
            Text = text, 
            Height = 35, 
            Dock = DockStyle.Top,
            Margin = new Padding(0, 5, 0, 0)
        };
        btn.Click += (s, e) => action();
        panel.Controls.Add(btn);
    }

    private void AppendLog(string message)
    {
        if (richTextBox1.InvokeRequired)
        {
            richTextBox1.Invoke(new Action(() => AppendLogInternal(message)));
        }
        else
        {
            AppendLogInternal(message);
        }
    }

    private void AppendLogInternal(string message)
    {
        richTextBox1.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
        richTextBox1.ScrollToCaret();
    }

    private RichTextBox richTextBox1;
}
```

= Результат работы программы

При запуске приложения пользователь видит:

- Слева: панель управления с кнопками отправки команд и управления роботами.
- Справа: окно лога с цветным выводом всех действий (время, сообщение от склада и роботов).

Программа демонстрирует:
- Потокобезопасную работу нескольких роботов через TPL.
- Корректное использование паттерна Singleton для склада.
- Инкапсуляцию действий через паттерн Command.
- Безопасный вывод информации из фоновых потоков в UI.

#pagebreak()
#appendix-heading()[Приложение А]
#align(center)[(обязательное)]

Весь необходимый код классов и главной формы приведён выше.

Готово!  

Теперь приложение полностью на *WinForms* с удобным разделением:  
*левая колонка* — кнопки команд,  
*правая колонка* — результат выполнения (лог).

Если нужно добавить кнопки Undo, изменить товары, количество роботов или сделать дизайн красивее — скажите, доработаю быстро.