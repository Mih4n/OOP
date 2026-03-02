namespace Second.ClassLib.First;

public class ManualStage : AssemblyStage
{
    public bool RequiresCertification { get; set; }

    public ManualStage(
        string name, int duration, DateTime date, 
        string workshop, DangerLevel danger, bool cert
    ) : base(name, duration, date, workshop, danger)
    {
        RequiresCertification = cert;
    }
}