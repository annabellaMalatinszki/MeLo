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
        //List to store OpenFileDialogs of files loaded in the player
        private List<OpenFileDialog> openFileDialogList = new List<OpenFileDialog>();
        // List to store only the filenames of the dialogs
        private ObservableCollection<String> observableFileNames = new ObservableCollection<string>();
        private CsvHandler csv = new CsvHandler();
        private string mediafileFilter = "Media files (*.mp4;*.mp3;*.mpg;*.mpeg;*.jpg;*.png;*.gif)|" +
                "*.mp4;*.mp3;*.mpg;*.mpeg;*.jpg;*.png;*.gif|" +
                "All files (*.*)|*.*";

        public List<OpenFileDialog> RecentlyOpenedFiles
        {
            get { return openFileDialogList; }
        }

        public ObservableCollection<String> Filenames
        {
            get { return observableFileNames; }
        }

        public void ReadBackFilesFromMemory()
        {
            string[] savedFilePaths = csv.ReadBackMemoryFile();
            foreach (string filePath in savedFilePaths)
            {
                var openFileDialog = new OpenFileDialog
                {
                    FileName = filePath
                };
                AddItemsToTemporaryMemory(openFileDialog);
            }
        }

        private void AddItemsToTemporaryMemory(OpenFileDialog openFileDialog)
        {
            observableFileNames.Add(openFileDialog.SafeFileName);
            openFileDialogList.Add(openFileDialog);
        }

        public Uri OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = mediafileFilter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                AddItemsToTemporaryMemory(openFileDialog);
                csv.SaveFilePath(openFileDialog.FileName);
                return new Uri(openFileDialog.FileName);
            }
            else
            {
                return null;
            }
        }

        // Two sample methods used only for testing reasons
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
