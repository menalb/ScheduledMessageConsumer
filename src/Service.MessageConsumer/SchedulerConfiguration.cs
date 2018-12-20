using Quartz;

namespace Service.MessageConsumer
{
    public class SchedulerConfiguration
    {
        public int IntervalInSeconds { get; set; }
        public AtHour StartingAt { get; set; }
        public AtHour EndingAt { get; set; }
        public bool RunsFromMondayToFriday { get; set; }
    }
    public class AtHour
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

    public static class AtHourExtensions
    {
        public static TimeOfDay AsTimeOfDay(this AtHour at) => TimeOfDay.HourAndMinuteOfDay(at.Hour, at.Minute);
    }
}