using System.ComponentModel;
using System.Data;
using ClassLib.Eight;
using ClassLib.Eight.Contracts;
using ClassLib.Eight.Services;

namespace WinForm.Eight;

public partial class Form1 : Form
{
    private BindingList<IGeometricShape> shapes = new BindingList<IGeometricShape>();
    private string currentFilePath = string.Empty;

    public Form1()
    {
        InitializeComponent();
        dgvShapes.DataSource = shapes;
        dgvShapes.ReadOnly = false; 
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            currentFilePath = openFileDialog1.FileName; 
            shapes.Clear();
            rtbOutput.Clear();
            try
            {
                string[] lines = File.ReadAllLines(currentFilePath);
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
        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 8)
        {
            if (parts.All(p => double.TryParse(p, out _)))
            {
                double[] coords = parts.Select(double.Parse).ToArray();
                shapes.Add(new Square(coords));
            }
        }
        else if (parts.Length == 4)
        {
            if (double.TryParse(parts[0], out double x) && 
                double.TryParse(parts[1], out double y) &&
                double.TryParse(parts[2], out double r))
            {
                string color = parts[3];
                shapes.Add(new Circle(x, y, r, color));
            }
        }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentFilePath))
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = saveFileDialog1.FileName;
            }
            else
            {
                return; 
            }
        }

        try
        {
            var linesToSave = shapes.Select(shape =>
                {
                    if (shape is Square sq)
                    {
                        return sq.ToDataString(); 
                    }
                    else if (shape is Circle c)
                    {
                        return c.ToDataString();
                    }
                    return null;
                }
            )
            .Where(line => line != null);

            File.WriteAllLines(currentFilePath, linesToSave);
            MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка записи файла: " + ex.Message);
        }
    }
    
    private void btnAdd_Click(object sender, EventArgs e)
    {
        string combinedInput = $"{txtInput1.Text} {txtInput2.Text} {txtInput3.Text} {txtInput4.Text}";
        var parts = combinedInput.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        try
        {
            if (parts.Length == 8)
            {
                if (parts.All(p => double.TryParse(p, out _)))
                {
                    double[] coords = parts.Select(double.Parse).ToArray();
                    shapes.Add(new Square(coords));
                }
                else
                {
                     MessageBox.Show("Для квадрата нужно ввести 8 чисел.");
                     return;
                }
            }
            else if (parts.Length == 4)
            {
                if (double.TryParse(parts[0], out double x) && 
                    double.TryParse(parts[1], out double y) &&
                    double.TryParse(parts[2], out double r))
                {
                    string color = parts[3];
                    shapes.Add(new Circle(x, y, r, color));
                }
                else
                {
                    MessageBox.Show("Неверный формат для круга (ожидается: x y r color).");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Введите 4 параметра для круга или 8 чисел для квадрата (можно использовать пробелы в полях ввода).");
                return;
            }
            
            txtInput1.Clear();
            txtInput2.Clear();
            txtInput3.Clear();
            txtInput4.Clear();

            DisplayShapes(shapes);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при создании фигуры: " + ex.Message);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvShapes.SelectedRows.Count > 0)
        {
            for (int i = dgvShapes.SelectedRows.Count - 1; i >= 0; i--)
            {
                var selectedRow = dgvShapes.SelectedRows[i];
                if (selectedRow.DataBoundItem is IGeometricShape shapeToDelete)
                {
                    shapes.Remove(shapeToDelete);
                }
            }
            DisplayShapes(shapes);
        }
        else
        {
            MessageBox.Show("Выберите строку для удаления.");
        }
    }

    private void DisplayShapes(IEnumerable<IGeometricShape> shapesToDisplay)
    {
        rtbOutput.Clear();
        foreach (var shape in shapesToDisplay)
        {
            if (shape is Circle circle)
            {
                try
                {
                    rtbOutput.SelectionColor = Color.FromName(circle.ShapeColor.Name);
                }
                catch
                {
                    rtbOutput.SelectionColor = Color.Black;
                }
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
        var sortedList = shapes.ToList(); 
        sortedList.Sort(new AreaComparer());
        
        shapes.Clear();
        foreach (var shape in sortedList)
        {
            shapes.Add(shape);
        }

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
            try
            {
                rtbOutput.SelectionColor = Color.FromName(c.ShapeColor.Name);
            }
            catch
            {
                rtbOutput.SelectionColor = Color.Black;
            }
            rtbOutput.AppendText($"Длина окружности: {c.CalculateCircumference():F2} (Цвет: {c.ShapeColor.Name})\n");
        }
    }
}