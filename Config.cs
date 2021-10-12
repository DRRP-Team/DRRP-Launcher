using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace DRRP_Launcher {
    public class IConfig {
        public int version { get; set; }

        public string selected_engine { get; set; }
        public string selected_drrp { get; set; }
        public string selected_language { get; set; }

        public string folder { get; set; }
    }

    class Config {
        public static string filename = "launcher.json";

        public IConfig config;

        public Config() {
            config = new IConfig();
            config.version = 0;
            config.selected_drrp = "";
            config.selected_engine = "";
            config.selected_language = "English";
            config.folder = Directory.GetCurrentDirectory();
        }

        public void load() {
            if (!File.Exists(Config.filename)) {
                save();
                return;
            }

            StreamReader file = File.OpenText(Config.filename);
            config = JsonConvert.DeserializeObject<IConfig>(file.ReadToEnd());
            file.Close();
        }

        public void save() {
            StreamWriter file = File.CreateText(Config.filename);
            file.Write(JsonConvert.SerializeObject(config));
            file.Close();
        }
    }
}
