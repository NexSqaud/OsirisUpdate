using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsirisUpdater
{
    class JsonClass
    {
        public void LoadConfig(ref Config config)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "settings.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject<Config>(json);
            }
        }

        public void SaveConfig(Config config)
        {
            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "settings.json"), json);
        }

    }

    public struct Config
    {
        public int state;
        public string pathToOsiris;
        public string pathToBuildTools;
        public string pathToDll;
        public string pathToInjector;
        public int injectType;
    }
}
