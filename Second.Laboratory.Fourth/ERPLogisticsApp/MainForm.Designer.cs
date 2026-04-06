namespace ERPLogisticsApp;

// Designer code for MainForm
partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel panelStatus;
    private Label labelStatus;
    private Button buttonNextState;
    private ComboBox comboBoxStrategy;
    private Button buttonExecuteStrategy;
    private ListBox listBoxLog;
    private Button buttonReset;

    private void InitializeComponent()
    {
        this.panelStatus = new Panel();
        this.labelStatus = new Label();
        this.buttonNextState = new Button();
        this.comboBoxStrategy = new ComboBox();
        this.buttonExecuteStrategy = new Button();
        this.listBoxLog = new ListBox();
        this.buttonReset = new Button();
        this.SuspendLayout();

        // panelStatus
        this.panelStatus.Location = new Point(12, 12);
        this.panelStatus.Name = "panelStatus";
        this.panelStatus.Size = new Size(360, 40);
        this.panelStatus.TabIndex = 0;

        // labelStatus
        this.labelStatus.AutoSize = true;
        this.labelStatus.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
        this.labelStatus.Location = new Point(12, 15);
        this.labelStatus.Name = "labelStatus";
        this.labelStatus.Size = new Size(60, 20);
        this.labelStatus.TabIndex = 1;
        this.labelStatus.Text = "Статус";

        // buttonNextState
        this.buttonNextState.Location = new Point(12, 65);
        this.buttonNextState.Name = "buttonNextState";
        this.buttonNextState.Size = new Size(100, 30);
        this.buttonNextState.TabIndex = 2;
        this.buttonNextState.Text = "Следующее состояние";
        this.buttonNextState.UseVisualStyleBackColor = true;
        this.buttonNextState.Click += new EventHandler(this.buttonNextState_Click);

        // comboBoxStrategy
        this.comboBoxStrategy.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxStrategy.FormattingEnabled = true;
        this.comboBoxStrategy.Items.AddRange(new object[] {
            "Железная дорога",
            "Автомобильный транспорт",
            "Авиадоставка"});
        this.comboBoxStrategy.Location = new Point(12, 105);
        this.comboBoxStrategy.Name = "comboBoxStrategy";
        this.comboBoxStrategy.Size = new Size(200, 21);
        this.comboBoxStrategy.TabIndex = 3;
        this.comboBoxStrategy.SelectedIndexChanged += new EventHandler(this.comboBoxStrategy_SelectedIndexChanged);

        // buttonExecuteStrategy
        this.buttonExecuteStrategy.Location = new Point(12, 132);
        this.buttonExecuteStrategy.Name = "buttonExecuteStrategy";
        this.buttonExecuteStrategy.Size = new Size(100, 30);
        this.buttonExecuteStrategy.TabIndex = 4;
        this.buttonExecuteStrategy.Text = "Выполнить стратегию";
        this.buttonExecuteStrategy.UseVisualStyleBackColor = true;
        this.buttonExecuteStrategy.Click += new EventHandler(this.buttonExecuteStrategy_Click);

        // listBoxLog
        this.listBoxLog.FormattingEnabled = true;
        this.listBoxLog.Location = new Point(12, 170);
        this.listBoxLog.Name = "listBoxLog";
        this.listBoxLog.Size = new Size(360, 147);
        this.listBoxLog.TabIndex = 5;

        // buttonReset
        this.buttonReset.Location = new Point(12, 323);
        this.buttonReset.Name = "buttonReset";
        this.buttonReset.Size = new Size(100, 30);
        this.buttonReset.TabIndex = 6;
        this.buttonReset.Text = "Сбросить";
        this.buttonReset.UseVisualStyleBackColor = true;
        this.buttonReset.Click += new EventHandler(this.buttonReset_Click);

        // MainForm
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(384, 361);
        this.Controls.Add(this.buttonReset);
        this.Controls.Add(this.listBoxLog);
        this.Controls.Add(this.buttonExecuteStrategy);
        this.Controls.Add(this.comboBoxStrategy);
        this.Controls.Add(this.buttonNextState);
        this.Controls.Add(this.labelStatus);
        this.Controls.Add(this.panelStatus);
        this.Name = "MainForm";
        this.Text = "Управление заказами ERP";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
