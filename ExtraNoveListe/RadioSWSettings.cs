using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtraNoveListe
{
    public class RadioSWSettings
    {
        ///////////////////////////////////  VARIABLES  ///////////////////////////////////
        private string _radioName = null;
        public string RadioName { get { return _radioName; } }
        public string RadioLetter { get; set; }

        public string RadioSweepersFolderPath { get; set; }

        /// <summary>
        /// List of folders by path
        /// And radio
        /// </summary>
        public List<RadioSWSettingsFolders> RadioFolders = new List<RadioSWSettingsFolders>();
        ///////////////////////////////////  VARIABLES  ///////////////////////////////////

        ///////////////////////////////////  STATIC VARS  ///////////////////////////////////
        public static List<RadioSWSettings> AllRadioSWSettings = new List<RadioSWSettings>();
        ///////////////////////////////////  STATIC VARS  ///////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////  CONSTRUCTORS  ///////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////

        public static void ClearSettings()
        {
            RadioSWSettings.AllRadioSWSettings = new List<RadioSWSettings>();
            RadioSWSettingsFolders.ClearFolderSettings();
        }


        /// <summary>
        /// Sets u a temporary instance of this object type without adding it to the AllRaduiSWSettings list
        /// </summary>
        public RadioSWSettings()
        {

        }

        /// <summary>
        /// Creates an instance of this object with the name given
        /// </summary>
        /// <param name="radioname"></param>
        public RadioSWSettings(string radioname)
        {
            _radioName = radioname;
            AllRadioSWSettings.Add(this);
        }

        /// <summary>
        /// Creates an instance of this object with the name given
        /// </summary>
        /// <param name="radioname"></param>
        public RadioSWSettings(string radioname, string[] folders)
        {
            _radioName = radioname;
            folders.ToList().ForEach((x) => RadioFolders.Add(new RadioSWSettingsFolders(x, radioname)));
            AllRadioSWSettings.Add(this);
        }

        /// <summary>
        /// Creates an instance of this object with the name given
        /// </summary>
        /// <param name="radioname"></param>
        public RadioSWSettings(string radioname, List<SweeperSelectSettingsForm.Folder> folders)
        {
            _radioName = radioname;
            folders.ForEach((x) => RadioFolders.Add(new RadioSWSettingsFolders(x)));
            AllRadioSWSettings.Add(this);
        }


        /// <summary>
        /// Sets u a temporary instance of this object type without adding it to the AllRaduiSWSettings list
        /// </summary>
        public RadioSWSettings(string radioname, string sweeper_folder_path)
        {
            _radioName = radioname;
            RadioSweepersFolderPath = sweeper_folder_path;
        }

        public static List<RadioSWSettings> CreateFromSweeperSelectSettings(Dictionary<string, List<SweeperSelectSettingsForm.Folder>> Radios)
        {
            List<RadioSWSettings> RSWS = new List<RadioSWSettings>();
            
            foreach (KeyValuePair<string, List<SweeperSelectSettingsForm.Folder>> pair in Radios)
            {
                RSWS.Add(new RadioSWSettings(pair.Key, pair.Value));
            }
            return RSWS;
        }
        
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////  OBJECT METHODS  //////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Adds this instance of the object to the list of all objects
        /// </summary>
        public void AddToAll()
        {
            if (AllRadioSWSettings.Where((x) => x == this).Count() < 1) AllRadioSWSettings.Add(this);
        }

        /// <summary>
        /// Adds a folder of the right type to the radio settings
        /// </summary>
        /// <param name="folder"></param>
        public void AddFolder(RadioSWSettingsFolders folder)
        {
            if (!RadioFolders.Contains(folder)) RadioFolders.Add(folder);
            //UpdateSweepersFolderPath();
        }

        /// <summary>
        /// Adds the folder to radio setting by giving it path of the folder
        /// </summary>
        /// <param name="path"></param>
        public void AddFolder(string path)
        {
            if (RadioFolders.Where((x) => x.Folder == path).Count() == 0) RadioFolders.Add(new RadioSWSettingsFolders(path, RadioName));
            UpdateSweepersFolderPath();
        }

        /// <summary>
        /// Returns the list of the names of all sweeper folders for a given radio
        /// </summary>
        /// <returns></returns>
        public List<string> GetFolderNameList()
        {
            List<string> temp = new List<string>();
            try
            {
                RadioFolders.ForEach((x) => temp.Add(new System.IO.DirectoryInfo(x.Folder).Name));
            }
            catch
            {
            }
            return temp;
        }

        /// <summary>
        /// Returns the path of all sweeper folders for a given radio
        /// </summary>
        /// <returns></returns>
        public List<string> GetFolderPathList()
        {
            List<string> temp = new List<string>();
            RadioFolders.ForEach((d) => temp.Add(d.Folder));
            return temp;
        }

        /// <summary>
        /// Updates the sweeper's folder path
        /// </summary>
        public void UpdateSweepersFolderPath()
        {
           //List<string> folderpaths = new List<string>(RadioFolders.Select((x) => x.Folder));

           List<string> folderpaths = new List<string>();
            //folderpaths.AddRange(RadioFolders.Select((x) => x.Folder));
           RadioFolders.ForEach((x) => folderpaths.Add(x.Folder));

            Dictionary<string, int> dict = new Dictionary<string, int>();
            do
            {
                dict = new Dictionary<string, int>();
                for (int i = 0; i < folderpaths.Count; i++) folderpaths[i] = System.IO.Directory.GetParent(folderpaths[i]).FullName;
                folderpaths.ForEach((x) =>
                {
                    if (!dict.ContainsKey(x)) dict.Add(x, 0);
                    else dict[x]++;
                });
            } while (dict.Keys.Count > 1);
            RadioSweepersFolderPath = dict.Keys.First();
        }



        ////////////////////////////////////////////////////////////////////////
        ///////////////////////////  STATIC METHODS  ///////////////////////////
        ////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Clears Al lRadio Sweeper Settings
        /// </summary>
        public static void ClearAll()
        {
            AllRadioSWSettings = new List<RadioSWSettings>();
        }
    }


    public class RadioSWSettingsFolders
    {
        /*
         * What i call exceptio files here are actualy files that we skip when we reach them
         * I started calling them ExceptioFiles before i realised the the name didn't fit
         * but was afraid to change the name so that the consistent methods are stil vadlid
        */

        /////////////////////////////////  VARIABLES  /////////////////////////////////
        public string RadioName { get; set; }

        /// <summary>
        /// Folder path
        /// </summary>
        public string Folder { get; set; }
        public string FolderName { get; set; }
        public SweeperEveryHour PerHour { get; set; }
        private int _perHourNumber = 1;
        public int PerHourNumber { get { return _perHourNumber; } set { _perHourNumber = value; } }

        public List<string> Files = new List<string>();
        public List<string> ExceptionFiles = new List<string>();
        public List<string> FilesWithoutException = new List<string>();

        public List<FirePlaySong> AllSongs = new List<FirePlaySong>();
        /////////////////////////////////  VARIABLES  /////////////////////////////////

        ////////////////////////////////  STATIC VARS  ////////////////////////////////
        public static List<RadioSWSettingsFolders> _allFolders = new List<RadioSWSettingsFolders>();

        public static void ClearFolderSettings()
        {
            _allFolders = new List<RadioSWSettingsFolders>();
        }
        ////////////////////////////////  STATIC VARS  ////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////// CONSTRUCTORS ////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Sets up an instance of this given class with no set parametars
        /// </summary>
        public RadioSWSettingsFolders()
        {
            _allFolders.Add(this);
        }
        
        /// <summary>
        /// Sets up an instance of this given class with no set parametars
        /// </summary>
        public RadioSWSettingsFolders(SweeperSelectSettingsForm.Folder Folders)
        {
            _allFolders.Add(this);
            Folder = Folders.FolderPath;
            FolderName = Folders.FolderName;
            PerHour = Folders.SweeperEveryHour;
            GetFiles(Folders.Files);
        }

        /// <summary>
        /// Sets up an instance of this given class acording to the parametars
        /// </summary>
        /// <param name="path"></param>
        public RadioSWSettingsFolders(string path)
        {
            Folder = path;
            FolderName = new System.IO.DirectoryInfo("path").Name;
            PerHour = SweeperEveryHour.Normal;
            GetFiles();
            _allFolders.Add(this);
        }

        /// <summary>
        /// Sets up an instance of this object with the given  parameters
        /// </summary>
        /// <param name="path"></param>
        /// <param name="radio"></param>
        public RadioSWSettingsFolders(string path, string radio)
        {
            Folder = path;
            FolderName = new System.IO.DirectoryInfo(path).Name;
            RadioName = radio;
            GetFiles();
            _allFolders.Add(this);
        }


        /// <summary>
        /// Sets up an instance of this given class acording to the parametars
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sph"></param>
        public RadioSWSettingsFolders(string path, string radio, SweeperEveryHour sph)
        {
            Folder = path;
            FolderName = new System.IO.DirectoryInfo("path").Name;
            PerHour = sph;
            RadioName = radio;
            GetFiles();
            _allFolders.Add(this);
        }

        /// <summary>
        /// Sets up an instance of this given class acording to the parametars
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sph"></param>
        /// <param name="sphn"></param>
        public RadioSWSettingsFolders(string path, string radio, SweeperEveryHour sph, int sphn)
        {
            Folder = path;
            FolderName = new System.IO.DirectoryInfo("path").Name;
            PerHour = sph;
            PerHourNumber = sphn;
            RadioName = radio;
            GetFiles();
            _allFolders.Add(this);
        }

        /////////////////////////////////////////////////////////////////////////
        //////////////////////////////// METHODS ////////////////////////////////
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Creates the songs od all sweepers
        /// </summary>
        protected virtual void GenerateSongs()
        {
            AllSongs = new List<FirePlaySong>();
            Files.ForEach((X) => AllSongs.Add(new FirePlaySong(X)));
        }

        /// <summary>
        /// Changes the Exceptio file based on its index in the sweeper listview
        /// </summary>
        /// <param name="index"></param>
        public void ChangeExeption(int index)
        {
            string file = Files[index];
            if (ExceptionFiles.Contains(file))
            {
                ExceptionFiles.Remove(file);
                ExceptionFiles.Sort();
                FilesWithoutException.Add(file);
                FilesWithoutException.Sort();
            }
            else
            {
                ExceptionFiles.Add(file);
                ExceptionFiles.Sort();
                FilesWithoutException.Remove(file);
                FilesWithoutException.Sort();
            }
            UpdateFilesWithoutExceptionFiles();
            OnExceptionsChanged();
        }
        
        /// <summary>
        /// Changes the Exceptio file based on its filepath in the sweeper listview
        /// </summary>
        /// <param name="index"></param>
        public void ChangeExeption(string file_path)
        {
            if (ExceptionFiles.Contains(file_path))
            {
                ExceptionFiles.Remove(file_path);
                ExceptionFiles.Sort();
                FilesWithoutException.Add(file_path);
                FilesWithoutException.Sort();
            }
            else
            {
                ExceptionFiles.Add(file_path);
                ExceptionFiles.Sort();
                FilesWithoutException.Remove(file_path);
                FilesWithoutException.Sort();
            }
            UpdateFilesWithoutExceptionFiles();
            OnExceptionsChanged();
        }

        /// <summary>
        /// Updates the list without the exceptions
        /// </summary>
        public void UpdateFilesWithoutExceptionFiles()
        {
            FilesWithoutException = new List<string>();
            Files.ForEach((x) => FilesWithoutException.Add(x));
            ExceptionFiles.ForEach((y) => FilesWithoutException.Remove(y));
        }

        /// <summary>
        /// Gets all the files form the current folder
        /// </summary>
        public void GetFiles()
        {
            List<string> the_files = new List<string>();
            if (System.IO.Directory.Exists(Folder))
            {
                the_files = System.IO.Directory.GetFiles(Folder).ToList();
            }
            the_files.ForEach((x) =>
            {
                if ((x.ToLower().Contains("sw") || x.ToLower().Contains("dr") || x.ToLower().Contains(" j ") || x.ToLower().Contains("jingle")) && (System.IO.Path.GetExtension(x) == "mp3" || System.IO.Path.GetExtension(x) == "wav"))
                    Files.Add(x);
                FilesWithoutException.Add(x);
            });
            Files.Sort();
            FilesWithoutException.Sort();
            GenerateSongs();
        }

        /// <summary>
        /// Gets all the files form the current folder
        /// </summary>
        public void GetFiles(List<SweeperSelectSettingsForm.File> Filess)
        {
            List<string> the_files = new List<string>();
            foreach (SweeperSelectSettingsForm.File f in Filess)
            {
                the_files.Add(f.FilePath);   
            }
            the_files.ForEach((x) =>
            {
                //if ((x.ToLower().Contains("sw") || x.ToLower().Contains("dr") || x.ToLower().Contains(" j ") || x.ToLower().Contains("jingle")) && (System.IO.Path.GetExtension(x) == "mp3" || System.IO.Path.GetExtension(x) == "wav"))
                    Files.Add(x);
                
            });
            Files.Sort();
            FilesWithoutException.Sort();
            //GenerateSongs();
        }

        public void GetFiles(List<string> the_files)
        {
            the_files.ForEach((x) =>
            {
                if ((x.ToLower().Contains("sw") || x.ToLower().Contains("dr") || x.ToLower().Contains(" j ") || x.ToLower().Contains("jingle")) && (System.IO.Path.GetExtension(x) == "mp3" || System.IO.Path.GetExtension(x) == "wav"))
                    Files.Add(x);
                FilesWithoutException.Add(x);
            });
            Files.Sort();
            FilesWithoutException.Sort();
            GenerateSongs();
        }

        public List<string> GetFileNames()
        {
            List<string> temp = new List<string>();
            Files.ForEach((x) => temp.Add(new System.IO.DirectoryInfo(x).Name));
            return temp;
        }

        /// <summary>
        /// Returns the indices of the exception files (files to skip)
        /// </summary>
        /// <returns></returns>
        public List<int> GetIndicesOfExceptionFiles()
        {
            List<int> indices = new List<int>();
            ExceptionFiles.ForEach((x) => indices.Add(Files.IndexOf(x)));
            return indices;
        }

        /// <summary>
        /// Returns the song on the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public FirePlaySong GetSongByIndex(int index)
        {
            return AllSongs[index];
        }

        /// <summary>
        /// Returns the song for the given path
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public FirePlaySong GetSong(string path)
        {
            try
            {
                return AllSongs.Where((d) => d.Path == path).ToList()[0];
            }
            catch { throw; }
            return null;
        }

        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////  STATIC METHODS  //////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Returns a List of all RadioSWSettingsFolder for a given radio
        /// </summary>
        /// <param name="radio"></param>
        /// <returns></returns>
        public static List<RadioSWSettingsFolders> GetAllFoldersForRadio(string radio)
        {
            List<RadioSWSettingsFolders> temp = new List<RadioSWSettingsFolders>();
            return _allFolders.Where((z) => z.RadioName == radio).ToList();
        }

        /// <summary>
        /// Returns a List of names of the sweeper folders for a given radio
        /// </summary>
        /// <param name="radio"></param>
        /// <returns></returns>
        public static List<string> GetFolderNameListForRadio(string radio)
        {
            List<string> temp = new List<string>();
            try
            {
                _allFolders.Where((x) => x.RadioName == radio).ToList().ForEach((y) =>
                    {
                        temp.Add(new System.IO.DirectoryInfo(y.Folder).Name);
                    });
            }
            catch 
            {
                
            }
            return temp;
        }

        /// <summary>
        /// Returns a list of this object type form a list of paths
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<RadioSWSettingsFolders> CreateMany(string[] paths)
        {
            List<RadioSWSettingsFolders> temp = new List<RadioSWSettingsFolders>();
            paths.ToList().ForEach((x) => temp.Add(new RadioSWSettingsFolders(x)));
            return temp;
        }

        /// <summary>
        /// Returns a list of this object type form a list of paths
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<RadioSWSettingsFolders> CreateMany(List<string> paths)
        {
            List<RadioSWSettingsFolders> temp = new List<RadioSWSettingsFolders>();
            paths.ForEach((x) => temp.Add(new RadioSWSettingsFolders(x)));
            return temp;
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////  EVENTS  //////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////

        public delegate void ExceptionChangeEventHandler(object sender, EventArgs e);
        public event ExceptionChangeEventHandler ExceptionsChanged;

        public void OnExceptionsChanged()
        {
            if (ExceptionsChanged != null) ExceptionsChanged(this, new EventArgs());
        }
    }    
}
