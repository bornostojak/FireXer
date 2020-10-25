using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    public enum Radio { Antena = 0, Enter, Extra, Gold, Narondi, None}

    public class SweeperSettings
    {
        private string[] RadioLokacije = { $@"C:\Users\Borno\Music\GG\V", $@"C:\Users\Borno\Music\GG\V", $@"C:\Users\Borno\Music\GG\V", $@"C:\Users\Borno\Music\GG\V", $@"C:\Users\Borno\Music\GG\V", null };

        private static List<RadioSWSettings> _allSweeperSettings = new List<RadioSWSettings>();
        public static List<RadioSWSettings> AllSweeperSettings { get { return _allSweeperSettings; } }
        public static string SweeperSettingsFileName { get; private set; } = null;

        public static string DefaultSweeperSettingsDirectoryPath { get; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\FireXer\Settings";


        ///////////////////////  SWEEPER SETTINGS  ///////////////////////
        public static StartingWindowForm main_window { get; set; }
        public static SweeperSelectSettingsForm SweeperSelecctSettings { get; set; }

        public static string CurrentSettingsFilePath { get; set; }
        ///////////////////////  SWEEPER SETTINGS  ///////////////////////

        

        public SweeperSettings()
        {
            SweeperSelecctSettings = new SweeperSelectSettingsForm();

        }

        
        //////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////  STATIC METHODS  ////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets the settings from the xml file, and if it doesn't exist asks whether to create one
        /// </summary>
        private static void GetSettings()
        {
            if (!System.IO.Directory.Exists(DefaultSweeperSettingsDirectoryPath))
            {
                if(main_window.CreateMessageBoxYesNo("No settings were found. \nDO YOU WANT TO CREATE THEM?", "NO SETTINGS FOUND"))
                {
                    System.IO.Directory.CreateDirectory(DefaultSweeperSettingsDirectoryPath);
                    SweeperSelecctSettings.Initialize();
                }
            }
            else
            {
                if (System.IO.Directory.GetFiles(DefaultSweeperSettingsDirectoryPath).Count() != 0)
                {
                    bool file_found = false;
                    List<string> files = new List<string>();
                    files = System.IO.Directory.GetFiles(DefaultSweeperSettingsDirectoryPath).ToList();
                    files.ForEach((f) =>
                    {
                        if (f.ToLower().EndsWith(".sws.xml"))
                        {
                            XmlDocument k = new XmlDocument();

                            using (System.IO.StreamReader sr = new System.IO.StreamReader(f, Encoding.Default))
                            {
                                try
                                {
                                    k.Load(sr);
                                }
                                catch
                                {

                                }
                                XmlNode ee = k.FirstChild;
                                if (ee.Name == "SweeperSettings")
                                {
                                    file_found = true;
                                    SweeperSettings.SweeperSettingsFileName = f;
                                    GetDataFromXMLFile(f);
                                    return;
                                } 
                            }
                        }
                    });
                    if (!file_found)
                        if (main_window.CreateMessageBoxYesNo("No settings were found. \nDO YOU WANT TO CREATE THEM?", "NO SETTINGS FOUND"))
                        {
                            System.IO.Directory.CreateDirectory(DefaultSweeperSettingsDirectoryPath);
                            SweeperSelecctSettings.Initialize();
                        }

                }
                else
                {
                    if (main_window.CreateMessageBoxYesNo("No settings were found!\n Do you want to create new settigs?", "Error: No Settings Found!"))
                    {
                        SweeperSelecctSettings.Initialize();
                    }

                }
            }
        }

        /// <summary>
        /// Gets the radio sweeper settings data from the xml file
        /// </summary>
        /// <param name="path"></param>
        public static void GetDataFromXMLFile(string path)
        {
            CurrentSettingsFilePath = path;
            XmlDocument doc = new XmlDocument();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                try { doc.Load(sr); }
                catch { }
            }

            if (!doc.HasChildNodes)   ////////////////// COME BACK COME BACK COME BACK COME BACK
            {
                if(main_window.CreateMessageBoxYesNo("No settings detected in file. Do you want to create them?",  "Error: NO SETTINGS DETECTED!"))
                {
                    System.IO.Directory.CreateDirectory(DefaultSweeperSettingsDirectoryPath);
                    SweeperSelecctSettings.Initialize();
                }
                
            }
            else
            {
                XmlNode root = doc.FirstChild;
                if (root.Name == "SweeperSettings")
                {
                    XmlNodeList radio_children = root.ChildNodes;
                    foreach (XmlElement r in radio_children)
                    {
                        if (r.Name == "Radio")
                        {
                            //Gets the name and the letter of the given radio
                            RadioSWSettings radioSWS = new RadioSWSettings(r.GetAttribute("name"));
                            radioSWS.RadioLetter = r.GetAttribute("letter");
                            SweeperSettings.AllSweeperSettings.Add(radioSWS);

                            XmlNodeList folder_children = r.ChildNodes;
                            foreach (XmlElement ff in folder_children)
                            {
                                if (ff.Name == "Folders")
                                {
                                    foreach (XmlElement f in ff.ChildNodes)
                                    {
                                        RadioSWSettingsFolders fdr = new RadioSWSettingsFolders(f.GetAttribute("path"), radioSWS.RadioName);

                                        //Gets all child nodes including <Files> , <ExceptionFiles>, <SweepersPerHour> & <SweepersPerHourNumber>
                                        XmlNodeList folder_nodes = f.ChildNodes;
                                        foreach (XmlNode node in folder_nodes)
                                        {
                                            int n;
                                            if (node.Name == "Files" && node.InnerText != string.Empty) fdr.Files = node.InnerText.Split(';').ToList();
                                            if (node.Name == "ExceptionFiles" && node.InnerText != string.Empty) { fdr.ExceptionFiles = node.InnerText.Split(';').ToList(); fdr.UpdateFilesWithoutExceptionFiles(); }
                                            if (node.Name == "SweepersPerHour") fdr.PerHour = node.InnerText == "One" ? SweeperEveryHour.One :
                                                                                                            (node.Value == "Two" ? SweeperEveryHour.Two :
                                                                                                            (node.Value == "Number" ? SweeperEveryHour.Number :
                                                                                                            (node.Value == "Norman" ? SweeperEveryHour.Normal : SweeperEveryHour.None)));
                                            if (node.Name == "SweepersPerHourNumber") fdr.PerHourNumber = int.TryParse(node.InnerText, out n) ? n : 0;
                                        }
                                        radioSWS.AddFolder(fdr);
                                    } 
                                }
                            } 
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the data in the settings file misaligns with the data in the given folders
        /// </summary>
        public static void CheckIfUpdateNeeded()
        {
            bool changed = false;
            RadioSWSettings sws = new RadioSWSettings();
            AllSweeperSettings.ForEach((x) =>
            {
                x.RadioFolders.ForEach((y) =>
                {
                    List<string> files = System.IO.Directory.GetFiles(y.Folder).ToList();

                    if(files.Count == y.Files.Count)
                    {
                        files.ForEach((d) =>
                        {
                            if (!y.Files.Contains(d)) changed = true;
                        });
                    }
                    else
                    {
                        changed = true;
                    }
                });
            });
            if (changed) ;//if (main_window.CreateMessageBoxYesNo("Changes have been made to the data in the sweepers location. Do you want to update the settings?", "Changes detected")) SweeperSelecctSettings.Initialize();

        }

        public static RadioSWSettings GetSettingsFromFolders()
        {
            
            return null;
        }

        public void Setup()
        {
            GetSettings();
            SweeperSelecctSettings.LoadCurrentData();
            SweeperSelecctSettings.Initialize();
        }

        public void SetupOnStartup()
        {
            GetSettings();
            CheckIfUpdateNeeded();
        }

        ///////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////  METHODS  //////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////

        


        /// <summary>
        /// Creates an xml file with the given path to store the settings
        /// </summary>
        /// <param name="file"></param>
        public static void CreateXMLSettingsFile(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("SweeperSettings");
            doc.AppendChild(root);

            //Creates a radio xml node for each radio
            RadioSWSettings.AllRadioSWSettings.ForEach((radio) =>
            {
                // Create radio node and add the radio name to it
                XmlNode radio_node = doc.CreateElement("Radio");
                root.AppendChild(radio_node);
                XmlAttribute attr0 = doc.CreateAttribute("name");
                attr0.Value = radio.RadioName;
                radio_node.Attributes.Append(attr0);
                XmlAttribute attr1 = doc.CreateAttribute("letter");  //The letter the radio sees as the letter location of the sweepers
                attr1.Value = radio.RadioLetter;
                radio_node.Attributes.Append(attr1);

                //Cretae folders node
                XmlNode many_folder_node = doc.CreateElement("Folders");
                radio_node.AppendChild(many_folder_node);

                radio.RadioFolders.ForEach((folder) =>
                {
                    //Create a new node for each folder for the radio given
                    XmlNode folder_node = doc.CreateElement("Folder");
                    XmlAttribute attr2 = doc.CreateAttribute("name");
                    attr2.Value = folder.FolderName;
                    folder_node.Attributes.Append(attr2);
                    XmlAttribute attr3 = doc.CreateAttribute("path");
                    attr3.Value = folder.Folder;
                    folder_node.Attributes.Append(attr3);
                    many_folder_node.AppendChild(folder_node);

                    //Create files node
                    XmlNode files_node = doc.CreateElement("Files");

                    /*
                     * string s = string.Empty;
                     * folder.Files.ForEach((files) =>
                     * {
                     *  if (s == string.Empty) s += files;
                     *  else s += ";" + files;
                     *  });
                     */

                    if (folder.Files.Count > 0) files_node.InnerText = String.Join(";", folder.Files);
                    folder_node.AppendChild(files_node);


                    //Create exception files node
                    XmlNode exception_files_node = doc.CreateElement("Files");
                    if (folder.Files.Count > 0) exception_files_node.InnerText = String.Join(";", folder.ExceptionFiles);
                    folder_node.AppendChild(exception_files_node);

                    //Create SweepersPerHour node
                    XmlNode sph_node = doc.CreateElement("SweepersPerHour");
                    sph_node.InnerText = folder.PerHour == SweeperEveryHour.One ? "One" :
                                    (folder.PerHour == SweeperEveryHour.Two ? "Two" :
                                    (folder.PerHour == SweeperEveryHour.Number ? "Number" :
                                    (folder.PerHour == SweeperEveryHour.Normal ? "Normal" : "None")));
                    folder_node.AppendChild(sph_node);

                    //Create SweepersPerHourNumber node
                    XmlNode sphn_node = doc.CreateElement("SweepersPerHourNumber");
                    sphn_node.InnerText = folder.PerHourNumber.ToString();
                    folder_node.AppendChild(sphn_node);

                });
            });

            //Save the file in the correct path
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;
            string xcv = string.Empty;
            using (var strw = new StringWriter()) using (var xtw = XmlWriter.Create(strw, settings))
            {
                doc.WriteTo(xtw);
                xtw.Flush();
                xcv += strw.GetStringBuilder().ToString();
            }

            string savefilepath = filepath == "Default" || filepath == "default"
                ? $@"C:\Users\Public\AppData\FireXer\Settings\{DateTime.Now.ToString().Replace(' ', '_')}.sws.xml"
                : filepath;
            if (System.IO.File.Exists(savefilepath)) System.IO.File.Delete(savefilepath);
            using (StreamWriter sw = new StreamWriter(System.IO.File.Open(savefilepath, FileMode.OpenOrCreate), Encoding.Default))
            {
                sw.Write(xcv);
            }
        }

        


        ///////////////////////////////////////////////////////////////////////////
        /////////////////////////////  STATIC MEHODS  /////////////////////////////
        ///////////////////////////////////////////////////////////////////////////

        public static void CreateSettingsPath()
        {
            if (System.IO.Directory.Exists(SweeperSettings.DefaultSweeperSettingsDirectoryPath))
            {
                System.IO.Directory.CreateDirectory(SweeperSettings.DefaultSweeperSettingsDirectoryPath);
            }

        }
    }
}