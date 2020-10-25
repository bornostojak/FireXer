using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Timers;
using Color = System.Drawing.Color;

namespace ExtraNoveListe
{
    public enum SweeperEveryHour { One = 0, Two, Number, Normal, None }

    public partial class SweeperSelectSettingsForm : Form
    {

        private string[] _files = null;
        private Dictionary<string, List<Folder>> Radios = new Dictionary<string, List<Folder>>();

        private List<CheckBox> CheckBoxes = new List<CheckBox>();

        public SweeperSelectSettingsForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.MaximumSize = new Size(600, 400);
            this.MinimumSize = new Size(600, 400);

            CheckBoxes.Add(checkBox1);
            CheckBoxes.Add(checkBox2);
            CheckBoxes.Add(checkBox3);
            CheckBoxes.Add(checkBox4);

            RadioLetterComboBoxSetup();

            
            add_radio_textbox.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    if (add_radio_textbox.Focused)
                    {
                        add_radio_button_Click(sender, args); 
                    }
                    else if (location_textbox.Focused)
                    {
                        add_location_button_Click(sender, args);
                    }
                }
            };
            //radio_letter_combo_box.SelectedIndexChanged += RadioLetterComboBoxIndexChange;

            Setup();
            WindowColors.ColorChangeEvent += () => WindowColors.Colorize(this);
            WindowColors.Colorize(this);
        }

        private void Setup()
        {
            radio_listbox.MultiColumn = true;
            radio_listbox.SelectionMode = SelectionMode.MultiExtended;


            radio_listbox.SelectedIndexChanged += RadioChanged;
            folder_listbox.SelectedIndexChanged += FolderChanged;

            SweeperSelectSettingsForm.Folder.FolderChange += (sender, args) =>
            {
                int lastselected = radio_listbox.SelectedIndex;
                radio_listbox.ClearSelected();
                radio_listbox.SelectedIndex = lastselected;
            };

            try
            {
                int[] up = { 0, 1, 2, 3, 4, 5, 6 };
                this.comboBox1.Items.Add(up[1]);
                comboBox1.Items.Add(1.ToString());
                comboBox1.Items.Add(2.ToString());
                comboBox1.Items.Add(3.ToString());
                comboBox1.Items.Add(4.ToString());
                comboBox1.Items.Add(5.ToString());
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
            catch 
            {
                
            }

            this.DragEnter += DragEnter_;
            this.DragDrop += DragDrop_;


        }

        private void FolderChanged(object sender, EventArgs e)
        {
            sweeper_listbox.Items.Clear();
            if (folder_listbox.SelectedIndices.Count > 0)
            {
                List<Folder> pp = Radios[radio_listbox.SelectedItems[0].ToString()]
                    .Where((x) => x.FolderName == folder_listbox.SelectedItem.ToString()).ToList();
                if (pp.Count > 0)
                {
                    pp[0]?.Files.ForEach((x) =>
                    {

                        sweeper_listbox.Items.Add(x.IsException ? ">>>>>>>" + x.FileName + "<<<<<<<" : x.FileName);
                    });
                }

                #region Checkboxes

                checkboxes_deselect();
                object selected = folder_listbox.SelectedItem;
                
                Folder f = Radios[radio_listbox.SelectedItems[0].ToString()].Where((y) => y.FolderName == folder_listbox.SelectedItem.ToString())?.FirstOrDefault();

                if ((int) f.SweeperEveryHour < 4)
                {
                    CheckBoxes[(int) f.SweeperEveryHour].Checked = true;
                    int z = 0;
                    if (int.TryParse(comboBox1.Text, out z)) f.SweeperEveryHourNumber = z;

                }
                #endregion
            }
            else
            {
                checkboxes_deselect();
            }
        }

        private void RadioChanged(object sender, EventArgs e)
        {
            folder_listbox.Items.Clear();
            if (radio_listbox.SelectedIndices.Count > 0)
            {
                Radios[radio_listbox.SelectedItems[0].ToString()]
                    .ForEach((x) => folder_listbox.Items.Add(x.FolderName));
                checkboxes_deselect();
            }
            else
            {
                checkboxes_deselect();
            }
            FolderChanged(sender, e);
        }

        public void Colorize()
        {
            
            Colorize(this);
        }

        private void Colorize(Control c)
        {
            this.BackColor = WindowColors.Base;

            if (c.Controls.Count > 0)
            {
                Control.ControlCollection controls = c.Controls;
                foreach (Control cont in controls)
                {
                    #region Settings

                    if (cont.GetType() == typeof(Button))
                    {
                        ((Button)cont).FlatStyle = FlatStyle.Flat;
                        ((Button)cont).FlatAppearance.BorderColor = WindowColors.BorderColor;
                        ((Button)cont).FlatAppearance.BorderSize = 1;
                        ((Button)cont).BackColor = WindowColors.Side2;
                    }

                    if (cont.GetType() == typeof(TextBox))
                    {
                        ((TextBox)cont).ForeColor = WindowColors.BorderColor;
                        ((TextBox)cont).BorderStyle = BorderStyle.FixedSingle;
                        ((TextBox)cont).BackColor = WindowColors.Side1;
                    }

                    if (cont.GetType() == typeof(ComboBox))
                    {
                        //((ComboBox)cont).ForeColor = WindowColors.BorderColor;
                        ((ComboBox) cont).FlatStyle = FlatStyle.Flat;
                        ((ComboBox)cont).BackColor = WindowColors.Side1;
                    }

                    if (cont.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)cont).ForeColor = WindowColors.BorderColor;
                        ((CheckBox)cont).FlatStyle = FlatStyle.Flat;
                        ((CheckBox)cont).BackColor = WindowColors.Base;
                        Colorize(cont);
                    }

                    if (cont.GetType() == typeof(ListBox))
                    {
                        ((ListBox)cont).BackColor = WindowColors.Side1;
                        ((ListBox)cont).ForeColor = WindowColors.BorderColor;
                        ((ListBox)cont).BorderStyle = BorderStyle.FixedSingle;
                    }

                    if (cont.GetType() == typeof(MenuStrip))
                    {
                        ((MenuStrip)cont).BackColor = WindowColors.Base;
                    }

                    if (cont.GetType() == typeof(Label))
                    {
                        ((Label)cont).ForeColor = Color.White;
                    }

                    if (cont.GetType() == typeof(Label))
                    {
                        ((Label)cont).ForeColor = Color.White;
                    }
                    
                    if (cont.GetType() == typeof(GroupBox))
                    {
                        Colorize(cont);
                    }
        

                    #endregion

                    #region Recursion

                    //if(cont.Controls.Count > 0) Colorize(cont);
                    
                    #endregion
                } 
            }
        }

        public void DragEnter_(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        public void DragDrop_(object sender, DragEventArgs e)
        {
            try
            {
                _files = ((string[])e.Data.GetData(DataFormats.FileDrop));
                

            }
            catch
            {

            }
        }

        /// <summary>
        /// Adds the name from the radio letter text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_radio_button_Click(object sender, EventArgs e)
        {
                
            if (!string.IsNullOrWhiteSpace(this.add_radio_textbox.Text))// && RegExRemoveSpacesBeginingAndEnd(add_radio_textbox.Text) != "")
            {
                radio_listbox.Items.Add(RegExRemoveSpacesBeginingAndEnd(add_radio_textbox.Text));
                Radios.Add(add_radio_textbox.Text, new List<Folder>());
                //radio_listbox.Items.Add(add_radio_textbox.Text);
                add_radio_textbox.Text = string.Empty;
            }
        }

        private void add_location_dialog_button_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            DialogResult dr = folderDialog.ShowDialog();
            if (dr == DialogResult.OK) { location_textbox.Text = folderDialog.SelectedPath ;}


            /*SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "FirePlay Color File|*.fxcol|All Files|*.*";
            saveFileDialog1.Title = "Load FirePlay Color File";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = ColorSettingsFolderPath;
            saveFileDialog1.ShowDialog();
            return saveFileDialog1;*/
        }

        private void add_location_button_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(location_textbox.Text))
            {
                if (radio_listbox.SelectedIndices.Count == 1)
                {
                    string folder = location_textbox.Text;
                    string radioname = radio_listbox.Items[radio_listbox.SelectedIndex].ToString();


                    Radios[radioname].Add(new Folder(radioname, folder));
                    location_textbox.Text = string.Empty;
                    RadioChanged(new object(), new EventArgs());
                }
                else
                {
                    MessageBox.Show("Please make sure you only have one radio selected", "Select 1 radio only", MessageBoxButtons.OK);
                }
            }
        }

        private void remove_location_button_Click(object sender, EventArgs e)
        {
            if (folder_listbox.SelectedIndices.Count > 0)
            {
                try
                {
                    Radios[(string)radio_listbox.SelectedItems[0]].Where((x) => folder_listbox.SelectedItems.Contains(x.FolderName)).ToList().ForEach((y) => Radios[(string)radio_listbox.SelectedItems[0]].Remove(y));
                }
                catch 
                {
                   
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Removes selected indicies from the radio_listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_radio_button_Click(object sender, EventArgs e)
        {
            if (radio_listbox.Items.Count > 0 && radio_listbox.SelectedIndices.Count > 0)
            {
                if (radio_listbox.SelectedIndices.Count == 1)
                {
                    string name = (string)radio_listbox.Items[radio_listbox.SelectedIndex];
                    Radios.Remove(name);
                    radio_listbox.Items.RemoveAt(radio_listbox.SelectedIndex);
                }
                else
                {
                    for (int i = radio_listbox.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        string name = (string) radio_listbox.SelectedItems[i];
                        Radios.Remove(name);
                        radio_listbox.Items.Remove(name);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool state = ((CheckBox) sender).Checked;
            checkboxes_deselect();
            ((CheckBox) sender).Checked = state;
            GetSelectedFolder().SweeperEveryHour = SweeperEveryHour.One;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool state = ((CheckBox)sender).Checked;
            checkboxes_deselect();
            ((CheckBox)sender).Checked = state;
            GetSelectedFolder().SweeperEveryHour = SweeperEveryHour.Two;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool state = ((CheckBox)sender).Checked;
            checkboxes_deselect();
            ((CheckBox)sender).Checked = state;
            GetSelectedFolder().SweeperEveryHour = SweeperEveryHour.Number;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            bool state = ((CheckBox)sender).Checked;
            checkboxes_deselect();
            ((CheckBox)sender).Checked = state;
            GetSelectedFolder().SweeperEveryHour = SweeperEveryHour.Normal;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Initialize()
        {
            this.Show();
        }

        private void checkboxes_deselect()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }

        private Folder GetSelectedFolder()
        {
            return Radios[radio_listbox.SelectedItems[0].ToString()].Where((y) => y.FolderName == folder_listbox.SelectedItem.ToString())?.FirstOrDefault();
        }

        public void LoadCurrentData()
        {
            Radios = new Dictionary<string, List<Folder>>();

            RadioSWSettings.AllRadioSWSettings.ForEach((x) =>
            {
                if(!Radios.ContainsKey(x.RadioName)) Radios.Add(x.RadioName, Folder.CreateFromRadioSWSwttingsList(x.RadioFolders));
            });
            foreach (KeyValuePair<string, List<Folder>> r in Radios)
            {
                radio_listbox.Items.Add(r.Key);
            }
            RadioChanged(new object(), new EventArgs());
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////




        private void RadioLetterComboBoxIndexChange(object sender, EventArgs e)
        {
            
        }

        private void RadioLetterComboBoxSetup()
        {
            radio_letter_combo_box.DropDownStyle = ComboBoxStyle.DropDownList;

            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            this.radio_letter_combo_box.Items.AddRange(drives.Select((x) => x.Name).ToArray());
        }

        private string RegExRemoveSpacesBeginingAndEnd(string s)
        {
            Match m = Regex.Match(Regex.Match(s, @"( *)(.*)").Groups[2].Value, @"(.*)([^ ])( *)$");

            return m.Groups[1].Value + m.Groups[2].Value;
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            RadioSWSettings.ClearAll();
            UpdateSweeperSettings();
            OnSweeperSettingsUpdate();
        }

        private void UpdateSweeperSettings()
        {
            RadioSWSettings.CreateFromSweeperSelectSettings(Radios);
            SweeperSettings.CreateXMLSettingsFile(SweeperSettings.DefaultSweeperSettingsDirectoryPath+@"\SweeperSettings.sws.xml");
        }

        public event EventHandler SweeperSettingsUpdate;
        private void OnSweeperSettingsUpdate()
        {
            SweeperSettingsUpdate?.Invoke(this, new EventArgs());
        }

        public class Folder
        {
            public SweeperEveryHour SweeperEveryHour { get; set; } = SweeperEveryHour.Normal;
            public int SweeperEveryHourNumber { get; set; } = 1;

            public string Radio { get; set; }
            public string FolderName { get { return FolderPath != null ? System.IO.Path.GetFileName(FolderPath) : null; } }
            public string FolderPath { get; set; } = null;
            public List<File> Files = new List<File>();

            public Folder(string name)
            {
                Radio = name;
            }

            public Folder(string name, string folderpath)
            {
                Radio = name;
                FolderPath = folderpath;
                CheckForFiles();
            }

            private void CheckForFiles()
            {
                List<string> files = new List<string>();
                files.AddRange(System.IO.Directory.GetFiles(FolderPath));
                if (files.Count > 0)
                {
                    files.ForEach((x) =>
                    {
                        if (IsAudioFile(x))
                        {
                            this.Files.Add(new File(x)); 
                        }
                    }); 
                    OnFolderChange();
                }
            }

            public void Add(string path)
            {
                Files.Add(new File(path));
                OnFolderChange();
            }

            public void AddRange(string[] paths)
            {
                foreach (string path in paths)
                {
                    Files.Add(new File(path)); 
                }
                OnFolderChange();
            }

            public void AddRange(List<string> paths)
            {
                foreach (string path in paths)
                {
                    Files.Add(new File(path)); 
                }
                OnFolderChange();
            }

            public void Remove(string path)
            {
                Files.Where((x) => x.FilePath == path).ToList().ForEach((f) => Files.Remove(f));
                OnFolderChange();
            }

            public void RemoveRange(string[] path)
            {
                Files.Where((x) => path.Contains(x.FilePath)).ToList().ForEach((f) => Files.Remove(f));
                OnFolderChange();
            }

            public void RemoveRange(List<string> path)
            {
                Files.Where((x) => path.Contains(x.FilePath)).ToList().ForEach((f) => Files.Remove(f));
                OnFolderChange();
            }

            public static event EventHandler FolderChange;
            private static void OnFolderChange()
            {
                FolderChange?.Invoke(new object(), new EventArgs());
            }

            private bool IsAudioFile(string x)
            {
                return x.EndsWith(".mp3") || x.EndsWith(".wav") ? true : false;
            }

            public static List<Folder> CreateFromRadioSWSwttingsList(List<RadioSWSettingsFolders> rswsfl)
            {
                List<Folder> list = new List<Folder>();
                rswsfl.ForEach((x) =>
                {
                    Folder f = new Folder(x.RadioName);
                    f.FolderPath = x.Folder;

                    x.Files.ForEach((y) =>
                    {
                        f.Files.Add(new File(y));
                    });

                    list.Add(f);
                });

                return list;
            }
        }

        public class File
        {
            public bool IsException { get; set; } = false;

            public string FilePath { get; set; } = null;
            public string FileName { get
                {
                    return FilePath != null ? System.IO.Path.GetFileNameWithoutExtension(FilePath) : null;
                } }
            public FirePlaySong Song { get; set; }

            public File(string path)
            {
                FilePath = path;
            }

            public void ChangeExceptionState()
            {
                IsException = !IsException;
            }


            public event EventHandler FilesChange;
            private void OnFilesChange()
            {
                FilesChange?.Invoke(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sweeper_listbox.SelectedIndices.Count > 0)
            {
                List<string> objs = new List<string>();

                foreach (var item in sweeper_listbox.SelectedItems)
                {
                    objs.Add(item.ToString());
                }
                if (radio_listbox.SelectedIndices.Count == 1 && folder_listbox.SelectedIndices.Count == 1)
                {
                    objs.ForEach((item) => Radios[radio_listbox.SelectedItem.ToString()].Where((x) => x.FolderName == folder_listbox.SelectedItem.ToString()).ToList()[0]?.Files.Where((f) => Regex.Match(item, @"(>{3})?([^<]*)(<{3})?").Groups[2].Value.Contains(f.FileName))?.FirstOrDefault()?.ChangeExceptionState());
                    FolderChanged(this, new EventArgs());
                }
            }
        }

        private string RemoveExceptioBrackets(string s)
        {
            return Regex.Match(s, @"(>{7})?([^<]*)(<{7})?").Groups[2].Value;
        }
    }

    
}
