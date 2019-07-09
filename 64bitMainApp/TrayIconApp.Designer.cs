﻿namespace AltInjector
{
    partial class TrayIconApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayIconApp));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuInject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHotkey = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWhitelistAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWhitelistEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenSpecialK = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuTrayIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMenuTrayIcon;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "SK-TinyInjector";
            this.trayIcon.Visible = true;
            // 
            // contextMenuTrayIcon
            // 
            this.contextMenuTrayIcon.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInject,
            this.menuSettings,
            this.menuHelp,
            this.menuExit});
            this.contextMenuTrayIcon.Name = "contextMenuStrip1";
            this.contextMenuTrayIcon.Size = new System.Drawing.Size(117, 92);
            this.contextMenuTrayIcon.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuTrayIcon_Opening);
            // 
            // menuInject
            // 
            this.menuInject.Name = "menuInject";
            this.menuInject.Size = new System.Drawing.Size(116, 22);
            this.menuInject.Text = "Inject";
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHotkey,
            this.menuWhitelistAuto,
            this.menuWhitelistEdit});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(116, 22);
            this.menuSettings.Text = "Settings";
            // 
            // menuHotkey
            // 
            this.menuHotkey.CheckOnClick = true;
            this.menuHotkey.Name = "menuHotkey";
            this.menuHotkey.Size = new System.Drawing.Size(212, 22);
            this.menuHotkey.Text = "Keyboard shortcut (Alt+X)";
            this.menuHotkey.CheckedChanged += new System.EventHandler(this.MenuHotkey_CheckedChanged);
            // 
            // menuWhitelistAuto
            // 
            this.menuWhitelistAuto.CheckOnClick = true;
            this.menuWhitelistAuto.Name = "menuWhitelistAuto";
            this.menuWhitelistAuto.Size = new System.Drawing.Size(212, 22);
            this.menuWhitelistAuto.Text = "Whitelist automatically";
            this.menuWhitelistAuto.CheckedChanged += new System.EventHandler(this.MenuWhitelistAuto_CheckedChanged);
            // 
            // menuWhitelistEdit
            // 
            this.menuWhitelistEdit.Name = "menuWhitelistEdit";
            this.menuWhitelistEdit.Size = new System.Drawing.Size(212, 22);
            this.menuWhitelistEdit.Text = "Edit whitelist.ini";
            this.menuWhitelistEdit.Click += new System.EventHandler(this.MenuWhitelistEdit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenLog,
            this.menuOpenSpecialK,
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(116, 22);
            this.menuHelp.Text = "Help";
            // 
            // menuOpenLog
            // 
            this.menuOpenLog.Name = "menuOpenLog";
            this.menuOpenLog.Size = new System.Drawing.Size(196, 22);
            this.menuOpenLog.Text = "Log";
            this.menuOpenLog.Click += new System.EventHandler(this.OpenLogFileToolStripMenuItem_Click);
            // 
            // menuOpenSpecialK
            // 
            this.menuOpenSpecialK.Name = "menuOpenSpecialK";
            this.menuOpenSpecialK.Size = new System.Drawing.Size(196, 22);
            this.menuOpenSpecialK.Text = "Browse Special K folder";
            this.menuOpenSpecialK.Click += new System.EventHandler(this.OpenSpecialKFolderToolStripMenuItem_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(196, 22);
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(116, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(307, 78);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "This window should not be visible. :)\r\nIf it is, then something probably broke.\r\n" +
    "\r\nUse the icon down in the notification area to manage the app.\r\n\r\n// SK-TinyInj" +
    "ector";
            // 
            // TrayIconApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 104);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TrayIconApp";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SK-TinyInjector";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrayIconWindow_FormClosing);
            this.contextMenuTrayIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem menuInject;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuHotkey;
        private System.Windows.Forms.ToolStripMenuItem menuWhitelistEdit;
        private System.Windows.Forms.ToolStripMenuItem menuOpenLog;
        private System.Windows.Forms.ToolStripMenuItem menuOpenSpecialK;
        private System.Windows.Forms.ToolStripMenuItem menuWhitelistAuto;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.TextBox textBox1;
    }
}