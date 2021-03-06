﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AltInjector
{
    /* 
     * Is64Bit = https://stackoverflow.com/a/33206186
     * GetProcessUser = https://stackoverflow.com/a/38676215
     * InjectDLL = https://stackoverflow.com/a/51016927
     * InjectDLLIntoActiveWindow is based on https://social.msdn.microsoft.com/Forums/vstudio/en-US/1d8d5069-9451-4388-85ba-888fa29f4edf/how-to-get-the-active-application
     */

    internal static class NativeMethods
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string[] blacklist = File.ReadAllLines("blacklist.ini");

        /* ======================================================================================================= */

        // see https://msdn.microsoft.com/en-us/library/windows/desktop/ms684139%28v=vs.85%29.aspx
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);

        private static bool Is64Bit(Process process)
        {
            if (!Environment.Is64BitOperatingSystem)
                return false;

            if (!IsWow64Process(process.Handle, out bool isWow64))
                throw new Win32Exception();

            return !isWow64;
        }

        /* ======================================================================================================= */

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        private static extern Int32 CloseHandle(IntPtr hObject);

        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        public static bool InjectDLL(int processID)
        {
            bool injected = false;

            logger.Info(">>>InjectDLL({PID})", processID);
            try
            {
                Process targetProcess = Process.GetProcessById(processID);
                string DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                logger.Info("Target process name: {processName}", targetProcess.ProcessName);
                
                bool isBlacklisted = Array.Exists(blacklist, x => x == targetProcess.ProcessName.ToLower());
                if (isBlacklisted == false)
                {
                    if(TrayIconApp.WhitelistAutomatically)
                    {
                        TrayIconApp.AddProcessToWhitelist(targetProcess.ProcessName);
                    }

                    logger.Info("Trying to inject into {processName}", targetProcess.ProcessName);
                    if (Is64Bit(targetProcess))
                    {
                        logger.Info("Target is 64-bit process!");

                        // getting the handle of the process - with required privileges
                        IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);

                        // searching for the address of LoadLibraryA and storing it in a pointer
                        IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                        // name of the dll we want to inject
                        string dllName = DocumentsPath + "\\My Mods\\SpecialK\\SpecialK64.dll";

                        // alocating some memory on the target process - enough to store the name of the dll
                        // and storing its address in a pointer
                        IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

                        // writing the name of the dll there
                        WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out UIntPtr bytesWritten);

                        // creating a thread that will call LoadLibraryA with allocMemAddress as argument
                        IntPtr threadHandle = CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                        if(threadHandle == IntPtr.Zero)
                        {
                            logger.Error("Error when trying to inject into {processName}!", targetProcess.ProcessName);
                            
                        } else
                        {
                            logger.Info("Successfully injected into {processName}.", targetProcess.ProcessName);
                            injected = true;
                        }

                        // No need for the handles any longer
                        CloseHandle(threadHandle);
                        CloseHandle(procHandle);
                    }
                    else
                    {
                        logger.Info("Target is 32-bit process!", processID);
                        try
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo("32bitHelper.exe", processID.ToString());
                            startInfo.CreateNoWindow = true;
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                            logger.Info("Running '32bitHelper {PID}'", processID);
                            Process p = Process.Start(startInfo);
                            p.WaitForExit();
                            logger.Info("'32bitHelper {PID}' exited with {ExitCode}", processID, p.ExitCode);
                            p.Dispose();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Could not launch 32bitHelper.exe!");
                        }
                    }
                } else {
                    logger.Warn("Blacklisted process; aborting!");
                }

                targetProcess.Dispose();
            } catch { }

            logger.Info("<<<InjectDLL({PID})", processID);
            return injected;
        }

        /* ======================================================================================================= */

        [DllImport("user32.dll")]
        private static extern int GetForegroundWindow();

        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        public static void InjectDLLIntoActiveWindow()
        {
            logger.Info(">>>InjectDLLIntoActiveWindow()");
            Int32 processID = 0;

            GetWindowThreadProcessId(GetForegroundWindow(), out processID);
            logger.Info("GetWindowThreadProcessId(GetForegroundWindow(), out processID) returned {processID}", processID);

            if (processID != 0)
            {
                InjectDLL(processID);
            }

            logger.Info("<<<InjectDLLIntoActiveWindow()");
        }
    }
}
