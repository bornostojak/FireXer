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
    public partial class WinNoForm : Form
    {
        public WinNoForm()
        {
            InitializeComponent();
        }

        public WinNoForm(Control c)
        {
            this.Size = c.Size;
            c.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
        }


        private void WinNoForm_Load(object sender, EventArgs e)
        {

        }
    }
}
