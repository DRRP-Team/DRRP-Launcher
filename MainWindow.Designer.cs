﻿namespace DRRP_Launcher {
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.in_mainFolder = new System.Windows.Forms.TextBox();
            this.in_args = new System.Windows.Forms.TextBox();
            this.btn_changeMainFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_GZDoomLang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_RunStatus = new System.Windows.Forms.Label();
            this.btn_run = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btn_openDRRPFolder = new System.Windows.Forms.Button();
            this.cmb_DRRPVer = new System.Windows.Forms.ComboBox();
            this.btn_openGZDoomFolder = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lb_notes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.in_mainFolder);
            this.groupBox1.Controls.Add(this.in_args);
            this.groupBox1.Controls.Add(this.btn_changeMainFolder);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // in_args
            // 
            resources.ApplyResources(this.in_args, "in_args");
            this.in_args.Name = "in_args";
            // 
            // btn_changeMainFolder
            // 
            resources.ApplyResources(this.btn_changeMainFolder, "btn_changeMainFolder");
            this.btn_changeMainFolder.Name = "btn_changeMainFolder";
            this.btn_changeMainFolder.UseVisualStyleBackColor = true;
            this.btn_changeMainFolder.Click += new System.EventHandler(this.Btn_changeMainFolder_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_GZDoomLang);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lb_RunStatus);
            this.groupBox2.Controls.Add(this.btn_run);
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Controls.Add(this.btn_openDRRPFolder);
            this.groupBox2.Controls.Add(this.cmb_DRRPVer);
            this.groupBox2.Controls.Add(this.btn_openGZDoomFolder);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // cmb_GZDoomLang
            // 
            this.cmb_GZDoomLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_GZDoomLang, "cmb_GZDoomLang");
            this.cmb_GZDoomLang.Items.AddRange(new object[] {
            resources.GetString("cmb_GZDoomLang.Items"),
            resources.GetString("cmb_GZDoomLang.Items1")});
            this.cmb_GZDoomLang.Name = "cmb_GZDoomLang";
            this.cmb_GZDoomLang.SelectedIndexChanged += new System.EventHandler(this.Cmb_GZDoomLang_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lb_RunStatus
            // 
            this.lb_RunStatus.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lb_RunStatus, "lb_RunStatus");
            this.lb_RunStatus.Name = "lb_RunStatus";
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
            // btn_openDRRPFolder
            // 
            resources.ApplyResources(this.btn_openDRRPFolder, "btn_openDRRPFolder");
            this.btn_openDRRPFolder.Name = "btn_openDRRPFolder";
            this.btn_openDRRPFolder.UseVisualStyleBackColor = true;
            this.btn_openDRRPFolder.Click += new System.EventHandler(this.Btn_openDRRPFolder_Click);
            // 
            // cmb_DRRPVer
            // 
            this.cmb_DRRPVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmb_DRRPVer, "cmb_DRRPVer");
            this.cmb_DRRPVer.Name = "cmb_DRRPVer";
            this.cmb_DRRPVer.SelectedIndexChanged += new System.EventHandler(this.Cmb_DRRPVer_SelectedIndexChanged);
            // 
            // btn_openGZDoomFolder
            // 
            resources.ApplyResources(this.btn_openGZDoomFolder, "btn_openGZDoomFolder");
            this.btn_openGZDoomFolder.Name = "btn_openGZDoomFolder";
            this.btn_openGZDoomFolder.UseVisualStyleBackColor = true;
            this.btn_openGZDoomFolder.Click += new System.EventHandler(this.Btn_openGZDoomFolder_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lb_notes);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // lb_notes
            // 
            resources.ApplyResources(this.lb_notes, "lb_notes");
            this.lb_notes.Name = "lb_notes";
            // 
            // Window
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox in_mainFolder;
        private System.Windows.Forms.Button btn_changeMainFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_DRRPVer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label lb_notes;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_openDRRPFolder;
        private System.Windows.Forms.Button btn_openGZDoomFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox in_args;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lb_RunStatus;
        private System.Windows.Forms.ComboBox cmb_GZDoomLang;
    }
}

