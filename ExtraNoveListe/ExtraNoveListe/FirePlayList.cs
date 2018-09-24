using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExtraNoveListe
{
    public class FirePlayList
    {

        /////////////////////////////////////////////    STATIC VAR    /////////////////////////////////////////////

        private static int _list_counter = 0;
        public static int GetListCount { get { return _list_counter; } }
        private static List<FirePlayList> _all_Lists = new List<FirePlayList>();
        public static List<FirePlayList> AllLists { get { return _all_Lists; } }


        ////////////////////////////////////////////////    VAR    ////////////////////////////////////////////////

        private int _song_counter = 0;
        public int GetSongCount { get { return _list.Count; } }

        private string _list_name = string.Empty;
        public string ListName { get { return _list_name; } set { _list_name = value != string.Empty ? value : _list_name; } }

        private List<FirePlaySong> _list = new List<FirePlaySong>();
        public List<FirePlaySong> Songs { get { return _list; } }

        private string _list_path = null;
        public string ListPath { get { return _list_path; } set { _list_path = value; } }

        public string firstTime = string.Empty;

        public string ZeroTime { get; set; } = FPTime.DefaultTime;


        //////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////   CONSTRUCTORS   //////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////

        //CREATES A TEMPORARRY LIST
        public FirePlayList()
        {
            _list_counter++;
            _all_Lists.Add(this);
        }

        public FirePlayList(string path)
        {
            if (path == "temp" || path == "Temp") { _list_name = "temp"; return; }
            _list_counter++;
            _list_name = System.IO.Path.GetFileName(path);
            CreateFromFileXML(path);
            UpdateTimes();
            _all_Lists.Add(this);
            _list_path = path;
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////   STATIC   ///////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////


        public static FirePlayList CrearteFromMany(List<string> paths)
        {
            var nl = new FirePlayList("temp");
            foreach (string str in paths) nl.AddList(new FirePlayList(str));
            return nl;
        }

        public static FirePlayList CrearteFromMany(string[] paths)
        {
            var nl = new FirePlayList("temp");
            foreach (string str in paths) nl.AddList(new FirePlayList(str));
            return nl;
        }

        public static FirePlayList Clone(FirePlayList list)
        {
            var temp = new FirePlayList("temp");
            temp.ListPath = list.ListPath;
            temp._list = list.Songs;
            return temp;
        }

        public static FirePlayList MergeLists(FirePlayList a, FirePlayList b)
        {
            var temp = new FirePlayList();
            temp.ListName = a.ListName;
            temp.AddList(a);
            temp.AddList(b);
            temp._list_path = a.ListPath;
            temp.UpdateTimes();
            return temp;
        }


        public static void RemoveFromAll(FirePlayList list) { _all_Lists.Remove(list); }


        ///////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////   METHODS   ///////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////

        public void Add(FirePlaySong song) { _list.Add(song); OnListChanged(); }

        public void AddMultiple(List<FirePlaySong> list) { list.ForEach((x) => { _list.Add(x); OnListChanged(); }); }
        public void AddMultiple(FirePlayList list) { list.Songs.ForEach((x) => { _list.Add(x); OnListChanged(); }); }

        private void CreateFromFileXML(string path)
        {

            XmlDocument doc = new XmlDocument();
            using (System.IO.StreamReader fs = new System.IO.StreamReader(path, Encoding.Default))
            {
                try
                {
                    doc.Load(fs);
                }
                catch
                {

                }
                List<XmlNode> _nodes = new List<XmlNode>();
                XmlNode root = doc.FirstChild;

                if (root.Name == "PlayList")
                {
                    if (root.HasChildNodes)
                    {
                        XmlNodeList children = root.ChildNodes;
                        foreach (XmlNode ch in children)
                        {
                            if (ch.HasChildNodes && ch.Name == "PlayItem")
                            {
                                FirePlaySong songsy = FirePlaySong.CreatorSongFromXMLNode(ch);
                                songsy.PartOfList = this;
                                _list.Add(songsy);
                                _song_counter++;
                            }
                        }
                    }
                }
            }
            if (_list.Count > 0) this.ZeroTime = _list[0].Vrijeme;
        }

        public FirePlaySong GetFirst()
        {
            return _list.ToArray()[0];
        }

        /// It's a zero-base index
        public FirePlaySong GetByIndex(int index)
        {
            return (index > _list.Count) ? FirePlaySong.CreateSong() : _list.ToArray()[index];
        }

        public void MoveSongsUp(int index, int count)
        {
            if (index != 0)
            {
                for (int i = index; i < i + count; i++)
                {
                    Insert(i - 1, ExtractAt(i));
                }
                OnListChanged();
            }
        }
        public void MoveSongsDown(int index, int count)
        {
            if (index + count - 1 != _list.Count)
            {
                for (int i = index + count - 1; i >= index; i--)
                {
                    Insert(i + 1, ExtractAt(i));
                }
                OnListChanged();
            }
        }

        protected virtual void OnListChanged()
        {
            UpdateTimes();
            if (ListChanged != null) ListChanged(this, new EventArgs());
        }


        public void UpdateTimes()
        {
            if (_list.Count > 0)
            {
                _list[0].Vrijeme = ZeroTime;
                string time = string.Empty;
                int z = 0;
                _list.ForEach((X) =>
                {
                    X.Index = z;
                    z++;
                    if (time != string.Empty) X.Vrijeme = time;
                    time = FPTime.IduceVrijeme(X.Time, X.Trajanje);
                }); 
            }
        }

        public FirePlaySong ExtractAt(int index)
        {
            FirePlaySong t = _list.ToArray()[index];
            _list.RemoveAt(index);
            OnListChanged();
            return t;
        }

        public void Insert(int index, FirePlaySong song)
        {
            if (!(index < 0) && !(index > GetSongCount - 1)) _list.Insert(index, song);
            else if (index == GetSongCount) _list.Add(song);
            UpdateTimes();
            OnListChanged();
        }

        public void Insert(int index, List<FirePlaySong> songs)
        {
            if (!(index < 0) && !(index > GetSongCount - 1)) _list.InsertRange(index, songs);
            else if (index == GetSongCount) _list.AddRange(songs);
            //if (index == 0 && _list.Count != 0) _list[0].Vrijeme = ZeroTime;
            UpdateTimes();
            OnListChanged();
        }

        public void Insert(int index, FirePlayList list)
        {
            this.Insert(index, list.Songs);
        }

        public void AddList(FirePlayList a)
        {
            if (ListName == "temp") ListName = a.ListName;
            _list.AddRange(a.Songs);
            _song_counter += a.GetSongCount;
            if (_list.Count > 0) ZeroTime = _list[0].Vrijeme;
            OnListChanged();
        }

        public void AddMultipleLists(List<FirePlayList> lists)
        {
            foreach (FirePlayList a in lists)
            {
                _list.AddRange(a.Songs);
                OnListChanged();
            }
        }

        public void AddMultipleLists(FirePlayList[] lists)
        {
            foreach (FirePlayList a in lists)
            {
                _list.AddRange(a.Songs);
                OnListChanged();
            }
        }

        internal void RemoveSong(FirePlaySong song)
        {
            try { song.SongView.Parent.Controls.Remove(song.SongView); }
            catch { }
            _list.Remove(song);
            OnListChanged();
        }

        public void RemoveMany(List<int> indices)
        {
            indices.OrderByDescending((x) => x).ToList().ForEach((x) =>
            {
                try { _list[x].SongView.Parent.Controls.Remove(_list[x].SongView); }
                catch { }
                _list.RemoveAt(x);
            });
            OnListChanged();
        }


        /// <summary>
        /// Returns the new list as unmixing (default mix)
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public void DefaultTheList()
        {
            _list.ForEach((x) => x.DefaultTheSong());
            OnListChanged();
        }

        public void RemoveAt(int index)
        {
            try {  _list[index].SongView.Parent.Controls.Remove(_list[index].SongView); }
            catch { }
            _list.RemoveAt(index);
            _song_counter--;
            OnListChanged();
        }

        public FirePlayList Clone()
        {
            FirePlayList temp = new FirePlayList("temp");
            temp.ListPath = ListPath;
            temp.ListName = ListName;
            Songs.ForEach((x) => { temp._list.Add(x.GetClone()); });
            return temp;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    EVENTS    //////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        private EventArgs ev = new EventArgs();
        public delegate void ListChangedEventHandler(FirePlayList sender, EventArgs e);
        public event ListChangedEventHandler ListChanged;
    }
}
