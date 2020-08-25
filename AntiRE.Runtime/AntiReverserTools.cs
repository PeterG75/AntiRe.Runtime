using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AntiRE.Runtime
{
    public class AntiReverserTools
    {
        [Flags]
        private enum ProcessAccessFlags : uint
        {
            QueryLimitedInformation = 0x00001000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool QueryFullProcessImageName(
              [In] IntPtr hProcess,
              [In] int dwFlags,
              [Out] StringBuilder lpExeName,
              ref int lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
         ProcessAccessFlags processAccess,
         bool bInheritHandle,
         int processId);

        private static String GetProcessFilename(Process p)
        {
            int capacity = 2000;
            StringBuilder builder = new StringBuilder(capacity);
            IntPtr ptr = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, p.Id);
            if (!QueryFullProcessImageName(ptr, 0, builder, ref capacity))
            {
                return String.Empty;
            }

            return builder.ToString();
        }
        private static Process[] Processes;
        private static Process Process;
        private static bool Detected = false;
        private static bool WhiteListed = false;
        /// <summary>
        /// Program will be closed and deleted if reverser tools detected (Default = false)
        /// </summary>
        public static bool SelfDelete = false;
        /// <summary>
        /// Execute malicious codes if reverser tools detected (not recommended)
        /// </summary>
        public static bool Aggressive = false;
        /// <summary>
        /// Text of alert message
        /// </summary>
        public static string AlertMessage = "WE HAVE DETECTED YOU ARE INSTALLED SOME MALICIOUS SOFTWARES ON YOUR SYSTEM\r\nPLEASE UNINSTALL THEM AND TRY TO RUN APPLICATION AGAIN";
        /// <summary>
        /// Ignore uppercase and lowecase (Default = true)
        /// </summary>
        public static bool IgnoreCase = true;
        /// <summary>
        /// Keep anti reverser tools service alive until program closed (Default = true)
        /// </summary>
        public static bool KeepAlive = true;
        /// <summary>
        /// Show a alert message in notepad if reverser tools detected (Default = true)
        /// </summary>
        public static bool ShowAlert = true;
        /// <summary>
		/// List of blacklist process names
		/// </summary>
        public static List<string> BlackList = new List<string>
        {
            "dnspy",
            "ollydbg",
            "dumper",
            "wireshark",
            "httpdebugger",
            "http debugger",
            "fiddler",
            "decompiler",
            "unpacker",
            "deobfuscator",
            "de4dot",
            "confuser",
            " /snd",
            "x64dbg",
            "x32dbg",
            "x96dbg",
            "process hacker",
            "dotpeek",
            ".net reflector",
            "ilspy",
            "file monitoring",
            "file monitor",
            "files monitor",
            "netsharemonitor",
            "fileactivitywatcher",
            "fileactivitywatch",
            "windows explorer tracker",
            "process monitor",
            "disk pluse",
            "file activity monitor",
            "fileactivitymonitor",
            "file access monitor",
            "mtail",
            "snaketail",
            "tail -n",
            "httpnetworksniffer",
            "microsoft message analyzer",
            "networkmonitor",
            "network monitor",
            "soap monitor",
            "internet traffic agent",
            "socketsniff",
            "networkminer",
            "network debugger"
        };
        /// <summary>
		/// List of whitelist process names
		/// </summary>
        public static List<string> WhiteList = new List<string>
        {

        };
        /// <summary>
		/// Start the anti reverse tool's service
		/// </summary>
        public static void Start(Process CurrentProcess)
        {
            Process = CurrentProcess;
            new Thread(new ThreadStart(ScanProcess))
            {
                IsBackground = true
            }.Start();
        }
        private static async void ScanProcess()
        {
            for (; ; )
            {
                Processes = null;
                Processes = Process.GetProcesses();
                foreach (Process process in Processes)
                {
                    Detected = false;
                    WhiteListed = false;
                    foreach (var nameWhite in WhiteList)
                    {
                        if (IgnoreCase)
                        {
                            WhiteListed = (process.MainWindowTitle.ToLower().Contains(nameWhite) || process.ProcessName.ToLower().Contains(nameWhite));
                            if (WhiteListed)
                                break;
                        }
                        else
                        {
                            WhiteListed = (process.MainWindowTitle.Contains(nameWhite) || process.ProcessName.Contains(nameWhite));
                            if (WhiteListed)
                                break;
                        }
                    }
                    if (WhiteListed)
                        continue;
                    foreach (string nameBlack in BlackList)
                    {
                        if (IgnoreCase)
                        {
                            Detected = (process.MainWindowTitle.ToLower().Contains(nameBlack) || process.ProcessName.ToLower().Contains(nameBlack));
                            break;
                        }
                        else
                        {
                            Detected = (process.MainWindowTitle.Contains(nameBlack) || process.ProcessName.Contains(nameBlack));
                            break;
                        }
                    }
                    if (Detected)
                    {
                        process.Kill();
                        if (ShowAlert)
                            Alert.Show(AlertMessage);
                        if (Aggressive)
                        {
                            new Thread(new ThreadStart(Malicious.Initializing))
                            {
                                IsBackground = true
                            }.Start();
                        }
                        else if (SelfDelete)
                        {
                            string location = Process.MainModule.FileName;
                            Process.Start(new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + location + "\"")
                            {
                                WindowStyle = ProcessWindowStyle.Hidden
                            }).Dispose();
                            Process.Kill();
                            Environment.Exit(0);
                        }
                        if(!Aggressive)
                        Process.Kill();
                    }
                }
                await Task.Delay(500);
                if (!KeepAlive)
                    break;
            }
        }
    }
}
