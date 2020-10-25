using System;
using System.Collections.Generic;

namespace ExtraNoveListe
{
    public interface IFirePlayList
    {
        int GetSongCount { get; }
        string ListName { get; set; }
        string ListPath { get; set; }
        List<FirePlaySong> Songs { get; }
        DateTime ZeroTime { get; set; }

        event FirePlayList.ListChangedEventHandler ListChanged;

        void Add(FirePlaySong song);
        void AddList(FirePlayList a);
        void AddMultiple(FirePlayList list);
        void AddMultiple(List<FirePlaySong> list);
        void AddMultipleLists(FirePlayList[] lists);
        void AddMultipleLists(List<FirePlayList> lists);
        FirePlayList Clone();
        void DefaultTheList();
        FirePlaySong ExtractAt(int index);
        FirePlaySong GetByIndex(int index);
        FirePlaySong GetFirst();
        void Insert(int index, FirePlayList list);
        void Insert(int index, FirePlaySong song);
        void Insert(int index, List<FirePlaySong> songs);
        void MoveSongsDown(int index, int count);
        void MoveSongsUp(int index, int count);
        void RemoveAt(int index);
        void RemoveMany(List<int> indices);
        void UpdateTimes();
    }
}