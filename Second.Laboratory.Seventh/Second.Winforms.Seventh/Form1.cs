using Second.Classlib.Seventh;

namespace Second.Winforms.Seventh;

public partial class Form1 : Form
{
    private readonly RobotManager robotManager = new RobotManager();

    public Form1()
    {
        InitializeComponent();
        Warehouse.Log = AppendLog;         
        SetupUI();
    }

    private void SetupUI()
    {
        var leftPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Left,
            Width = 280,
            Padding = new Padding(10)
        };

        leftPanel.Controls.Add(new Label { Text = "Команды перемещения", Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true }, 0, 0);

        AddCommandButton(leftPanel, "Переместить 10 Ноутбуков", () => robotManager.EnqueueCommand(new MoveGoodsCommand("Ноутбук", 10)));
        AddCommandButton(leftPanel, "Переместить 8 Мониторов", () => robotManager.EnqueueCommand(new MoveGoodsCommand("Монитор", 8)));
        AddCommandButton(leftPanel, "Переместить 20 Клавиатур", () => robotManager.EnqueueCommand(new MoveGoodsCommand("Клавиатура", 20)));
        AddCommandButton(leftPanel, "Переместить 15 Мышей", () => robotManager.EnqueueCommand(new MoveGoodsCommand("Мышь", 15)));
        AddCommandButton(leftPanel, "Переместить 5 Ноутбуков", () => robotManager.EnqueueCommand(new MoveGoodsCommand("Ноутбук", 5)));

        var btnStart = new Button { Text = "Запустить роботов (4 шт.)", Height = 40, Dock = DockStyle.Top };
        btnStart.Click += (s, e) => { robotManager.StartRobots(4); AppendLog("Роботы-погрузчики запущены (4 шт.)"); };
        
        var btnStop = new Button { Text = "Остановить всех роботов", Height = 40, Dock = DockStyle.Top };
        btnStop.Click += (s, e) => robotManager.Stop();

        var btnStock = new Button { Text = "Показать состояние склада", Height = 40, Dock = DockStyle.Top };
        btnStock.Click += (s, e) => Warehouse.Instance.ShowStock();

        leftPanel.Controls.Add(btnStart, 0, 6);
        leftPanel.Controls.Add(btnStop, 0, 7);
        leftPanel.Controls.Add(btnStock, 0, 8);

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
