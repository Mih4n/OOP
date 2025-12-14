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
        ParseFileEditorToShapes();
    }

    private void ParseLine(string line)
    {
        var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);

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

    private void btnFile_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("Нажмите 'Да' для Загрузки файла в редактор или 'Нет' для Сохранения содержимого редактора в файл.", 
                                     "Операция с файлом", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog1.FileName; 
                try
                {
                    rtbFileEditor.Text = File.ReadAllText(currentFilePath);
                    
                    ParseFileEditorToShapes(); 
                    DisplayShapes(shapes, "Файл загружен. Текущий список фигур:");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла: " + ex.Message);
                }
            }
        }
        else if (result == DialogResult.No)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return; 
                currentFilePath = saveFileDialog1.FileName;
            }

            try
            {
                File.WriteAllText(currentFilePath, rtbFileEditor.Text);
                MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи файла: " + ex.Message);
            }
        }
    }

    private void btnSort_Click(object sender, EventArgs e)
    {
        ParseFileEditorToShapes(); 

        if (shapes.Count == 0)
        {
            rtbOutput.AppendText("\nНет фигур для сортировки.");
            return;
        }

        var sortedList = shapes.ToList(); 
        sortedList.Sort(new AreaComparer());
        
        shapes.Clear();
        foreach (var shape in sortedList)
        {
            shapes.Add(shape);
        }

        UpdateFileEditorFromShapes(shapes);

        DisplayShapes(shapes, "--- Отсортировано по площади ---");
    }

    private void btnPerimeters_Click(object sender, EventArgs e)
    {
        ParseFileEditorToShapes(); 
        
        if (shapes.Count == 0)
        {
            rtbOutput.AppendText("\nНет фигур для анализа.");
            return;
        }
        
        CalculatePerimeters(); 
    }
    
    private void btnCircumferences_Click(object sender, EventArgs e)
    {
        ParseFileEditorToShapes(); 

        if (shapes.Count == 0)
        {
            rtbOutput.AppendText("\nНет фигур для анализа.");
            return;
        }
        
        CalculateCircumferences(); 
    }

    private void rtbFileEditor_TextChanged(object sender, EventArgs e)
    {
        if (rtbFileEditor.Focused)
        {
            rtbOutput.Clear();
            rtbOutput.AppendText("Данные в редакторе изменены. Нажмите любую кнопку для обработки и отображения результатов.");
        }
    }

    private void ParseFileEditorToShapes()
    {
        shapes.Clear();
        var lines = rtbFileEditor.Text.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            try
            {
                ParseLine(line.Trim());
            }
            catch {} 
        }
    }

    private void UpdateFileEditorFromShapes(IEnumerable<IGeometricShape> shapesToUse)
    {
        rtbFileEditor.Clear();
        var linesToSave = shapesToUse.Select(shape =>
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
        }).Where(line => line != null);
        
        rtbFileEditor.Text = string.Join(Environment.NewLine, linesToSave);
    }

    private void DisplayShapes(IEnumerable<IGeometricShape> shapesToDisplay, string header)
    {
        rtbOutput.Clear();
        rtbOutput.AppendText(header + Environment.NewLine);
        rtbOutput.AppendText($"Обработано фигур: {shapes.Count}. Отображается: {shapesToDisplay.Count()}\n\n");
        
        foreach (var shape in shapesToDisplay)
        {
            Color displayColor = Color.Black;
            if (shape is Circle circle)
            {
                try
                {
                    displayColor = Color.FromName(circle.ShapeColor.Name);
                }
                catch
                {
                    displayColor = Color.Black;
                }
            }
            
            rtbOutput.SelectionColor = displayColor;
            rtbOutput.AppendText(shape.GetInfo() + Environment.NewLine);
        }
        rtbOutput.SelectionColor = Color.Black;
    }
    
    private void CalculatePerimeters()
    {
        rtbOutput.Clear();
        rtbOutput.SelectionColor = Color.Black;
        rtbOutput.AppendText("--- Периметры квадратов, пересекающих несколько четвертей ---\n");

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

    // Logic for Circumferences
    private void CalculateCircumferences()
    {
        rtbOutput.Clear();
        rtbOutput.AppendText("--- Длины окружностей (по убыванию) ---\n");
        
        var circles = shapes.OfType<Circle>()
            .OrderByDescending(c => c.CalculateCircumference())
            .ToList();

        foreach (var c in circles)
        {
            Color displayColor = Color.Black;
            try
            {
                displayColor = Color.FromName(c.ShapeColor.Name);
            }
            catch {}

            rtbOutput.SelectionColor = displayColor;
            rtbOutput.AppendText($"Длина окружности: {c.CalculateCircumference():F2} (Цвет: {c.ShapeColor.Name})\n");
        }
        rtbOutput.SelectionColor = Color.Black;
    }
}