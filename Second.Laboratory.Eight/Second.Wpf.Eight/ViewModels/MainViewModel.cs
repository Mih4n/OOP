using System.Collections.ObjectModel;
using System.Windows.Input;
using Second.Wpf.Eight.Commands;
using Second.Wpf.Eight.Models;

namespace Second.Wpf.Eight.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<SparePart> Parts { get; set; }

    private string _newName = string.Empty;
    private int _newQuantity;
    private int _newMinThreshold;

    public string NewName
    {
        get => _newName;
        set { _newName = value; OnPropertyChanged(); }
    }

    public int NewQuantity
    {
        get => _newQuantity;
        set { _newQuantity = value; OnPropertyChanged(); }
    }

    public int NewMinThreshold
    {
        get => _newMinThreshold;
        set { _newMinThreshold = value; OnPropertyChanged(); }
    }

    public ICommand AddPartCommand { get; }

    public MainViewModel()
    {
        Parts = new ObservableCollection<SparePart>
        {
            new SparePart { Name = "Тормозные колодки", Quantity = 15, MinThreshold = 10 },
            new SparePart { Name = "Масляный фильтр", Quantity = 3, MinThreshold = 5 }, 
            new SparePart { Name = "Свеча зажигания", Quantity = 40, MinThreshold = 20 },
            new SparePart { Name = "Ремень ГРМ", Quantity = 1, MinThreshold = 2 }
        };

        AddPartCommand = new RelayCommand(AddPart, CanAddPart);
    }

    private void AddPart(object? parameter)
    {
        var newPart = new SparePart
        {
            Name = NewName,
            Quantity = NewQuantity,
            MinThreshold = NewMinThreshold
        };

        Parts.Add(newPart);

        NewName = string.Empty;
        NewQuantity = 0;
        NewMinThreshold = 0;
    }

    private bool CanAddPart(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(NewName);
    }
}