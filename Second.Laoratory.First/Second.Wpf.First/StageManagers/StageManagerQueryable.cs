using Second.ClassLib.First;

namespace Second.Wpf.First.StageManagers;

public class StageManagerQueryable : IStageManager
{
    public IEnumerable<AssemblyStage> GetStagesLongerThan(
        IEnumerable<AssemblyStage> source,
        int minutes
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        IQueryable<AssemblyStage> queryable = source.AsQueryable();

        var query =
            from s in queryable
            where s.DurationMinutes > minutes
            select s;

        return query;
    }


    public double GetAverageDurationByWorkshop(
        IEnumerable<AssemblyStage> source,
        string workshop
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        IQueryable<AssemblyStage> queryable = source.AsQueryable();

        var durationsQuery =
            from s in queryable
            where s.Workshop == workshop
            select s.DurationMinutes;

        return durationsQuery.Any()
            ? durationsQuery.Average()
            : 0;
    }

    public IEnumerable<IGrouping<DangerLevel, AssemblyStage>> GroupByDangerLevel(
        IEnumerable<AssemblyStage> source
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        IQueryable<AssemblyStage> queryable = source.AsQueryable();

        var query =
            from s in queryable
            group s by s.Danger;

        return query;
    }
}