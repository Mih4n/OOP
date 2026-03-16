#import "@mih4n/ghost:1.0.0": *

#show: styles.standard

#(pages.title)(
  theme: "Структурные паттерны проектирования: Декоратор (Decorator) и Адаптер (Adapter).",
  course: "ООПиП",
  number: 2
)

*Цель работы:* Изучение и практическое применение структурных паттернов
проектирования. Научиться расширять функциональность объектов без изменения их
исходного кода (Декоратор) и обеспечивать взаимодействие классов с
несовместимыми интерфейсами (Адаптер) в рамках выбранной специализации на
языке C\#.

\

*Задание:*

+ Проектирование иерархии:
    + Для Декоратора определить базовый интерфейс (или абстрактный класс) и реализовать один основной класс. Создать не менее 3 конкретных декораторов, расширяющих поведение базового объекта. 
    + Для Адаптера определить «целевой» интерфейс (который нужен вашей системе) и «адаптируемый» класс (сторонний/устаревший код). Реализовать класс-адаптер.
+ Классовая структура:
    + Суммарно в работе должно быть задействовано не менее 6 классов. Классы должны содержать логику, специфичную для выбранного направления.
+ Использование LINQ:
    + Применить LINQ для управления списком примененных декораторов (например, фильтрация активных эффектов, подсчет суммарной стоимости или характеристик через Sum() , SelectMany() или Where() ).
+ Визуализация (GUI):
    + Реализовать графический интерфейс (WPF или WinForms).

\

= Описание реализации:

\

= Реализация структурных паттернов проектирования

\

== Проектирование иерархии

\

В рамках лабораторной работы были реализованы структурные паттерны проектирования *Decorator* и *Adapter* на языке программирования C\#.

\

=== Реализация паттерна Decorator

\

Для реализации паттерна *Decorator* был определён базовый интерфейс IProductionProcess, который описывает общий интерфейс технологического процесса. Интерфейс содержит два метода:

+ GetDescription() — возвращает текстовое описание технологического процесса.
+ GetDuration() — возвращает длительность выполнения процесса.

На основе данного интерфейса реализован основной класс BaseTechProcess, представляющий базовый технологический процесс сборки изделия. Данный класс возвращает базовое описание процесса и его стандартную длительность.

Для расширения функциональности без изменения исходного класса был создан абстрактный класс ProcessDecorator. Он также реализует интерфейс IProductionProcess и содержит ссылку на объект типа IProductionProcess, который декорируется. Это позволяет динамически добавлять новые этапы технологического процесса.

На основе абстрактного декоратора реализованы три конкретных декоратора:

+ QualityControlDecorator — добавляет этап контроля качества продукции.
+ PackagingDecorator — добавляет этап упаковки изделия.
+ LabelingDecorator — добавляет этап маркировки продукции.

Каждый из декораторов расширяет функциональность базового объекта, добавляя дополнительное описание процесса и увеличивая его общую длительность.

Таким образом, итоговый технологический процесс формируется динамически путем последовательного "оборачивания" базового объекта соответствующими декораторами.

\

=== Реализация паттерна Adapter

\

Для демонстрации паттерна *Adapter* была реализована система взаимодействия с температурным датчиком.

В системе определён целевой интерфейс ITemperatureSensor, который используется внутренней логикой приложения. Данный интерфейс содержит метод:

+ GetTemperatureCelsius() — получение температуры в градусах Цельсия.

В качестве адаптируемого класса используется FahrenheitSensor, который имитирует сторонний или устаревший датчик, возвращающий температуру в градусах Фаренгейта через метод ReadFahrenheit().

Для обеспечения совместимости был реализован класс SensorAdapter, который преобразует данные старого датчика к формату, используемому системой. Адаптер получает значение температуры в Фаренгейтах и выполняет преобразование в градусы Цельсия по соответствующей формуле.

Таким образом обеспечивается взаимодействие между несовместимыми интерфейсами без изменения исходного кода стороннего класса.

\

== Классовая структура

\

В реализации лабораторной работы используется следующая структура классов:

+ IProductionProcess — интерфейс технологического процесса.
+ BaseTechProcess — базовый технологический процесс.
+ ProcessDecorator — абстрактный декоратор.
+ QualityControlDecorator — декоратор этапа контроля качества.
+ PackagingDecorator — декоратор этапа упаковки.
+ LabelingDecorator — декоратор этапа маркировки.
+ ITemperatureSensor — целевой интерфейс температурного датчика.
+ FahrenheitSensor — сторонний класс датчика температуры.
+ SensorAdapter — адаптер для преобразования температурных данных.

Таким образом, в работе используется более шести классов, каждый из которых реализует логику, соответствующую предметной области производственного процесса.

\

== Использование LINQ

\

Для управления набором применённых декораторов используется технология LINQ. В программе формируется коллекция возможных декораторов, после чего с помощью метода Where() выполняется фильтрация активных декораторов, выбранных пользователем в интерфейсе.

Далее метод Aggregate() применяется для последовательного оборачивания базового объекта выбранными декораторами. Это позволяет динамически формировать итоговый технологический процесс на основе выбранных этапов.

Использование LINQ делает код более компактным, читаемым и позволяет эффективно работать со списком декораторов.

\

== Визуализация (GUI)

\

Для взаимодействия пользователя с программой реализован графический интерфейс с использованием технологии *WPF (Windows Presentation Foundation)*.

Интерфейс приложения состоит из двух основных частей:

+ левая часть предназначена для управления системой;
+ правая часть отображает результаты работы программы.

В левой части интерфейса расположены элементы управления:

+ флажки (CheckBox), позволяющие включать или отключать дополнительные этапы технологического процесса (декораторы);
+ кнопка получения температуры с датчика.

В правой части интерфейса отображаются:

+ итоговое описание сформированного технологического процесса;
+ суммарная длительность выполнения процесса;
+ значение температуры, полученное через адаптер.

При изменении состояния флажков система автоматически пересчитывает итоговую конфигурацию технологического процесса и отображает обновлённые характеристики.

\

= Графический интерфейс WPF:

\

+ Левая колонка – фильтры и действия, включая расчет температуры и группировку.

+ Правая колонка – результаты фильтров и группировок.

\

#picture(image("image.png", height: 150pt), "Интерфейс программы")

\

= UML -- диаграмма классов системы

#picture(image("uml.png", height: 150pt), [UML -- диаграмма классов системы])


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
using System.Windows;
using System.Text.Json;
using Second.ClassLib.First;
using Second.Wpf.First.StageManagers;

namespace Second.Wpf.First;

public partial class MainWindow : Window
{
    private readonly MyLinkedList<AssemblyStage> data = new MyLinkedList<AssemblyStage>();
    private readonly IStageManager manager = new StageManagerQueryable();

    public MainWindow()
    {
        InitializeComponent();

        SeedData();
        RefreshSourceView();
        RefreshResultView(data);
    }

    private void SeedData()
    {
        data.Add(new ManualStage("Cutting", 40, DateTime.Now, "Workshop1", DangerLevel.High, true));
        data.Add(new AutomatedStage("Welding", 25, DateTime.Now, "Workshop2", DangerLevel.Medium, "M-01"));
        data.Add(new ManualStage("Painting", 55, DateTime.Now, "Workshop1", DangerLevel.Low, false));
        data.Add(new AutomatedStage("Inspection", 15, DateTime.Now, "Workshop3", DangerLevel.Low, "M-02"));
    }

    private void RefreshSourceView()
    {
        SourceList.ItemsSource = data.Select(ToJson).ToList();
    }

    private void RefreshResultView(IEnumerable<AssemblyStage> result)
    {
        ResultList.ItemsSource = result.Select(ToJson).ToList();
    }

    private static string ToJson(AssemblyStage stage)
    {
        return JsonSerializer.Serialize(stage, new JsonSerializerOptions { WriteIndented = false });
    }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        var query = data.AsEnumerable();

        if (UseDurationFilter.IsChecked == true && int.TryParse(DurationBox.Text, out int minDuration))
            query = manager.GetStagesLongerThan(query, minDuration);

        if (UseWorkshopFilter.IsChecked == true && !string.IsNullOrWhiteSpace(WorkshopBox.Text))
            query = query.Where(s => s.Workshop == WorkshopBox.Text);

        RefreshResultView(query);
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        UseDurationFilter.IsChecked = true;
        UseWorkshopFilter.IsChecked = false;
        DurationBox.Text = "30";
        WorkshopBox.Text = "";
        AverageResult.Text = "";
        GroupResult.ItemsSource = null;

        RefreshResultView(data);
    }

    private void Average_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(WorkshopBox.Text))
        {
            MessageBox.Show("Enter a workshop to calculate average");
            return;
        }

        double avg = manager.GetAverageDurationByWorkshop(data, WorkshopBox.Text);
        AverageResult.Text = $"Average duration in {WorkshopBox.Text}: {avg:F2} min";
    }

    private void Group_Click(object sender, RoutedEventArgs e)
    {
        var groups = manager.GroupByDangerLevel(data);

        GroupResult.ItemsSource = groups
            .Select(g => $"{g.Key}: {g.Count()} stage(s)")
            .ToList();
    }
}
```