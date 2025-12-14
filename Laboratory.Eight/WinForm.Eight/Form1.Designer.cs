namespace WinForm.Eight;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.rtbOutput = new System.Windows.Forms.RichTextBox();
        this.rtbFileEditor = new System.Windows.Forms.RichTextBox(); 
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.btnFile = new System.Windows.Forms.Button(); // Объединено: Load / Save
        this.btnSort = new System.Windows.Forms.Button(); // Отдельно: Сортировать
        this.btnPerimeters = new System.Windows.Forms.Button(); // Отдельно: Периметры
        this.btnCircumferences = new System.Windows.Forms.Button(); // Отдельно: Длины окружностей
        this.SuspendLayout();
        // 
        // rtbOutput
        // 
        // RIGHT SIDE: Data Display/Analysis Output
        this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right) 
        | System.Windows.Forms.AnchorStyles.Bottom)));
        this.rtbOutput.Location = new System.Drawing.Point(448, 12);
        this.rtbOutput.Name = "rtbOutput";
        this.rtbOutput.ReadOnly = true;
        this.rtbOutput.Size = new System.Drawing.Size(422, 390);
        this.rtbOutput.TabIndex = 0;
        this.rtbOutput.Text = "Результаты анализа и текущий список фигур появятся здесь после обработки.";
        // 
        // rtbFileEditor
        // 
        // LEFT SIDE: File Content Editor
        this.rtbFileEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.rtbFileEditor.Location = new System.Drawing.Point(12, 12);
        this.rtbFileEditor.Name = "rtbFileEditor";
        this.rtbFileEditor.Size = new System.Drawing.Size(430, 390);
        this.rtbFileEditor.TabIndex = 1;
        this.rtbFileEditor.Text = "Введите данные фигур здесь, по одной на строке.\n(Круг: x y r color)\n(Квадрат: x1 y1 x2 y2 x3 y3 x4 y4)";
        this.rtbFileEditor.TextChanged += new System.EventHandler(this.rtbFileEditor_TextChanged);
        // 
        // openFileDialog1
        // 
        this.openFileDialog1.FileName = "openFileDialog1";
        this.openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        // 
        // saveFileDialog1
        // 
        this.saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.txt)|*.*";
        // 
        // btnFile
        // 
        this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnFile.Location = new System.Drawing.Point(12, 414);
        this.btnFile.Name = "btnFile";
        this.btnFile.Size = new System.Drawing.Size(210, 35);
        this.btnFile.TabIndex = 2;
        this.btnFile.Text = "Загрузить / Сохранить Файл";
        this.btnFile.UseVisualStyleBackColor = true;
        this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
        // 
        // btnSort
        // 
        this.btnSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnSort.Location = new System.Drawing.Point(228, 414);
        this.btnSort.Name = "btnSort";
        this.btnSort.Size = new System.Drawing.Size(210, 35);
        this.btnSort.TabIndex = 3;
        this.btnSort.Text = "Сортировать по площади";
        this.btnSort.UseVisualStyleBackColor = true;
        this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
        // 
        // btnPerimeters
        // 
        this.btnPerimeters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnPerimeters.Location = new System.Drawing.Point(444, 414);
        this.btnPerimeters.Name = "btnPerimeters";
        this.btnPerimeters.Size = new System.Drawing.Size(210, 35);
        this.btnPerimeters.TabIndex = 4;
        this.btnPerimeters.Text = "Периметры (>1 четверти)";
        this.btnPerimeters.UseVisualStyleBackColor = true;
        this.btnPerimeters.Click += new System.EventHandler(this.btnPerimeters_Click);
        // 
        // btnCircumferences
        // 
        this.btnCircumferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCircumferences.Location = new System.Drawing.Point(660, 414);
        this.btnCircumferences.Name = "btnCircumferences";
        this.btnCircumferences.Size = new System.Drawing.Size(210, 35);
        this.btnCircumferences.TabIndex = 5;
        this.btnCircumferences.Text = "Длины окружностей (убывание)";
        this.btnCircumferences.UseVisualStyleBackColor = true;
        this.btnCircumferences.Click += new System.EventHandler(this.btnCircumferences_Click);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(882, 461);
        this.Controls.Add(this.btnCircumferences);
        this.Controls.Add(this.btnPerimeters);
        this.Controls.Add(this.btnSort);
        this.Controls.Add(this.btnFile);
        this.Controls.Add(this.rtbFileEditor);
        this.Controls.Add(this.rtbOutput);
        this.MinimumSize = new System.Drawing.Size(890, 500);
        this.Name = "Form1";
        this.Text = "Shapes Editor & Analyzer (File Input)";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtbOutput;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Button btnFile; // Load/Save
    private System.Windows.Forms.Button btnSort; // Sort
    private System.Windows.Forms.Button btnPerimeters; // Perimeters
    private System.Windows.Forms.Button btnCircumferences; // Circumferences
    private System.Windows.Forms.RichTextBox rtbFileEditor; 
}