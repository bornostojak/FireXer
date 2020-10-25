using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    public partial class QuickSweeper : System.Windows.Forms.Form
    {
        public static Sweeper_Form SWF { get; set; }
        private string folderPath { get; set; }

        public Dictionary<string, string> DictListOfFiles  = new Dictionary<string, string>();

        private static bool _swModeOn = false;
        public static bool SweeperModeOn
        {
            get { return _swModeOn; }
            set
            {
                _swModeOn = value;
                CallChange();
            }
        }

        public static void Updater()
        {
            Current.box.Items.Clear();
            Current.DictListOfFiles = new Dictionary<string, string>();
            if (SweeperModeOn)
            {
                if (SWF.radio_listbox.SelectedIndices.Count > 0 && SWF.sweeper_listbox.SelectedIndices.Count > 0)
                {
                    RadioSWSettings.AllRadioSWSettings.Where((y) => y.RadioName == SWF.radio_listbox.SelectedItem.ToString())?.FirstOrDefault().RadioFolders.Where((w) => w.FolderName == SWF.sweeper_listbox.SelectedItem.ToString())?.FirstOrDefault().Files.ForEach(
                        (p) =>
                        {
                            Current.DictListOfFiles.Add(System.IO.Path.GetFileNameWithoutExtension(p), p);         
                        });
                    foreach (KeyValuePair<string, string> ss in Current.DictListOfFiles)
                    {
                        Current.box.Items.Add(ss.Key);
                    }
                }
            }
        }

        private static void CallChange()
        {
            if (SweeperModeOn == true)
            {
                try
                {
                    //Current.box.Items.AddRange(new string[] {"a,", "b", "c"});
                    Current.Show();
                    Updater();
                }
                catch (ObjectDisposedException)
                {
                    Current = new QuickSweeper();
                    Current.Show();
                }
                Current.Location = new System.Drawing.Point(MousePosition.X +20, MousePosition.Y + 20);
            }
                else Current.Hide(); 

        }

        public static void OnMouseMovement(object s, MouseEventArgs e)
        {
            Current.Location = new Point(MousePosition.X +40, MousePosition.Y -150);
        }

        public ListBox box;

        public QuickSweeper()
        {
            box = new ListBox();
            box.Location = Cursor.Position;
            box.Size = new System.Drawing.Size(180, 22*6);
            this.InitializeComponent();
            Setup();

            this.FormBorderStyle = FormBorderStyle.None; 
            this.Size = box.Size;
            this.Controls.Add(box);
            this.BringToFront();
            this.AllowTransparency = true;

            //this.MouseMove += WithMouseMove;
            //this.MouseWheel += QuickSweeper_MouseWheel;
            ColorSetup();
            //Subscribe();
        }

        public void QuickSweeper_MouseWheel(object sender, MouseEventArgs e)
        {
            /*if (box.SelectedIndices.Count == 0)
            {
                box.SelectedIndex = 0;
                return;
            }
            if (ModifierKeys.HasFlag(Keys.Alt))
            {

                if (e.Delta < 0)
                {
                    int i = box.SelectedIndex;
                    if (++i >= box.Controls.Count) box.SetSelected(0, true);
                    else box.SetSelected(i, true);
                }
                else if (e.Delta > 0) box.SelectedIndex = box.SelectedIndex < 0 ? box.Controls.Count - 1 : box.SelectedIndex - 1; 
            }*/
        }

        private void WithMouseMove(object sender, MouseEventArgs e)
        {
            if(!this.Focused && this.Visible) this.BringToFront();
            if(!(ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Shift) && ModifierKeys.HasFlag(Keys.Alt)))this.Location = new Point(e.Location.X + 10, e.Location.Y + 10);
        }


        public void Setup()
        {
            box.FormattingEnabled = true;
            box.Location = new System.Drawing.Point(0,0);
            box.Name = "momom";
            box.Size = new System.Drawing.Size(296, 199);
            box.TabIndex = 6;
            box.BringToFront();
        }

        private static QuickSweeper _curr = new QuickSweeper();

        public static QuickSweeper Current
        {
            get
            {
                //_curr = new QuickSweeper();
                return _curr;
            }
            private set { _curr = value; }
        }

        public void MouseTrack(object s, MouseEventArgs e)
        {
            
        }
    }
}
