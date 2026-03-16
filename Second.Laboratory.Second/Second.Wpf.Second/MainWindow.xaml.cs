using Second.ClassLib.Second;
using System.Windows;


namespace Second
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateProcess();
        }

        private void DecoratorChanged(object sender, RoutedEventArgs e)
        {
            UpdateProcess();
        }

        private void UpdateProcess()
        {
            IProductionProcess process = new BaseTechProcess();

            var decorators = new[]
            {
                new {Enabled = cbQuality.IsChecked == true, Create = new Func<IProductionProcess, IProductionProcess>(p => new QualityControlDecorator(p))},
                new {Enabled = cbPackaging.IsChecked == true, Create = new Func<IProductionProcess, IProductionProcess>(p => new PackagingDecorator(p))},
                new {Enabled = cbLabeling.IsChecked == true, Create = new Func<IProductionProcess, IProductionProcess>(p => new LabelingDecorator(p))}
            };

            process = decorators
                .Where(d => d.Enabled)
                .Aggregate(process, (current, d) => d.Create(current));

            tbDescription.Text = process.GetDescription();
            tbDuration.Text = process.GetDuration().ToString();
        }

        private void ReadTemperature(object sender, RoutedEventArgs e)
        {
            FahrenheitSensor oldSensor = new FahrenheitSensor();

            ITemperatureSensor sensor = new SensorAdapter(oldSensor);

            double temp = sensor.GetTemperatureCelsius();

            tbTemperature.Text = temp.ToString();
        }
    }
}