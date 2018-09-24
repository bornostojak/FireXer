﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.InteropServices;

namespace ExtraNoveListe
{
    public class FirePlaySong
    {
        
        private FirePlaySong()
        {
            _Vrijeme = FPTime.DefaultTime;
            this.SongView = new SongView(this);
        }

        private FirePlaySong(int i)
        {

            _Vrijeme = FPTime.DefaultTime;
            this.SongView = new SongView(this);
        }

        public FirePlaySong(string path)
        {
            if (System.IO.Path.GetExtension(path) == ".mp3" || System.IO.Path.GetExtension(path) == ".wav")
            {
                Path = path;
                TagLib.File tl_file = TagLib.File.Create(path);
                _ID = "-2";
                _Naziv = System.IO.Path.GetFileNameWithoutExtension(path);
                _Autor = "";
                _Album = "";
                _Info = "";
                _Tip = "10";
                _Color = "0x0000000a";
                _NaKanalu = "0";
                _PathName = path.ToLower();//new System.IO.DirectoryInfo(path).FullName.ToLower() + "\\" + System.IO.Path.GetFileName(path);
                _ItemType = "3";
                _StartCue = "0";
                _EndCue = (tl_file.Properties.Duration.Hours * 60 + tl_file.Properties.Duration.Minutes * 60 + tl_file.Properties.Duration.Seconds).ToString() + "." + tl_file.Properties.Duration.Milliseconds;
                _Pocetak = "0";
                _Trajanje = _EndCue;
                _Vrijeme = FPTime.DefaultTime;
                _StvarnoVrijemePocetka = FPTime.DefaultSuperOld;
                _VrijemeMinTermin = FPTime.DefaultSuperOld;
                _VrijemeMaxTermin = FPTime.DefaultSuperOld;
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
                if ((path.ToLower().Contains("sw") || path.ToLower().Contains("dr") || path.ToLower().Contains(" j ")) && tl_file.Properties.Duration.Seconds < 60) Sweeper = true;
                Sweeper = true;
            }


           // RadioSWSettings.AllRadioSWSettings.ForEach((r) =>
           // {
            //    r.RadioFolders.ForEach((f) =>
            //    {
            //        f.Files.ForEach((x) =>
             //       {
            //            if (x.ToLower().Contains(_Naziv.ToLower())) Sweeper = true;
            //        });
            //    });
            //});
            
            this.SongView = new SongView(this);
        }
        
        ////////////////////////////////////////////////////////////////////////
        ///////////////////////////   STATIC VARS    ///////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /*private FirePlaySong(XmlNode node)
        {
            _counter++;
        }*/
        private static long _counter;
        public static long Counter { get { return _counter; } }

        private static List<FirePlaySong> _all_songs = new List<FirePlaySong>();
        public static List<FirePlaySong> AllSongs { get { return _all_songs; } }


        /**
        private static XmlDocument docy = new XmlDocument();
        private XmlNode _song_xml = docy.CreateElement("Placeholder");
        public XmlNode SongXMLNode { get { return _song_xml; } set { _song_xml = value; } }
    */

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////   SONG VARS   ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private bool _isSweeper = false;
        public string Path { get; set; }

        private string _ID = String.Empty;
        private string _Naziv = String.Empty;
        private string _Autor = String.Empty;
        private string _Album = String.Empty;
        private string _Info = String.Empty;
        private string _Tip = String.Empty;
        private string _Color = String.Empty;
        private string _NaKanalu = String.Empty;
        private string _PathName = String.Empty;
        private string _ItemType = String.Empty;
        private string _StartCue = String.Empty;
        private string _EndCue = String.Empty;
        private string _Pocetak = String.Empty;
        private string _Trajanje = String.Empty;
        private string _Vrijeme = String.Empty;
        private FPTime _Time;
        private string _StvarnoVrijemePocetka = String.Empty;
        private string _VrijemeMinTermin = String.Empty;
        private string _VrijemeMaxTermin = String.Empty;
        private string _PrviU_Bloku = String.Empty;
        private string _ZadnjiU_Bloku = String.Empty;
        private string _JediniU_Bloku = String.Empty;
        private string _FiksniU_Terminu = String.Empty;
        private string _Reklama = String.Empty;
        private string _WaveIn = String.Empty;
        private string _SoftIn = String.Empty;
        private string _SoftOut = String.Empty;
        private string _Volume = String.Empty;
        private string _OriginalStartCue = String.Empty;
        private string _OriginalEndCue = String.Empty;
        private string _OriginalPocetak = String.Empty;
        private string _OriginalTrajanje = String.Empty;

        /// <summary>
        /// Determines wether the song is a sweeper
        /// </summary>
        public bool Sweeper { get { return _isSweeper; } set { _isSweeper = value; } }

        public string ID { get { return _ID; } set { _ID = value; OnSongChange(); } }
        public string Naziv { get { return _Naziv; } set { _Naziv = value; OnSongChange(); } }
        public string Autor { get { return _Autor; } set { _Autor = value; OnSongChange(); } }
        public string Album { get { return _Album; } set { _Album = value; OnSongChange(); } }
        public string Info {  get { return _Info; } set { _Info = value; OnSongChange(); } }
        public string Tip { get { return _Tip; } set { _Tip = value; OnSongChange(); } }

        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                if (_Color.ToLower() == "0x0000000a") Sweeper = true;
                else Sweeper = false;
                OnSongChange();
            }
        }

        public string NaKanalu { get { return _NaKanalu; } set { _NaKanalu = value; OnSongChange(); } }
        public string PathName { get { return _PathName; } set { _PathName = value; OnSongChange(); } }
        public string ItemType { get { return _ItemType; } set { _ItemType = value; OnSongChange(); } }
        public string StartCue { get { return _StartCue; } set { _StartCue = value; OnSongChange(); } }
        public string EndCue { get { return _EndCue; } set { _EndCue = value; OnSongChange(); } }
        public string Pocetak { get { return _Pocetak; } set { _Pocetak = value; OnSongChange(); } }
        public string Trajanje { get { return _Trajanje; } set { _Trajanje = value; OnSongChange(); } }////////////////////////////////////////////////////
        public string Vrijeme { get { return _Vrijeme; } set { _Vrijeme = value; _Time = new FPTime(_Vrijeme); OnSongChange();} }/////////////////////////////////////////
        public FPTime Time { get { return _Time; } }
        public string StvarnoVrijemePocetka { get { return _StvarnoVrijemePocetka; } set { _StvarnoVrijemePocetka = value; OnSongChange(); } }
        public string VrijemeMinTermin { get { return _VrijemeMinTermin; } set { _VrijemeMinTermin = value; OnSongChange(); } }
        public string VrijemeMaxTermin { get { return _VrijemeMaxTermin; } set { _VrijemeMaxTermin = value; OnSongChange(); } }
        public string PrviU_Bloku { get { return _PrviU_Bloku; } set { _PrviU_Bloku = value; OnSongChange(); } }
        public string ZadnjiU_Bloku { get { return _ZadnjiU_Bloku; } set { _ZadnjiU_Bloku = value; OnSongChange(); } }
        public string JediniU_Bloku { get { return _JediniU_Bloku; } set { _JediniU_Bloku = value; OnSongChange(); } }
        public string FiksniU_Terminu { get { return _FiksniU_Terminu; } set { _FiksniU_Terminu = value; OnSongChange(); } }
        public string Reklama { get { return _Reklama; } set { _Reklama = value; OnSongChange(); } }
        public bool ReklamaBool { get { return (_Reklama == "true") ? true : false; } }
        public string WaveIn { get { return _WaveIn; } set { _WaveIn = value; OnSongChange(); } }
        public bool WaveInBool { get { return (_WaveIn == "true") ? true : false; } }
        public string SoftIn { get { return _SoftIn; } set { _SoftIn = value; OnSongChange(); } }
        public string SoftOut { get { return _SoftOut; } set { _SoftOut = value; OnSongChange(); } }
        public string Volume { get { return _Volume; } set { _Volume = value; OnSongChange(); } }
        public string OriginalStartCue { get { return _OriginalStartCue; } set { _OriginalStartCue = value; OnSongChange(); } }
        public string OriginalEndCue { get { return _OriginalEndCue; } set { _OriginalEndCue = value; OnSongChange(); } }
        public string OriginalPocetak { get { return _OriginalPocetak; } set { _OriginalPocetak = value; OnSongChange(); } }
        public string OriginalTrajanje { get { return _OriginalTrajanje; } set { _OriginalTrajanje = value; OnSongChange(); } }

        public SongView SongView { get; set; }

        public int _index = -1;
        public int Index
        {
            get { return _index; }
            set
            {
                this.SongView.Index = value;
                _index = value;
            }
        }

        private FirePlayList _partOfList;
        public FirePlayList PartOfList { get { if (_partOfList == null) _partOfList = new FirePlayList(); return _partOfList;} set { _partOfList = value;} }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////   EVENTS   ////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////



        EventArgs e = null;
        public delegate void SongChangedEvent(FirePlaySong m, EventArgs e);
        public event SongChangedEvent SongChanged;

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
            for(int i = 0; i < FirePlaySong.Elements.Length; i++)
            {
                XmlNode c = d.CreateElement(FirePlaySong.Elements[i]);
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

        public FirePlaySong GetClone()
        {
            FirePlaySong temp = FirePlaySong.CreateTemp();

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

            temp.SongView = new SongView(temp);

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
        public static void DefaultTheSong(FirePlaySong song)
        {
            song._SoftIn = "0";
            song._SoftOut = "0";
            song._StartCue = song._OriginalStartCue;
            song._EndCue = song._OriginalEndCue;
            song._Pocetak = song._OriginalPocetak;
            song._Trajanje = song._OriginalTrajanje;
            
        }
        
        public static FirePlaySong CreateSong()
        {
            FirePlaySong n = new FirePlaySong();
            _all_songs.Add(n);
            return n;
        }

        public static FirePlaySong CreateTemp()
        {
            return new FirePlaySong(0);
        }

        public static string IduceVrijeme(FPTime time, string trajanje)
        {
            string default_end = string.Empty;

            int min = 0, sec = 0, millis = 0;
            if (trajanje.Contains("."))
            {
                int index = trajanje.IndexOf(".");
                if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (!(int.TryParse(new String(trajanje.ToCharArray(), index + 1, trajanje.Length), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");

                if (sec > 60)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(time, new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }
            else
            {
                if (!(int.TryParse(new String(trajanje.ToCharArray(), 0, trajanje.Length), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (sec > 60)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(time, new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }


            return string.Empty;
        }

        public static string IduceVrijeme(FirePlaySong song)
        {
            string default_end = string.Empty;

            int min = 0, sec = 0, millis = 0;
            //int index = -1;
            if (song.Trajanje.Contains("."))
            {
                int index = song.Trajanje.IndexOf(".");
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), 0, index), out sec))) throw new IndexingErrorExeption("Index for dot missaligned");
                if (!(int.TryParse(new String(song.Trajanje.ToCharArray(), index + 1, song.Trajanje.Length), out millis))) throw new IndexingErrorExeption("Index for dot missaligned");

                if (sec > 60)
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
                if (sec > 60)
                {
                    min = (int)(sec / 60);
                    sec -= (min * 60);
                    while (millis > 1000) millis /= 10;
                    return FPTime.Sum(new FPTime(song.Vrijeme), new FPTime(0, 0, 0, 0, min, sec, millis, default_end)).GetTimeString();
                }
            }


            return string.Empty;
        }

        public static FirePlaySong CreatorSongFromXMLNode(XmlNode node)
        {
            FirePlaySong new_song = FirePlaySong.CreateSong();


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

        public static FirePlayList CreateListFromXMLNode(XmlNode node)
        {
            FirePlayList new_list = new FirePlayList();
            if (node.Name == "PlasList" && node.HasChildNodes)
            {
                if (node.HasChildNodes)
                {
                    XmlNodeList children = node.ChildNodes;
                    foreach (XmlNode ch in children)
                    {
                        if (ch.HasChildNodes && ch.Name == "PlayItem")
                        {
                            FirePlaySong new_song = FirePlaySong.CreateSong();
                            foreach (XmlNode c in ch.ChildNodes)
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
                            new_list.Add(new_song);
                        }
                    }
                }
            }
            else if (node.Name == "PlayItem" && node.HasChildNodes)
            {
                FirePlaySong new_song = FirePlaySong.CreateSong();
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
                new_list.Add(new_song);
            }
            return new_list;
        }

        public static string[] Elements = { "ID", "Naziv", "Autor", "Album", "Info", "Tip", "Color", "NaKanalu", "PathName", "ItemType", "StartCue", "EndCue", "Pocetak", "Trajanje", "Vrijeme", "StvarnoVrijemePocetka", "VrijemeMinTermin", "VrijemeMaxTermin", "PrviU_Bloku", "ZadnjiU_Bloku", "JediniU_Bloku", "FiksniU_Terminu", "Reklama", "WaveIn", "SoftIn", "SoftOut", "Volume", "OriginalStartCue", "OriginalEndCue", "OriginalPocetak", "OriginalTrajanje" };

    }
}
