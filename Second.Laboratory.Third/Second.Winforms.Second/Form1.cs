using System.Diagnostics;
using System.Text.Json;
using Second.ClassLib.Third;

namespace Second.Winforms.Second;

public partial class Form1 : Form
{
    private Panel controlPanel;
    private Panel dataPanel;
    private Button btnGenerate;
    private Button btnLoad;
    private Button btnFilter;
    private RadioButton rbDom;
    private RadioButton rbSax;
    private TextBox txtFilterThreshold;
    private DataGridView dgvMain;
    private DataGridView dgvFiltered;

    public Form1()
    {
        Text = "Sensor Manager";
        Size = new Size(1000, 600);

        controlPanel = new Panel { Dock = DockStyle.Left, Width = 250, Padding = new Padding(10) };
        dataPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

        Controls.Add(dataPanel);
        Controls.Add(controlPanel);

        btnGenerate = new Button { Text = "Generate & Save XML", Dock = DockStyle.Top, Height = 40 };
        btnGenerate.Click += BtnGenerate_Click;
        
        rbDom = new RadioButton { Text = "DOM (XmlDocument)", Dock = DockStyle.Top, Checked = true };
        rbSax = new RadioButton { Text = "SAX (XmlReader)", Dock = DockStyle.Top };
        
        btnLoad = new Button { Text = "Validate & Load", Dock = DockStyle.Top, Height = 40 };
        btnLoad.Click += BtnLoad_Click;

        txtFilterThreshold = new TextBox { Dock = DockStyle.Top, Text = "50" };
        btnFilter = new Button { Text = "LINQ Filter (> Threshold)", Dock = DockStyle.Top, Height = 40 };
        btnFilter.Click += BtnFilter_Click;

        controlPanel.Controls.Add(btnFilter);
        controlPanel.Controls.Add(txtFilterThreshold);
        controlPanel.Controls.Add(btnLoad);
        controlPanel.Controls.Add(rbSax);
        controlPanel.Controls.Add(rbDom);
        controlPanel.Controls.Add(btnGenerate);

        dgvMain = new DataGridView { Dock = DockStyle.Top, Height = 250, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        dgvFiltered = new DataGridView { Dock = DockStyle.Top, Height = 250, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

        dataPanel.Controls.Add(dgvFiltered);
        dataPanel.Controls.Add(dgvMain);

        InitializeComponent();
    }

    private IXmlManager GetManager() => rbDom.Checked ? new DomXmlManager() : new SaxXmlManager();

    private void BtnGenerate_Click(object? sender, EventArgs e)
    {
        var sensors = new List<Sensor>
        {
            new TemperatureSensor { Id = "T1", Threshold = 25.5, Unit = "C" },
            new PressureSensor { Id = "P1", Threshold = 101.3, MaxPressure = 150.0 },
            new HumiditySensor { Id = "H1", Threshold = 60.0, IsOutdoor = true }
        };
        GetManager().Save(sensors, "data.xml");
        MessageBox.Show("data.xml created.");
    }

    private void BtnLoad_Click(object? sender, EventArgs e)
    {
        if (XmlTools.Validate("data.xml", "schema.xsd", out string msg))
        {
            var sw = Stopwatch.StartNew();
            var loaded = GetManager().Load("data.xml");
            sw.Stop();
            
            dgvMain.DataSource = loaded;
            MessageBox.Show($"Loaded in {sw.ElapsedTicks} ms");
        }
        else
        {
            MessageBox.Show("Validation Error:\n" + msg);
        }
    }

    private void BtnFilter_Click(object? sender, EventArgs e)
    {
        if (double.TryParse(txtFilterThreshold.Text, out double th))
        {
            dgvFiltered.DataSource = GetManager().Load("data.xml").Where(s => s.Threshold > th).ToList();
            Console.WriteLine($"Filtered in {JsonSerializer.Serialize(dgvFiltered.DataSource)} ms");
        }
    }
}
