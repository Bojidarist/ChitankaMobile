using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChitankaMobileUI.Configuration
{
    public class Config
    {
        public event Action OnConfigSaved;

        public Dictionary<string, object> Properties { get; set; }
        public string CfgPath { get; set; }

        public Config(string path = null)
        {
            if (path == null)
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appconfig.cfg");
            }
            if (!File.Exists(path))
            {
                File.Create(path);
                Properties = new Dictionary<string, object>();
            }
            else
            {
                CfgPath = path;
                Load();
            }
        }

        public void Load()
        {
            string json = File.ReadAllText(CfgPath);
            Properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (Properties == null)
            {
                Properties = new Dictionary<string, object>();
            }
        }

        public void Save()
        {
            var cfg = JsonConvert.SerializeObject(Properties);
            File.WriteAllText(CfgPath, cfg);
            OnConfigSaved?.Invoke();
        }
    }
}
