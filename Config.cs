using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace DRRP_Launcher
{
    public class IEngine {
        public int version { get; set; }
        public string binary { get; set; }
    }

    public class IConfig
    {
        public int version { get; set; }
        public IEngine[] engines { get; set; }
        public string engines_api { get; set; }
        public int engine_version { get; set; }

        public string drrp_version { get; set; }
        public string drrp_repository { get; set; }

        public bool fetch_on_load { get; set; }

        public string folder { get; set; }
    }

    class Config {
        public static string filename = "launcher.json";

        public IConfig config;

        public Config() {
            config = new IConfig();
            config.version = 0;
            config.drrp_repository = "https://github.com/DRRP-Team/DRRP";
            config.drrp_version = "0.4.0";
            config.engines_api = "";
            config.engine_version = 351;
            config.fetch_on_load = true;
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
