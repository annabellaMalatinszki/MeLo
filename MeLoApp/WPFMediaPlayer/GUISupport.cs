using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMediaPlayer
{
    class GUISupport
    {
        List<OpenFileDialog> recentlyOpenedFiles = new List<OpenFileDialog>();
        private ObservableCollection<String> filenames = new ObservableCollection<string>();

        public ObservableCollection<String> Filenames
        {
            get { return filenames; }
        }

        public Uri OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp4;*.mp3;*.mpg;*.mpeg;*.jpg;*.png;*.gif)|" +
                "*.mp4;*.mp3;*.mpg;*.mpeg;*.jpg;*.png;*.gif|" +
                "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                recentlyOpenedFiles.Add(openFileDialog);
                filenames.Add(openFileDialog.SafeFileName);
                return new Uri(openFileDialog.FileName);
            }
            else
            {
                return null;
            }
        }
    }
}
