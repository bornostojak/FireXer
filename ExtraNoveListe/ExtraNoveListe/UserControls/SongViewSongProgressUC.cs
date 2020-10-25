using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    public partial class SongViewSongProgressUC : UserControl
    {
        int maxLength;
        int Height;
        System.Threading.Thread tttt = System.Threading.Thread.CurrentThread;
        public SongViewSongProgressUC()
        {
            InitializeComponent();
            var t = System.Threading.Thread.CurrentThread.ManagedThreadId;

            maxLength = this.Size.Width;
            Height = this.Size.Height;

            this.Visible = false;
            //this.Size = new Size(0, Height);

            this.BackColor = WindowColors.Side1;
        }
        
        public SongViewSongProgressUC(bool f)
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
        }

        public void Progress(int percentage)
        {
            //this.Size = new Size(maxLength * percentage / 100, Height);

        }
        public void ChangeVisibility()
        {
            this.Visible = !this.Visible; 
         
        }

        public void IsVisible()
        {
            this.Visible = true;
        }

        public void IsInvisible()
        {
            this.Visible = false;
        }
    }
}
