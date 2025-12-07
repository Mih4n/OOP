using ClassLib.Seventh.Enums;

namespace ClassLib.Seventh.Contracts;

public interface IWearable 
{
    public int Id { get; }
    public Size Size { get; }
    public Gender Gender { get; }
    public Fabric Fabric { get; }
}
