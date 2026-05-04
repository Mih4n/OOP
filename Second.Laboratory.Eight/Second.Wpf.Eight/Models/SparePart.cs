using Second.Wpf.Eight.ViewModels;

namespace Second.Wpf.Eight.Models;

public class SparePart : ViewModelBase
{
    private string _name = string.Empty;
    private int _quantity;
    private int _minThreshold;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsDeficient)); // Обновляем статус дефицита при изменении количества
        }
    }

    public int MinThreshold
    {
        get => _minThreshold;
        set
        {
            _minThreshold = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsDeficient));
        }
    }

    // Бизнес-логика: если количество меньше порога, позиция в дефиците
    public bool IsDeficient => Quantity < MinThreshold;
}