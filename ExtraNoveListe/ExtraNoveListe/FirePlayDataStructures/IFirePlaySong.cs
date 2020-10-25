using System;

namespace ExtraNoveListe
{
    public interface IFirePlaySong
    {
        int Index { get; set; }

        string Album { get; set; }
        string Autor { get; set; }
        string Color { get; set; }
        string EndCue { get; set; }
        string FiksniU_Terminu { get; set; }
        string ID { get; set; }
        string Info { get; set; }
        string ItemType { get; set; }
        string JediniU_Bloku { get; set; }
        string NaKanalu { get; set; }
        string Naziv { get; set; }
        string OriginalEndCue { get; set; }
        string OriginalPocetak { get; set; }
        string OriginalStartCue { get; set; }
        string OriginalTrajanje { get; set; }
        string Path { get; set; }
        string PathName { get; set; }
        string Pocetak { get; set; }
        string PrviU_Bloku { get; set; }
        string Reklama { get; set; }
        string SoftIn { get; set; }
        string SoftOut { get; set; }
        string StartCue { get; set; }
        string StvarnoVrijemePocetka { get; set; }
        string Tip { get; set; }
        string Trajanje { get; set; }
        string Volume { get; set; }
        string Vrijeme { get; set; }
        string VrijemeMaxTermin { get; set; }
        string VrijemeMinTermin { get; set; }
        string WaveIn { get; set; }
        string ZadnjiU_Bloku { get; set; }

        IFirePlayList PartOfList { get; set; }
        //IPlayAudio play { get; }

        bool ReklamaBool { get; }
        bool Sweeper { get; set; }
        bool WaveInBool { get; }

        ISongView SongView { get; set; }
        DateTime Time { get; set; }
        TimeSpan Duration { get; set; }
        TimeSpan Start { get; set; }
        TimeSpan Finish { get; set; }

        event FirePlaySong.SongChangedEvent SongChanged;

        void DefaultTheSong();
        void SongXML();
        void IsSweeper();
        string SongElementByIndex(int index);
        FirePlaySong GetClone();
    }
}