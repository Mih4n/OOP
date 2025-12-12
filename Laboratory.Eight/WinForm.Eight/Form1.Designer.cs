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
        this.btnLoad = new System.Windows.Forms.Button();
        this.btnSortArea = new System.Windows.Forms.Button();
        this.btnCalcPerim = new System.Windows.Forms.Button();
        this.btnCircumference = new System.Windows.Forms.Button();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.dgvShapes = new System.Windows.Forms.DataGridView();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnDelete = new System.Windows.Forms.Button();
        this.btnSave = new System.Windows.Forms.Button();
        this.lblInfo = new System.Windows.Forms.Label();
        this.txtInput1 = new System.Windows.Forms.TextBox();
        this.txtInput2 = new System.Windows.Forms.TextBox();
        this.txtInput3 = new System.Windows.Forms.TextBox();
        this.txtInput4 = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.dgvShapes)).BeginInit();
        this.SuspendLayout();
        // 
        // rtbOutput
        // 
        this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.rtbOutput.Location = new System.Drawing.Point(580, 12);
        this.rtbOutput.Name = "rtbOutput";
        this.rtbOutput.ReadOnly = true;
        this.rtbOutput.Size = new System.Drawing.Size(300, 396);
        this.rtbOutput.TabIndex = 0;
        this.rtbOutput.Text = "";
        // 
        // btnLoad
        // 
        this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnLoad.Location = new System.Drawing.Point(12, 414);
        this.btnLoad.Name = "btnLoad";
        this.btnLoad.Size = new System.Drawing.Size(130, 35);
        this.btnLoad.TabIndex = 1;
        this.btnLoad.Text = "Загрузить данные";
        this.btnLoad.UseVisualStyleBackColor = true;
        this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
        // 
        // btnSortArea
        // 
        this.btnSortArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnSortArea.Location = new System.Drawing.Point(148, 414);
        this.btnSortArea.Name = "btnSortArea";
        this.btnSortArea.Size = new System.Drawing.Size(130, 35);
        this.btnSortArea.TabIndex = 2;
        this.btnSortArea.Text = "Сортировать по площади";
        this.btnSortArea.UseVisualStyleBackColor = true;
        this.btnSortArea.Click += new System.EventHandler(this.btnSortArea_Click);
        // 
        // btnCalcPerim
        // 
        this.btnCalcPerim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnCalcPerim.Location = new System.Drawing.Point(284, 414);
        this.btnCalcPerim.Name = "btnCalcPerim";
        this.btnCalcPerim.Size = new System.Drawing.Size(130, 35);
        this.btnCalcPerim.TabIndex = 3;
        this.btnCalcPerim.Text = "Периметры (>1 четверти)";
        this.btnCalcPerim.UseVisualStyleBackColor = true;
        this.btnCalcPerim.Click += new System.EventHandler(this.btnCalcPerim_Click);
        // 
        // btnCircumference
        // 
        this.btnCircumference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnCircumference.Location = new System.Drawing.Point(420, 414);
        this.btnCircumference.Name = "btnCircumference";
        this.btnCircumference.Size = new System.Drawing.Size(152, 35);
        this.btnCircumference.TabIndex = 4;
        this.btnCircumference.Text = "Длины окружностей (убывание)";
        this.btnCircumference.UseVisualStyleBackColor = true;
        this.btnCircumference.Click += new System.EventHandler(this.btnCircumference_Click);
        // 
        // openFileDialog1
        // 
        this.openFileDialog1.FileName = "openFileDialog1";
        this.openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        // 
        // saveFileDialog1
        // 
        this.saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        // 
        // dgvShapes
        // 
        this.dgvShapes.AllowUserToAddRows = false;
        this.dgvShapes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.dgvShapes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvShapes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvShapes.Location = new System.Drawing.Point(12, 12);
        this.dgvShapes.Name = "dgvShapes";
        this.dgvShapes.ReadOnly = true;
        this.dgvShapes.Size = new System.Drawing.Size(560, 250);
        this.dgvShapes.TabIndex = 5;
        // 
        // btnAdd
        // 
        this.btnAdd.Location = new System.Drawing.Point(420, 337);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(152, 29);
        this.btnAdd.TabIndex = 6;
        this.btnAdd.Text = "Добавить фигуру";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
        // 
        // btnDelete
        // 
        this.btnDelete.Location = new System.Drawing.Point(12, 370);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(130, 38);
        this.btnDelete.TabIndex = 7;
        this.btnDelete.Text = "Удалить выбранную";
        this.btnDelete.UseVisualStyleBackColor = true;
        this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
        // 
        // btnSave
        // 
        this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSave.Location = new System.Drawing.Point(580, 414);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(300, 35);
        this.btnSave.TabIndex = 8;
        this.btnSave.Text = "Сохранить изменения в файл";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // 
        // lblInfo
        // 
        this.lblInfo.AutoSize = true;
        this.lblInfo.Location = new System.Drawing.Point(12, 275);
        this.lblInfo.Name = "lblInfo";
        this.lblInfo.Size = new System.Drawing.Size(393, 13);
        this.lblInfo.TabIndex = 9;
        this.lblInfo.Text = "Введите данные для новой фигуры (Круг: x y r color; Квадрат: x1 y1 x2 y2 x3 y3 x4 y4)";
        // 
        // txtInput1
        // 
        this.txtInput1.Location = new System.Drawing.Point(12, 300);
        this.txtInput1.Name = "txtInput1";
        this.txtInput1.Size = new System.Drawing.Size(130, 20);
        this.txtInput1.TabIndex = 10;
        // 
        // txtInput2
        // 
        this.txtInput2.Location = new System.Drawing.Point(148, 300);
        this.txtInput2.Name = "txtInput2";
        this.txtInput2.Size = new System.Drawing.Size(130, 20);
        this.txtInput2.TabIndex = 11;
        // 
        // txtInput3
        // 
        this.txtInput3.Location = new System.Drawing.Point(284, 300);
        this.txtInput3.Name = "txtInput3";
        this.txtInput3.Size = new System.Drawing.Size(130, 20);
        this.txtInput3.TabIndex = 12;
        // 
        // txtInput4
        // 
        this.txtInput4.Location = new System.Drawing.Point(420, 300);
        this.txtInput4.Name = "txtInput4";
        this.txtInput4.Size = new System.Drawing.Size(152, 20);
        this.txtInput4.TabIndex = 13;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(892, 461);
        this.Controls.Add(this.txtInput4);
        this.Controls.Add(this.txtInput3);
        this.Controls.Add(this.txtInput2);
        this.Controls.Add(this.txtInput1);
        this.Controls.Add(this.lblInfo);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.btnDelete);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.dgvShapes);
        this.Controls.Add(this.btnCircumference);
        this.Controls.Add(this.btnCalcPerim);
        this.Controls.Add(this.btnSortArea);
        this.Controls.Add(this.btnLoad);
        this.Controls.Add(this.rtbOutput);
        this.MinimumSize = new System.Drawing.Size(900, 500);
        this.Name = "Form1";
        this.Text = "Shapes Editor & Analyzer";
        ((System.ComponentModel.ISupportInitialize)(this.dgvShapes)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtbOutput;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnSortArea;
    private System.Windows.Forms.Button btnCalcPerim;
    private System.Windows.Forms.Button btnCircumference;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.DataGridView dgvShapes;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.TextBox txtInput1;
    private System.Windows.Forms.TextBox txtInput2;
    private System.Windows.Forms.TextBox txtInput3;
    private System.Windows.Forms.TextBox txtInput4;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
}