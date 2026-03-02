using Second.ClassLib.First;

namespace Second.Wpf.First.StageManagers;

public interface IStageManager
{
    IEnumerable<AssemblyStage> GetStagesLongerThan(
        IEnumerable<AssemblyStage> source,
        int minutes
    );

    double GetAverageDurationByWorkshop(
        IEnumerable<AssemblyStage> source,
        string workshop
    );

    IEnumerable<IGrouping<DangerLevel, AssemblyStage>> GroupByDangerLevel(
        IEnumerable<AssemblyStage> source
    );
}