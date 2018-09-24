using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{
    public class Clipboard
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////    VARS    //////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        private int _counter = 0;
        public bool IsEmpty { get { return Songs.Count == 0 ? true : false; } }

        private List<FirePlaySong> _songs;
        public List<FirePlaySong> Songs
        {
            get
            {
                if (_songs == null) _songs = new List<FirePlaySong>();
                return _songs;
            }
            set { _songs = value; }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////

        public Clipboard()
        {
            
        }

        public Clipboard(List<FirePlaySong> songs)
        {
            Songs = songs;
        }

        public Clipboard(FirePlayList list)
        {
            list.Songs.ForEach((s) => Songs.Add(s.GetClone()));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      STATIC      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        /**************************************      PUBLIC      **************************************/


        /**************************************      PRIVATE      **************************************/



        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      METHOD      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        /**************************************      PUBLIC      **************************************/
        public void Clear()
        {
            Songs = new List<FirePlaySong>();
            _counter = 0;
        }

        public void Add(FirePlaySong song)
        {
            Songs.Add(song);
            _counter++;
        }

        public void AddRange(List<FirePlaySong> songs)
        {
            songs.ForEach((s) => Songs.Add(s));
            _counter++;
        }
        public void AddRange(FirePlaySong[] songs)
        {
            foreach (FirePlaySong s in songs)
            { 
                Songs.Add(s);
                _counter++;
            }
        }
        public void AddRange(FirePlayList list)
        {
            list.Songs.ForEach((s) => Songs.Add(s));
            _counter++;
        }

        public List<FirePlaySong> Retrive()
        {
            List<FirePlaySong> n = new List<FirePlaySong>();
            Songs.ForEach((s) => n.Add(s.GetClone()));
            this.Clear();
            return n;
        }

        public FirePlaySong RetriveOne()
        {
            FirePlaySong s;
            s = Songs[0];
            _counter--;
            Songs.RemoveAt(0);
            return s;
        }
        /**************************************      PRIVATE      **************************************/


        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        /**************************************      PUBLIC      **************************************/


        /**************************************      PRIVATE      **************************************/





    }
}
