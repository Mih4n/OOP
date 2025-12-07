using System;
using System.Windows;
using ClassLib.Seventh;
using ClassLib.Seventh.Contracts;
using ClassLib.Seventh.Enums;
using ClassLib.Seventh.LowerBody;
using ClassLib.Seventh.UpperBody;
using System.Drawing; 
using System.Collections.ObjectModel; 
using System.Linq;
using App.Seventh;

namespace Seventh.WPF
{
    public partial class MainWindow : Window
    {
        private readonly AtelierRepository repository = new();
        private readonly ObservableCollection<InventoryItem> displayItems = new();

        public MainWindow()
        {
            InitializeComponent();
            InventoryListView.ItemsSource = displayItems;

            SearchGenderComboBox.SelectedIndex = 2;
            ProductTypeComboBox.SelectedIndex = 0; 
            GenderComboBox.SelectedIndex = 0;

            SeedInitialData();
            RefreshDisplayList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductTypeComboBox.SelectedItem == null || GenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите Тип и Пол.", "Ошибка ввода");
                return;
            }

            string colorStr = ColorTextBox.Text;
            string? genderStr = GenderComboBox.SelectedItem.ToString();
            string? productType = ProductTypeComboBox.SelectedItem.ToString();

            var gender = (Gender)Enum.Parse(typeof(Gender), genderStr ?? "Male");
            var size = new ClassLib.Seventh.Size(gender == Gender.Male ? 180 : 165, BodyType.Mesomorph);
            
            Color color;
            try
            {
                color = Color.FromName(colorStr);
            }
            catch
            {
                MessageBox.Show("Некорректное имя цвета.", "Ошибка цвета");
                return;
            }
            var fabric = new Fabric(FabricType.Mixed, color);
            
            object? newItem = null;

            switch (productType)
            {
                case "Jacket":
                    newItem = new Jacket(size, gender, fabric, 3);
                    break;
                case "Trousers":
                    newItem = new Trousers(size, gender, fabric, "New Line");
                    break;
                case "Skirt":
                    newItem = new Skirt(size, gender, fabric, 1.2f);
                    break;
                case "Suit (Trousers)":
                case "Suit (Skirt)":
                    var suitJacket = new Jacket(size, gender, fabric, 2);
                    ILowerBody lowerBody = productType == "Suit (Trousers)" 
                        ? new Trousers(size, gender, fabric, "Suit Brand")
                        : new Skirt(size, gender, fabric, 1.0f);
                    newItem = new Suit(suitJacket, lowerBody);
                    break;
            }

            if (newItem != null)
            {
                repository.Add(newItem);
                
                FilterButton_Click(null, null); 
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryListView.SelectedItem is InventoryItem selectedItem)
            {
                repository.Remove(selectedItem.SourceObject);
                
                FilterButton_Click(null, null); 
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент для удаления.", "Ошибка");
            }
        }

        private void FilterButton_Click(object? sender, RoutedEventArgs? e)
        {
            string colorFilter = SearchColorTextBox.Text.Trim();
            string? genderFilter = SearchGenderComboBox.SelectedItem?.ToString();
            
            var allProducts = repository.GetAll();
            displayItems.Clear();

            foreach (var obj in allProducts)
            {
                InventoryItem? item = null;

                if (obj is IWearable w)
                {
                    item = CreateInventoryItem(w.GetType().Name, w.Gender.ToString(), w.Fabric.Color.Name, w, w);
                }
                else if (obj is Suit s)
                {
                    string suitType = s.LowerBody is Trousers ? "Suit (Trousers)" : "Suit (Skirt)";
                    string desc = $"Верх: {s.UpperBody.Fabric.Color.Name}, Низ: {s.LowerBody.Fabric.Color.Name}";
                    
                    item = CreateInventoryItem(suitType, s.UpperBody.Gender.ToString(), s.UpperBody.Fabric.Color.Name, s, s.UpperBody);
                    item.Description = desc;
                }

                if (item != null && ApplyFilters(item, colorFilter, genderFilter))
                {
                    displayItems.Add(item);
                }
            }
        }
        
        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchColorTextBox.Clear();
            SearchGenderComboBox.SelectedIndex = 2;
            RefreshDisplayList();
        }

        private void RefreshDisplayList()
        {
            SearchColorTextBox.Clear();
            SearchGenderComboBox.SelectedIndex = 2;
            FilterButton_Click(null, null);
        }
        
        private bool ApplyFilters(InventoryItem item, string colorFilter, string? genderFilter)
        {
            if (!string.IsNullOrEmpty(colorFilter) && !item.Color.Equals(colorFilter, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (genderFilter != "Все" && !string.IsNullOrEmpty(genderFilter) && !item.Gender.Equals(genderFilter, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private InventoryItem CreateInventoryItem(string type, string gender, string color, object sourceObject, IWearable component)
        {
            return new InventoryItem
            {
                DisplayId = sourceObject.GetHashCode(),
                Type = type,
                Gender = gender,
                Color = color,
                SourceObject = sourceObject,
                Description = $"Размер: {component.Size.Height} {component.Size.Completeness}, Ткань: {component.Fabric.Type}"
            };
        }

        private void SeedInitialData()
        {
            var sizeM = new ClassLib.Seventh.Size(180, BodyType.Mesomorph);
            var blackFabric = new Fabric(FabricType.Natural, Color.Black);
            var mJacket = new Jacket(sizeM, Gender.Male, blackFabric, 2);
            var mPants = new Trousers(sizeM, Gender.Male, blackFabric, "Armani");
            repository.Add(new Suit(mJacket, mPants));

            var sizeF = new ClassLib.Seventh.Size(165, BodyType.Ectomorph);
            var redFabric = new Fabric(FabricType.Mixed, Color.Red);
            var fJacket = new Jacket(sizeF, Gender.Female, redFabric, 2);
            var fSkirt = new Skirt(sizeF, Gender.Female, redFabric, 1.0f);
            repository.Add(new Suit(fJacket, fSkirt));
            
            repository.Add(new Trousers(new ClassLib.Seventh.Size(170, BodyType.Mesomorph), Gender.Female, redFabric, "Gucci"));
            
            repository.Add(new Jacket(new ClassLib.Seventh.Size(190, BodyType.Endomorph), Gender.Male, new Fabric(FabricType.Synthetic, Color.Blue), 3));
        }
    }
}