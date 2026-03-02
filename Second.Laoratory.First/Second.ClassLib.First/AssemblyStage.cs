namespace Second.ClassLib.First;

public abstract class AssemblyStage
{
    public string Name { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime StartDate { get; set; }
    public string Workshop { get; set; }
    public DangerLevel Danger { get; set; }

    protected AssemblyStage(
        string name, int duration, 
        DateTime date, string workshop, DangerLevel danger
    )
    {
        Name = name;
        DurationMinutes = duration;
        StartDate = date;
        Workshop = workshop;
        Danger = danger;
    }

    public override string ToString()
    {
        return $"{Name} ({DurationMinutes} мин)";
    }
}

public enum DangerLevel
{
    Low,
    Medium,
    High
}
