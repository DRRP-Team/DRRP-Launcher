using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DRRP_Launcher {
    public partial class Window : Form {
        private string[] drrp_versions;
        private string[] gzdoom_versions;
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
                "Engines"
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

            var engines = (JArray)data["engines"];

            cmb_GZDoomVer.Items.Clear();

            foreach (var engine in engines) {
                cmb_GZDoomVer.Items.Add((string)engine["name"]);
            }

            var drrp_versions = (JArray)data["drrp_versions"];

            cmb_DRRPVer.Items.Clear();

            foreach (var version in drrp_versions) {
                cmb_DRRPVer.Items.Add((string)version["name"]);
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
    }
}
