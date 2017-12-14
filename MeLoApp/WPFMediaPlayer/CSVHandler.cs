using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMediaPlayer
{
    class CsvHandler
    {
        string memoryPath = Environment.CurrentDirectory + "memoryTest.csv";

        public void SaveFilePath(string filePath)
        {
            File.AppendAllText(memoryPath, filePath + Environment.NewLine);
        }
        
        public string[] ReadBackMemoryFile()
        {
            if (File.Exists(memoryPath))
            {
                string[] readText = File.ReadAllLines(memoryPath);
                return readText;
            }
            return null;
        }
    }
}
