namespace Second.ClassLib.First;

public class AutomatedStage : AssemblyStage
{
    public string MachineId { get; set; }

    public AutomatedStage(
        string name, int duration, DateTime date,
        string workshop, DangerLevel danger, string machineId
    ) : base(name, duration, date, workshop, danger)
    {
        MachineId = machineId;
    }
}