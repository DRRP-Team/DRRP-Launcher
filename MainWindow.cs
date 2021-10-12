using Alienlab.Zip;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DRRP_Launcher {
    public partial class Window : Form {
        private Dictionary<string, DrrpVersion> drrp_versions = new Dictionary<string, DrrpVersion>();
        private Dictionary<string, EngineVersion> gzdoom_versions = new Dictionary<string, EngineVersion>();
        private Config config;

        public Window() {
            InitializeComponent();
            config = new Config();
        }
        private void Window_Load(object sender, EventArgs e) {
            config.load();
            initFolders();
            fetchConfig();

            in_mainFolder.Text = config.config.folder;
            if (cmb_DRRPVer.SelectedIndex == -1) cmb_DRRPVer.SelectedIndex = 0;
            if (cmb_GZDoomLang.SelectedIndex == -1) cmb_GZDoomLang.SelectedIndex = 0;
            if (cmb_GZDoomVer.SelectedIndex == -1) cmb_GZDoomVer.SelectedIndex = 0;
        }

        private void Btn_DRRPUpdateVersions_Click(object sender, EventArgs e) {
            fetchConfig();
        }

        private void initFolders() {
            string[] folders = {
                "DRRP",
                "Engines",
                "Global", // doom2.wad and configuration will be stored here
                "Temp" // Download files will be stored here
            };

            foreach (string folder in folders) {
                Directory.CreateDirectory(config.config.folder + @"\" + folder);
            }
        }

        private void fetchConfig() {
            var data = Internet.GetJsonObject("https://raw.githubusercontent.com/DRRP-Team/DRRP-Launcher/master/launcher_data.json");
            
            if ((int)data["version"] != 0 ) {
                MessageBox.Show("Информация по версиям пришла для более поздней версии лаунчера! Пожалуйста, обновите ваш лаунчер.");
                return;
            }

            gzdoom_versions.Clear();

            var engines = (JArray)data["engines"];

            cmb_GZDoomVer.Items.Clear();

            foreach (var engine in engines) {
                cmb_GZDoomVer.Items.Add(engine["name"].ToString());
                gzdoom_versions.Add(
                    engine["name"].ToString(),
                    new EngineVersion(engine["name"].ToString(), engine["url"].ToString(), engine["foldername"].ToString())
                );
            }

            drrp_versions.Clear();

            var versions = (JArray)data["drrp_versions"];

            cmb_DRRPVer.Items.Clear();

            foreach (var version in versions) {
                cmb_DRRPVer.Items.Add((string)version["name"]);
                drrp_versions.Add(
                    version["name"].ToString(), 
                    new DrrpVersion(version["name"].ToString(), version["url"].ToString(), version["foldername"].ToString())
                );
            }
        }

        private void Btn_GZDoomUpdateVersions_Click(object sender, EventArgs e) {
            fetchConfig();
        }

        private void Btn_openDRRPFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", config.config.folder + @"\DRRP");
        }

        private void Btn_openGZDoomFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", config.config.folder + @"\Engines");
        }

        private void Btn_run_Click(object sender, EventArgs e) {
            progressBar.Value = 0;
            status("Подготовка к запуску...");

            if (cmb_DRRPVer.SelectedIndex == -1 || cmb_GZDoomLang.SelectedIndex == -1 || cmb_GZDoomVer.SelectedIndex == -1) {
                status("Ошибка! Выберите нужные версии и язык.");
                return;
            }

            run_InstallEngine();


            run_InstallDoom2();

            return;

            run_InstallDrrp();

            status("Настройка...");

            status("Запуск GZDoom с DRRP...");
            progressBar.Value = 100;
        }

        private void run_InstallEngine() {
            EngineVersion version = gzdoom_versions[cmb_GZDoomVer.SelectedItem.ToString()];
            DirectoryInfo extractDir = new DirectoryInfo(config.config.folder + @"\Engines\" + version.foldername);

            if (extractDir.Exists) {
                status($"{version.name} уже установлен.");
                return;
            }

            progressBar.Value = 0;
            
            status($"Скачивание {version.name}...");

            DirectoryInfo downloadDir = new DirectoryInfo(config.config.folder + @"\Temp");

            ClearDirectory(downloadDir);

            using (var client = new WebClient()) {
                client.DownloadFile(version.url, downloadDir.FullName + @"\Engine.zip");
            }

            progressBar.Value = 50;

            status($"Распаковка {version.name}...");

            extractDir.Create();

            using (var zipFile = new ZipFile(downloadDir.FullName + @"\Engine.zip")) {
                zipFile.ExtractAll(extractDir.FullName);
            }

            progressBar.Value = 100;
            status($"{version.name} успешно установлен!");
        }

        private void ClearDirectory(DirectoryInfo directory) {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        private void run_InstallDrrp() {
            status("Скачивание DRRP...");
        }

        private void run_InstallDoom2() {
            FileInfo doom2wad = new FileInfo(config.config.folder + @"\Global\doom2.wad");

            if (doom2wad.Exists) {
                status("doom2.wad уже установлен.");
                return;
            }

            progressBar.Value = 0;

            status("Скачивание doom2.wad...");

            string url = "https://github.com/Akbar30Bill/DOOM_wads/raw/master/doom2.wad";

            using (var client = new WebClient()) {
                client.DownloadFile(url, doom2wad.FullName);
            }

            progressBar.Value = 100;
            status("doom2.wad успешно установлен!");
        }

        private void status(string text) {
            lb_RunStatus.Text = text;
        }
    }

    public class DrrpVersion {
        public string name;
        public string url;
        public string foldername;

        public DrrpVersion(string _name, string _url, string _foldername) {
            name = _name;
            url = _url;
            foldername = _foldername;
        }
    }

    public class EngineVersion {
        public string name;
        public string url;
        public string foldername;

        public EngineVersion(string _name, string _url, string _foldername) {
            name = _name;
            url = _url;
            foldername = _foldername;
        }
    }
}
