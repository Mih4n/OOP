#import "@mih4n/ghost:1.0.0": *

#show: styles.standard

#(pages.title)(
  theme: "Делегаты и события. Паттерн проектирования «Наблюдатель» (Observer).",
  course: "ООПиП",
  number: 5
)

*Цель работы:* Изучение механизмов событийно-ориентированного программирования
в C\#. Научиться проектировать слабосвязанные системы, где один объект (Издатель)
уведомляет множество других объектов (Подписчиков) об изменениях своего
состояния, не зная об их внутренней реализации.

\

*Задание:*

Необходимо разработать иерархию классов, реализующую паттерн Observer
(Наблюдатель) с использованием механизма событий C\#

\

= Вывод в консоль
\

#picture(image("image.png", height: 150pt), "Интерфейс программы")

\

= UML -- диаграмма классов системы

\

#picture(image("umll.png", height: 150pt), [UML -- диаграмма классов системы])


\

*Вывод:* Работа продемонстрировала создание собственной коллекции, реализацию интерфейсов .NET, применение LINQ для фильтрации, проекции и группировки, а также визуализацию данных через WPF. Все операции корректно работают и результаты совпадают между разными реализациями менеджера.

#pagebreak()

#align(center)[
  *ПРИЛОЖЕНИЕ А*

  (обязательное)
]

\

#set text(size: 12pt)
#show raw.line: it => {
  set text(fill: black)
  it
}

MainWindow.xaml.cs

\

```txt
namespace Second.ConsoleApp.Fifth;

public class JamEventArgs : EventArgs
{
    public string ItemName { get; }
    public DateTime TimeOccurred { get; }

    public JamEventArgs(string itemName)
    {
        ItemName = itemName;
        TimeOccurred = DateTime.Now;
    }
}

public class ConveyorBelt
{
    public event EventHandler<JamEventArgs> ItemStuck = delegate { };

    public void SimulateWork()
    {
        Console.WriteLine("Конвейер запущен. Обработка деталей...");
        Thread.Sleep(1000);
        
        OnItemStuck(new JamEventArgs("Зубчатое колесо №42"));
    }

    protected virtual void OnItemStuck(JamEventArgs e)
    {
        Console.WriteLine($"\n[ДАТЧИК]: Обнаружена блокировка детали: {e.ItemName}!");
        ItemStuck?.Invoke(this, e);
    }
}


public class LightIndicator
{
    public void OnItemStuck(object? sender, JamEventArgs e)
    {
        Console.WriteLine("[СВЕТ]: Мигает КРАСНЫЙ индикатор! (Опасность)");
    }
}

public class SoundAlarm
{
    public void OnItemStuck(object? sender, JamEventArgs e)
    {
        Console.Beep(); 
        Console.WriteLine("[ЗВУК]: Активирован сигнал BEEP-BEEP-BEEP!");
    }
}

public class BeltDrive
{
    public void OnItemStuck(object? sender, JamEventArgs e)
    {
        Console.WriteLine("[ПРИВОД]: ЭКСТРЕННОЕ ТОРМОЖЕНИЕ. Лента остановлена.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Лабораторная работа №5: Паттерн Наблюдатель";

        ConveyorBelt belt = new ConveyorBelt();
        LightIndicator light = new LightIndicator();
        SoundAlarm sound = new SoundAlarm();
        BeltDrive drive = new BeltDrive();

        belt.ItemStuck += light.OnItemStuck;
        belt.ItemStuck += sound.OnItemStuck;
        belt.ItemStuck += drive.OnItemStuck;

        belt.SimulateWork();

        Console.WriteLine("\n" + new string('-', 45));

        Console.WriteLine("Отключаем звуковой сигнал для техобслуживания...");
        belt.ItemStuck -= sound.OnItemStuck;

        belt.SimulateWork();

        Console.WriteLine("\nРабота завершена. Нажмите любую клавишу...");
        Console.ReadKey();
    }
}
```