using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DRRP_Launcher {
    public partial class Window : Form {
        private string[] drrp_versions;
        private string[] gzdoom_versions;

        public Window() {
            InitializeComponent();
        }

        private void Btn_DRRPUpdateVersions_Click(object sender, EventArgs e) {
            //
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
        }
    }
}
