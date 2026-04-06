// Интерфейс стратегии доставки
public interface ITransportStrategy
{
    string ExecuteDelivery();
}

// Конкретные стратегии доставки
public class RailwayTransportStrategy : ITransportStrategy
{
    public string ExecuteDelivery()
    {
        return "Доставка по железной дороге";
    }
}

public class RoadTransportStrategy : ITransportStrategy
{
    public string ExecuteDelivery()
    {
        return "Доставка автомобильным транспортом";
    }
}

public class AirDeliveryStrategy : ITransportStrategy
{
    public string ExecuteDelivery()
    {
        return "Авиадоставка";
    }
}

// Абстрактное состояние
public abstract class OrderState
{
    public abstract string GetStatus();
    public abstract void Handle(OrderContext context);
}

// Конкретные состояния
public class AcceptedState : OrderState
{
    public override string GetStatus()
    {
        return "Принят";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки принятого заказа
        context.CurrentState = new ProcurementState();
    }
}

public class ProcurementState : OrderState
{
    public override string GetStatus()
    {
        return "Снабжение";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки снабжения
        context.CurrentState = new ProductionState();
    }
}

public class ProductionState : OrderState
{
    public override string GetStatus()
    {
        return "Производство";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки производства
        context.CurrentState = new ShippedState();
    }
}

public class ShippedState : OrderState
{
    public override string GetStatus()
    {
        return "Отгружен";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки отгрузки
        context.CurrentState = new RailwayTransportState();
    }
}

public class RailwayTransportState : OrderState
{
    public override string GetStatus()
    {
        return "Ж/Д перевозка";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки перевозки железной дорогой
        context.CurrentState = new RoadTransportState();
    }
}

public class RoadTransportState : OrderState
{
    public override string GetStatus()
    {
        return "Авто перевозка";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки перевозки автомобильным транспортом
        context.CurrentState = new AirDeliveryState();
    }
}

public class AirDeliveryState : OrderState
{
    public override string GetStatus()
    {
        return "Авиадоставка";
    }

    public override void Handle(OrderContext context)
    {
        // Логика обработки авиадоставки
        context.CurrentState = new AcceptedState(); // Возвращаемся к началу цикла
    }
}

// Контекст заказа
public class OrderContext
{
    private OrderState _currentState;
    
    public OrderState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    public string Status => _currentState.GetStatus();

    public OrderContext()
    {
        _currentState = new AcceptedState();
    }

    public void HandleRequest()
    {
        _currentState.Handle(this);
    }

    public string GetDeliveryInfo(ITransportStrategy strategy)
    {
        return strategy.ExecuteDelivery();
    }
}
