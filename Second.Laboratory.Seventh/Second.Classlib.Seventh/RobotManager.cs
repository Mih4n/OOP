using System.Collections.Concurrent;

namespace Second.Classlib.Seventh;

public class RobotManager
{
    private volatile bool isRunning = true;
    private readonly List<Task> robots = new();
    private readonly ConcurrentQueue<ICommand> queue = new();

    public void EnqueueCommand(ICommand command)
    {
        queue.Enqueue(command);
    }

    public void StartRobots(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            int robotId = i;
            robots.Add(Task.Run(() => RobotWorker(robotId)));
        }
    }

    private async Task RobotWorker(int robotId)
    {
        while (isRunning || !queue.IsEmpty)
        {
            if (queue.TryDequeue(out ICommand? command))
            {
                Warehouse.Log($"Робот #{robotId} → выполняет команду...");
                command.Execute();
                await Task.Delay(700);
            }
            else
            {
                await Task.Delay(200);
            }
        }
    }

    public void Stop()
    {
        isRunning = false;
        Task.WaitAll(robots.ToArray());
        Warehouse.Log("Все роботы завершены.");
    }
}
