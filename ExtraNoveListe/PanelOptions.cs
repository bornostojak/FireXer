using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    public class PanelOptions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////    VARS    //////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly System.Threading.SynchronizationContext context;
        public System.Threading.SynchronizationContext Context
        {
            get { return context; }
        }

        public FirePlayList FirePlayList {
            get
            {
                if (_firePlayList == null)
                {
                    this._firePlayList = new FirePlayList();
                    FirstControlIndex = 0;
                }
                return _firePlayList;
            }
            set
            {
                if (_firePlayList != null) _firePlayList.ListChanged -= OnFirePlayListChange;
                _firePlayList = value;
                _firePlayList.ListChanged += OnFirePlayListChange;
                FirstControlIndex = 0;
                OnFirePlayListChange(new object(), new EventArgs());
            } 
        }
        private FirePlayList _firePlayList ;
        public async void SetFirePlayListAsync(FirePlayList list)
        {
            await Task.Run(() => FirePlayList = list);
        }

        private Panel _panel;
        public Panel Panel
        {
            get
            {
                if (_panel == null) _panel = new Panel();
                return _panel;
            }
            set { _panel = value; }
        }

        private int _firstControlIndex = 0;
        private int _prevIndex = 0;
        public int FirstControlIndex
        {
            get { return _firstControlIndex; }
            set
            {
                _prevIndex = _firstControlIndex;
                if(FirePlayList.Songs.Count - MaxCapacity >= _firstControlIndex) _firstControlIndex = value;
                while (FirePlayList.Songs.Count - MaxCapacity < _firstControlIndex && FirePlayList.Songs.Count != 0)
                    _firstControlIndex--;
                if (_firstControlIndex < 0) _firstControlIndex = 0;
                if (_prevIndex != _firstControlIndex) { UpdatePanel(); }//EditedIndex?.Invoke(this, PanelOptionsEventArgs.SetFirstIndex(FirstControlIndex));}
            }
        }

        //public static event PanelOptionsEventArgs.PanelOptionEventHandler EditedIndex;
        public int NumberOfVisibleInPanel { get; set; } = 0;

        private int _maxCap, _prevMaxCap = 0;
        public int MaxCapacity
        {
            get { return _maxCap; }
            set { _maxCap = value; if(_prevMaxCap != _maxCap) { _prevMaxCap = _maxCap; if(!(_maxCap > FirePlayList.Songs.Count)) UpdatePanel(); } }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////


        public PanelOptions(Panel p)
        {
            Panel = p;
            Panel.SizeChanged += OnPanelSizeChange;
            //Panel.ControlAdded += OnControlChange;
            //Panel.ControlRemoved += OnControlChange;

            OnPanelSizeChange(this, new EventArgs());
            this.context = SynchronizationContext.Current;
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


        /**************************************      PRIVATE      **************************************/

        private void OnPanelSizeChange(object s, EventArgs e)
        {
            MaxCapacity = Panel.Size.Height / SongView.Margin;
            FirstControlIndex = FirstControlIndex;
        }

        private async void OnFirePlayListChange(object s, EventArgs e)
        {
            Panel.Controls.Clear();
            int z = 0;
            FirePlayList.Songs.ForEach((song) =>
            {
                Panel.Parent.Invoke(new MethodInvoker(delegate { 
                    Panel.Controls.Add(song.SongView.UserControl);
                    song.SongView.UserControl.Top = (SongView.Margin - 1) * z++;
                }));
                
                
            });

            await UpdatePanel();
        }

        public async Task UpdatePanel()
        {
            await Task.Run(() =>
            {
                this.Context.Post(new SendOrPostCallback((s) =>
                {
                    if (FirePlayList.Songs.Count > 0)
                    {
                        for (int j = 0; j < FirstControlIndex; j++) FirePlayList.Songs[j].SongView.UserControl.Hide();

                        int z = 0;
                        for (int i = FirstControlIndex; i < FirePlayList.Songs.Count && z < MaxCapacity + 1; i++)
                        {
                            Panel.Controls[i].Top = (SongView.Margin - 1) * z++;
                            Panel.Controls[i].Show();
                        }
                    }
                }), null);
            });

        }
        
        public void UpdatePanel(object o, EventArgs e)
        {
            UpdatePanel();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        /*
        public delegate void PanelOptionsEventHandler(object sender, PanelOptionsEventArgs e);
        public event PanelOptionsEventHandler PanelSizeChange;
        /// <summary>
        /// Triiggers the PanelSizeChange event
        /// </summary>
        protected void OnPanelSizeChange()
        {
            PanelSizeChange?.Invoke(this, new PanelOptionsEventArgs());
        }
        */




    }

    /// <summary>
    /// Special class to further inplement any necessary PanelOpetions EventArgs properties
    /// </summary>
    public class PanelOptionsEventArgs : EventArgs
    {
        public int FirstIndex { get; set; }= 0;

        public PanelOptionsEventArgs()
        {
            
        }

        public static PanelOptionsEventArgs SetFirstIndex(int index)
        {
            PanelOptionsEventArgs p = new PanelOptionsEventArgs();
            p.FirstIndex = index;
            return p;
        }

        public delegate void PanelOptionEventHandler(object sender, PanelOptionsEventArgs e);
    }
}
