using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml;

namespace ExtraNoveListe
{
    class WindowColors
    {
        public enum WindowColor
        {
            Base,
            Side1,  
            Side2
        }

        public static string LastColor { get; private set; }

        public class Settings
        {
            public double S1LuminosityOffset { get; } = 1.8f;

            public double S2HueOffset { get; } = 1f - 0.143f;
            public double S2SaturationOffset { get; } = 1f - 0.164f;
            public double S2LuminosityOffset { get; } = 1.25f;
        }

        public static class DefaultColors
        {
            public class DefaultColor
            {
                public Color BaseColor { get; private set; }
                public Color S1Color { get; private set; }
                public Color S2Color { get; private set; }

                public DefaultColor(Color baseColor)
                {
                    BaseColor = baseColor;
                    HSLColor hsl = new HSLColor(baseColor);
                    hsl.Luminosity *= 1.8f;
                    S1Color = (Color)hsl;
                    HSLColor o = new HSLColor(hsl.Hue, hsl.Saturation, hsl.Luminosity);
                    o.Hue *= 1f - 0.143f;
                    o.Saturation *= 1f - 0.164f;
                    o.Luminosity *= 1.25f;
                    S2Color = (Color)o;
                }
            }

            public static DefaultColor Aqua { get; } = new DefaultColor((Color)new HSLColor(140f, 240f, 60f));
            public static DefaultColor Buddah { get; } = new DefaultColor((Color)new HSLColor(114f, 87f, 85f));
            public static DefaultColor Cyan { get; } = new DefaultColor((Color)new HSLColor(120f, 108f, 86f));
            public static DefaultColor Dragon { get; } = new DefaultColor((Color)new HSLColor(13f, 240f, 95f));
            public static DefaultColor Earth { get; } = new DefaultColor((Color)new HSLColor(25f, 123f, 60f));
            public static DefaultColor Gold { get; } = new DefaultColor((Color)new HSLColor(46f, 169f, 108f));
            public static DefaultColor Olive { get; } = new DefaultColor((Color)new HSLColor(70f, 120f, 80f));
            public static DefaultColor Red { get; } = new DefaultColor((Color)new HSLColor(239f, 187f, 86f));
            public static DefaultColor Violet { get; } = new DefaultColor((Color)new HSLColor(206f, 43f, 82f));

            public static Dictionary<string, DefaultColor> AllDefaultColors { get; } = new Dictionary<string, DefaultColor>()
            {
                {"Aqua", Aqua },
                {"Buddah", Buddah },
                {"Cyan", Cyan },
                {"Dragon", Dragon },
                {"Earth", Earth },
                {"Gold", Gold },
                {"Olive", Olive },
                {"Red", Red },
                {"Violet", Violet }
            };
        }

        private static string dataColorFile { get; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\FireXer\Colors\ColorScheme.fxcol";
        public static string ColorSettingsFolderPath { get; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\FireXer\Colors";

        public static Color _baseh = new Color();
        public static Color Base
        {
            get
            {
                if (_baseh.Equals(new Color())) SetColorsFromXml();
                return _baseh;
            }
            set
            {
                _baseh = value;
                OnChange();
            }
        }

        public static Color _side1h = new Color();
        public static Color Side1
        {
            get
            {
                 if(_side1h.Equals(new Color())) SetColorsFromXml();
                return _side1h;
            }
            set
            {
                _side1h = value;
                OnChange();
            }
        }

        public static Color _side2h;
        public static Color Side2
        {
            get
            {
                if (_side2h.Equals(new Color())) SetColorsFromXml();
                return _side2h;
            }
            set
            {
                _side2h = value;
                OnChange();
            }
        }

        public static Color BorderColor { get; private set; } = Color.Black;
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////   METHODS   /////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string ToHexString(WindowColor c)
        {
            /*
            if (c == WindowColor.Base) return "#72e076";
            else if (c == WindowColor.Side1) return "#97ed9b";
            else if (c == WindowColor.Side2) return "#8ccc8f";
            else return "#ffffff";
            */

          
            if (c == WindowColor.Base) return "#444444";
            else if (c == WindowColor.Side2) return "#a6a6a6";
            else if (c == WindowColor.Side1) return "#7a897b";
            else return "#ffffff";
        }

        public static System.Drawing.Color GetColor(WindowColor c)
        {
            //return ColorTranslator.FromHtml(ToHexString(c));
            return c == WindowColor.Base
                ? Base
                : (c == WindowColor.Side1 ? Side1 
                : (c == WindowColor.Side2 ? Side2 
                : Color.White));

        }

        public static void ChangeColors(Color baseColor, Color side1Color, Color side2Color)
        {
            _baseh = baseColor;
            _side1h = side1Color;
            _side2h = side2Color;
            SetColorsToXml();
            OnChange();
        }

        public static void SetColorsFromXml()
        {
            Color _base = Color.AliceBlue, _side1 = Color.AliceBlue, _side2 = Color.AliceBlue;
            double h = 1f, s = 1f, l = 1f;
            XmlDocument doc = new XmlDocument();

            if (System.IO.File.Exists(dataColorFile))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(dataColorFile, Encoding.Default))
                {
                    try
                    {
                        doc.Load(sr);
                    }
                    catch
                    {
                        _baseh = ColorTranslator.FromHtml("#444444");
                        _side1h = ColorTranslator.FromHtml("#a6a6a6");
                        _side2h = ColorTranslator.FromHtml("#7a897b");
                        //ChangeColors(_baseh, _side1h, _side2h);
                        return;
                    }
                    XmlNode root = doc.FirstChild;
                    if (root.Name == "Colors")
                        foreach (XmlNode n in root.ChildNodes)
                        {
                            if (n.Name == "Base")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name == "Hue") if (!double.TryParse(nn.InnerText, out h)) h = 1f;
                                    if (nn.Name == "Saturation") if (!double.TryParse(nn.InnerText, out s)) s = 1f;
                                    if (nn.Name == "Luminosity") if (!double.TryParse(nn.InnerText, out l)) l = 1f;
                                }
                                HSLColor ko = new HSLColor(h, s, l);
                                _base = (Color) ko;
                            }
                            if (n.Name == "Side1")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name == "Hue") if (!double.TryParse(nn.InnerText, out h)) h = 1f;
                                    if (nn.Name == "Saturation") if (!double.TryParse(nn.InnerText, out s)) s = 1f;
                                    if (nn.Name == "Luminosity") if (!double.TryParse(nn.InnerText, out l)) l = 1f;
                                }
                                HSLColor ko = new HSLColor(h, s, l);
                                _side1 = (Color) ko;
                            }

                            if (n.Name == "Side2")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name == "Hue") if (!double.TryParse(nn.InnerText, out h)) h = 1f;
                                    if (nn.Name == "Saturation") if (!double.TryParse(nn.InnerText, out s)) s = 1f;
                                    if (nn.Name == "Luminosity") if (!double.TryParse(nn.InnerText, out l)) l = 1f;
                                }
                                HSLColor ko = new HSLColor(h, s, l);
                                _side2 = (Color) ko;
                            }
                        }
                }
            }
            else
            {
                _baseh = ColorTranslator.FromHtml("#444444");
                _side1h = ColorTranslator.FromHtml("#a6a6a6");
                _side2h = ColorTranslator.FromHtml("#7a897b");
                //ChangeColors(_baseh, _side1h, _side2h);
                return;
            }
            ChangeColors(_base, _side1, _side2);
        }

        public static void SetColorsFromXml(string path)
        {
            Color _base = Color.AliceBlue, _side1 = Color.AliceBlue, _side2 = Color.AliceBlue;
            double h = 1f, s = 1f, l = 1f;

            LastColor = path;

            XmlDocument doc = new XmlDocument();

            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path, Encoding.Default))
                {
                    try
                    {
                        doc.Load(sr);
                    }
                    catch
                    {
                        _baseh = ColorTranslator.FromHtml("#444444");
                        _side1h = ColorTranslator.FromHtml("#a6a6a6");
                        _side2h = ColorTranslator.FromHtml("#7a897b");
                        //ChangeColors(_baseh, _side1h, _side2h);
                        return;
                    }
                    XmlNode root = doc.FirstChild;
                    if (root.Name == "Colors")
                        foreach (XmlNode n in root.ChildNodes)
                        {
                            if (n.Name == "Base")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name == "Hue") if (!double.TryParse(nn.InnerText, out h)) h = 1f;
                                    if (nn.Name == "Saturation") if (!double.TryParse(nn.InnerText, out s)) s = 1f;
                                    if (nn.Name == "Luminosity") if (!double.TryParse(nn.InnerText, out l)) l = 1f;
                                }
                                HSLColor ko = new HSLColor(h, s, l);
                                _base = (Color)ko;
                            }
                            if (n.Name == "Side1")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name == "Hue") if (!double.TryParse(nn.InnerText, out h)) h = 1f;
                                    if (nn.Name == "Saturation") if (!double.TryParse(nn.InnerText, out s)) s = 1f;
                                    if (nn.Name == "Luminosity") if (!double.TryParse(nn.InnerText, out l)) l = 1f;
                                }
                                HSLColor ko = new HSLColor(h, s, l);
                                _side1 = (Color)ko;
                            }

                            if (n.Name == "Side2")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name == "Hue") if (!double.TryParse(nn.InnerText, out h)) h = 1f;
                                    if (nn.Name == "Saturation") if (!double.TryParse(nn.InnerText, out s)) s = 1f;
                                    if (nn.Name == "Luminosity") if (!double.TryParse(nn.InnerText, out l)) l = 1f;
                                }
                                HSLColor ko = new HSLColor(h, s, l);
                                _side2 = (Color)ko;
                            }
                        }
                }
            }
            else
            {
                _baseh = ColorTranslator.FromHtml("#444444");
                _side1h = ColorTranslator.FromHtml("#a6a6a6");
                _side2h = ColorTranslator.FromHtml("#7a897b");
                //ChangeColors(_baseh, _side1h, _side2h);
                return;
            }
            ChangeColors(_base, _side1, _side2);
            AddBackLog(path);
            OnCheckCheckedColorSchemesInToolStrip();
        }

        public static void SetColorsToXml()
        {
            if (System.IO.File.Exists(dataColorFile)) System.IO.File.Delete(dataColorFile);
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("Colors");
            XmlNode hue, sat, lum;
            HSLColor hls;
            
            XmlNode baseC = doc.CreateElement("Base");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(Base);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            baseC.AppendChild(hue);
            baseC.AppendChild(sat);
            baseC.AppendChild(lum);
            root.AppendChild(baseC);

            XmlNode side1C = doc.CreateElement("Side1");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(Side1);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            side1C.AppendChild(hue);
            side1C.AppendChild(sat);
            side1C.AppendChild(lum);
            root.AppendChild(side1C);

            XmlNode side2C = doc.CreateElement("Side2");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(Side2);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            side2C.AppendChild(hue);
            side2C.AppendChild(sat);
            side2C.AppendChild(lum);
            root.AppendChild(side2C);

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(dataColorFile, FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));
        }

        public static void SetColorsToXml(string path)
        {
            if (System.IO.File.Exists(dataColorFile)) System.IO.File.Delete(dataColorFile);
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("Colors");
            XmlNode hue, sat, lum;
            HSLColor hls;
            
            XmlNode baseC = doc.CreateElement("Base");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(Base);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            baseC.AppendChild(hue);
            baseC.AppendChild(sat);
            baseC.AppendChild(lum);
            root.AppendChild(baseC);

            XmlNode side1C = doc.CreateElement("Side1");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(Side1);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            side1C.AppendChild(hue);
            side1C.AppendChild(sat);
            side1C.AppendChild(lum);
            root.AppendChild(side1C);

            XmlNode side2C = doc.CreateElement("Side2");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(Side2);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            side2C.AppendChild(hue);
            side2C.AppendChild(sat);
            side2C.AppendChild(lum);
            root.AppendChild(side2C);

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));
        }

        public static void SetColorsToXml(string path, WindowColors.DefaultColors.DefaultColor dcc)
        {
            Color bb = dcc.BaseColor, ss1 = dcc.S1Color, ss2 = dcc.S2Color;
            if (System.IO.File.Exists(dataColorFile)) System.IO.File.Delete(dataColorFile);
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("Colors");
            XmlNode hue, sat, lum;
            HSLColor hls;
            
            XmlNode baseC = doc.CreateElement("Base");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(bb);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            baseC.AppendChild(hue);
            baseC.AppendChild(sat);
            baseC.AppendChild(lum);
            root.AppendChild(baseC);

            XmlNode side1C = doc.CreateElement("Side1");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(ss1);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            side1C.AppendChild(hue);
            side1C.AppendChild(sat);
            side1C.AppendChild(lum);
            root.AppendChild(side1C);

            XmlNode side2C = doc.CreateElement("Side2");
            hue = doc.CreateElement("Hue");
            sat = doc.CreateElement("Saturation");
            lum = doc.CreateElement("Luminosity");
            hls = new HSLColor(ss2);
            hue.InnerText = hls.Hue.ToString();
            sat.InnerText = hls.Saturation.ToString();
            lum.InnerText = hls.Luminosity.ToString();
            side2C.AppendChild(hue);
            side2C.AppendChild(sat);
            side2C.AppendChild(lum);
            root.AppendChild(side2C);

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

        public static OpenFileDialog LoadColorDialog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "FirePlay Color File|*.fxcol|All Files|*.*";
            openFileDialog1.Title = "Load FirePlay Color File";
            openFileDialog1.ShowDialog();
            return openFileDialog1;
        }

        public static OpenFileDialog LoadColorDialogWithoutShow()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "FirePlay Color File|*.fxcol|All Files|*.*";
            openFileDialog1.Title = "Load FirePlay Color File";
            return openFileDialog1;
        }

        public static SaveFileDialog SaveColorDialog()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "FirePlay Color File|*.fxcol|All Files|*.*";
            saveFileDialog1.Title = "Load FirePlay Color File";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = ColorSettingsFolderPath;
            saveFileDialog1.ShowDialog();
            return saveFileDialog1;
        }

        public static SaveFileDialog SaveColorDialogWithoutShow()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "FirePlay Color File|*.fxcol|All Files|*.*";
            saveFileDialog1.Title = "Load FirePlay Color File";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = ColorSettingsFolderPath;
            return saveFileDialog1;
        }

        public static void LastLoaded(string s)
        {
            LastColor = s;

        }

        private static void AddBackLog(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("Path");
            node.InnerText = path;

            XmlAttribute atr = doc.CreateAttribute("Time");
            atr.Value = DateTime.Now.ToString();
            node.Attributes.Append(atr);
            doc.AppendChild(node);
            

            using (System.IO.StreamWriter tw = new StreamWriter(File.Open(ColorSettingsFolderPath+"\\LastColorUsed.xml", FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        public static void GetBackLog()
        {
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(ColorSettingsFolderPath + "\\LastColorUsed.xml"))
            {
                using (System.IO.StreamReader sr =
                    new System.IO.StreamReader(ColorSettingsFolderPath + "\\LastColorUsed.xml", Encoding.Default))
                {
                    try
                    {
                        doc.Load(sr);
                    }
                    catch
                    {

                    }
                    foreach (XmlNode node in doc.ChildNodes)
                    {
                        if (node.Name == "Path")
                        {
                            LastColor = node.InnerText;
                            OnCheckCheckedColorSchemesInToolStrip();
                        }
                    }
                }
            }
        }

        public static void Colorize(Control c)
        {
            if (c.GetType() == typeof(Form) || c.GetType() == typeof(ExtraNoveListe.SweeperSelectSettingsForm) || c.GetType() == typeof(StartingWindowForm))
            {
                ((Form)c).BackColor = WindowColors.Base;
            }

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

                    if (cont.GetType() == typeof(TabPage))
                    {
                        ((TabPage)cont).BackColor = WindowColors.Base;
                    }

                    if (cont.GetType() == typeof(ComboBox))
                    {
                        //((ComboBox)cont).ForeColor = WindowColors.BorderColor;
                        ((ComboBox)cont).FlatStyle = FlatStyle.Flat;
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////   EVENTS   /////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public delegate void Pump();
        public static event Pump ColorChangeEvent;
        public static event Pump CheckCheckedColorSchemesInToolStrip;

        private static void OnChange()
        {
            ColorChangeEvent?.Invoke();
        }
        private static void OnCheckCheckedColorSchemesInToolStrip() { CheckCheckedColorSchemesInToolStrip?.Invoke(); }
    }
}
