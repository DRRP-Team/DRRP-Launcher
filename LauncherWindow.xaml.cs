using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Alienlab.Zip;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Resources;
using System.Reflection;
using Infralution.Localization.Wpf;
using System.Globalization;

namespace DRRP_Launcher
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window {
        private Dictionary<string, DrrpVersion> drrp_versions = new Dictionary<string, DrrpVersion>();
        private Dictionary<string, EngineVersion> gzdoom_versions = new Dictionary<string, EngineVersion>();
        private Dictionary<string, Pack> packs = new Dictionary<string, Pack>();
        private Config config = new Config();

        private JObject launcherConfig;

        public LauncherWindow() {
            InitializeComponent();
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_BeforeLoad(object sender, EventArgs e) {
            config.load();
            updateLocalization();
        }

        private void Window_Load(object sender, EventArgs e) {
            initFolders();
            fetchConfig();

            in_mainFolder.Text = config.config.folder;
            in_args.Text = config.config.additional_args;

            cmb_performance.Items.Add(Properties.Resources.performance_ugly);
            cmb_performance.Items.Add(Properties.Resources.performance_low);
            cmb_performance.Items.Add(Properties.Resources.performance_normal);
            cmb_performance.Items.Add(Properties.Resources.performance_high);
            cmb_performance.Items.Add(Properties.Resources.performance_ultra);
            cmb_performance.Items.Add(Properties.Resources.performance_custom);

            cmb_language.Items.Add("English");
            cmb_language.Items.Add("Русский");

            cmb_pack.SelectedIndex = cmb_pack.Items.IndexOf(config.config.selected_pack);
            cmb_language.SelectedIndex = cmb_language.Items.IndexOf(config.config.selected_language);
            cmb_performance.SelectedIndex = config.config.selected_performance;

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
                Directory.CreateDirectory(System.IO.Path.Combine(config.config.folder, folder));
            }
        }

        private void fetchConfig() {
            // const string url = "https://raw.githubusercontent.com/DRRP-Team/DRRP-Launcher/master/launcher_data.json";
            const string url = "https://raw.githubusercontent.com/DRRP-Team/DRRP-Launcher/feat/wpf/launcher_data.json";

            const string lastLoadedConfigFilename = "last_loaded_launcher_data.json";

            try {
                launcherConfig = Internet.GetJsonObject(url);

                StreamWriter file = File.CreateText(lastLoadedConfigFilename);
                file.Write(JsonConvert.SerializeObject(launcherConfig));
                file.Close();
            } catch (Exception e) {
                Console.WriteLine($"Failed to fetch launcher data {e.Message}");
                // Try to load previous config
                if (File.Exists(lastLoadedConfigFilename)) {
                    StreamReader file = File.OpenText(lastLoadedConfigFilename);
                    launcherConfig = JsonConvert.DeserializeObject<JObject>(file.ReadToEnd());
                    file.Close();
                } else {
                    // If there's no config, notify that launcher requires internet connection
                    MessageBox.Show("DRRP Launcher requires Internet connection, at least for the first run", "No Internet connection :(");
                    Close();
                    return;
                }
            }

            var data = launcherConfig;
            
            if ((int)data["version"] > 2) {
                MessageBox.Show(Properties.Resources.error_upgradeRequired);
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

            foreach (var pack in confpacks) {
                var notes = (JObject)pack["notes"];
                cmb_pack.Items.Add(pack["name"].ToString());
                packs.Add(
                    pack["name"].ToString(),
                    new Pack(pack["name"].ToString(), pack["engine"].ToString(), pack["drrp"].ToString(), notes["English"].ToString(), notes["Русский"].ToString())
                );
            }
        }

        private void Btn_GZDoomUpdateVersions_Click(object sender, EventArgs e) {
            fetchConfig();
        }

        private void Btn_openBaseFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", config.config.folder);
        }

        private void Btn_openDRRPFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", System.IO.Path.Combine(config.config.folder, "DRRP"));
        }

        private void Btn_openGZDoomFolder_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("explorer.exe", System.IO.Path.Combine(config.config.folder, "Engines"));
        }

        private async void Btn_run_Click(object sender, EventArgs e) {
            IsEnabled = false;
            //progressBar.Value = 0;
            status(Properties.Resources.status_preparingToLaunch);

            if (cmb_pack.SelectedIndex == -1 || cmb_language.SelectedIndex == -1) {
                System.Windows.Forms.MessageBox.Show(
                    Properties.Resources.error_noLanguageOrPack,
                    Properties.Resources.error_startupErrorTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            config.config.additional_args = in_args.Text;
            config.save();

            try {

                if (!await run_InstallEngine()) return;

                if (!await run_InstallDoom2()) return;

                if (!await run_InstallDrrp()) return;

                if (!await run_InstallConfig()) return;

                if (!await run_launch()) return;

            } finally {
                IsEnabled = true;
            }
        }

        private async Task<bool> run_InstallEngine() {
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            EngineVersion version = gzdoom_versions[pack.engine];
            DirectoryInfo extractDir = new DirectoryInfo(System.IO.Path.Combine(config.config.folder, "Engines", version.foldername));

            if (extractDir.Exists) {
                status(string.Format(Properties.Resources.status_alreadyInstalled, version.name));
                return true;
            }

            //progressBar.Value = 0;

            status(string.Format(Properties.Resources.status_downloading, version.name));

            DirectoryInfo downloadDir = new DirectoryInfo(System.IO.Path.Combine(config.config.folder, "Temp"));

            ClearDirectory(downloadDir);

            using (var client = new WebClient()) {
                await client.DownloadFileTaskAsync(version.url, System.IO.Path.Combine(downloadDir.FullName, "Engine.zip"));
            }

            //progressBar.Value = 50;

            status(string.Format(Properties.Resources.status_extracting, version.name));

            extractDir.Create();

            using (var zipFile = new ZipFile(System.IO.Path.Combine(downloadDir.FullName, "Engine.zip"))) {
                zipFile.ExtractAll(extractDir.FullName);
            }

            ClearDirectory(downloadDir);

            //progressBar.Value = 100;
            status(string.Format(Properties.Resources.status_installed, version.name));

            return true;
        }

        private void ClearDirectory(DirectoryInfo directory) {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        private async Task<bool> run_InstallDrrp() {
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            DrrpVersion version = drrp_versions[pack.drrp];
            FileInfo drrpfilepk3 = new FileInfo(System.IO.Path.Combine(config.config.folder, "DRRP", version.foldername + ".pk3"));          

            if (drrpfilepk3.Exists && !version.forceDownload) {
                status(string.Format(Properties.Resources.status_alreadyInstalled, version.name));
                return true;
            }

            //progressBar.Value = 0;

            status(string.Format(Properties.Resources.status_downloading, version.name));

            using (var client = new WebClient()) {
                await client.DownloadFileTaskAsync(version.url, drrpfilepk3.FullName);
            }

            //progressBar.Value = 100;
            status(string.Format(Properties.Resources.status_installed, version.name));

            return true;
        }

        private async Task<bool> run_InstallDoom2() {
            FileInfo doom2wad = new FileInfo(System.IO.Path.Combine(config.config.folder, "Global", "doom2.wad"));

            if (doom2wad.Exists) {
                status(string.Format(Properties.Resources.status_alreadyInstalled, "doom2.wad"));

                return true;
            }

            //progressBar.Value = 0;

            status(string.Format(Properties.Resources.status_downloading, "doom2.wad"));

            string url = "https://github.com/Akbar30Bill/DOOM_wads/raw/master/doom2.wad";

            using (var client = new WebClient()) {
                await client.DownloadFileTaskAsync(url, doom2wad.FullName);
            }

            //progressBar.Value = 100;
            status(string.Format(Properties.Resources.status_installed, "doom2.wad"));

            return true;
        }

        private async Task<bool> run_InstallConfig()
        {
            FileInfo gameconfig = new FileInfo(System.IO.Path.Combine(config.config.folder, "Global", "config.ini"));

            if (gameconfig.Exists) {
                status(string.Format(Properties.Resources.status_alreadyExists, "config.ini"));

                return true;
            }

            //progressBar.Value = 0;

            status(string.Format(Properties.Resources.status_downloading, "config.ini"));

            string url = "https://raw.githubusercontent.com/DRRP-Team/DRRP-Launcher/master/config_default.ini";

            using (var client = new WebClient()) {
                await client.DownloadFileTaskAsync(url, gameconfig.FullName);
            }

            //progressBar.Value = 100;
            status(string.Format(Properties.Resources.status_installed, "config.ini"));

            return true;
        }

        private Task<bool> run_launch() {
            //Update();
            //progressBar.Value = 100;
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            EngineVersion engineVersion = gzdoom_versions[pack.engine];
            FileInfo engine = new FileInfo(System.IO.Path.Combine(config.config.folder, "Engines", engineVersion.foldername, $"{engineVersion.binaryname}.exe"));

            if (!engine.Exists) {
                System.Windows.Forms.MessageBox.Show(
                    Properties.Resources.error_binaryNotFound, string.Format(Properties.Resources.error_somethingNotFoundTitle, $"{engineVersion.binaryname}.exe"),
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error
                );
                return Task.FromResult(false);
            }

            FileInfo doom2wad = new FileInfo(System.IO.Path.Combine(config.config.folder, "Global", "doom2.wad"));

            if (!doom2wad.Exists) {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.error_doom2NotFound, string.Format(Properties.Resources.error_somethingNotFoundTitle, "doom2.wad"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); ;
                return Task.FromResult(false);
            }

            DrrpVersion drrpVersion = drrp_versions[pack.drrp];
            FileInfo drrpfilepk3 = new FileInfo(System.IO.Path.Combine(config.config.folder, "DRRP", $"{drrpVersion.foldername}.pk3"));

            if (!drrpfilepk3.Exists) {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.error_pk3NotFound, Properties.Resources.error_pk3NotFoundTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Task.FromResult(false);
            }

            status(string.Format(Properties.Resources.status_launching, pack.name));

            var performanceSettings = (JArray)launcherConfig["performances"];
            string performanceArgs = performanceSettings[cmb_performance.SelectedIndex].ToString();

            DateTime startTime = DateTime.Now;

            string[] args = {
                "-iwad", $"\"{doom2wad.FullName}\"",
                "-file", $"\"{drrpfilepk3.FullName}\"",
                "-config", $"\"{System.IO.Path.Combine(config.config.folder, "Global", "config.ini")}\"",
                "+language", cmb_language.SelectedIndex == 1 ? "ru" : "en",
                performanceArgs,
                in_args.Text
            };

            WindowState = WindowState.Minimized;

            System.Diagnostics.Process a = System.Diagnostics.Process.Start(engine.FullName, String.Join(" ", args));
            a.WaitForExit();
            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            status(string.Format(Properties.Resources.status_statistics, duration.Hours, duration.Minutes, duration.Seconds));
            WindowState = WindowState.Normal;
            Activate();

            return Task.FromResult(true);
        }

        private void status(string text) {
            lb_RunStatus.Content = text;
            lb_RunStatus.UpdateLayout();    
        }

        private void Cmb_GZDoomLang_SelectedIndexChanged(object sender, EventArgs e) {
            if (config.config.selected_language == cmb_language.SelectedItem.ToString()) return;

            config.config.selected_language = cmb_language.SelectedItem.ToString();
            config.save();
            updateLocalization(true);
        }

        private void updateLocalization(bool repaint=false) {
            var langCode = config.config.selected_language == "Русский" ? "ru" : "en";
            CultureManager.UICulture = new CultureInfo(langCode);
            if (repaint) {
                Cmb_pack_SelectedIndexChanged(null, null);

                cmb_performance.Items.Clear();
                cmb_performance.Items.Add(Properties.Resources.performance_ugly);
                cmb_performance.Items.Add(Properties.Resources.performance_low);
                cmb_performance.Items.Add(Properties.Resources.performance_normal);
                cmb_performance.Items.Add(Properties.Resources.performance_high);
                cmb_performance.Items.Add(Properties.Resources.performance_ultra);
                cmb_performance.Items.Add(Properties.Resources.performance_custom);
                cmb_performance.SelectedIndex = config.config.selected_performance;
            }
        }

        private void Btn_changeMainFolder_Click(object sender, EventArgs e) {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog()) {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath)) {
                    in_mainFolder.Text = dialog.SelectedPath;
                    config.config.folder = dialog.SelectedPath;
                    config.save();
                    initFolders();
                }
            }
        }

        private void Cmb_pack_SelectedIndexChanged(object sender, EventArgs e) {
            Pack pack = packs[cmb_pack.SelectedItem.ToString()];
            lb_notes.Text = config.config.selected_language == "Русский" ? pack.notes_ru : pack.notes_en; //.Split('\n');
            config.config.selected_pack = pack.name;
            config.save();
        }
    }
}
