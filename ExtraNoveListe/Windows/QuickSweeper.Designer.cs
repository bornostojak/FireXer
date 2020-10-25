using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ExtraNoveListe
{
    public partial class QuickSweeper : Form
    {
        #region Initialize

        private void ColorSetup()
        {
            this.BackColor = WindowColors.Side2;
            box.BackColor = WindowColors.Side2;

            box.BorderStyle = BorderStyle.FixedSingle;
            //this.BackColor = Color.Transparent;

            Subscribe();
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseMove += WithMouseMove;
            m_GlobalHook.MouseWheel += (sender, args) =>
            {
                if (ModifierKeys.HasFlag(Keys.Alt))
                {
                    if(box.SelectedIndices.Count == 0) box.SelectedIndex = 0;
                    if (args.Delta < 0)
                    {
                        
                        Current.box.SelectedIndex = Current.box.SelectedIndex +1 > Current.box.Items.Count-1 ? Current.box.Items.Count - 1 : Current.box.SelectedIndex + 1;
                    }
                    else if (args.Delta > 0)
                        if (box.Items.Count > 0)
                        {
                            Current.box.SelectedIndex = Current.box.SelectedIndex <= 0 ? 0 : Current.box.SelectedIndex - 1; 
                        }
                }
            };
            m_GlobalHook.MouseDown += (sender, args) =>
            {
                if (QuickSweeper.SweeperModeOn)
                {
                    if (args.Button == MouseButtons.Right)
                    {
                        tpb(this, new EventArgs());
                        return;
                    }

                    if (this.Visible && !(box.ClientRectangle.Contains(PointToClient(Control.MousePosition)))) this.SendToBack(); 
                }
                
            };
            m_GlobalHook.MouseUp += (a, b) =>
            {
                try
                {
                    if (QuickSweeper.SweeperModeOn)
                    {
                        this.Show();
                        QuickSweeper.Current.BringToFront();
                    }
                }
                catch
                {
                    
                }
            };
            m_GlobalHook.KeyDown += (a, bbb) =>
            {
                if (bbb.KeyCode == Keys.Escape && QuickSweeper.SweeperModeOn) tpb(this, new EventArgs());
                    //QuickSweeper.Current.Close();
            };
        }



        public delegate void thebutton(object a, EventArgs e);
        public static thebutton tpb { get; set; }
        #endregion
    }
}
