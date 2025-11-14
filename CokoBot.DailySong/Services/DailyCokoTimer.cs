namespace CokoBot.DailySong.Services
{
    public class DailyCokoTimer
    {
        private Timer _timer;
        private Func<Task> method;

        public DailyCokoTimer(Func<Task> method)
        {
            this.method = method;
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

            _timer = new Timer(async _ => await RunTask(), null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private async Task RunTask()
        {
            ScheduleDailyTask(14, 0);
            await method();
        }
    }
}
