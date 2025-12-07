using ClassLib.Seventh.Enums;

namespace ClassLib.Seventh.Contracts;

public interface IWearable 
{
    public Size Size { get; }
    public Gender Gender { get; }
    public Fabric Fabric { get; }
}
