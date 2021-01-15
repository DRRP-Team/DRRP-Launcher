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

        private void Btn_DRRPUpdateVersions_Click(object sender, EventArgs e) {
            config.save();
        }

        private void initFolders() {
            string[] folders = {
                "DRRP",
                "Engines"
            };

            foreach (string folder in folders) {
                Directory.CreateDirectory(folder);
            }
        }



        private void Window_Load(object sender, EventArgs e) {
            initFolders();
            config.load();
        }
    }
}
