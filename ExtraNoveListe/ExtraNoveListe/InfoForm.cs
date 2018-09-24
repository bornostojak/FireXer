using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections; 

namespace ExtraNoveListe
{
    public partial class InfoForm : Form
    {
        private FirePlaySong Song { get; set; }

        public InfoForm(FirePlaySong song)
        {
            Song = song;
            InitializeComponent();
            this.GridView();
            this.SetUpAnchors();
            InitializeList();
            Show();

            this.KeyPreview = true;
            this.KeyDown += (a, b) =>
            {
                if (b.KeyCode == Keys.C && ModifierKeys.HasFlag(Keys.Control))
                {
                    foreach (ListViewItem lvi in infoListView.Items)
                    {
                        if (lvi.Selected)
                        {
                            System.Windows.Forms.Clipboard.SetText(lvi.SubItems[1].Text);
                        }
                    }
                }
            };


        }

        private void InitializeList()
        {
            ListViewItem itemID = new ListViewItem("ID");
            itemID.SubItems.Add(Song.ID);

            ListViewItem itemNaziv = new ListViewItem("Naziv");
            itemNaziv.SubItems.Add(Song.Naziv);

            ListViewItem itemAutor = new ListViewItem("Autor");
            itemAutor.SubItems.Add(Song.Autor);

            ListViewItem itemAlbum = new ListViewItem("Album");
            itemAlbum.SubItems.Add(Song.Album);

            ListViewItem itemInfo = new ListViewItem("Info");
            itemInfo.SubItems.Add(Song.Info);

            ListViewItem itemTip = new ListViewItem("Tip");
            itemTip.SubItems.Add(Song.Tip);

            ListViewItem itemColor = new ListViewItem("Color");
            itemColor.SubItems.Add(Song.Color);

            ListViewItem itemNaKanalu = new ListViewItem("NaKanalu");
            itemNaKanalu.SubItems.Add(Song.NaKanalu);

            ListViewItem itemPathName = new ListViewItem("PathName");
            itemPathName.SubItems.Add(Song.PathName);

            ListViewItem itemItemType = new ListViewItem("ItemType");
            itemItemType.SubItems.Add(Song.ItemType);
            
            ListViewItem itemStartCue = new ListViewItem("StartCue");
            itemStartCue.SubItems.Add(Song.StartCue);

            ListViewItem itemEndCue = new ListViewItem("EndCue");
            itemEndCue.SubItems.Add(Song.EndCue);

            ListViewItem itemPocetak = new ListViewItem("Pocetak");
            itemPocetak.SubItems.Add(Song.Pocetak);

            ListViewItem itemTrajanje = new ListViewItem("Trajanje");
            itemTrajanje.SubItems.Add(Song.Trajanje);

            ListViewItem itemVrijeme = new ListViewItem("Vrijeme");
            itemVrijeme.SubItems.Add(Song.Vrijeme);

            ListViewItem itemStvarnoVrijemePocetka = new ListViewItem("StvarnoVrijemePocetka");
            itemStvarnoVrijemePocetka.SubItems.Add(Song.StvarnoVrijemePocetka);

            ListViewItem itemVrijemeMinTermin = new ListViewItem("VrijemeMinTermin");
            itemVrijemeMinTermin.SubItems.Add(Song.VrijemeMinTermin);

            ListViewItem itemVrijemeMaxTermin = new ListViewItem("VrijemeMaxTermin");
            itemVrijemeMaxTermin.SubItems.Add(Song.VrijemeMaxTermin);

            ListViewItem itemPrviU_Bloku = new ListViewItem("PrviU_loku");
            itemPrviU_Bloku.SubItems.Add(Song.PrviU_Bloku);

            ListViewItem itemZadnjiU_Bloku = new ListViewItem("ZadnjiU_Bloku");
            itemZadnjiU_Bloku.SubItems.Add(Song.ZadnjiU_Bloku);

            ListViewItem itemJediniU_Bloku = new ListViewItem("JediniU_Bloku");
            itemJediniU_Bloku.SubItems.Add(Song.JediniU_Bloku);

            ListViewItem itemFiksniU_Terminu = new ListViewItem("FiksniU_Terminu");
            itemFiksniU_Terminu.SubItems.Add(Song.FiksniU_Terminu);
            
            ListViewItem itemReklama = new ListViewItem("Reklama");
            itemReklama.SubItems.Add(Song.Reklama);

            ListViewItem itemWaveIn = new ListViewItem("WaveIn");
            itemWaveIn.SubItems.Add(Song.WaveIn);
            
            ListViewItem itemSoftIn = new ListViewItem("SoftIn");
            itemSoftIn.SubItems.Add(Song.SoftIn);

            ListViewItem itemSoftOut = new ListViewItem("SoftOut");
            itemSoftOut.SubItems.Add(Song.SoftOut);

            ListViewItem itemVolume = new ListViewItem("Volume");
            itemVolume.SubItems.Add(Song.Volume);

            ListViewItem itemOriginalStartCue = new ListViewItem("OriginalStartCue");
            itemOriginalStartCue.SubItems.Add(Song.OriginalStartCue);

            ListViewItem itemOriginalEndCue = new ListViewItem("OriginalEndCue");
            itemOriginalEndCue.SubItems.Add(Song.OriginalEndCue);

            ListViewItem itemOriginalPocetak = new ListViewItem("OriginalPocetak");
            itemOriginalPocetak.SubItems.Add(Song.OriginalPocetak);

            ListViewItem itemOriginalTrajanje = new ListViewItem("OriginalTrajanje");
            itemOriginalTrajanje.SubItems.Add(Song.OriginalTrajanje);

            infoListView.Items.AddRange(new ListViewItem[]
            {
                itemID,
                itemNaziv,
                itemAutor,
                itemAlbum,
                itemInfo,
                itemTip,
                itemColor,
                itemNaKanalu,
                itemPathName,
                itemItemType,
                itemStartCue,
                itemEndCue,
                itemPocetak,
                itemTrajanje,
                itemVrijeme,
                itemStvarnoVrijemePocetka,
                itemVrijemeMinTermin,
                itemVrijemeMaxTermin,
                itemPrviU_Bloku,
                itemZadnjiU_Bloku,
                itemJediniU_Bloku,
                itemFiksniU_Terminu,
                itemReklama,
                itemWaveIn,
                itemSoftIn,
                itemSoftOut,
                itemVolume,
                itemOriginalStartCue,
                itemOriginalEndCue,
                itemOriginalPocetak,
                itemOriginalTrajanje
            });

            infoListView.MouseDown += (a, b) =>
            {
                if (b.Button == MouseButtons.Right)
                {
                    if (infoListView.SelectedIndices.Count > 0)
                    {
                        InfoFormContextMenueStrip gg = new InfoFormContextMenueStrip(infoListView.SelectedItems[0]);
                        gg.Location = b.Location;
                        gg.Show(this, b.Location);
                    }
                        
                }
            };
        }

        private void infoListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = infoListView.ListViewItemSorter as ItemComparer;

            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                infoListView.ListViewItemSorter = sorter;
            }
            // if clicked column is already the column that is being sorted
            if (e.Column == sorter.Column)
            {
                // Reverse the current sort direction
                if (sorter.Order == SortOrder.Ascending)
                    sorter.Order = SortOrder.Descending;
                else
                    sorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.Column = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            infoListView.Sort();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.Apply();
        }

        public void Apply()
        {
            
        }

        private void okButon_Click(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }
    }

    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal;
            // Determine whether the type being compared is a date type.  
            try
            {
                // Parse the two objects passed as a parameter as a DateTime.  
                System.DateTime firstDate =
                        DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                System.DateTime secondDate =
                        DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                // Compare the two dates.  
                returnVal = DateTime.Compare(firstDate, secondDate);
            }
            // If neither compared object has a valid date format, compare  
            // as a string.  
            catch
            {
                // Compare the two items as a string.  
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                            ((ListViewItem)y).SubItems[col].Text);
            }
            // Determine whether the sort order is descending.  
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.  
                returnVal *= -1;
            return returnVal;
        }
    }

    public class InfoFormContextMenueStrip : ContextMenuStrip
    {
        private ListViewItem _listViewItem = new ListViewItem();
        public ListViewItem ListViewItem_1
        {
            get
            {
                return _listViewItem;
            }
            set { _listViewItem = value; }
        }

        public InfoFormContextMenueStrip(ListViewItem item)
        {
            ListViewItem_1 = item;
            ToolStripMenuItem copyItem = new ToolStripMenuItem();
            copyItem.Name = "Copy";
            copyItem.Size = new System.Drawing.Size(152, 22);
            copyItem.Text = "Copy";
            copyItem.Click += (a, b) =>
            {
                System.Windows.Forms.Clipboard.SetText(ListViewItem_1.SubItems[1].Text);
            };
            this.Items.Add(copyItem);

            //this.Show();

        }
        
    }
}
