namespace ERPLogisticsApp;

public partial class MainForm : Form
{
    private OrderContext order;
    private ITransportStrategy currentStrategy;

    public MainForm()
    {
        InitializeComponent();
        InitializeOrder();
        UpdateUI();
    }

    private void InitializeOrder()
    {
        order = new OrderContext();
        currentStrategy = new RailwayTransportStrategy(); // По умолчанию
    }

    private void UpdateUI()
    {
        labelStatus.Text = $"Текущий статус: {order.Status}";
        
        // Цвет фона в зависимости от состояния
        switch (order.Status)
        {
            case "Принят":
                panelStatus.BackColor = Color.LightBlue;
                break;
            case "Снабжение":
                panelStatus.BackColor = Color.LightGreen;
                break;
            case "Производство":
                panelStatus.BackColor = Color.LightYellow;
                break;
            case "Отгружен":
                panelStatus.BackColor = Color.LightCoral;
                break;
            case "Ж/Д перевозка":
                panelStatus.BackColor = Color.LightGray;
                break;
            case "Авто перевозка":
                panelStatus.BackColor = Color.LightPink;
                break;
            case "Авиадоставка":
                panelStatus.BackColor = Color.LightSeaGreen;
                break;
        }

        // Обновление списка стратегий
        comboBoxStrategy.SelectedIndex = 0; // Выбираем первую стратегию по умолчанию
    }

    private void buttonNextState_Click(object sender, EventArgs e)
    {
        order.HandleRequest();
        UpdateUI();
        listBoxLog.Items.Add($"Состояние изменено на: {order.Status}");
    }

    private void comboBoxStrategy_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (comboBoxStrategy.SelectedIndex)
        {
            case 0:
                currentStrategy = new RailwayTransportStrategy();
                break;
            case 1:
                currentStrategy = new RoadTransportStrategy();
                break;
            case 2:
                currentStrategy = new AirDeliveryStrategy();
                break;
        }
    }

    private void buttonExecuteStrategy_Click(object sender, EventArgs e)
    {
        string result = order.GetDeliveryInfo(currentStrategy);
        listBoxLog.Items.Add($"Результат стратегии: {result}");
    }

    private void buttonReset_Click(object sender, EventArgs e)
    {
        InitializeOrder();
        UpdateUI();
        listBoxLog.Items.Clear();
    }
}