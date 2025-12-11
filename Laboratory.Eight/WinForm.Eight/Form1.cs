using ClassLib.Eight;
using ClassLib.Eight.Contracts;
using ClassLib.Eight.Services;

namespace WinForm.Eight;

public partial class Form1 : Form
{
    private List<IGeometricShape> shapes = new List<IGeometricShape>();

    public Form1()
    {
        InitializeComponent();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            shapes.Clear();
            rtbOutput.Clear();
            try
            {
                string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                foreach (var line in lines)
                {
                    ParseLine(line);
                }
                DisplayShapes(shapes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message);
            }
        }
    }

    private void ParseLine(string line)
    {
        var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 8)
        {
            double[] coords = parts.Select(double.Parse).ToArray();
            shapes.Add(new Square(coords));
        }

        else if (parts.Length == 4)
        {
            double x = double.Parse(parts[0]);
            double y = double.Parse(parts[1]);
            double r = double.Parse(parts[2]);
            string color = parts[3];
            shapes.Add(new Circle(x, y, r, color));
        }
    }

    private void DisplayShapes(IEnumerable<IGeometricShape> shapesToDisplay)
    {
        rtbOutput.Clear();
        foreach (var shape in shapesToDisplay)
        {
            if (shape is Circle circle)
            {
                rtbOutput.SelectionColor = circle.ShapeColor;
            }
            else
            {
                rtbOutput.SelectionColor = Color.Black;
            }
            
            rtbOutput.AppendText(shape.GetInfo() + Environment.NewLine);
        }
    }

    private void btnSortArea_Click(object sender, EventArgs e)
    {
        shapes.Sort(new AreaComparer());
        DisplayShapes(shapes);
    }

    private void btnCalcPerim_Click(object sender, EventArgs e)
    {
        rtbOutput.Clear();
        rtbOutput.SelectionColor = Color.Black;
        rtbOutput.AppendText("Квадраты в разных четвертях:\n");

        foreach (var shape in shapes)
        {
            if (shape is Square sq)
            {
                if (sq.SpansMultipleQuadrants())
                {
                    rtbOutput.AppendText($"Периметр: {sq.CalculatePerimeter():F2}\n");
                }
            }
        }
    }

    private void btnCircumference_Click(object sender, EventArgs e)
    {
        rtbOutput.Clear();
        
        var circles = shapes.OfType<Circle>()
            .OrderByDescending(c => c.CalculateCircumference())
            .ToList();

        foreach (var c in circles)
        {
            rtbOutput.SelectionColor = c.ShapeColor;
            rtbOutput.AppendText($"Длина окружности: {c.CalculateCircumference():F2} (Цвет: {c.ShapeColor.Name})\n");
        }
    }
}