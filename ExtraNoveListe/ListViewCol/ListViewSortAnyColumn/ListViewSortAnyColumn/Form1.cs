using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListViewSortAnyColumn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fill the list with data
            FillItems();
        }

        private void listViewSample_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ItemComparer sorter = listViewSample.ListViewItemSorter as ItemComparer;
            
            if (sorter == null)
            {
                sorter = new ItemComparer(e.Column);
                sorter.Order = SortOrder.Ascending;
                listViewSample.ListViewItemSorter = sorter;
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
            listViewSample.Sort();
        }

        private void FillItems()
        {
            // Add items
            ListViewItem item1 = new ListViewItem("Nipun Tomar");
            item1.SubItems.Add("10/11/2000");
            item1.SubItems.Add("Email@domain.com");
            item1.SubItems.Add("123.456");

            ListViewItem item2 = new ListViewItem("First Last");
            item2.SubItems.Add("12/12/2010");
            item2.SubItems.Add("test@test.com");
            item2.SubItems.Add("123.4561");

            ListViewItem item3 = new ListViewItem("User User");
            item3.SubItems.Add("12/01/1800");
            item3.SubItems.Add("sample@Sample.net");
            item3.SubItems.Add("123.4559");

            ListViewItem item4 = new ListViewItem("Sample");
            item4.SubItems.Add("05/30/1900");
            item4.SubItems.Add("user@sample.com");
            item4.SubItems.Add("-123.456000");

            // Add the items to the ListView.
            listViewSample.Items.AddRange(
                                    new ListViewItem[] {item1, 
                                                item2, 
                                                item3,
                                                item4}
                                    );

        }
    }
}
