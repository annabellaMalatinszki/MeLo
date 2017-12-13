using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeLoBackEnd
{
    [Serializable]
    class AppConfig
    {
        private string preferredPath;

        public string PreferredPath
        {
            get { return preferredPath; }
            set { preferredPath = value; }
        }

        private List<OpenFileDialog> recentlyOpenedFiles = new List<OpenFileDialog>();
        
        public List<OpenFileDialog> RecentlyOpenedFiles
        {
            get { return recentlyOpenedFiles = new List<OpenFileDialog>(); }
            set { recentlyOpenedFiles = value; }
        }

        private List<string> recentFilePaths = new List<string>();

        public List<string> RecentFilePaths
        {
            get { return recentFilePaths; }
            set { recentFilePaths = value; }
        }


        public AppConfig(string preferredPath, List<OpenFileDialog> recentlyOpenedFiles)
        {
            PreferredPath = preferredPath;
            RecentlyOpenedFiles = recentlyOpenedFiles;
            RecentFilePaths.Clear();
            foreach (OpenFileDialog dialog in RecentlyOpenedFiles)
            {
                RecentFilePaths.Add(dialog.SafeFileName);
            }
        }


    }
}
