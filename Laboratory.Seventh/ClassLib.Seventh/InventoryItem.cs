namespace ClassLib.Seventh;

public class InventoryItem
{
    public int DisplayId { get; set; } 
    public string Type { get; set; }
    public string Gender { get; set; }
    public string Color { get; set; }
    
    public object SourceObject { get; set; } 

    public string Description { get; set; }
}