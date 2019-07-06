﻿using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Utilities;

namespace AltInjector
{
    public partial class Form1 : Form
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string[] blacklist = File.ReadAllLines("blacklist.ini");

        private List<KeyValuePair<string, int>> processList = null;
        private globalKeyboardHook keyboardHook = null;
        private bool keyAlt = false,
                     keyX = false,
                     keyCombo = false;

        public Form1()
        {
            InitializeComponent();

            toolStripMenuItemHotkey.Checked = Properties.Settings.Default.keyboardShortcut;
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateProcessList()
        {
            this.toolStripMenuItemManual.DropDownItems.Clear();
            processList = new List<KeyValuePair<string, int>>();

            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    if (p.MainWindowTitle.Length > 0 && NativeMethods.GetProcessUser(p) == Environment.UserName)
                    {

                        bool isBlacklisted = Array.Exists(blacklist, x => x == p.ProcessName);
                        if (isBlacklisted == false)
                        {
                            System.Windows.Forms.ToolStripMenuItem newMenuItem = new System.Windows.Forms.ToolStripMenuItem(p.MainWindowTitle, null);
                            newMenuItem.Click += new System.EventHandler(this.ManualInjection_Click);
                            this.toolStripMenuItemManual.DropDownItems.Add(newMenuItem);

                            processList.Add(new KeyValuePair<string, int>(p.MainWindowTitle, p.Id));
                        }
                        else
                        {
                            logger.Warn("Skipped window from blacklisted process {ProcessName}: {WindowTitle}", p.ProcessName, p.MainWindowTitle);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void ManualInjection_Click(object sender, EventArgs e)
        {
            int processID = (from process in processList where process.Key == sender.ToString() select process.Value).FirstOrDefault();

            if(processID > 0)
            {
                logger.Info("Trying to manually inject into process {PID}", processID);
                NativeMethods.InjectDLL(processID);
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            PopulateProcessList();
        }
        private void toolStripMenuItemHotkey_CheckedChanged(object sender, EventArgs e)
        {
            if(toolStripMenuItemHotkey.Checked == true)
            {
                if(keyboardHook == null)
                {
                    keyboardHook = new globalKeyboardHook();
                    keyboardHook.HookedKeys.Add(Keys.LMenu); // Left Alt
                    keyboardHook.HookedKeys.Add(Keys.X); // X
                    keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
                    keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
                } else
                {
                    keyboardHook.hook();
                }
            } else
            {
                if (keyboardHook != null)
                {
                    keyboardHook.unhook();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.keyboardShortcut = toolStripMenuItemHotkey.Checked;
            Properties.Settings.Default.Save();
        }

        private void EditWhitelistiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string whitelistPath = DocumentsPath + "\\My Mods\\SpecialK\\Global\\whitelist.ini";
            if (!File.Exists(whitelistPath))
            {
                using (FileStream fs = File.Create(whitelistPath))
                {
                    fs.Close();
                }
            }
            Process.Start(whitelistPath);
        }

        private void OpenLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileTarget fileTarget = LogManager.Configuration.FindTargetByName<FileTarget>("logfile");
            if (fileTarget != null)
            {
                string fileName = fileTarget.FileName.ToString().Replace("'", "");
                Process.Start(fileName);
            }
        }

        private void OpenSpecialKFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SpecialK = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Mods\\SpecialK";
            if (Directory.Exists(SpecialK))
            {
                Process.Start(SpecialK);
            }
        }

        private void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LMenu:
                    this.keyAlt = false;
                    break;
                case Keys.X:
                    this.keyX = false;
                    break;
            }

            if(this.keyCombo && !this.keyAlt && !this.keyX)
            {
                this.keyCombo = !this.keyCombo; // Resets the state when all keys have been let go.
            }
        }

        private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LMenu:
                    this.keyAlt = true;
                    break;
                case Keys.X:
                    this.keyX = true;
                    break;
            }

            if(this.keyAlt && this.keyX && !this.keyCombo)
            {
                this.keyCombo = !this.keyCombo; // Prevents this section from executing more than once per key press combo
                NativeMethods.InjectDLLIntoActiveWindow();
            }
        }
    }
}
