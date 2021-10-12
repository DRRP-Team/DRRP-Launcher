namespace DRRP_Launcher {
    partial class Window {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_refreshOnStart = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.in_mainFolder = new System.Windows.Forms.TextBox();
            this.btn_changeMainFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_openDRRPFolder = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_DRRPDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_DRRPUpdateVersions = new System.Windows.Forms.Button();
            this.cmb_DRRPVer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.in_args = new System.Windows.Forms.TextBox();
            this.btn_openGZDoomFolder = new System.Windows.Forms.Button();
            this.cmb_GZDoomLang = new System.Windows.Forms.ComboBox();
            this.cmb_GZDoomVer = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_GZDoomUpdateVersions = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_run = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lb_RunStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.chk_refreshOnStart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.in_mainFolder);
            this.groupBox1.Controls.Add(this.btn_changeMainFolder);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chk_refreshOnStart
            // 
            resources.ApplyResources(this.chk_refreshOnStart, "chk_refreshOnStart");
            this.chk_refreshOnStart.Checked = true;
            this.chk_refreshOnStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_refreshOnStart.Name = "chk_refreshOnStart";
            this.chk_refreshOnStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // in_mainFolder
            // 
            resources.ApplyResources(this.in_mainFolder, "in_mainFolder");
            this.in_mainFolder.Name = "in_mainFolder";
            this.in_mainFolder.ReadOnly = true;
            // 
            // btn_changeMainFolder
            // 
            resources.ApplyResources(this.btn_changeMainFolder, "btn_changeMainFolder");
            this.btn_changeMainFolder.Name = "btn_changeMainFolder";
            this.btn_changeMainFolder.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_openDRRPFolder);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.btn_DRRPUpdateVersions);
            this.groupBox2.Controls.Add(this.cmb_DRRPVer);
            this.groupBox2.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btn_openDRRPFolder
            // 
            resources.ApplyResources(this.btn_openDRRPFolder, "btn_openDRRPFolder");
            this.btn_openDRRPFolder.Name = "btn_openDRRPFolder";
            this.btn_openDRRPFolder.UseVisualStyleBackColor = true;
            this.btn_openDRRPFolder.Click += new System.EventHandler(this.Btn_openDRRPFolder_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_DRRPDate);
            this.groupBox4.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // txt_DRRPDate
            // 
            resources.ApplyResources(this.txt_DRRPDate, "txt_DRRPDate");
            this.txt_DRRPDate.Name = "txt_DRRPDate";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btn_DRRPUpdateVersions
            // 
            resources.ApplyResources(this.btn_DRRPUpdateVersions, "btn_DRRPUpdateVersions");
            this.btn_DRRPUpdateVersions.Name = "btn_DRRPUpdateVersions";
            this.btn_DRRPUpdateVersions.UseVisualStyleBackColor = true;
            this.btn_DRRPUpdateVersions.Click += new System.EventHandler(this.Btn_DRRPUpdateVersions_Click);
            // 
            // cmb_DRRPVer
            // 
            resources.ApplyResources(this.cmb_DRRPVer, "cmb_DRRPVer");
            this.cmb_DRRPVer.Name = "cmb_DRRPVer";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.in_args);
            this.groupBox3.Controls.Add(this.btn_openGZDoomFolder);
            this.groupBox3.Controls.Add(this.cmb_GZDoomLang);
            this.groupBox3.Controls.Add(this.cmb_GZDoomVer);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btn_GZDoomUpdateVersions);
            this.groupBox3.Controls.Add(this.label5);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // in_args
            // 
            resources.ApplyResources(this.in_args, "in_args");
            this.in_args.Name = "in_args";
            // 
            // btn_openGZDoomFolder
            // 
            resources.ApplyResources(this.btn_openGZDoomFolder, "btn_openGZDoomFolder");
            this.btn_openGZDoomFolder.Name = "btn_openGZDoomFolder";
            this.btn_openGZDoomFolder.UseVisualStyleBackColor = true;
            this.btn_openGZDoomFolder.Click += new System.EventHandler(this.Btn_openGZDoomFolder_Click);
            // 
            // cmb_GZDoomLang
            // 
            resources.ApplyResources(this.cmb_GZDoomLang, "cmb_GZDoomLang");
            this.cmb_GZDoomLang.Items.AddRange(new object[] {
            resources.GetString("cmb_GZDoomLang.Items"),
            resources.GetString("cmb_GZDoomLang.Items1")});
            this.cmb_GZDoomLang.Name = "cmb_GZDoomLang";
            // 
            // cmb_GZDoomVer
            // 
            resources.ApplyResources(this.cmb_GZDoomVer, "cmb_GZDoomVer");
            this.cmb_GZDoomVer.Name = "cmb_GZDoomVer";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btn_GZDoomUpdateVersions
            // 
            resources.ApplyResources(this.btn_GZDoomUpdateVersions, "btn_GZDoomUpdateVersions");
            this.btn_GZDoomUpdateVersions.Name = "btn_GZDoomUpdateVersions";
            this.btn_GZDoomUpdateVersions.UseVisualStyleBackColor = true;
            this.btn_GZDoomUpdateVersions.Click += new System.EventHandler(this.Btn_GZDoomUpdateVersions_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btn_run
            // 
            resources.ApplyResources(this.btn_run, "btn_run");
            this.btn_run.Name = "btn_run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.Btn_run_Click);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // lb_RunStatus
            // 
            resources.ApplyResources(this.lb_RunStatus, "lb_RunStatus");
            this.lb_RunStatus.Name = "lb_RunStatus";
            // 
            // Window
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_RunStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Window";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.Window_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox in_mainFolder;
        private System.Windows.Forms.Button btn_changeMainFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_DRRPUpdateVersions;
        private System.Windows.Forms.ComboBox cmb_DRRPVer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label txt_DRRPDate;
        private System.Windows.Forms.ComboBox cmb_GZDoomVer;
        private System.Windows.Forms.Button btn_GZDoomUpdateVersions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_GZDoomLang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_openDRRPFolder;
        private System.Windows.Forms.Button btn_openGZDoomFolder;
        private System.Windows.Forms.CheckBox chk_refreshOnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox in_args;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lb_RunStatus;
    }
}

