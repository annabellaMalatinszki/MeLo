using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMediaPlayer
{
    class FileSystemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<FileInfo> _filesCollection = new List<FileInfo>();

        public List<FileInfo> FilesCollection
        {
            get { return _filesCollection; }
            set
            {
                _filesCollection = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FilesCollection"));
            }
        }

        public FileSystemViewModel()
        {
            UpdateCollection();
            FileSystem_WatchDog();
        }

        public void FileSystem_WatchDog()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"C:\Users\Zsolt Nagy\dotNET";

            /* Watch for changes in LastAccess and LastWrite times, and
                the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Watch all types of files.
            watcher.Filter = "*.*";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            UpdateCollection();
        }

        private void OnRenamed(object source, FileSystemEventArgs e)
        {
            UpdateCollection();
        }

        public void UpdateCollection()
        {
            string path = @"C:\Users\Zsolt Nagy\dotNET";
            DirectoryInfo di = new DirectoryInfo(path);
            FilesCollection = di.GetFiles().ToList();
        }
    }
}
