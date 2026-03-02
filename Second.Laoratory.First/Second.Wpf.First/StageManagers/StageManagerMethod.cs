using Second.ClassLib.First;

namespace Second.Wpf.First.StageManagers;

public class StageManagerMethod : IStageManager
{
    public IEnumerable<AssemblyStage> GetStagesLongerThan(
        IEnumerable<AssemblyStage> source,
        int minutes
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.Where(s => s.DurationMinutes > minutes);
    }

    public double GetAverageDurationByWorkshop(
        IEnumerable<AssemblyStage> source,
        string workshop
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var filtered = source.Where(s => s.Workshop == workshop);

        return filtered.Any()
            ? filtered.Average(s => s.DurationMinutes)
            : 0;
    }

    public IEnumerable<IGrouping<DangerLevel, AssemblyStage>> GroupByDangerLevel(
        IEnumerable<AssemblyStage> source
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.GroupBy(s => s.Danger);
    }
}