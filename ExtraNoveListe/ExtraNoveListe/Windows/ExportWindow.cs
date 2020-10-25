using System.IO;
using System.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    public partial class ExportWindow : Form
    {
        public ExportClass EPClass { get; set; }
        public static string Location { get; set; }

        public ExportWindow(ExportClass epClass)
        {
            InitializeComponent();
            EPClass = epClass;
            Location = EPClass.Location;
            save_textBox.Text = Location;
            this.KeyDown += new KeyEventHandler(OnEnterClick);

            save_textBox.Focus();
            this.checkBox_fps.CheckedChanged += this.CheckCheckedState;
            this.checkBox_xml.CheckedChanged += this.CheckCheckedState;

            this.save_textBox.TextChanged += (o, e) =>
            {
                Location = save_textBox.Text;
                EPClass.Location = Location;
            };

            this.Show();

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
                if (!checkBox_fps.Checked) save_textBox.Text = save_textBox.Text + ".fps" + ".xml";
            }
        }

        private void ExportWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void antenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(save_textBox.Text))) temp = System.IO.Path.GetFileNameWithoutExtension(save_textBox.Text);
            save_textBox.Text = @"\\TOWER\Lists\Antena\DJGotove\";
            save_textBox.Focus();
        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(save_textBox.Text))) temp = System.IO.Path.GetFileNameWithoutExtension(save_textBox.Text);
            save_textBox.Text = @"\\TOWER\Lists\Enter\ENTER\ gotoveliste\";
            save_textBox.Focus();
        }

        private void extraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(save_textBox.Text))) temp = System.IO.Path.GetFileNameWithoutExtension(save_textBox.Text);
            save_textBox.Text = @"\\TOWER\Lists\Extra\Glazba\";
            save_textBox.Focus();
        }

        private void goldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(save_textBox.Text))) temp = System.IO.Path.GetFileNameWithoutExtension(save_textBox.Text);
            save_textBox.Text = @"\\TOWER\Lists\GFM\Gotove\";
            save_textBox.Focus();
        }

        private void narodniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(save_textBox.Text))) temp = System.IO.Path.GetFileNameWithoutExtension(save_textBox.Text);
            save_textBox.Text = @"\\TOWER\Lists\Narodni\DJ Gotove\";
            save_textBox.Focus();
        }

        public void SaveList(FirePlayList list)
        {
            if (save_textBox.Text == string.Empty)
            {
                MessageBox.Show("No save file selected! ", "Select Save File", MessageBoxButtons.OK);
                return;
            }
            if (File.Exists(save_textBox.Text)) File.Delete(save_textBox.Text);
            if (!save_textBox.Text.EndsWith(".fps.xml") && !save_textBox.Text.EndsWith(".xml")) save_textBox.Text = save_textBox.Text + ".fps.xml";

            if (EPClass.ExportList == null || EPClass.ExportListDefaulted == null)
            {
                MessageBox.Show("Missing Playlists", "You have no sellected playlists", MessageBoxButtons.OK);
                return;
            }
            if (!System.IO.Directory.Exists(Path.GetDirectoryName(EPClass.Location)))
            {
                MessageBox.Show("Location Dyrectory is wrong!", "Wrong dyrectory", MessageBoxButtons.OK);
            }
            ExportWindow.SavePlaylistToFileAsPlainTextXML(list, EPClass.Location);
            Task.Run(() => MessageBox.Show("Done ", "Done", MessageBoxButtons.OK));
        }

        public void OnEnterClick(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (ModifierKeys.HasFlag(Keys.Control)) SaveList(EPClass.ExportListDefaulted);
                else SaveList(EPClass.ExportList);
            }
        }

        private void export_button_Click(object sender, EventArgs e)
        {
            //SaveList(EPClass.ExportList);
            StartingWindowForm.SavePlaylistToFileAsPlainTextXML(Location, EPClass.ExportList);
            this.Close();
        }
        private void default_button_Click(object sender, EventArgs e)
        {
            FirePlayList l = EPClass.ExportList.Clone();
            l.DefaultTheList();
            StartingWindowForm.SavePlaylistToFileAsPlainTextXML(Location, l);
            l = null;
            //SaveList(EPClass.ExportListDefaulted);
            this.Close();
        }



        public static void SavePlaylistToFileAsPlainTextXML(FirePlayList list, string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            list.Songs.ForEach((s) =>
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
            });

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        public static string ReturnXMLAsString(XmlNode node)
        {
            using (var strw = new StringWriter()) using (var xtw = XmlWriter.Create(strw))
            {
                node.WriteTo(xtw);
                xtw.Flush();
                return strw.GetStringBuilder().ToString();
            }
        }
    }
}
