using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ExtraNoveListe
{
    public partial class SongView : UserControl, ISongView
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////

        public static ISongView lastSelected { get; set; }
        public static bool checked_flag = false;
        private static bool _allVisibel = false;
        public static bool AllVisible { get { return _allVisibel; } }
        private static List<ISongView> _allSongViews = new List<ISongView>();
        public static List<ISongView> SongViewList { get { return _allSongViews; } }
        private static List<int> _selectedIndices = new List<int>();
        public static List<int> SelectedIndices { get { return _selectedIndices; } set { _selectedIndices = value; } }
        private static IFirePlayList currentListImplemented = null;
        public static IFirePlayList CurrentListImplemented { get { return currentListImplemented; } set { currentListImplemented = value; } }
        public static List<int> LastRemoved = new List<int>();


        public static int Margin { get; } = 40;

        public static bool MouseDownBool { get; set; } = false;
        Control a;
        private static Clipboard _clip;
        public static Clipboard ClipBoard
        {
            get
            {
                if (_clip == null) _clip = new Clipboard();
                return _clip;
            }
            set { _clip = value; }
        }

        public bool Checked { get { return checkbox_selected.Checked; } }

        //////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    VARS    ///////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        public IPlayAudio play { get; private set; }

        private IFirePlaySong _song;
        public IFirePlaySong Song { get { return _song; }
            set
            {
                _song = value;
                _song.SongChanged += UpdateThis;

            }
        }

        private bool IsSW { get { return _isSW; } }

        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                //UserControl.Location = new Point(0, SongView.Margin * value);                             //Uncomment when not using PanelOptions    <<<------------------------------  IMPORTANT
            }
        }

        private string _name;
        public string GetName { get { return _name; } set { _name = value; } }

        private string _time_info;
        public string TimeInfo { get { return _time_info; } set { _time_info = value; } }

        private bool _isSW = false;

        public Color DefaultColor { get; private set; }
        Color temp_color;

        /// <summary>
        /// UserControl for given SongView
        /// </summary>
        public UserControl UserControl { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////

        public SongView(IFirePlaySong song)
        {
            InitializeComponent();
            checkbox_selected.Visible = false;
            _allSongViews.Add(this);
            if (!_allVisibel) this.Hide();
            else if (_allVisibel) this.Show();


            TimeInfo = "0:0:00.000 @00:00:00.000";

            Song = song;
            SetPlayAudio();
            Song.SongChanged += UpdateThis;
            /*
            this.MouseEnter += (a, b) =>
            {
                if (SongView.MouseDownBool) this.Select();
            };

            this.MouseLeave += (a, b) =>
            {
                if (SongView.MouseDownBool) this.Unselect();
            };
            */

            UpdateThis(this, new EventArgs());


            if (song.Sweeper) this.Back_Color(SongViewColor.Sweeper);
            else this.Back_Color(SongViewColor.Song);

            //this.MouseDown += (z, p) => MouseDownBool = true;
            //this.MouseUp += (z, p) => MouseDownBool = false;
            //this.MouseMove += (a, b) => this.Select();
            //this.MouseEnter += (a, b) => Select(); 

            this.DefaultColor = this.Back_Color();

            this.MouseClick += Selector;
            this.song_info.MouseClick += Selector;
            this.song_time_info.MouseClick += Selector;

            KeyPress += OnKeyPressed;
            KeyDown += OnKeyDown;

            this.UserControl = (System.Windows.Forms.UserControl)this;

            linkLabel1.Visible = false;
            /*PanelOptions.EditedIndex += (a, b) =>
            {
                if (this.Parent.Equals(a))
                {
                    if (this.Index < b.FirstIndex) this.UserControl.Hide();
                    else
                    {
                        this.UserControl.Show();
                        this.UserControl.Top = (SongView.Margin - 1) * (this.Index - b.FirstIndex);
                    }
                }
            };*/
            //DrawRectangleForSelect();

            this.MouseWheel += (s, e) =>
            {
                if (play != null)
                {
                    if (play.IsPlaying)
                    {
                        if (e.Delta > 0 && ModifierKeys.HasFlag(Keys.Shift))
                        {
                            long l = play.AudioReader.Position - 96000 * 4 * 2;
                            play.AudioReader.Position = l < 0 ? 0 : l;
                        }
                        else if (e.Delta < 0 && ModifierKeys.HasFlag(Keys.Shift))
                        {
                            long l = play.AudioReader.Position + 96000 * 4 * 2;
                            play.AudioReader.Position = (double)l > (play.AudioReader.TotalTime.TotalSeconds * 4 * 96000) ? (int)(play.AudioReader.TotalTime.TotalSeconds * 4 * 96000) : l;
                        }
                    }
                }
            };
        }

        private SongView()
        {

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    METHODS    ////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _selectedIndices.Count > 0) _allSongViews[0].Remove();
        }

        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'A' && ModifierKeys.HasFlag(Keys.Control))
                _allSongViews.ForEach((x) => x.checkbox_selected.Checked = true);
        }

        private void Selector(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    PlayEndOfSelectedAndStartOfNextSong();
                    return;
                }
                else
                {
                    PlayThisSong();
                    return;
                }
            }

            if (QuickSweeper.SweeperModeOn && !(ModifierKeys.HasFlag(Keys.Control) || ModifierKeys.HasFlag(Keys.Alt) ||
                                                ModifierKeys.HasFlag(Keys.Shift)))
            {
                if (QuickSweeper.Current?.box.Items.Count > 0)
                {
                    if (QuickSweeper.Current.box.SelectedItems.Count > 0)
                    {
                        currentListImplemented.Insert(this.Song.Index, new FirePlaySong(QuickSweeper.Current.DictListOfFiles[QuickSweeper.Current.box.SelectedItem.ToString()]));
                        return;
                    }
                }
            }

            if (ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Shift))
            {
                //PlayEndOfSelectedAndStartOfNextSong();
                return;
            }

            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (!ClipBoard.IsEmpty)
                {
                    currentListImplemented.Insert(Index, ClipBoard.Retrive());
                }
                else this.checkbox_selected.Checked = !this.checkbox_selected.Checked;
            }
            else if (ModifierKeys.HasFlag(Keys.Shift) || ModifierKeys.HasFlag(Keys.ShiftKey))
            {
                if (lastSelected != null && this != lastSelected)
                {
                    if (this.Song.Index < lastSelected.Song.Index)
                    {
                        currentListImplemented.Songs.ForEach((x) =>
                        {
                            x.SongView.checkbox_selected.Checked = false;
                            if (x.Index >= this.Song.Index && x.Index <= lastSelected.Song.Index) x.SongView.checkbox_selected.Checked = true;
                        });
                    }
                    if (this.Song.Index > lastSelected.Song.Index)
                    {
                        currentListImplemented.Songs.ForEach((x) =>
                        {
                            x.SongView.checkbox_selected.Checked = false;
                            if (x.Index <= this.Song.Index && x.Index >= lastSelected.Song.Index) x.SongView.checkbox_selected.Checked = true;
                        });
                    }
                }
            }
            else
            {
                if (!ClipBoard.IsEmpty) currentListImplemented.Insert(Index, ClipBoard.RetriveOne());
                else
                {
                    currentListImplemented.Songs.ForEach((x) => x.SongView.Unselect());
                    this.Select();
                }
            }

            if (!(ModifierKeys.HasFlag(Keys.Shift) || ModifierKeys.HasFlag(Keys.ShiftKey))) lastSelected = this;
        }

        private void UpdateThis(object o, EventArgs e)
        {
            if (Song != null)
            {
                this.Name = Song.Naziv + " - " + Song.Autor;
                this.song_info.Text = Name.Length > 40 ? new String(Name.ToArray(), 0, 30) + "..." : Name;


                string lenStr = (Song.Finish-Song.Start).ToString(@"hh\:mm\:ss\.fff");
                string durStr = Song.Duration.ToString(@"hh\:mm\:ss\.fff");
                string dateStr = Song.Time.ToString(@"HH\:mm\:ss\.fff");
                if (Song.EndCue != String.Empty) this.TimeInfo = $"{lenStr} @{dateStr}";
                song_time_info.Text = TimeInfo;
            }
            if (Song.Sweeper)
            {
                this.Back_Color(SongViewColor.Sweeper);
                this.DefaultColor = SongViewColor.Sweeper;
            }

            if (!Song.Sweeper)
            {
                this.Back_Color(SongViewColor.Song);
                this.DefaultColor = SongViewColor.Song;
            }
        }

        public Control Control => (Control)this;
        //public Control Parent => this.Parent;


        /////////////////////////////////////////////// CONTROLS IN THE SONGVIEW WINDOW ///////////////////////////////////////////////
        /// <summary>
        /// Remove button on the song view itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkbox_selected.Checked = true;
            RemoveSelected();
            _selectedIndices = new List<int>();
            //Pocko();
        }

        private void checkbox_selected_CheckedChanged(object sender, EventArgs e)
        {
            this.Back_Color(this.checkbox_selected.Checked ? HSLColor.HighLightColor(this.Back_Color()) : this.DefaultColor);
        }
        /////////////////////////////////////////////// CONTROLS IN THE SONGVIEW WINDOW ///////////////////////////////////////////////


        /// <summary>
        /// Hides all parametars
        /// </summary>
        public static void HideAll()
        {
            if (_allVisibel)
            {
                _allVisibel = false;
                _allSongViews.ForEach((sv) => sv.Hide());
            }
        }
        /// <summary>
        /// Shows all parametars
        /// </summary>
        public static void ShowAll()
        {
            if (!_allVisibel)
            {
                _allVisibel = true;
                _allSongViews.ForEach((sv) => sv.Show());
            }
        }

        //public void Hide()
        //{
        //    this.Hide();
        //}

        //public void Show()
        //{
        //    this.Show();
        //}

        public static void CreateSongViewFromList(IFirePlayList list)
        {
            CreateNewList(list);
            if (!(list == currentListImplemented) && currentListImplemented != null)
            {
                currentListImplemented.ListChanged -= OnListChanged;
                currentListImplemented = list;
                currentListImplemented.ListChanged += OnListChanged;
            }
            if (currentListImplemented == null)
            {
                currentListImplemented = list;
                currentListImplemented.ListChanged += OnListChanged;
            }
        }

        internal static void SelectAll()
        {
            _allSongViews.ForEach((i) =>
            {
                if (_selectedIndices.Count == _allSongViews.Count) _selectedIndices = new List<int>();
                else
                {
                    if (_selectedIndices.Contains(i.Index)) _selectedIndices.Add(i.Index);
                }
                UpdateChecked();
            });
        }

        /// <summary>
        /// Creates a new SongView based on all songs from the implemented list
        /// </summary>
        /// <param name="list"></param>
        private static void CreateNewList(IFirePlayList list)
        {
            _allSongViews = new List<ISongView>();
            list.Songs.ForEach((x) => new SongView(x));
        }

        /// <summary>
        /// Updates all the changed indices: selected indices & songview indices (Raises SongViewChange event)
        /// </summary>
        public static void UpdateSongView()
        {
            for (int i = 0; i < _allSongViews.Count; i++)
            {
                _allSongViews[i].Index = i;  //Goes through all SongViews and updates their Index
            }
            //UpdateSelectedindicesParallel();
        }


        /// <summary>
        /// Updates selected incices in Parallel
        /// </summary>
        public static void UpdateSelectedindicesParallel()
        {
            Task.Run(() =>
            {
                //_selectedIndices = new List<int>();
                //_allSongViews.ForEach((i) => { if (i.checkbox_selected.Checked) _selectedIndices.Add(i.Index); });
                //_selectedIndices.Sort();
            });
        }

        /// <summary>
        /// Updates selected incices in Parallel
        /// </summary>
        public static void UpdateSelectedindices()
        {
            _selectedIndices = new List<int>();
            _allSongViews.ForEach((i) => { if (i.checkbox_selected.Checked) _selectedIndices.Add(i.Index); });
            _selectedIndices.Sort();
        }




        public static void UpdateCheckedByIndices()
        {
            _selectedIndices.ForEach((x) => SongViewList.Where((y) => _selectedIndices.Contains(y.Index)).ToList().ForEach((z) => z.checkbox_selected.Checked = true));
        }

        public static void Cutter()
        {
            currentListImplemented.Songs.ForEach((s) =>
            {
                if (s.SongView.Checked)
                {
                    SongView.ClipBoard.Add(s);
                }
            });
            RemoveSelected();
        }

        public static void Coppier()
        {
            currentListImplemented.Songs.ForEach((s) =>
            {
                if (s.SongView.Checked)
                {
                    SongView.ClipBoard.Add(s.GetClone());
                }
            });
            currentListImplemented.Songs.ForEach((s) => s.SongView.Unselect());
        }


        private static void OnListChanged(IFirePlayList sender, EventArgs e)
        {
            //CreateSongViewFromList((FirePlayList)sender);
            //if (currentListImplemented.Songs.Count != _allSongViews.Count) OnSongViewChanged();
            UpdateSongView();
            OnSongViewChanged();
        }

        public static string GetPathFromSongView(ISongView s)
        {
            string path = String.Empty;
            string or_path = s.Song.PathName;
            while (or_path != string.Empty)
            {
                var m = Regex.Match(or_path, @"([^\\]*)\\*(.*)");
                path += path == String.Empty ? m.Groups[1].Value : @"\" + m.Groups[1].Value;
                or_path = m.Groups[2].Value;
            }
            return path;
        }

        /*public static void Delete()
        {
            if (_selectedIndices.Count != 0) SongView.SongViewList.ToArray()[_selectedIndices.ToArray()[0]].RemoveSelected();
            
        }*/

        //////////////////////////////////////////////////////   EVENTS   //////////////////////////////////////////////////////

        public delegate void SongViewChangedEventHandler(IFirePlayList list, EventArgs e);
        public static event SongViewChangedEventHandler SongViewChanged;
        protected static void OnSongViewChanged(EventArgs e)
        {
            if (SongViewChanged != null) SongViewChanged(currentListImplemented, e);
            UpdateChecked();
        }
        protected static void OnSongViewChanged()
        {
            if (SongViewChanged != null) SongViewChanged(currentListImplemented, new EventArgs());
        }

        public delegate void SongInSongViewChangedEventHandler(ISongView sender, SongChangeEventArgs e);
        public static event SongInSongViewChangedEventHandler SongInSongViewChanged;
        public static void OnSongInSongViewChanged(ISongView sender, ChangeMade change)
        {
            if (SongInSongViewChanged != null) SongInSongViewChanged(sender, new SongChangeEventArgs(change));
        }

        public event EventHandler SongViewChecked;
        protected virtual void OnSongViewChecked()
        {
            if (SongViewChecked != null) SongViewChecked(this, new EventArgs());
        }

        //////////////////////////////////////////////////////   EVENTS   //////////////////////////////////////////////////////

        /// <summary>
        /// Moves all selected Songs in SongView Down
        /// </summary>
        public static void MoveCheckedDown()
        {
            if (_selectedIndices.Contains(_allSongViews.Count - 1)) return;
            List<int> temp = _selectedIndices.OrderByDescending((x) => x).ToList();

            temp.ForEach((i) =>
            {
                currentListImplemented.MoveSongsDown(i, 1);
            });
            //UpdateSongView();

        }
        public static void MoveCheckedUp()
        {
            if (_selectedIndices.First() == 0) return;
            List<int> temp = _selectedIndices.OrderBy((x) => x).ToList();
            temp.ForEach((i) =>
            {
                currentListImplemented.MoveSongsUp(i, 1);
            });
            //UpdateSongView();
        }

        /// <summary>
        /// Moves all selected Songs in SongView Up
        /// </summary>
        public static void UpdateChecked()
        {
            _allSongViews.Where((g) => g.checkbox_selected.Checked == true).ToList().ForEach((c) => { if (!_selectedIndices.Contains(c.Index)) _selectedIndices.Add(c.Index); });
            _allSongViews.ForEach((x) => x.checkbox_selected.Checked = _selectedIndices.Contains(x.Index) ? true : false);
            _allSongViews.ForEach((sv) => sv.Back_Color(sv.checkbox_selected.Checked ? HSLColor.HighLightColor(sv.Back_Color()) : sv.DefaultColor));
            _selectedIndices.Sort();
            //UpdateSelectedindicesParallel();
        }

        public void Back_Color(Color c)
        {
            this.BackColor = c;
        }

        public Color Back_Color() => BackColor;



        public static void RemoveSelected()
        {
            List<int> ind = new List<int>();
            currentListImplemented.Songs.ForEach((x) =>
            {
                if(x.SongView.checkbox_selected.Checked) ind.Add(x.SongView.Index);
            });
            currentListImplemented.RemoveMany(ind);
        }

        public static void RemoveAt(int index)
        {
            currentListImplemented.ExtractAt(index);
        }

        public void Remove()
        {
            if (this.Index >= 0 && this.Index < currentListImplemented.Songs.Count) SongView.RemoveAt(this.Index);
        }
        
        private void RemoveSong(object sender, EventArgs e)
        {
            RemoveSelected();
        }

        public void Select()
        {
            this.checkbox_selected.Checked = true;
        }

        public void Unselect()
        {
            this.checkbox_selected.Checked = false;
        }

        public void ChangeSelectState()
        {
            this.checkbox_selected.Checked = !this.checkbox_selected.Checked;
        }

        public SongView GetClone()
        {
            return new SongView(this.Song);
        }

        private void songToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "MP3 (*.mp3) |*.mp3|WAV (*.wav)|*.wav|All (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                currentListImplemented.Insert(this.Index, new FirePlaySong(fd.FileName));
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form info = new InfoForm(Song);
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "FirePLay list (*.fps.xml)|*.fps.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                currentListImplemented.Insert(this.Index, new FirePlayList(fd.FileName));
            }
        }

        private void songViewConntextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void playSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayThisSong();
        }

        private void PlayThisSong()
        {
            if (play != null)
            {
                play.PlayPause();
            }
            else
            {
                if (play == null)
                {
                    SetPlayAudio(); 
                }
                PlayThisSong();
            }
        }

        private void PlayEndOfSelectedAndStartOfNextSong()
        {

        }

        private void saveSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            string origina_file_path = PlayAudio.GetPathFromString(this.Song.PathName);
            sd.FileName = System.IO.Path.GetFileName(origina_file_path);
            var original_extension = System.IO.Path.GetExtension(origina_file_path);
            sd.Filter = $"(*{original_extension}|{original_extension})|All (*.*)|*.*";
            int i = 1;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                string new_file_path = sd.FileName;
                if (System.IO.File.Exists(new_file_path))
                {
                    while (System.IO.File.Exists($"{new_file_path}({i})"))
                    {
                        i++;
                    }
                    new_file_path = $"{new_file_path}({i})";
                }
                System.IO.File.Copy(origina_file_path, new_file_path, true );
            }
        }

        private void SongView_Load(object sender, EventArgs e)
        {
            
        }

        protected virtual void SetPlayAudio()
        {
            //if (Song != null && Song.play != null)
            //{
            //    if (play == null)
            //    {
            //        play = Song.play;
            //        play.PlayingStarted += OnStartPlaying;
            //        play.PlayingStopped += OnStopPlaying;
            //    }
            //    else
            //    {
            //        play.PlayingStarted -= OnStartPlaying;
            //        play.ReloadAudio(Song.PathName);
            //    }
            //}
        }

        protected virtual void OnStartPlaying(object s, EventArgs e)
        {
            progressUC.IsVisible();
            play.UpdatePositionEvent += UUU;
        }

        protected virtual void OnStopPlaying(object s, EventArgs e)
        {
            progressUC.IsInvisible();
            progressUC.Progress(0);
        }

        public void UUU(object s, PlayAudioUpdatePositionEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                progressUC.Size = new Size((int)(this.Size.Width/3 * e.PercentageDouble / 100), this.Size.Height);
                progressUC.BackColor = WindowColors.Side1;
            }, ct, tco, ts);
        }

        public static async Task<SongView> CreateSongViewAsync(IFirePlaySong song)
        {
            return await Task.Run(() => new SongView(song));
        }

        
    }
}
