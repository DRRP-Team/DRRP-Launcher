using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace DRRP_Launcher {
    public class IConfig {
        public int version { get; set; }

        public string selected_pack { get; set; }
        public string selected_language { get; set; }
        public string additional_args { get; set; }

        public string folder { get; set; }
    }

    class Config {
        public static string filename = "launcher.json";

        public IConfig config;

        public Config() {
            config = new IConfig();
            config.version = 1;
            config.selected_pack = "";
            config.selected_language = "English";
            config.additional_args = "";
            config.folder = Directory.GetCurrentDirectory();
        }

        public void load() {
            if (!File.Exists(filename)) {
                save();
                return;
            }

            StreamReader file = File.OpenText(filename);
            config = JsonConvert.DeserializeObject<IConfig>(file.ReadToEnd());
            file.Close();
        }

        public void save() {
            StreamWriter file = File.CreateText(filename);
            file.Write(JsonConvert.SerializeObject(config));
            file.Close();
        }
    }
}
