using System.IO;
using System.Xml.Serialization;

namespace MeLoBackEnd
{
    class Serializer
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));

        public void SerializeConfig(AppConfig appConfig)
        {
            TextWriter writer = new StreamWriter("AppConfig.xml", false);
            serializer.Serialize(writer, appConfig);
            writer.Close();
        }

        public AppConfig DeserializeConfig()
        {
            TextReader reader = new StreamReader("AppConfig.xml");
            AppConfig appConfig = (AppConfig)serializer.Deserialize(reader);
            reader.Close();

            return appConfig;
        }
    }
}
