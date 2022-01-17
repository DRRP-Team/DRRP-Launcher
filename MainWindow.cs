using Alienlab.Zip;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.Drawing;

namespace DRRP_Launcher {
    public partial class Window : Form {
        private Dictionary<string, DrrpVersion> drrp_versions = new Dictionary<string, DrrpVersion>();
        private Dictionary<string, EngineVersion> gzdoom_versions = new Dictionary<string, EngineVersion>();
        private Dictionary<string, Pack> packs = new Dictionary<string, Pack>();
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
            in_args.Text = config.config.additional_args;

            cmb_pack.SelectedIndex = cmb_pack.FindStringExact(config.config.selected_pack);
            cmb_language.SelectedIndex = cmb_language.FindStringExact(config.config.selected_language);

            if (cmb_pack.SelectedIndex == -1) cmb_pack.SelectedIndex = 0;
            if (cmb_language.SelectedIndex == -1) cmb_language.SelectedIndex = 0;
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
                Directory.CreateDirectory(Path.Combine(config.config.folder, folder));
            }
        }

        private void fetchConfig() {
            string url = "https://raw.githubusercontent.com/DRRP-Team/DRRP-Launcher/master/launcher_data.json";
            var data = Internet.GetJsonObject(url);
            
            if ((int)data["version"] != 1) {
                MessageBox.Show("Update your launcher to continue.");
                //MessageBox.Show("Информация по версиям пришла для более поздней версии лаунчера! Пожалуйста, обновите ваш лаунчер.");
                return;
            }

            gzdoom_versions.Clear();

            var engines = (JArray)data["engines"];

            foreach (var engine in engines) {
                gzdoom_versions.Add(
                    engine["name"].ToString(),
                    new EngineVersion(engine["name"].ToString(), engine["url"].ToString(),
                    engine["foldername"].ToString(), engine["binaryname"].ToString())
                );
            }

            drrp_versions.Clear();

            var versions = (JArray)data["drrp_versions"];

            foreach (var version in versions) {
                drrp_versions.Add(
                    version["name"].ToString(), 
                    new DrrpVersion(
                        version["name"].ToString(), version["url"].ToString(),
                        version["foldername"].ToString(), (bool)version["force_download"])
                );
            }

            packs.Clear();

            var confpacks = (JArray)data["packs"];

            foreach (var pack in confpacks)
            {
                cmb_pack.Items.Add(pack["name"].ToString());
                packs.Add(
                    pack["name"].ToString(),
                    new Pack(pack["name"].ToString(), pack["engine"].ToString(), pack["drrp"].ToString(), pack["notes"].ToString())
                );
            }
        }

        private void Btn_GZDoomUpdateVersions_Click(object sender, EventArgs e) {
            fetchConfig();
        }

        private void Btn_openDRRPFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", Path.Combine(config.config.folder, "DRRP"));
        }

        private void Btn_openGZDoomFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", Path.Combine(config.config.folder, "Engines"));
        }

        private void Btn_run_Click(object sender, EventArgs e) {
            progressBar.Value = 0;
            status("Preparing to launch...");
            //status("Подготовка к запуску...");

            if (cmb_pack.SelectedIndex == -1 || cmb_language.SelectedIndex == -1) {
                MessageBox.Show("Error! You have to select language and pack to continue.", "Startup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Ошибка! Выберите нужную версию и язык.", "Ошибка запуска", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            config.config.additional_args = in_args.Text;
            config.save();

            run_InstallEngine();

            run_InstallDoom2();

            run_InstallDrrp();

            run_InstallConfig();

            // status("Настройка..."); // TODO: Download GZDoom config

            run_launch();
        }

        private void run_InstallEngine() {
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            EngineVersion version = gzdoom_versions[pack.engine];
            DirectoryInfo extractDir = new DirectoryInfo(Path.Combine(config.config.folder, "Engines", version.foldername));

            if (extractDir.Exists) {
                status($"{version.name} is already installed.");
                //status($"{version.name} уже установлен.");
                return;
            }

            progressBar.Value = 0;

            status($"Downloading {version.name}...");
            //status($"Скачивание {version.name}...");

            DirectoryInfo downloadDir = new DirectoryInfo(Path.Combine(config.config.folder, "Temp"));

            ClearDirectory(downloadDir);

            using (var client = new WebClient()) {
                client.DownloadFile(version.url, Path.Combine(downloadDir.FullName, "Engine.zip"));
            }

            progressBar.Value = 50;

            status($"Extracting {version.name}...");
            //status($"Распаковка {version.name}...");

            extractDir.Create();

            using (var zipFile = new ZipFile(Path.Combine(downloadDir.FullName, "Engine.zip"))) {
                zipFile.ExtractAll(extractDir.FullName);
            }

            ClearDirectory(downloadDir);

            progressBar.Value = 100;
            status($"{version.name} installed successfully!");
            //status($"{version.name} успешно установлен!");
        }

        private void ClearDirectory(DirectoryInfo directory) {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        private void run_InstallDrrp() {
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            DrrpVersion version = drrp_versions[pack.drrp];
            FileInfo drrpfilepk3 = new FileInfo(Path.Combine(config.config.folder, "DRRP", version.foldername + ".pk3"));

            if (drrpfilepk3.Exists && !version.forceDownload) {
                status($"{version.name} is already installed.");
                //status($"{version.name} уже установлен.");
                return;
            }

            progressBar.Value = 0;

            status($"Downloading {version.name}...");
            //status($"Скачивание {version.name}...");

            using (var client = new WebClient()) {
                client.DownloadFile(version.url, drrpfilepk3.FullName);
            }

            progressBar.Value = 100;
            status($"{version.name} installed successfully!");
            //status($"{version.name} успешно установлен!");
        }

        private void run_InstallDoom2() {
            FileInfo doom2wad = new FileInfo(Path.Combine(config.config.folder, "Global", "doom2.wad"));

            if (doom2wad.Exists) {
                status("doom2.wad is already installed.");
                //status("doom2.wad уже установлен.");
                return;
            }

            progressBar.Value = 0;

            status("Downloading doom2.wad...");
            //status("Скачивание doom2.wad...");

            string url = "https://github.com/Akbar30Bill/DOOM_wads/raw/master/doom2.wad";

            using (var client = new WebClient()) {
                client.DownloadFile(url, doom2wad.FullName);
            }

            progressBar.Value = 100;
            status("doom2.wad is already installed!");
            //status("doom2.wad успешно установлен!");
        }

        private void run_InstallConfig()
        {
            FileInfo gameconfig = new FileInfo(Path.Combine(config.config.folder, "Global", "config.ini"));

            if (gameconfig.Exists) {
                status("config.ini already exists.");
                //status("doom2.wad уже установлен.");
                return;
            }

            progressBar.Value = 0;

            status("Downloading config.ini...");
            //status("Скачивание doom2.wad...");

            string url = "https://raw.githubusercontent.com/DRRP-Team/DRRP-Launcher/master/config_default.ini";

            using (var client = new WebClient()) {
                client.DownloadFile(url, gameconfig.FullName);
            }

            progressBar.Value = 100;
            status("config.ini installed successfully!");
            //status("doom2.wad успешно установлен!");
        }

        private void run_launch() {
            Enabled = false;
            Update();
            progressBar.Value = 100;
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            EngineVersion engineVersion = gzdoom_versions[pack.engine];
            FileInfo engine = new FileInfo(Path.Combine(config.config.folder, "Engines", engineVersion.foldername, $"{engineVersion.binaryname}.exe"));

            if (!engine.Exists) {
                MessageBox.Show("Error! Engine binary was removed before running. Try again.", $"{engineVersion.binaryname}.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Ошибка! Файл порта был удалён перед попыткой запуска. Попробуйте ещё раз.", "gzdoom.exe не найден", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Enabled = true;
                return;
            }

            FileInfo doom2wad = new FileInfo(Path.Combine(config.config.folder, "Global", "doom2.wad"));

            if (!doom2wad.Exists) {
                MessageBox.Show("Error! File doom2.wad was removed before running. Try again.", "doom2.wad not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Ошибка! Файл doom2.wad был удалён перед попыткой запуска. Попробуйте ещё раз.", "doom2.wad не найден", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Enabled = true;
                return;
            }

            DrrpVersion drrpVersion = drrp_versions[pack.drrp];
            FileInfo drrpfilepk3 = new FileInfo(Path.Combine(config.config.folder, "DRRP", $"{drrpVersion.foldername}.pk3"));

            if (!drrpfilepk3.Exists) {
                MessageBox.Show("Error! DRRP package was removed before running. Try again.", "pk3 file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Ошибка! Файл мода был удалён перед попыткой запуска. Попробуйте ещё раз.", "pk3 файл не найден", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Enabled = true;
                return;
            }

            status($"Starting {pack.name}...");
            //status($"Запуск {engineVersion.name} с {drrpVersion.name}...");

            string[] args = {
                "-iwad", doom2wad.FullName,
                "-file", drrpfilepk3.FullName,
                "-config", Path.Combine(config.config.folder, "Global", "config.ini"),
                "+language", cmb_language.SelectedIndex == 1 ? "ru" : "en",
                in_args.Text
            };

            System.Diagnostics.Process a = System.Diagnostics.Process.Start(engine.FullName, String.Join(" ", args));
            a.WaitForExit();
            Enabled = true;
        }

        private void status(string text) {
            lb_RunStatus.Text = text;
        }

        private void Cmb_GZDoomLang_SelectedIndexChanged(object sender, EventArgs e) {
            config.config.selected_language = cmb_language.SelectedItem.ToString();
            config.save();
        }

        private void Btn_changeMainFolder_Click(object sender, EventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)) {
                    in_mainFolder.Text = dialog.SelectedPath;
                    config.config.folder = dialog.SelectedPath;
                    config.save();
                    initFolders();
                }
            }
        }

        private void Cmb_pack_SelectedIndexChanged(object sender, EventArgs e) {
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            lb_notes.Lines = pack.notes.Split('\n');
            config.config.selected_pack = pack.name;
            config.save();
        }
    }

    public class DrrpVersion {
        public string name;
        public string url;
        public string foldername;
        public bool forceDownload;

        public DrrpVersion(string _name, string _url, string _foldername, bool _forceDownload) {
            name = _name;
            url = _url;
            foldername = _foldername;
            forceDownload = _forceDownload;
        }
    }

    public class EngineVersion {
        public string name;
        public string url;
        public string foldername;
        public string binaryname;

        public EngineVersion(string _name, string _url, string _foldername, string _binaryname) {
            name = _name;
            url = _url;
            foldername = _foldername;
            binaryname = _binaryname;
        }
    }

    public class Pack {
        public string name;
        public string notes;
        public string engine;
        public string drrp;
        
        public Pack(string _name, string _engine, string _drrp, string _notes) {
            name = _name;
            engine = _engine;
            drrp = _drrp;
            notes = _notes;
        }
    }
}
