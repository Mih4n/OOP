using Exceptions;
using System.Windows;
using System.Xml.Linq;

namespace WPF.Second
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            ClassLib.Second.Vector firstVector;
            ClassLib.Second.Vector secondVector;
            try
            {
                firstVector = ReadVector(VectorA_X.Text, VectorA_Y.Text, VectorA_Z.Text);
                secondVector = ReadVector(VectorB_X.Text, VectorB_Y.Text, VectorB_Z.Text);
            }
            catch (VectorParametersException ex)
            {
                MessageBox.Show($"Ошибка ввода. Все координаты должны быть действительными числами.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch
            {
                MessageBox.Show($"Что то пошло не так", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClassLib.Second.Vector subtractionResult = firstVector - secondVector;
            ClassLib.Second.Vector additionResult = firstVector + secondVector;
            ClassLib.Second.Vector multiplicationResult = firstVector * secondVector;

            Result_Subtraction.Text = $"Вычитание (A - B): {subtractionResult}";
            Result_Addition.Text = $"Сложение (A + B): {additionResult}";

            Result_Multiplication.Text = $"Умножение (A * B ): {multiplicationResult:F2}";
        }

        private ClassLib.Second.Vector ReadVector(string xText, string yText, string zText)
        {
            if (double.TryParse(xText, out double x) &&
                double.TryParse(yText, out double y) &&
                double.TryParse(zText, out double z))
            {
                var vector = new ClassLib.Second.Vector(x, y, z);
                return vector;
            }
                
            throw new VectorParametersException();
        }
    }
}