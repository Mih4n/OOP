namespace Second.ClassLib.Second;

public interface IProductionProcess {
    string GetDescription();
    double GetDuration(); 
}

public class BaseTechProcess : IProductionProcess {
    public string GetDescription() => "Базовый техпроцесс сборки";
    public double GetDuration() => 2.0;
}

public abstract class ProcessDecorator : IProductionProcess {
    protected IProductionProcess process;
    public ProcessDecorator(IProductionProcess process) => this.process = process;
    
    public virtual string GetDescription() => process.GetDescription();
    public virtual double GetDuration() => process.GetDuration();
}

public class QualityControlDecorator : ProcessDecorator {
    public QualityControlDecorator(IProductionProcess p) : base(p) { }
    public override string GetDescription() => base.GetDescription() + " + Этап контроля";
    public override double GetDuration() => base.GetDuration() + 0.5;
}

public class PackagingDecorator : ProcessDecorator {
    public PackagingDecorator(IProductionProcess p) : base(p) { }
    public override string GetDescription() => base.GetDescription() + " + Упаковка";
    public override double GetDuration() => base.GetDuration() + 0.3;
}

public class LabelingDecorator : ProcessDecorator {
    public LabelingDecorator(IProductionProcess p) : base(p) { }
    public override string GetDescription() => base.GetDescription() + " + Маркировка";
    public override double GetDuration() => base.GetDuration() + 0.2;
}