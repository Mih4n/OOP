namespace WinForm.Eight;

partial class Form1
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.rtbOutput = new System.Windows.Forms.RichTextBox();
        this.btnLoad = new System.Windows.Forms.Button();
        this.btnSortArea = new System.Windows.Forms.Button();
        this.btnCalcPerim = new System.Windows.Forms.Button();
        this.btnCircumference = new System.Windows.Forms.Button();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.SuspendLayout();
        // 
        // rtbOutput
        // 
        this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.rtbOutput.Location = new System.Drawing.Point(12, 12);
        this.rtbOutput.Name = "rtbOutput";
        this.rtbOutput.ReadOnly = true;
        this.rtbOutput.Size = new System.Drawing.Size(560, 396);
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
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(584, 461);
        this.Controls.Add(this.btnCircumference);
        this.Controls.Add(this.btnCalcPerim);
        this.Controls.Add(this.btnSortArea);
        this.Controls.Add(this.btnLoad);
        this.Controls.Add(this.rtbOutput);
        this.MinimumSize = new System.Drawing.Size(600, 500);
        this.Name = "Form1";
        this.Text = "Shapes Application";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtbOutput;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnSortArea;
    private System.Windows.Forms.Button btnCalcPerim;
    private System.Windows.Forms.Button btnCircumference;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
}