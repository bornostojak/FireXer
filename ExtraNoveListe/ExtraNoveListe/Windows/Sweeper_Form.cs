//#define VIEW_ACTIVE
#define VIEW_INACTIVE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ExtraNoveListe
{

    public partial class Sweeper_Form : Form
    {
        public Radio CurrentRadioSelected { get { return _currentRadioSelected; } set { _currentRadioSelected = value; } }
        private Radio _currentRadioSelected = Radio.None;

        int def = 0;
        FPTime tttt = new FPTime("2018-05-30T00:17:18:956+01:00");
        FPTime tttt2 = new FPTime("2018-05-30T00:17:18:95Z");

        ScrollBar vScrollBar1 = new VScrollBar();

        private enum View { List, Detail}
        private static View _currentView = View.Detail;

        private string[] _files = new string[0];
        private FirePlayList imported_list = new FirePlayList("temp");
        
        private StartingWindowForm _main_window;
        
        private PanelOptions Panel_Options { get; set; }
        

        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////

        public Sweeper_Form(int z, StartingWindowForm main_window)
        {
            InitializeComponent();

            this.Panel_Options = new PanelOptions(panel1);

            panel1.BackColor = System.Drawing.ColorTranslator.FromHtml("#888888");
            SongView.ShowAll();

            this.Show();
            _main_window = main_window;
            Setup();
            this.AllowDrop = true;                                  //enables drag-n-drop functionaity
            this.DragEnter += new DragEventHandler(DragEnter_);     //Drag-n-drop funcitonality
            this.DragDrop += new DragEventHandler(DragDrop_);       //Drag-n-drop funcitonality
            this.SizeChanged += new EventHandler(Changed);          //Should reacto to window size change
            tabControl1.SelectedIndex = z;

            this.KeyPreview = true;
            this.KeyPress += KeyPressed;
            this.KeyDown += KeyDowned;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      SETUP      ///////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void Setup()
        {
            listBox3.Items.AddRange(new string[] { "ID", "Naziv", "Autor", "Album", "Info", "Tip", "Color", "NaKanalu", "PathName", "ItemType", "StartCue", "EndCue", "Pocetak", "Trajanje", "Vrijeme", "StvarnoVrijemePocetka", "VrijemeMinTermin", "VrijemeMaxTermin", "PrviUBloku", "ZadnjiUBloku", "JediniUBloku", "FiksniUTerminu", "Reklama", "WaveIn", "SoftIn", "SoftOut", "Volume", "OriginalStartCue", "OriginalEndCue", "OriginalPocetak", "OriginalTrajanje" });

            //this.FormBorderStyle = FormBorderStyle.None;

            def = this.Size.Width;
            label1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            label3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            label2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            brisanje_button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            listBox1.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            listBox2.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            listBox3.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
            names_button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            tabControl1.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            listBox2.SelectionMode = SelectionMode.MultiSimple;
            //this.SizeChanged += this.Label2_Size;
            tabPage1.Text = "Informacije";
            tabPage2.Text = "Sweeperi";
            tabPage3.Text = "Napravi Listu";
            this.KeyPreview = true;

            //panel1.AutoScroll = true;
            //listview_box.Visible = false;
            panel1.AutoScroll = false;


            this.MinimumSize = new Size(800, 500);
            this.MaximumSize = new Size(800, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

            //// SWEEPER TAB //////
            panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            sweeper_listbox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
            radio_listbox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);

            this.KeyDown += (t, u) =>
            {
                if (tabControl1.SelectedIndex == 1) if (u.KeyCode == (Keys.A | Keys.Control)) SongView.SelectAll();
            };

            tabControl1.KeyDown += (t, u) =>
            {
                if (tabControl1.SelectedIndex == 1) if (u.KeyCode == (Keys.A | Keys.Control)) SongView.SelectAll();
            };

            panel1.KeyDown += (t, u) =>
            {
                if (tabControl1.SelectedIndex == 1) if (u.KeyCode == (Keys.A | Keys.Control)) SongView.SelectAll();
            };
            //// SWEEPER TAB //////

            FilesChanged += UpdateWhatHasToBeDone;

            SongView.SongInSongViewChanged += UpdateSongViewAfterChange;
            SongView.SongViewChanged += UpdateSongViewAfterChange;

            panelSetUp();
            //ChangePanelView(_currentView);


            ////////////////////////////////////  RADIO LISTVIEW  ////////////////////////////////////
            RADIOSetup();
            ////////////////////////////////////  RADIO LISTVIEW  ////////////////////////////////////


            ////////////////////////////////////  RADIO LISTVIEW  ////////////////////////////////////
            SweepersSetup();
            ////////////////////////////////////  RADIO LISTVIEW  ////////////////////////////////////

            this.MouseWheel += ScrollFunction;
            QuickSweeper.tpb = view_button_Click;

            ColorSetup();
            WindowColors.ColorChangeEvent += new WindowColors.Pump(ColorSetup);
            Stylize();
            WindowColors.Colorize(this);
            WindowColors.ColorChangeEvent += () => WindowColors.Colorize(this);
        }

        private void ScrollFunction(object sender, MouseEventArgs e)
        {
            int DeltaFactor = 4;

            if (!ModifierKeys.HasFlag(Keys.Shift))
            {
                if (QuickSweeper.SweeperModeOn)
                {
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        if (e.Delta > 0 && sweeper_listbox.Items.Count > 0)
                        {
                            sweeper_listbox.SelectedIndex =
                                sweeper_listbox.SelectedIndex <= 0 ? 0 : sweeper_listbox.SelectedIndex - 1;
                        }
                        else if (e.Delta < 0 && sweeper_listbox.Items.Count > 0)
                        {

                            sweeper_listbox.SelectedIndex =
                                sweeper_listbox.SelectedIndex >= sweeper_listbox.Items.Count - 1 ? sweeper_listbox.Items.Count - 1 : sweeper_listbox.SelectedIndex + 1;
                        }
                        return;
                    }
                }
                if ((!ModifierKeys.HasFlag(Keys.Alt) && QuickSweeper.SweeperModeOn) || !QuickSweeper.SweeperModeOn)
                {
                    if (e.Delta > 0) Panel_Options.FirstControlIndex--;
                    else if (e.Delta < 0) Panel_Options.FirstControlIndex++;
                } 
            }
        }

        private void SweepersSetup()
        {
            QuickSweeper.SWF = this;

            radio_listbox.SelectedIndexChanged += (a, b) =>
            {
                if(radio_listbox.SelectedIndices.Count > 0) 
                {
                    string s = radio_listbox.SelectedItem.ToString();
                    sweeper_listbox.Items.Clear();
                    List<RadioSWSettingsFolders> folders = RadioSWSettings.AllRadioSWSettings.Where((y) => y.RadioName == s)?.FirstOrDefault().RadioFolders;

                    folders.ForEach((x) => sweeper_listbox.Items.Add(x.FolderName)); 
                }
            };

            sweeper_listbox.SelectedIndexChanged += (a, b) =>
            {
                QuickSweeper.Updater();
            };
        }

        private void RADIOSetup()
        {
            RadioSWSettings.AllRadioSWSettings.ForEach((r) =>
            {
                radio_listbox.Items.Add(r.RadioName);
            });

            radio_listbox.DoubleClick += (t, z) => radio_listbox.ClearSelected();
            radio_listbox.SelectedIndexChanged += (t, z) => UpdateSWView();
        }

        private void panelSetUp()
        {
            panel1.DragEnter += new DragEventHandler(DragEnter_);
            panel1.DragDrop += new DragEventHandler(DragDrop_);

            //panel1.MouseWheel += new MouseEventHandler(Panel1_MouseWheel);

        }

        private void Sweeper_Form_Load(object sender, EventArgs e)
        {

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
        
        public void UpdateWhatHasToBeDone(object sender, EventArgs e)
        {
            UpdateImportedList(this, e);
            UpdateListBox2(this, e);
            if (imported_list.Songs.Count != 0 && SongView.SongViewList.Count == 0) UpdateSongViewAfterChange(new SongChangeEventArgs(ChangeMade.Added));

        }

        public void UpdateListBox2(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(_files);
            if (listBox1.Items.Count != 0) names_button_Click_1(this, e);
        }

        public void UpdateSongView(object sender, EventArgs e)
        {
            ClearPanelControls();
            SongView.CreateSongViewFromList(imported_list);

            UpdateSongViewAfterChange(this, new EventArgs());
        }

        /**************************************      PRIVATE      **************************************/
        
        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
        }
        private void KeyDowned(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) SongView.RemoveSelected();
            if (e.KeyCode == Keys.A && ModifierKeys.HasFlag(Keys.Control)) imported_list.Songs.ForEach((x) => x.SongView.Select());
            if (e.KeyCode == Keys.X && ModifierKeys.HasFlag(Keys.Control)) SongView.Cutter();
            if (e.KeyCode == Keys.C && ModifierKeys.HasFlag(Keys.Control)) SongView.Coppier();
            if (e.KeyCode == Keys.Home) Panel_Options.FirstControlIndex = 0;
            if (e.KeyCode == Keys.End) Panel_Options.FirstControlIndex = Panel_Options.FirePlayList.Songs.Count;
            if (e.KeyCode == Keys.Multiply)
            {
                StopAllPlayingSongs();
            }
        }

        private void StopAllPlayingSongs()
        {
            //the_timer.Dispose();
            //the_timer = new System.Timers.Timer();
            //if (first != null)
            //{
            //    first.play.PlayingStopped -= PlayNext;
            //    first = null;
            //}
            //PlayAudio.StopPlayingAll();
        }

        private void UpdateSWView()
        {
            //throw new NotImplementedException();
        }

        private void UpdateSongViewAfterChange(object sender, EventArgs e)
        {
            UpdateSongViewAfterChangeAsync(sender, e);
            //if (panel1.Controls.Count == 0) imported_list.Songs.ForEach((X) => panel1.Controls.Add(X.SongView.Control));
            //else
            //{
            //    //panel1.
            //}

        }
        private async void UpdateSongViewAfterChangeAsync(object sender, EventArgs e)
        {

            if (panel1.Controls.Count == 0)
            {
                List<Control> clist = new List<Control>();
                foreach (var song in imported_list.Songs)
                {
                    await Task.Run(() => clist.Add(song.SongView.Control));
                }
                panel1.Controls.AddRange(clist.ToArray());
            }
                
            else
            {
                //panel1.
            }

        }


        private void UpdateSongViewAfterChange(SongChangeEventArgs e)
        {
            SongView.CreateSongViewFromList(imported_list);
            UpdateSongViewAfterChange(this, new EventArgs());
        }

        private void AddSongViewToPanel(FirePlayList _imported_list)
        {
            SongView.CreateSongViewFromList(_imported_list);
            UpdateSongViewAfterChange(this, new EventArgs());
        }

        private void brisanje_button_Click_1(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if (index >= 0)
            {
                List<string> temp = _files.ToList();
                temp.RemoveAt(index);
                _files = temp.ToArray();
                UpdateListBox2(this, new EventArgs());
                OnFilesChanged();
            }
        }

        private void Changed(object sender, EventArgs e)
        {

        }

        private void brisanje_button_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndices.Count > 0)
            {
                if (listBox2.SelectedItems.Count > 1)
                {
                    try
                    {
                        int index = listBox2.SelectedIndex;
                        int amm = listBox2.SelectedItems.Count;
                        for (int u = 0; u < amm; u++) listBox2.Items.RemoveAt(index);
                        listBox2.SelectedIndex = index > listBox2.Items.Count - 1 ? index - 1 : index;
                    }
                    catch { }
                }
                try
                {
                    int index = listBox2.SelectedIndex;
                    listBox2.Items.RemoveAt(index);
                    listBox2.SelectedIndex = index > listBox2.Items.Count - 1 ? index - 1 : index;
                }
                catch { }
            }
        }

        private void UpdateImportedList(Sweeper_Form sweeper_Form, EventArgs e)
        {
            if (imported_list.Songs.Count == 0) imported_list = FirePlayList.CrearteFromMany(_files);
        }

        private async void import_button_Click(object sender, EventArgs e)
        {
            _files = _main_window.files;
            if (_files != null)
            {
                if (imported_list.Songs.Count == 0) imported_list = await FirePlayList.CrearteFromManyAsync(_files);
                else
                {
                    DialogResult answer = MessageBox.Show("A list is already present. Do you want to owerwrite it?", "Warnong: List Present", MessageBoxButtons.YesNoCancel);
                    if ( answer == DialogResult.Yes) imported_list = FirePlayList.CrearteFromMany(_files);
                    else if (answer == DialogResult.No) imported_list.AddList(FirePlayList.CrearteFromMany(_files));
                }
                SongView.CurrentListImplemented = imported_list;
                Panel_Options.SetFirePlayListAsync(imported_list);
                imported_list.ListChanged += UpdateSongViewAfterChange;
                OnFilesChanged();
            }
        }

        private async void names_button_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (string x in listBox2.Items)
            {
                FirePlayList n = imported_list;
                listBox1.Items.Add("List: " + n.ListName);
                try
                {
                    foreach (FirePlaySong ppp in n.Songs)
                    {
                        listBox1.Items.Add("\t" + "Song: " + ppp.Autor + " - " + ppp.Naziv);
                        if (listBox3.SelectedItems.Count > 0)
                            listBox1.Items.Add("\t\t" + listBox3.SelectedItem.ToString() + ": " + ppp.SongElementByIndex(listBox3.SelectedIndex));
                    }
                }
                catch
                {
                    await Task.Run(() => MessageBox.Show("Something wen wrong\n ErrorCode: 1", "Error", MessageBoxButtons.OK));
                }
            }
        }

        private void insert_sw_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Number: " + System.Drawing.ColorTranslator.ToHtml(SongViewColor.Song));
        }

        private void ClearPanelControls()
        {
            for (int i = 1; i < panel1.Controls.Count; i++) panel1.Controls.RemoveAt(i);
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            SongView.RemoveSelected();
            AddSongViewToPanel(imported_list);
            AddSongViewToPanel(imported_list);
        }

        private void export_button_Click(object sender, EventArgs e)
        {
            ExportClass epc = new ExportClass(imported_list, _main_window.SavePath);
        }

        private void stopButton_button1_Click(object sender, EventArgs e)
        {
            StopAllPlayingSongs();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

#if VIEW_INACTIVE
        private void view_button_Click(object sender, EventArgs e)
        { 

            QuickSweeper.SweeperModeOn = !QuickSweeper.SweeperModeOn;
            if (QuickSweeper.SweeperModeOn) view_button.BackColor = Color.Red;
            else view_button.BackColor = WindowColors.Side2;
            
        }
#endif
        private void haoplatop(object s, MouseEventArgs e)
        {
            sweeper_listbox.Location = new Point(MousePosition.X -200, MousePosition.Y - 200);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        public void DragEnter_(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        public void DragDrop_(object sender, DragEventArgs e)
        {
            try
            {
                _files = null;
                _files = ((string[])e.Data.GetData(DataFormats.FileDrop));
                if (_files != null)
                {
                    if (imported_list.Songs.Count == 0) imported_list = FirePlayList.CrearteFromMany(_files);
                    else
                    {
                        bool truth = false;
                        int ind = -1;
                        foreach (SongView v in panel1.Controls)
                        {
                            if (v.ClientRectangle.Contains(v.PointToClient(Cursor.Position))) { truth = true; ind = v.Song.Index; }
                        }
                        if (truth && ModifierKeys.HasFlag(Keys.Shift))
                        {
                            if (ind >= 0) imported_list.Insert(ind, FirePlayList.CrearteFromMany(_files).Songs);
                        }
                        else
                        {
                            DialogResult answer = MessageBox.Show("A list is already present. Do you want to owerwrite it?", "Warnong: List Present", MessageBoxButtons.YesNoCancel);
                            if (answer == DialogResult.Yes) imported_list = FirePlayList.CrearteFromMany(_files);
                            else if (answer == DialogResult.No) imported_list.AddList(FirePlayList.CrearteFromMany(_files)); 
                        }
                    }
                    SongView.CurrentListImplemented = imported_list;
                    Panel_Options.FirePlayList = imported_list;
                    imported_list.ListChanged += UpdateSongViewAfterChange;
                    OnFilesChanged();
                }
            }
            catch
            {

            }
        }
        
        
        public static event EventHandler FilesChanged;
        protected virtual void OnFilesChanged()
        {
            if (FilesChanged != null) FilesChanged(this, new EventArgs());
        }
        protected virtual void OnFilesChanged(EventArgs e)
        {
            
            if (FilesChanged != null) FilesChanged(this, e);
        }

        FirePlaySong first;
        private void playButon_button1_Click(object sender, EventArgs e)
        {
            PlayingContinuously();
        }

        System.Timers.Timer the_timer = new System.Timers.Timer();

        private void PlayingContinuously()
        {
            //var temp = imported_list.Songs.Where(s => s.SongView.Checked == true).FirstOrDefault();
            //if (first == null)
            //{
            //    if (temp == null)
            //    {
            //        first = imported_list.Songs[0];
            //    }
            //    else
            //    {
            //        first = temp;
            //    }

            //}
            //else
            //{
            //    StopAllPlayingSongs();
            //    if (temp == null)
            //    {
            //        first = imported_list.Songs[0];
            //    }
            //    else
            //    {
            //        first = temp;
            //    }
            //}
            //SetInterval();
            //the_timer.Start();
            //first.play.Play();
            //the_timer.Elapsed += PlayNext;
        }

        private void SetInterval()
        {
            the_timer.AutoReset = true;
            the_timer.SynchronizingObject = panel1.Parent;
            var sec = Convert.ToDouble(first.Trajanje.Replace('.', ','));
            the_timer.Interval = (int)(sec * 1000);


        }

        private void PlayNext(object sender, EventArgs e)
        {
            //the_timer.Elapsed -= PlayNext;
            //the_timer.Stop();
            //first = imported_list.Songs.Where((x) => x.Index == first.Index + 1).ToList().FirstOrDefault();
            //SetInterval();
            //the_timer.Start();
            //first.play.Play();
            //the_timer.Elapsed += PlayNext;

        }
    }

}
