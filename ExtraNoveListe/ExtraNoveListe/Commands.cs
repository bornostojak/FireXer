using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExtraNoveListe
{
    public class Commands
    {
        private static bool _is_control_down = false;
        public static bool IsControlDown { get { return _is_control_down; } set { _is_control_down = value; } }

        static void SavePlayListToFile(string path, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
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
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate), Encoding.Default))
                doc.Save(tw);

        }

        static void SavePlayListToFile(StartingWindowForm t, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp");
            l.AddMultipleLists(List);
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
                        t.Close();
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(System.IO.File.Open(t.SavePath, System.IO.FileMode.OpenOrCreate), Encoding.Default))
                doc.Save(tw);

        }

        static void SavePlaylistToFileAsPlainTextXML(StartingWindowForm t, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
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
                        t.Close();
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(System.IO.File.Open(t.SavePath, System.IO.FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        static void SavePlaylistToFileAsPlainTextXML(string path, List<FirePlayList> List)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("PlayList");
            FirePlayList l = new FirePlayList("Temp"); l.AddMultipleLists(List);
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
                    }
                }

            doc.AppendChild(root);
            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate), Encoding.Default))
                tw.Write(ReturnXMLAsString(doc));

        }

        static string ReturnXMLAsString(XmlDocument doc)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            using (var strw = new System.IO.StringWriter()) using (var xtw = XmlWriter.Create(strw, settings))
            {
                doc.WriteTo(xtw);
                xtw.Flush();
                return strw.GetStringBuilder().ToString();
            }
        }

        static string ReturnXMLAsString(XmlNode node)
        {
            using (var strw = new System.IO.StringWriter()) using (var xtw = XmlWriter.Create(strw))
            {
                node.WriteTo(xtw);
                xtw.Flush();
                return strw.GetStringBuilder().ToString();
            }
        }
    }
}
