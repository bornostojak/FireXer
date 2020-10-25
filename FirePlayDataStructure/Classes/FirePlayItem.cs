//#define SONGVIEW
//#define TAGLIB
//#define PLAYAUDIO
//#define FIREPLAYLIST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace FirePlayDataStructure
{
    public class FirePlayItem : BaseFirePlayItem
    {
        
        private FirePlayItem()
        {
            _Vrijeme = FirePlayTime.DefaultTime;
#if SONGVIEW
            this.SongView = new SongView(this);
#endif
        }
        public FirePlayItem(string path) : base()
        {
            if (System.IO.Path.GetExtension(path) == ".mp3" || System.IO.Path.GetExtension(path) == ".wav")
            {
#if TAGLIB
                TagLib.File tl_file = TagLib.File.Create(path);
                _EndCue = (tl_file.Properties.Duration.Hours * 60 + tl_file.Properties.Duration.Minutes * 60 + tl_file.Properties.Duration.Seconds).ToString() + "." + tl_file.Properties.Duration.Milliseconds;
                if ((path.ToLower().Contains("sw") || path.ToLower().Contains("dr") || path.ToLower().Contains(" j ")) && tl_file.Properties.Duration.Seconds < 60) Sweeper = true;
#endif
                _ID = "-2";
                _Naziv = System.IO.Path.GetFileNameWithoutExtension(path);
                _Autor = "";
                _Album = "";
                _Info = "";
                _Tip = "10";
                _Color = "0x0000000a";
                _NaKanalu = "0";
                PathName = path.ToLower();//new System.IO.DirectoryInfo(path).FullName.ToLower() + "\\" + System.IO.Path.GetFileName(path);
                _ItemType = "3";
                _StartCue = "0";
                _Pocetak = "0";
                _Trajanje = _EndCue;
                _Vrijeme = FirePlayTime.DefaultTime;
                _StvarnoVrijemePocetka = FirePlayTime.DefaultSuperOld;
                _VrijemeMinTermin = FirePlayTime.DefaultSuperOld;
                _VrijemeMaxTermin = FirePlayTime.DefaultSuperOld;
                _PrviU_Bloku = "0";
                _ZadnjiU_Bloku = "0";
                _JediniU_Bloku = "0";
                _FiksniU_Terminu = "0";
                _Reklama = "false";
                _WaveIn = "false";
                _SoftIn = "0";
                _SoftOut = "0";
                _Volume = "65536";
                _OriginalStartCue = _StartCue;
                _OriginalEndCue = _EndCue;
                _OriginalPocetak = _Pocetak;
                _OriginalTrajanje = _Trajanje;

                Sweeper = true;

                base.InformationChange += (obj, ev_args) =>
                {
                    SongChanged?.Invoke(obj, ev_args);
                };
            }
#if SONGVIEW
            this.SongView = new SongView(this);
#endif
        }
        
        ////////////////////////////////////////////////////////////////////////
        ///////////////////////////   STATIC VARS    ///////////////////////////
        ////////////////////////////////////////////////////////////////////////
        

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////   SONG VARS   ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private bool _isSweeper = false;
        /// <summary>
        /// Determines wether the song is a sweeper
        /// </summary>
        public bool Sweeper { get { return _isSweeper; } set { _isSweeper = value; } }
        public string Path => PathName;
        
        private FirePlayTime _Time;
#if PLAYAUDIO
        public IPlayAudio play { get; private set; } = new PlayAudio();
#endif
        

#if PLAYAUDIO
        public string PathName { get { return _PathName; } set { _PathName = value; play.ReloadAudio(value); OnSongChange(); } } 
#else
        public override string PathName { get { return _PathName; } set { _PathName = value; OnSongChange(); } }
#endif
        
        public FirePlayTime Time { get { return _Time; } }

#if SONGVIEW
        public ISongView SongView { get; set; } 
#endif

        public int _index = -1;
        public int Index
        {
            get { return _index; }
            set
            {
#if SONGVIEW
                this.SongView.Index = value; 
#endif
                _index = value;
            }
        }
#if FIREPLAYLIST

        private IFirePlayList _partOfList;
        public IFirePlayList PartOfList { get { if (_partOfList == null) _partOfList = new FirePlayList(); return _partOfList; } set { _partOfList = value; } } 
#endif

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////   EVENTS   ////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////


            
        public event EventHandler SongChanged;
        private void OnSongChange()
        {
            SongChanged?.Invoke(this, new EventArgs());
        }
         
        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////   METHODS   //////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        
        public void SongXML()
        {
            XmlDocument d = new XmlDocument();
            XmlNode nn = d.CreateElement("PlayItem");
            d.AppendChild(nn);
            for(int i = 0; i < FirePlayItem.Elements.Length; i++)
            {
                XmlNode c = d.CreateElement(FirePlayItem.Elements[i]);
                c.InnerText = SongElementByIndex(i);
                nn.AppendChild(c);
            }
            //_song_xml = d.FirstChild;
        }

        /// <summary>
        /// Returns the elemnt of the song in its XML-based order (zero-based indexing)
        /// </summary>
        /// <returns></returns>
        public string SongElementByIndex(int index)
        {
            string[] elemnts = { this.ID, this.Naziv, this.Autor, this.Album, this.Info, this.Tip, this.Color, this.NaKanalu, this.PathName, this.ItemType, this.StartCue, this.EndCue, this.Pocetak, this.Trajanje, this.Vrijeme, this.StvarnoVrijemePocetka, this.VrijemeMinTermin, this.VrijemeMaxTermin, this.PrviU_Bloku, this.ZadnjiU_Bloku, this.JediniU_Bloku, this.FiksniU_Terminu, this.Reklama, this.WaveIn, this.SoftIn, this.SoftOut, this.Volume, this.OriginalStartCue, this.OriginalEndCue, this.OriginalPocetak, this.OriginalTrajanje};
            return (index < 0 || index > elemnts.Length) ? string.Empty : elemnts[index];
        }

        public FirePlayItem GetClone()
        {
            FirePlayItem temp = FirePlayItem.CreateTemp();

            temp.ID = this.ID;
            temp.Naziv = this.Naziv;
            temp.Autor = this.Autor;
            temp.Album = this.Album;
            temp.Info = this.Info;
            temp.Tip = this.Tip;
            temp.Color = this.Color;
            temp.NaKanalu = this.NaKanalu;
            temp.PathName = this.PathName;
            temp.ItemType = this.ItemType;
            temp.StartCue = this.StartCue;
            temp.EndCue = this.EndCue;
            temp.Pocetak = this.Pocetak;
            temp.Trajanje = this.Trajanje;
            temp.Vrijeme = this.Vrijeme;
            temp.StvarnoVrijemePocetka = this.StvarnoVrijemePocetka;
            temp.VrijemeMinTermin = this.VrijemeMinTermin;
            temp.VrijemeMaxTermin = this.VrijemeMaxTermin;
            temp.PrviU_Bloku = this.PrviU_Bloku;
            temp.ZadnjiU_Bloku = this.ZadnjiU_Bloku;
            temp.JediniU_Bloku = this.JediniU_Bloku;
            temp.FiksniU_Terminu = this.FiksniU_Terminu;
            temp.Reklama = this.Reklama;
            temp.WaveIn = this.WaveIn;
            temp.SoftIn = this.SoftIn;
            temp.SoftOut = this.SoftOut;
            temp.Volume = this.Volume;
            temp.OriginalStartCue = this.OriginalStartCue;
            temp.OriginalEndCue = this.OriginalEndCue;
            temp.OriginalPocetak = this.OriginalPocetak;
            temp.OriginalTrajanje = this._OriginalTrajanje;

#if SONGVIEW
            temp.SongView = new SongView(temp); 
#endif

            return temp;
        }
        
        /// <summary>
        /// Defaults the song (works fine)
        /// </summary>
        public void DefaultTheSong()
        {
            this._SoftIn = "0";
            this._SoftOut = "0";
            this._StartCue = this._OriginalStartCue;
            this._EndCue = this._OriginalEndCue;
            this._Pocetak = this._OriginalPocetak;
            this._Trajanje = this._OriginalTrajanje;
            
        }

        public void IsSweeper()
        {
            _isSweeper = true;
            _Tip = "10";
            _Color = "0x0000000a";
            _ItemType = "3";
        }


        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////   STATIC METHODS   ////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public static string GetPathNameString(string s)
        {
            return string.Empty;
        }

        /// <summary>
        /// Defaults the song (works fine)
        /// </summary>
        /// <param name="song"></param>
        public static void DefaultTheSong(FirePlayItem song)
        {
            song._SoftIn = "0";
            song._SoftOut = "0";
            song._StartCue = song._OriginalStartCue;
            song._EndCue = song._OriginalEndCue;
            song._Pocetak = song._OriginalPocetak;
            song._Trajanje = song._OriginalTrajanje;
            
        }
        
        public static FirePlayItem CreateSong()
        {
            FirePlayItem n = new FirePlayItem();
            return n;
        }
        public static FirePlayItem CreateTemp()
        {
            return new FirePlayItem();
        }

        public static FirePlayItem CreatorSongFromXMLNode(XmlNode node)
        {
            FirePlayItem new_song = FirePlayItem.CreateSong();
            
            if (node.Name == "PlayItem" && node.HasChildNodes)
            {
                foreach (XmlNode c in node.ChildNodes)
                {
                    if (c.Name == "ID") new_song.ID = c.InnerText;
                    else if (c.Name == "Naziv") new_song.Naziv = c.InnerText;
                    else if (c.Name == "Autor") new_song.Autor = c.InnerText;
                    else if (c.Name == "Album") new_song.Album = c.InnerText;
                    else if (c.Name == "Indo") new_song.Info = c.InnerText;
                    else if (c.Name == "Tip") new_song.Tip = c.InnerText;
                    else if (c.Name == "Color") new_song.Color = c.InnerText;
                    else if (c.Name == "NaKanalu") new_song.NaKanalu = c.InnerText;
                    else if (c.Name == "PathName") new_song.PathName = c.InnerText;
                    else if (c.Name == "ItemType") new_song.ItemType = c.InnerText;
                    else if (c.Name == "StartCue") new_song.StartCue = c.InnerText;
                    else if (c.Name == "EndCue") new_song.EndCue = c.InnerText;
                    else if (c.Name == "Pocetak") new_song.Pocetak = c.InnerText;
                    else if (c.Name == "Trajanje") new_song.Trajanje = c.InnerText;
                    else if (c.Name == "Vrijeme") new_song.Vrijeme = c.InnerText;
                    else if (c.Name == "StvarnoVrijemePocetka") new_song.StvarnoVrijemePocetka = c.InnerText;
                    else if (c.Name == "VrijemeMinTermin") new_song.VrijemeMinTermin = c.InnerText;
                    else if (c.Name == "VrijemeMaxTermin") new_song.VrijemeMaxTermin = c.InnerText;
                    else if (c.Name == "PrviU_Bloku") new_song.PrviU_Bloku = c.InnerText;
                    else if (c.Name == "ZadnjiU_Bloku") new_song.ZadnjiU_Bloku = c.InnerText;
                    else if (c.Name == "JediniU_Bloku") new_song.JediniU_Bloku = c.InnerText;
                    else if (c.Name == "FiksniU_Terminu") new_song.FiksniU_Terminu = c.InnerText;
                    else if (c.Name == "Reklama") new_song.Reklama = c.InnerText;
                    else if (c.Name == "WaveIn") new_song.WaveIn = c.InnerText;
                    else if (c.Name == "SoftIn") new_song.SoftIn = c.InnerText;
                    else if (c.Name == "SoftOut") new_song.SoftOut = c.InnerText;
                    else if (c.Name == "Volume") new_song.Volume = c.InnerText;
                    else if (c.Name == "OriginalStartCue") new_song.OriginalStartCue = c.InnerText;
                    else if (c.Name == "OriginalEndCue") new_song.OriginalEndCue = c.InnerText;
                    else if (c.Name == "OriginalPocetak") new_song.OriginalPocetak = c.InnerText;
                    else if (c.Name == "OriginalTrajanje") new_song.OriginalTrajanje = c.InnerText;
                }
            }
            new_song.SongXML();
            return new_song;
        }
        public static string[] Elements = { "ID", "Naziv", "Autor", "Album", "Info", "Tip", "Color", "NaKanalu", "PathName", "ItemType", "StartCue", "EndCue", "Pocetak", "Trajanje", "Vrijeme", "StvarnoVrijemePocetka", "VrijemeMinTermin", "VrijemeMaxTermin", "PrviU_Bloku", "ZadnjiU_Bloku", "JediniU_Bloku", "FiksniU_Terminu", "Reklama", "WaveIn", "SoftIn", "SoftOut", "Volume", "OriginalStartCue", "OriginalEndCue", "OriginalPocetak", "OriginalTrajanje" };
        

#region FIrePlayTime
        public class IndexingErrorExeption : ApplicationException
        {
            public IndexingErrorExeption()
                : base() { }
            public IndexingErrorExeption(String message)
                : base(message) { }
        }



        public class FirePlayTime : IFirePlayTime
        {
            public static string DefaultTime => $"{DateTime.Now.ToString("yyyy")}-{DateTime.Now.ToString("MM")}-{DateTime.Now.ToString("dd")}T00:00:00.000+01:00";
            public static string DefaultSuperOld => "1970-01-01T01:00:00.000+01:00";

            private int _YYYY, _MM, _DD, _hour, _min, _sec, _millis;        //private int values for different means
            private readonly string _theEnd;
            private string _end;                                            //the last part of the time string
            public bool MillisecondsFlag { get; private set; } = false;                         //flag that determins weather the milliseconds are two or three digits long
            private int milliseconds_length_flag = 3;

            public int Year => _YYYY;
            public int Month => _MM;
            public int Day => _DD;
            public int Hour => _hour;
            public int Minutes => _min;
            public int Seconds => _sec;
            public int Milliseconds => MillisecondsFlag ? _millis * 10 : _millis;
            public string Binder { get; set; } = String.Empty;
            public string Ender { get; set; } = String.Empty;
            


            public FirePlayTime(string time)
            {
                if (time == null)
                {
                    throw new ArgumentNullException("FirePlayTime constructor argument");
                }

                _theString = time;
                if (int.TryParse(RegExTime.Extrapolate(time, RegExTime.REX.Milliseconds), out _millis))
                {
                    while (_millis.ToString().Length < 3 && _millis != 0) _millis *= 10;
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

                _end = Ender = RegExTime.Extrapolate(time, RegExTime.REX.Ender);

                Validate();                                     // REMINDER: If _milis is greater than a thousand, this method will fix that
            }
            public FirePlayTime(int theYear, int theMonth, int theDay, int theHour, int theMinutes, int theSeconds, int theMilliseconds, string theEnd)
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

            public static FirePlayTime Sum(IFirePlayTime a, IFirePlayTime b)
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
                    throw new Exception($"wtf again: {b.Ender}");//{ a._theString }  ______ {b.Ender} ");
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
                return new FirePlayTime(year, month, day, hours, minutes, seconds, (a.MillisecondsFlag && b.MillisecondsFlag ? milliseconds / 10 : milliseconds), (a.MillisecondsFlag && b.MillisecondsFlag ? a.Ender : (a.Ender.Length > b.Ender.Length ? a.Ender : b.Ender)));

            }
            public static FirePlayTime Duration(string trajanje)
            {
                string default_end = "Z";

                int hour = 0, min = 0, sec = 0, millis = 0;
                if (trajanje.Contains("."))
                {
                    int index = trajanje.IndexOf(".");
                    if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                    if (!(int.TryParse(new String(trajanje.ToCharArray(), index + 1, trajanje.Length - index - 1), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");

                    if (sec > 59)
                    {
                        min = (int)(sec / 60);
                        sec -= (min * 60);
                        while (millis > 999) millis /= 10;
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
                    }
                }

                if (min > 59)
                {
                    hour = (int)(min / 60);
                    min -= (hour * 60);
                }
                return new FirePlayTime(0, 0, 0, hour, min, sec, millis, default_end);
            }

            public static string IduceVrijeme(IFirePlayTime time, string trajanje)
            {   
                return FirePlayTime.Sum(time, new FirePlayTime(2000, 1, 1, 0, 0, RegExTime.GetFromDuration(trajanje, RegExTime.REX.Seconds), RegExTime.GetFromDuration(trajanje, RegExTime.REX.Milliseconds), "Z")).GetTimeString();
            }
            public static string IduceVrijeme(IFirePlayTime time, int sec, int millis)
            {

                return FirePlayTime.Sum(time, new FirePlayTime(2000, 0, 0, 0, 0, sec, millis, "Z")).GetTimeString();
            }
            public static string IduceVrijeme(IFirePlayItem song)
            {
                string default_end = "Z";

                int min = 0, sec = 0, millis = 0;
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
                        return FirePlayTime.Sum(new FirePlayTime(song.Vrijeme), new FirePlayTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
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
                        return FirePlayTime.Sum(new FirePlayTime(song.Vrijeme), new FirePlayTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
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
                    var match = Regex.Match(s, @"(\d{4})-(\d{2})-(\d{2})([A-z]?)(\d{2})\:(\d{2})\:(\d{2})[:.]?(\d*)(.*)");
                    if (r == REX.Milliseconds && match.Groups[(int)r].Value == "") return "000";
                    return match.Groups[(int)r].Value;
                }

                public static FirePlayTime Trajanje(string trajanje)
                {
                    var match = Regex.Match(trajanje, @"(\d*).(\d*)");
                    int sec = 0, millis = 0;
                    if (!int.TryParse(match.Groups[1].Value, out sec)) sec = 0; ;
                    if (!int.TryParse(match.Groups[2].Value, out millis)) millis = 0;
                    return new FirePlayTime(0, 0, 0, 0, 0, sec, millis, "Z");
                }

                public static int GetFromDuration(string time, REX r)
                {
                    if (r != REX.Seconds && r != REX.Milliseconds) return 0;
                    int sec = 0, millis = 0;
                    Match match = Regex.Match(time, @"(\d+).?(\d*)?");
                    if (match.Groups[1].Value != String.Empty) if (!int.TryParse(match.Groups[1].Value, out sec)) sec = 0;
                        else if (match.Groups[1].Value == String.Empty) sec = 0;
                    if (match.Groups[2].Value != String.Empty) if (!int.TryParse(match.Groups[2].Value, out millis)) millis = 0;
                        else if (match.Groups[2].Value == String.Empty) millis = 0;
                    return r == REX.Seconds ? sec : millis;
                }
            }

            
        }
#endregion
    }
}
