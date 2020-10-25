using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ExtraNoveListe
{
    public class IndexingErrorExeption: ApplicationException
    {
        public IndexingErrorExeption()
            : base() { }
        public IndexingErrorExeption(String message) 
            : base(message) { }
    }

        

    public class FPTime
    {
        public static string DefaultTime => $"{DateTime.Now.ToString("yyyy")}-{DateTime.Now.ToString("MM")}-{DateTime.Now.ToString("dd")}T00:00:00.000+01:00";
        public static string DefaultSuperOld => "1970-01-01T01:00:00.000+01:00";

        private int _YYYY, _MM, _DD, _hour, _min, _sec, _millis;        //private int values for different means
        private readonly string _theEnd;
        private string _end;                                            //the last part of the time string
        private bool milliseconds_flag = false;                         //flag that determins weather the milliseconds are two or three digits long
        private int milliseconds_length_flag = 3;

        public int Year => _YYYY;
        public int Month => _MM;
        public int Day => _DD;
        public string Binder { get; set; } = String.Empty;
        public int Hour => _hour;
        public int Minutes => _min;
        public int Seconds => _sec;
        public int Milliseconds => milliseconds_flag ? _millis * 10 : _millis;
        public string Ender { get; set; } = String.Empty;

        //public string End => _end;

        public FPTime(string time)
        {
            if (time == null)
            {
                throw new ArgumentNullException("FPTime constructor argument");
            }
            
            _theString = time;
            if (int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Milliseconds), out _millis))
            {
                while (_millis.ToString().Length < 3 && _millis != 0) _millis *= 10;
                // LOOK UP REMINDER IN THE LINE Validate(); IN THIS CONSTRUCTOR
            }
            else _millis = 0;

            milliseconds_length_flag = RegExTime.Extrapolate(time, RegExTime.REX.Milliseconds).Length;
            if (!int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Year), out _YYYY)) _YYYY = 0;
            if (!int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Month), out _MM)) _MM = 0;
            if (!int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Day), out _DD)) _DD = 0;
            Binder = RegExTime.Extrapolate(time, RegExTime.REX.Binder);
            if (!int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Hours), out _hour)) _hour = 0;
            if (!int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Minutes), out _min)) _min = 0;
            if (!int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Seconds), out _sec)) _sec = 0;

            //_end = new String(time.ToCharArray(), time.LastIndexOf("."), time.Length - time.LastIndexOf("."));
            _end = Ender = RegExTime.Extrapolate(time, RegExTime.REX.Ender);

            Validate();                                     // REMINDER: If _milis is greater than a thousand, this method will fix that
        }

        public FPTime(int theYear, int theMonth, int theDay, int theHour, int theMinutes, int theSeconds, int theMilliseconds, string theEnd)
        {
            _YYYY = theYear;
            _MM = theMonth;
            _DD = theDay;
            _hour = theHour;
            _min = theMinutes;
            _sec = theSeconds;
            _millis = theMilliseconds;
            _theEnd = theEnd;
            _end = theEnd;
            Validate();                         //Checks if all the variables are well imported
        }

        /// <summary>
        /// Returns the year in the apropreate string value
        /// </summary>
        public string YearString
        {
            get
            {
                if (_YYYY.ToString().Length != 4) return "2000";
                else return _YYYY.ToString();
            }
        }
        public string MonthString
        {
            get
            {
                string tempStr = _MM.ToString();
                return (_MM > 0 && _MM < 13 ? (tempStr.Length == 2 ? tempStr : "0" + tempStr) : "01");

            }
        }
        public string DayString
        {
            get
            {
                string tempStr = _DD.ToString();
                return (_DD > 0 && _DD < 32 ? (tempStr.Length == 2 ? tempStr : "0" + tempStr) : "01");
            }
        }
        public string HourString
        {
            get
            {
                string tempStr = _hour.ToString();
                return (_hour >= 0 && _hour < 24 ? (tempStr.Length == 2 ? tempStr : "0" + tempStr) : "01");
            }
        }
        public string MinutesString
        {
            get
            {
                string tempStr = _min.ToString();
                return (_min >= 0 && _min < 60 ? (tempStr.Length == 2 ? tempStr : "0" + tempStr) : "01");
            }
        }
        public string SecondsString
        {
            get
            {
                string tempStr = _sec.ToString();
                return (_sec >= 0 && _sec < 60 ? (tempStr.Length == 2 ? tempStr : "0" + tempStr) : "01");
            }
        }
        public string MillisecondsString
        {
            get
            {
                string tempStr = _millis.ToString();
                while (tempStr.Length < milliseconds_length_flag)
                    tempStr += "0";
                return tempStr;
            }
        }
        public string GetTimeString()
        {
            return this.YearString + "-" + this.MonthString + "-" + this.DayString + "T" + this.HourString + ":" + this.MinutesString + ":" + this.SecondsString + "." + this.MillisecondsString + this.Ender;
        }

        private readonly string _theString;
        public string OriginalTimeInString => _theString;

        public static FPTime Sum(FPTime a, FPTime b)
        {
            int year, month, day, hours, minutes, seconds, milliseconds;
            string end;


            year = a.Year > b.Year ? a.Year : b.Year;
            month = a.Year > b.Year ? a.Month : (b.Year == a.Year ? (a.Month > b.Month ? a.Month : b.Month) : b.Month);
            day = a.Year > b.Year ? a.Day : (b.Year == a.Year ? (a.Month > b.Month ? a.Day : (b.Month == a.Month ? (a.Day > b.Day ? a.Day : b.Day) : b.Day)) : b.Day);
            hours = a.Hour + b.Hour;
            minutes = a.Minutes + b.Minutes;
            seconds = a.Seconds + b.Seconds;
            milliseconds = a.Milliseconds + b.Milliseconds;
            try
            {
                string temp_end = a.Ender.Length > b.Ender.Length ? a.Ender : b.Ender;
                end = temp_end == null ? "Z" : temp_end;
            }
            catch
            {
                throw new Exception("wtf again:" + a._theString+ "  ______  " + b.Ender);
            }
            DateTime before = new DateTime(a.Year, a.Month, a.Day, a.Hour, a.Minutes, a.Seconds, a.Milliseconds, DateTimeKind.Local);

            if (milliseconds > 999) { milliseconds -= 1000; seconds++; }
            if (seconds > 59) { seconds -= 60; minutes++; }
            if (minutes > 59) { minutes -= 60; hours++; }
            if (hours > 23) { hours -= 24; day++; }
            if (day > 28)
            {
                if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day > 31)
                {
                    day -= 31;
                    month++;
                }
                else if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)
                {
                    day -= 30;
                    month++;
                }
                else if (month == 2 && day > 28)
                {
                    if (DateTime.IsLeapYear(year) && day > 29)
                    {
                        day -= 29;
                        month++;
                    }
                    else if (!DateTime.IsLeapYear(year) && day > 28)
                    {
                        day -= 28;
                        month++;
                    }
                }
                if (month > 12) { month -= 12; year++; }
            }
            DateTime after = DateTime.Now;
            try
            {
                after = new DateTime(year, month, day, hours, minutes, seconds, milliseconds, DateTimeKind.Local);
            }
            catch
            {
                throw new Exception("Minute: " + minutes.ToString() + "  ::  Second: " + seconds.ToString());
            }
            
            if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(before) ^ TimeZone.CurrentTimeZone.IsDaylightSavingTime(after))
            {
                if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(before))
                {
                    hours--;
                }
                if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(after))
                {
                    hours++;
                }
            }
            return new FPTime(year, month, day, hours, minutes, seconds, (a.milliseconds_flag && b.milliseconds_flag ? milliseconds/10 : milliseconds), (a.milliseconds_flag && b.milliseconds_flag ? a.Ender : (a.Ender.Length > b.Ender.Length ? a.Ender : b.Ender)));
            
        }

        public static FPTime Duration(string trajanje)
        {
            string default_end = "Z";

            int hour = 0, min = 0, sec = 0, millis = 0;
            if (trajanje.Contains("."))
            {
                int index = trajanje.IndexOf(".");
                if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (!(int.TryParse(new String(trajanje.ToCharArray(), index + 1, trajanje.Length-index-1), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");

                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 999) millis /= 10;
                    //return new FPTime(0, 0, 0, 0, min, sec, millis, default_end);
                }
            }
            else
            {
                if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, trajanje.Length), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 999) millis /= 10;
                    //return new FPTime(0, 0, 0, 0, min, sec, millis, default_end);
                }
            }

            if(min > 59)
            {
                hour = (int)(min / 60);
                min -= (hour * 60);
            }
            return new FPTime(0, 0, 0, hour, min, sec, millis, default_end);
        }
        public static string IduceVrijeme(FPTime time, string trajanje)
        {
            string default_end = "Z";

            /*int min = 0, sec = 0, millis = 0;
            if (trajanje.Contains("."))
            {
                try
                {
                    int index = trajanje.IndexOf(".");
                    if (index < 0) throw new IndexingErrorExeption("Index can't be less than zero");
                    if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                    if (!(int.TryParse(new String(trajanje.ToCharArray(), index + 1, trajanje.Length-index-1), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");
                }
                catch 
                {
                    throw new Exception("WTF: trajanje" + trajanje + "   ::   vrijeme_string: " + time._theString);
                }

                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(time, new FPTime(2000, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }
            else
            {
                if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, trajanje.Length), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                }
                return FPTime.Sum(time, new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
            }*/

            return FPTime.Sum(time, new FPTime(2000, 1, 1, 0, 0, RegExTime.GetFromDuration(trajanje, RegExTime.REX.Seconds), RegExTime.GetFromDuration(trajanje, RegExTime.REX.Milliseconds), "Z")).GetTimeString();
            return string.Empty;
        }

        public static string IduceVrijeme(FPTime time, int sec, int millis)
        {

            return FPTime.Sum(time, new FPTime(2000, 0, 0, 0, 0, sec, millis, "Z")).GetTimeString();
        }


        public static string IduceVrijeme(FirePlaySong song)
        {
            string default_end = "Z";

            int min = 0, sec = 0, millis = 0;
            //int index = -1;
            if (song.Trajanje.Contains("."))
            {
                int index = song.Trajanje.IndexOf(".");
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), index + 1, song.Trajanje.Length), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");

                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(new FPTime(song.Vrijeme), new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }
            else
            {
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), 0, song.Trajanje.Length), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(new FPTime(song.Vrijeme), new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }


            return string.Empty;
        }
        public static string IduceVrijeme(IFirePlaySong song)
        {
            string default_end = "Z";

            int min = 0, sec = 0, millis = 0;
            //int index = -1;
            if (song.Trajanje.Contains("."))
            {
                int index = song.Trajanje.IndexOf(".");
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), index + 1, song.Trajanje.Length), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");

                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(new FPTime(song.Vrijeme), new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }
            else
            {
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), 0, song.Trajanje.Length), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (sec > 59)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(new FPTime(song.Vrijeme), new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }


            return string.Empty;
        }

        public void Validate()
        {
            while (_millis.ToString().Length < 3 && _millis != 0) _millis *= 10;
            while (_millis > 999) { _millis -= 1000; _sec++; }
            while (_sec > 59) { _sec -= 60; _min++; }
            while (_min > 59) { _min -= 60; _hour++; }
            while (_hour > 23) { _hour -= 24; _DD++; }
            while (_DD > 28)
            {
                if ((_MM == 1 || _MM == 3 || _MM == 5 || _MM == 7 || _MM == 8 || _MM == 10 || _MM == 12) && _DD > 31)
                {
                    _DD -= 31;
                    _MM++;
                }
                else if ((_MM == 4 || _MM == 6 || _MM == 9 || _MM == 11) && _DD > 30)
                {
                    _DD -= 30;
                    _MM++;
                }
                else if (_MM == 2 && _DD > 28)
                {
                    if (DateTime.IsLeapYear(_YYYY) && _DD > 29)
                    {
                        _DD -= 29;
                        _MM++;
                    }
                    else if (!DateTime.IsLeapYear(_YYYY) && _DD > 28)
                    {
                        _DD -= 28;
                        _MM++;
                    }

                }
                else break;
            }
            while (_MM > 12)
            {
                _MM -= 12;
                _YYYY++;
            }


            
        }

        

        public string DurationString => this.Hour.ToString() + ":" + this.Minutes.ToString() + ":" + this.Seconds.ToString() + "." + this.Milliseconds.ToString();

        public static class RegExTime
        {
            public enum REX { Year = 1, Month, Day, Binder, Hours, Minutes, Seconds, Milliseconds, Ender }

            public static string Extrapolate(string s, REX r)
            {
                
                /*if (Regex.Match(s, @".*(.)$").Groups[1].Value == "Z")
                {
                    if (r == REX.Milliseconds) return "000";
                    return Regex.Match(s, @"(\d{4})-(\d{2})-(\d{2})([A-z]?)(\d{2})\:(\d{2})\:(\d{2})(.*?)(Z)").Groups[(int) r].Value;
                }*/
                var match = Regex.Match(s, @"(\d{4})-(\d{2})-(\d{2})([A-z]?)(\d{2})\:(\d{2})\:(\d{2})[:.]?(\d*)(.*)");
                if (r == REX.Milliseconds && match.Groups[(int) r].Value == "") return "000";
                return match.Groups[(int) r].Value;
            }

            public static FPTime Trajanje(string trajanje)
            {
                var match = Regex.Match(trajanje, @"(\d*).(\d*)");
                int sec = 0, millis = 0; 
                if(!int.TryParse(match.Groups[1].Value, out sec)) sec = 0;;
                if(!int.TryParse(match.Groups[2].Value, out millis)) millis = 0;
                return new FPTime(0, 0, 0, 0, 0, sec, millis, "Z");
            }

            public static int GetFromDuration(string time, REX r)
            {
                if(r != REX.Seconds && r != REX.Milliseconds) return 0;
                int sec = 0, millis = 0;
                Match match = Regex.Match(time, @"(\d+).?(\d*)?");
                if(match.Groups[1].Value != String.Empty) if(!int.TryParse(match.Groups[1].Value, out sec)) sec = 0;
                else if (match.Groups[1].Value == String.Empty) sec = 0;
                if(match.Groups[2].Value != String.Empty) if(!int.TryParse(match.Groups[2].Value, out millis)) millis = 0;
                else if (match.Groups[2].Value == String.Empty) millis = 0;
                return r == REX.Seconds ? sec : millis;
            }
        }

        public class FPSoloTime
        {


            /////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////  CONSTRUCTOR  ///////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            public FPSoloTime()
            {
                
            }

            public static FPSoloTime Validate()
            {
                var temp = new FPSoloTime();

                return temp;
            }
        }
    }


}
