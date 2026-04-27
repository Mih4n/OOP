namespace Second.Classlib.Seventh;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class MoveGoodsCommand : ICommand
{
    private readonly int quantity;
    private readonly string item;

    public MoveGoodsCommand(string item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void Execute()
    {
        Warehouse.Instance.RemoveGoods(item, quantity);
    }

    public void Undo()
    {
        Warehouse.Instance.AddGoods(item, quantity);
    }
}
