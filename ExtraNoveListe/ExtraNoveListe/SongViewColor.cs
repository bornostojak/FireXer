using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ExtraNoveListe
{
    public class SongViewColor
    {

        public static Color Sweeper { get { return System.Drawing.ColorTranslator.FromHtml("#ff0000"); } }
        //public static Color Song { get { return System.Drawing.ColorTranslator.FromHtml("activecaption"); } }
        public static Color Song { get { return System.Drawing.ColorTranslator.FromHtml("#33ccff"); } }
        public static Color SongChecked { get { return System.Drawing.ColorTranslator.FromHtml("#0099cc"); } }
    }
}
