namespace CokoBot.DailySong.Infrastructure.Scheduling
{
    public class DailyTimer
    {
        private Timer timer;
        private readonly DailyJob _job;

        public DailyTimer(DailyJob job)
        {
            _job = job;
        }

        public void Start()
        {
            ScheduleDailyTask(14, 0);
        }

        private void ScheduleDailyTask(int hour, int minute)
        {
            DateTime now = DateTime.Now;
            DateTime nextRun = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);

            if (nextRun < now)
                nextRun = nextRun.AddDays(1);

            TimeSpan timeToGo = nextRun - now;

            timer = new Timer(async _ => await RunTask(), null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private async Task RunTask()
        {
            ScheduleDailyTask(14, 0);
            await _job.ExecuteAsync();
        }
    }
}
