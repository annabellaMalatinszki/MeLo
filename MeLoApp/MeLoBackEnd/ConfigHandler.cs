using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeLoBackEnd
{
    class ConfigHandler
    {
        AppConfig appConfig;
        Serializer serializer = new Serializer();

        private static string configFileName = "AppConfig.xml";

        public static string ConfigFileName
        {
            get { return configFileName = "AppConfig.xml"; }
        }


        public ConfigHandler()
        {
            if (File.Exists(configFileName))
            {
                appConfig = serializer.DeserializeConfig();
                // set the preferred filepath and the list of the recently opened files in the GUISupport according to AppConfig
            }
            else
            {
                appConfig = new AppConfig(null, null);
                serializer.SerializeConfig(appConfig);
            }
        }

        public void ChangePreferredPath(string Path)
        {
            appConfig.PreferredPath = Path;
            serializer.SerializeConfig(appConfig);
        }

        public void ChangeRecentlyOpenedFiles(List<OpenFileDialog> recentFiles)
        {
            appConfig.RecentlyOpenedFiles = recentFiles;
            serializer.SerializeConfig(appConfig);
        }
    }
}