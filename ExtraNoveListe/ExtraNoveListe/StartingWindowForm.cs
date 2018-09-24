using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace ExtraNoveListe
{
    public partial class StartingWindowForm : Form
    {
        static StartingWindowForm this_window;
        private FirePlayList lists_in_form;

        private bool _start_button_control_down = false;
        public bool StartButtonControlDown { get { return _start_button_control_down; }set { _start_button_control_down = value; } }
        private List<FirePlayList> _lists_in_program = new List<FirePlayList>();
        
        public List<FirePlayList> exported_list { get; set; }

        public string SavePath { get { return this.save_textBox.Text; } }

        private string[] _files = null;
        public string[] files
        {
            get
            {
                return _files;
            }
        }
        

        ////////////////////////////////////////////////////////////////////////////////////

        public static SweeperSettings Sweeper_Settings = null;
        
        ////////////////////////////////////////////////////////////////////////////////////
        

        public StartingWindowForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter && file_list_box.Items.Count > 0 && save_textBox.Text != "") start_button_Click(sender, args);
            };


            SweeperSettings.main_window = this;
            this.AllowDrop = true;                                                  //Allows for drag-drop functionality
            this.DragEnter += new DragEventHandler(Form1_DragEnter);                //Drag-drop
            this.DragDrop += new DragEventHandler(Form1_DragDrop);                  //Drag-drop function assigned
            Setup();
            this_window = this;

            StartingWindowForm.Sweeper_Settings = new SweeperSettings();
            Sweeper_Settings.SetupOnStartup();                                               //Settings for the sweepers
            if (ExtraNoveListe.Program.args != null)
            {
                string[] new_files = ExtraNoveListe.Program.args;
                //foreach (string h in new_files) _lists_in_program.Add(new FirePlayList(h));
                file_list_box.Items.AddRange(new_files);
                if (_files == null) _files = new_files;
                else
                {
                    List<string> temp = _files.ToList();
                    temp.AddRange(new_files);
                    _files = temp.ToArray();
                }
            }
            AddToRightClickFunction();
            ColorSetup();
            Stylize();

            WindowColors.GetBackLog();
            UpdateCheckedItemsInLoadToolStripMenu();
            WindowColors.CheckCheckedColorSchemesInToolStrip += UpdateCheckedItemsInLoadToolStripMenu;
            
        }

        public void UpdateSweeperSettings()
        {
            //Sweeper_Settings
        }

        public void UpdateCheckedItemsInLoadToolStripMenu()
        {
            if (loadToolStripMenuItem.DropDownItems.Count > 0)
            {
                foreach (ToolStripMenuItem t in loadToolStripMenuItem.DropDownItems)
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(WindowColors.LastColor) == t.Name)
                        t.Checked = true;
                    else t.Checked = false;
                }
            }
        }

        private void Stylize()
        {
            if (!System.IO.Directory.Exists(WindowColors.ColorSettingsFolderPath))
            {
                System.IO.Directory.CreateDirectory(WindowColors.ColorSettingsFolderPath);
                foreach (KeyValuePair<string, WindowColors.DefaultColors.DefaultColor> color in WindowColors.DefaultColors.AllDefaultColors)
                {
                    string path = WindowColors.ColorSettingsFolderPath + $"\\{color.Key}.fxcol";
                    WindowColors.SetColorsToXml(path, color.Value);
                }
            }

            Color BorderColor = Color.Black;

            this.save_textBox.ForeColor = Color.Black;

            this.file_list_box.BorderStyle = BorderStyle.FixedSingle;
            
            this.browse_button.FlatStyle = FlatStyle.Flat;
            this.browse_button.FlatAppearance.BorderColor = BorderColor;
            this.browse_button.FlatAppearance.BorderSize = 1;
            
            this.choose_button.FlatStyle = FlatStyle.Flat;
            this.choose_button.FlatAppearance.BorderColor = BorderColor;
            this.choose_button.FlatAppearance.BorderSize = 1;
            
            this.clear_button.FlatStyle = FlatStyle.Flat;
            this.clear_button.FlatAppearance.BorderColor = BorderColor;
            this.clear_button.FlatAppearance.BorderSize = 1;
            
            this.close_button.FlatStyle = FlatStyle.Flat;
            this.close_button.FlatAppearance.BorderColor = BorderColor;
            this.close_button.FlatAppearance.BorderSize = 1;
            
            this.down_button.FlatStyle = FlatStyle.Flat;
            this.down_button.FlatAppearance.BorderColor = BorderColor;
            this.down_button.FlatAppearance.BorderSize = 1;
            
            this.duplicate_button.FlatStyle = FlatStyle.Flat;
            this.duplicate_button.FlatAppearance.BorderColor = BorderColor;
            this.duplicate_button.FlatAppearance.BorderSize = 1;
            
            this.start_button.FlatStyle = FlatStyle.Flat;
            this.start_button.FlatAppearance.BorderColor = BorderColor;
            this.start_button.FlatAppearance.BorderSize = 1;
            
            this.up_button.FlatStyle = FlatStyle.Flat;
            this.up_button.FlatAppearance.BorderColor = BorderColor;
            this.up_button.FlatAppearance.BorderSize = 1;

            this.save_textBox.ForeColor = BorderColor;
            this.save_textBox.BorderStyle = BorderStyle.FixedSingle;


            UpdateLoadToolStripMenuItems();
        }

        public void UpdateLoadToolStripMenuItems()
        {
            #region Set-up for the color load and save 

            if(this.loadToolStripMenuItem.HasDropDownItems) for(int i = this.loadToolStripMenuItem.DropDownItems.Count; i > 0 ; i--) this.loadToolStripMenuItem.DropDownItems.RemoveAt(0); 
            
            Dictionary<string, string> paths = new Dictionary<string, string>();
            List<string> names = new List<string>();
            List<string> locations = new List<string>();

            if (System.IO.Directory.Exists(WindowColors.ColorSettingsFolderPath))
            {
                foreach (String file in System.IO.Directory.GetFiles(WindowColors.ColorSettingsFolderPath))
                {
                    if (!file.Contains("ColorScheme.fxcol") && Path.GetExtension(file) == ".fxcol")
                    {
                        paths.Add(System.IO.Path.GetFileNameWithoutExtension(file), file);
                        names.Add(System.IO.Path.GetFileName(file));
                        locations.Add(file);
                    }
                }
                paths.OrderBy((x) => x.Key);
                foreach (KeyValuePair<string, string> path in paths)
                {
                    ToolStripMenuItem tool = new ToolStripMenuItem();//new ToolStripButton();
                    tool.Name = path.Key;
                    tool.Size = new System.Drawing.Size(152, 22);
                    tool.Text = path.Key;
                    if (path.Value == WindowColors.LastColor && !WindowColors.LastColor.Contains("ColorScheme.fxcol")) ((ToolStripMenuItem) tool).Checked = true;
                    tool.Click += (a, b) => WindowColors.SetColorsFromXml(WindowColors.ColorSettingsFolderPath+"\\"+tool.Name+".fxcol");
                    this.loadToolStripMenuItem.DropDownItems.Add(tool);
                }
            }


            #endregion
        }

        private void ColorSetup()
        {


            this.BackColor = WindowColors.Base;

            this.file_list_box.BackColor = WindowColors.Side1;
            this.browse_button.BackColor = WindowColors.Side2;
            this.choose_button.BackColor = WindowColors.Side2;
            this.clear_button.BackColor = WindowColors.Side2;
            this.close_button.BackColor = WindowColors.Side2;
            this.down_button.BackColor = WindowColors.Side2;
            this.duplicate_button.BackColor = WindowColors.Side2;
            this.start_button.BackColor = WindowColors.Side2;
            this.up_button.BackColor = WindowColors.Side2;



            this.save_textBox.BackColor = WindowColors.Side1;
            this.save_textBox.ForeColor = Color.Black;
            this.save_textBox.BorderStyle = BorderStyle.FixedSingle;

            this.MainMenuStrip.BackColor = WindowColors.Base;

            this.label1.ForeColor = Color.White;
            this.label2.ForeColor = Color.White;

            this.checkBox_fps.ForeColor = Color.White;
            this.checkBox_xml.ForeColor = Color.White;
            this.clear_checkBox.ForeColor = Color.White;

            this.MainMenuStrip.ForeColor = Color.White;

            
        }

        void AddToRightClickFunction()
        {
            try
            {
                RegistryKey key;
                key = Registry.ClassesRoot.CreateSubKey(@"SystemFileAssociations\.xml\Shell\FireXer");
                key = Registry.ClassesRoot.CreateSubKey(@"SystemFileAssociations\.xml\Shell\FireXer\command");
                key.SetValue("", (string)Application.ExecutablePath + " \"%1\"");
            }
            catch
            {
            }
        }
        void Setup()
        {
            this.MinimumSize = new Size(430, 306);                                  //Sets window minimum size

            ///////////////////////////////-----ANCHORING-----///////////////////////////////
            start_button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            close_button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            choose_button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            label1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            label2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            file_list_box.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            save_textBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left);
            label2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            browse_button.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom);
            clear_button.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom);
            clear_checkBox.Anchor = (AnchorStyles.Right | AnchorStyles.Bottom);
            up_button.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            down_button.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            duplicate_button.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            checkBox_fps.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            checkBox_xml.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            ///////////////////////////////-----ANCHORING-----///////////////////////////////
            
            this.checkBox_fps.CheckedChanged += this.CheckCheckedState;             //Checkbox state change
            this.checkBox_xml.CheckedChanged += this.CheckCheckedState;             //Checkbox state change
            this.save_textBox.LostFocus += this.TextFieldChanged;                   //Checks if the textbox has changed






            WindowColors.ColorChangeEvent += ColorSetup;
        }
        
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {   
                string[] temp_arr = (string[])e.Data.GetData(DataFormats.FileDrop);
                save_textBox.Text = temp_arr[0];
                return;
            }
            string[] new_files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //foreach (string h in new_files) _lists_in_program.Add(new FirePlayList(h));
            file_list_box.Items.AddRange(new_files);
            if (_files == null) _files = new_files;
            else
            {
                List<string> temp = _files.ToList();
                temp.AddRange(new_files);
                _files = temp.ToArray();
            }

        }

        public async void start_button_Click(object sender, EventArgs e)
        {
            _lists_in_program = new List<FirePlayList>();
            foreach (string name in file_list_box.Items) _lists_in_program.Add(new FirePlayList(name));

            if (ModifierKeys.HasFlag(Keys.Control)) _start_button_control_down = true;
            if(save_textBox.Text == string.Empty)
            {
                MessageBox.Show("No save file selected! ", "Select Save File", MessageBoxButtons.OK);
                return;
            }
            if (File.Exists(save_textBox.Text)) File.Delete(save_textBox.Text);
            if (!save_textBox.Text.EndsWith(".fps.xml") && !save_textBox.Text.EndsWith(".xml")) save_textBox.Text = save_textBox.Text + ".fps.xml";

            if (_files == null)
            {
                MessageBox.Show("Missing Playlists", "You have no sellected playlists", MessageBoxButtons.OK);
                return;
            }
            foreach (string file in _files)
            {
                if (!System.IO.File.Exists(file))
                {
                    MessageBox.Show("Some playlist are missing", "There are some playlists missing. Please check the playlists", MessageBoxButtons.OK);
                }
            }
            SavePlaylistToFileAsPlainTextXML(this, _lists_in_program);
            start_button.Text = "Done";
            await Task.Run(() => MessageBox.Show("Done ", "Done", MessageBoxButtons.OK));
            start_button.Text = "Start";
            this._start_button_control_down = false;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(lists_in_form.GetSongCount.ToString(), "RRR", MessageBoxButtons.OK);
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = SaveTheFile();
            save_textBox.Text = SFD.FileName;
        }
        
        string openingFile()
        {
            string file = null;
            try
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                FD.Filter = "FirePlay File|*.fps.xml|FirePlay File|*.fps" ;
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    file = FD.FileName;
                }
            }
            catch 
            {

            }
            return file;
        }
        void choose_button_Click(object sender, EventArgs e)
        {
            string fileToOpen = null;
            List<string> temp_1 = null;
            try
            {
                fileToOpen = openingFile();
                if (fileToOpen == null) return;
                file_list_box.Items.Add(fileToOpen);
                if (_files == null)
                {
                    _files = new[] { fileToOpen };
                    //_lists_in_program.Add(new FirePlayList(fileToOpen));
                }
                else
                {
                    temp_1 = _files.ToList<string>();
                    temp_1.Add(fileToOpen);
                    _files = temp_1.ToArray();
                    //_lists_in_program.Add(new FirePlayList(fileToOpen));
                }
            }
            catch 
            {
                MessageBox.Show("Fuck", "yes, itsa a bug", MessageBoxButtons.OK);
            }

        }

        static void SavePlaylistToFile(string path, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
            if (this_window.StartButtonControlDown) l.DefaultTheList();
            foreach (FirePlaySong s in l.Songs)
                {
                    try
                    {
                        XmlNode item = doc.CreateElement("PlayItem");
                        for (int i = 0; i < FirePlaySong.Elements.Length; i++)
                        {
                            XmlNode c = doc.CreateElement(FirePlaySong.Elements[i]);
                            c.InnerText = s.SongElementByIndex(i);
                            item.AppendChild(c);
                        }
                        root.AppendChild(item);
                    }
                    catch (Exception e)
                    {
                       MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate), Encoding.Default))
                doc.Save(tw);
            
        }

        static void SavePlaylistToFile(StartingWindowForm t, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
            if (t.StartButtonControlDown) l.DefaultTheList();
            foreach (FirePlaySong s in l.Songs)
                {
                    try
                    {
                        XmlNode item = doc.CreateElement("PlayItem");
                        for (int i = 0; i < FirePlaySong.Elements.Length; i++)
                        {
                            XmlNode c = doc.CreateElement(FirePlaySong.Elements[i]);
                            c.InnerText = s.SongElementByIndex(i);
                            item.AppendChild(c);
                        }
                        root.AppendChild(item);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                        t.Close();
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(t.save_textBox.Text, FileMode.OpenOrCreate), Encoding.Default))
                doc.Save(tw);

        }

        public static void SavePlaylistToFileAsPlainTextXML(StartingWindowForm t, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
            l.ListPath = t.save_textBox.Text;
            this_window.lists_in_form = l;
            if (t.StartButtonControlDown) { l.DefaultTheList(); };
                foreach (FirePlaySong s in l.Songs)
                {
                    try
                    {
                        XmlNode item = doc.CreateElement("PlayItem");
                        for (int i = 0; i < FirePlaySong.Elements.Length; i++)
                        {
                            XmlNode c = doc.CreateElement(FirePlaySong.Elements[i]);
                            c.InnerText = s.SongElementByIndex(i);
                            item.AppendChild(c);
                        }
                        root.AppendChild(item);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                        t.Close();
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(t.save_textBox.Text, FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        public static void SavePlaylistToFileAsPlainTextXML(string path, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
            this_window.lists_in_form = l;
            if (this_window.StartButtonControlDown) l.DefaultTheList();
            foreach (FirePlaySong s in l.Songs)
                {
                    try
                    {
                        XmlNode item = doc.CreateElement("PlayItem");
                        for (int i = 0; i < FirePlaySong.Elements.Length; i++)
                        {
                            XmlNode c = doc.CreateElement(FirePlaySong.Elements[i]);
                            c.InnerText = s.SongElementByIndex(i);
                            item.AppendChild(c);
                        }
                        root.AppendChild(item);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        public static void SavePlaylistToFileAsPlainTextXML(string path, FirePlayList List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = List.Clone();
            this_window.lists_in_form = l;
            if (this_window.StartButtonControlDown) l.DefaultTheList();
            foreach (FirePlaySong s in l.Songs)
            {
                try
                {
                    XmlNode item = doc.CreateElement("PlayItem");
                    for (int i = 0; i < FirePlaySong.Elements.Length; i++)
                    {
                        XmlNode c = doc.CreateElement(FirePlaySong.Elements[i]);
                        c.InnerText = s.SongElementByIndex(i);
                        item.AppendChild(c);
                    }
                    root.AppendChild(item);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                }
            }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        static string ReturnXMLAsString(XmlDocument doc)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (var strw = new StringWriter()) using (var xtw = XmlWriter.Create(strw, settings))
            {
                doc.WriteTo(xtw);
                xtw.Flush();
                return strw.GetStringBuilder().ToString();
            }
        }

        static string ReturnXMLAsString(XmlNode node)
        {
            using (var strw = new StringWriter()) using (var xtw = XmlWriter.Create(strw))
            {
                node.WriteTo(xtw);
                xtw.Flush();
                return strw.GetStringBuilder().ToString();
            }
        }

        SaveFileDialog SaveTheFile()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "FirePlay File|*.fps.xml|FirePlay file|*.fps";
            saveFileDialog1.Title = "Save FirePlay File";
            saveFileDialog1.ShowDialog();
            return saveFileDialog1;
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            if (_files == null) return;
            if (clear_checkBox.Checked)
            {
                file_list_box.Items.Clear();
                _files = null;
                //_lists_in_program = new List<FirePlayList>();
                clear_checkBox.Checked = false;
                clear_checkBox.Update();
            }
            else
            {
                int c = file_list_box.SelectedIndex;
                List<string> temp = _files.ToList();
                temp.RemoveAt(c);
                _files = temp.ToArray();
                UpdateListBox();
                if (_files == null) { }
                else if (c > _files.Count() - 1 && _files != null) file_list_box.SelectedIndex = _files.Count() - 1;
                else file_list_box.SelectedIndex = c;  
            }
            
        }
        
        /*private void gg()
        {
            file_list_box.Items.Add(this.Width);
            file_list_box.Items.Add(this.Height);
        }*/

        private async void up_button_Click(object sender, EventArgs e)
        {
            if (file_list_box.SelectedItem != null && file_list_box.SelectedIndex != 0)
            {
                //FirePlayList temp_list = _lists_in_program.GetRange(file_list_box.SelectedIndex, 1).ToArray()[0];
                //_lists_in_program.RemoveAt(file_list_box.SelectedIndex);
                //_lists_in_program.Insert(file_list_box.SelectedIndex - 1, temp_list);


                int index = file_list_box.SelectedIndex;
                List<string> temp = _files.ToList();
                List<string> t = temp.GetRange(index, 1);
                temp.RemoveAt(index);
                temp.InsertRange(index - 1, t);
                _files = temp.ToArray();
                UpdateListBox();
                file_list_box.SelectedIndex = (index-1)< 0 ? 0 : index-1;
            }


        }

        private void down_button_Click(object sender, EventArgs e)
        {
            if (_files.Length > 0 && file_list_box.SelectedIndex >= 0)
            {
                int index = file_list_box.SelectedIndex;
                List<string> temp = _files.ToList();
                List<string> t = temp.GetRange(index, 1);
                temp.RemoveAt(index);
                temp.InsertRange(index + 1, t);
                _files = temp.ToArray();
                UpdateListBox();
                file_list_box.SelectedIndex = (index + 1) < 0 ? 0 : index + 1; 
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private async void eNTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await EnterOpenAsync();
        }
        async Task EnterOpenAsync()
        {
            if (await Task.Run(() => System.IO.Directory.Exists(@"\\TOWER\Lists\Enter\Enter"))) await Task.Run(() => System.Diagnostics.Process.Start(@"\\TOWER\Lists\Enter\Enter"));
            else await Task.Run(() => MessageBox.Show("The Path does not exist", "Wrong Path", MessageBoxButtons.OK));
            return;
        }
        private async void eXTRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ExtraOpenAsync();
        }

        async Task ExtraOpenAsync()
        {
            if (await Task.Run(() => System.IO.Directory.Exists(@"\\TOWER\Lists\Extra\Glazba"))) await Task.Run(() =>System.Diagnostics.Process.Start(@"\\TOWER\Lists\Extra\Glazba"));
            else if (await Task.Run(() => System.IO.Directory.Exists(@"\\TOWER\Lists\Extra"))) await Task.Run(() => System.Diagnostics.Process.Start(@"\\TOWER\Lists\Extra"));
            else await Task.Run(() => MessageBox.Show("The Path does not exist", "Wrong Path", MessageBoxButtons.OK));
            return;
        }

        void UpdateListBox()
        {
            file_list_box.Items.Clear();
            file_list_box.Items.AddRange(_files);
        }

        private void duplicate_button_Click(object sender, EventArgs e)
        {
            List<string> temp = _files.ToList();
            temp.AddRange(temp.GetRange(file_list_box.SelectedIndex, 1));
            _files = temp.ToArray();
            UpdateListBox();
            file_list_box.SelectedIndex = temp.Count() - 1;
        }

        private void extraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            save_textBox.Text = @"\\TOWER\Lists\Enter\ENTER gotoveliste";
        }

        private void enterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            save_textBox.Text = @"\\TOWER\Lists\Extra\Gotove";}

        private void sweeperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void sweeperiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form y = new Sweeper_Form(0, this);
        }

        private void sweeperiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form y = new Sweeper_Form(1, this);
        }

        private void CheckCheckedState(object sender, EventArgs e)
        {
            if (save_textBox.Text.EndsWith(".xml")) save_textBox.Text = save_textBox.Text.Remove(save_textBox.Text.LastIndexOf("."), 4);
            if (save_textBox.Text.EndsWith(".fps")) save_textBox.Text = save_textBox.Text.Remove(save_textBox.Text.LastIndexOf("."), 4);

            if (sender.Equals(checkBox_fps))
            {
                if (checkBox_xml.Checked && checkBox_fps.Checked) checkBox_xml.Checked = false;
                if (!checkBox_xml.Checked) save_textBox.Text = save_textBox.Text + ".fps";
            }
            else if (sender.Equals(checkBox_xml))
            {
                if (checkBox_xml.Checked && checkBox_fps.Checked) checkBox_fps.Checked = false;
                if (!checkBox_fps.Checked) save_textBox.Text = save_textBox.Text +".fps"+ ".xml";
            }
            
        }

        private void TextFieldChanged(object sender, EventArgs e)
        {
            if(!this.save_textBox.Text.EndsWith(".xml") && !this.save_textBox.Text.EndsWith(".XML") && !this.save_textBox.Text.EndsWith(".fps") && !this.save_textBox.Text.EndsWith(".FPS"))
            {
                if (checkBox_fps.Checked) this.save_textBox.Text += ".fps";
                else if (checkBox_xml.Checked || (!checkBox_xml.Checked && !checkBox_fps.Checked)) this.save_textBox.Text += ".fps.xml";
            }
        }

        private void napraviListuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form y = new Sweeper_Form(2, this);
        }

        public bool CreateMessageBoxYesNo(string text, string lable)
        {
            bool u = false;
            u = MessageBox.Show(text, lable, MessageBoxButtons.YesNo) == DialogResult.Yes ? true : false;
            return u;
        }

        public void CreateMessageBoxParallel(string text, string lable)
        {
            Task.Run(() =>
            {
                MessageBox.Show(text, lable, MessageBoxButtons.OK);
            });
        }

        public void AutoColorChangeDialog(object sender, EventArgs e)
        {
            ChangeColorsWithDialogue();
        }

        public void ChangeColorsWithDialogue()
        {
            Color B, S1, S2;
            ColorDialog dil = new ColorDialog();
            dil.AllowFullOpen = true;
            dil.FullOpen = true;
            dil.Color = WindowColors.Base;
            DialogResult dr = dil.ShowDialog();
            B = dil.Color;
            HSLColor hsl = new HSLColor(dil.Color);
            hsl.Luminosity *= 1.8f;
            S1 = (Color)hsl;
            HSLColor o = new HSLColor(hsl.Hue, hsl.Saturation, hsl.Luminosity);
            o.Hue *= 1f - 0.143f;
            o.Saturation *= 1f - 0.164f;
            o.Luminosity *= 1.25f;
            S2 = (Color)o;
            WindowColors.ChangeColors(B, S1, S2);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.colorSchemeToolStripMenuItem.Owner.Hide();
            OpenFileDialog dial = WindowColors.LoadColorDialogWithoutShow();
            if (dial.ShowDialog() == DialogResult.OK)
            {
                WindowColors.LastLoaded(dial.FileName);
                WindowColors.SetColorsFromXml(dial.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = WindowColors.SaveColorDialogWithoutShow();
            if(save.ShowDialog() == DialogResult.OK) WindowColors.SetColorsToXml(save.FileName);
            UpdateLoadToolStripMenuItems();
        }

        private void radioSweeperSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sweeper_Settings = new SweeperSettings();
            Sweeper_Settings.Setup();
        }
    }

}
