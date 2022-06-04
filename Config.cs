using System.IO;
using Newtonsoft.Json;


namespace DRRP_Launcher {
    public class IConfig {
        public int version { get; set; }

        public string selected_pack { get; set; }
        public string selected_language { get; set; }
        public int selected_performance { get; set; } // 0 - ugly, 1 - low, 2 - normal, 3 - high, 4 - ultra, 5 - custom

        public string additional_args { get; set; }

        public string folder { get; set; }
    }

    class Config {
        public static string filename = "launcher.json";

        public IConfig config;

        public Config() {
            config = new IConfig();
            config.version = 2;
            config.selected_pack = "";
            config.selected_language = "English";
            config.selected_performance = 3;
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
