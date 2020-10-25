namespace ExtraNoveListe
{
    public interface IFirePlayTime
    {
        int Year { get; }
        int Month { get; }
        int Day { get; }
        int Hour { get; }
        int Minutes { get; }
        int Seconds { get; }
        int Milliseconds { get; }
        string Binder { get; set; }
        string DayString { get; }
        string DurationString { get; }
        string Ender { get; set; }
        string HourString { get; }
        string MillisecondsString { get; }
        string MinutesString { get; }
        string MonthString { get; }
        string OriginalTimeInString { get; }
        string SecondsString { get; }
        string YearString { get; }

        string GetTimeString();
        void Validate();
    }
}