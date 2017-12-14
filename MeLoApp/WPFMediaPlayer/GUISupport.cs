using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMediaPlayer
{
    public class GUISupport
    {
        private List<OpenFileDialog> recentlyOpenedFiles = new List<OpenFileDialog>();
        private ObservableCollection<String> filenames = new ObservableCollection<string>();
        private CsvHandler csv = new CsvHandler();

        public List<OpenFileDialog> RecentlyOpenedFiles
        {
            get { return recentlyOpenedFiles; }
        }
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
                csv.SaveFilePath(openFileDialog.FileName);
                return new Uri(openFileDialog.FileName);
            }
            else
            {
                return null;
            }
        }
        public int Add(int num1, int num2)
        {
            int result = num1 + num2;
            return result;
        }

        public int Mul(int num1, int num2)
        {
            int result = num1 * num2;
            return result;
        }
    }
}
