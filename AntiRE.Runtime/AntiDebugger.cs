using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AntiRE.Runtime
{
    public class AntiDebugger
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        internal static extern IntPtr VM34523865(string a);
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        internal static extern IntPtr VM89283879(IntPtr a, string b);
        [DllImport("kernel32.dll", EntryPoint = "VirtualProtect")]
        internal static extern IntPtr VM607063457(IntPtr a, int b, uint c, ref uint d);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
        private static bool isDebuggerPresent = false;
        private static Process currentProcess;
        private static bool VM1094388875()
        {
            return false;
        }
        /// <summary>
        /// Program will be closed and deleted if debugger detected (Default = false)
        /// </summary>
        public static bool SelfDelete = false;
        /// <summary>
        /// Execute malicious codes if debugger detected (not recommended)
        /// </summary>
        public static bool Aggressive = false;
        /// <summary>
        /// Text of alert message
        /// </summary>
        public static string AlertMessage = "DEBUGGER DETECTED ON MACHINE, YOU CANNOT USE DEBUGGER TOOLS";
        /// <summary>
        /// Keep anti debugger service alive until program closed (Default = true)
        /// </summary>
        public static bool KeepAlive = true;
        /// <summary>
        /// Show a alert message in notepad if debugger detected (Default = true)
        /// </summary>
        public static bool ShowAlert = true;
        /// <summary>
		/// Start the anti debugger service
		/// </summary>
        public static async void Start(Process CurrentProcess)
        {
            for (; ; )
            {
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
                if (isDebuggerPresent)
                {
                    if (ShowAlert)
                        Alert.Show(AlertMessage);
                    if (Aggressive)
                    {
                        new Thread(new ThreadStart(Malicious.Initializing))
                        {
                            IsBackground = true
                        }.Start();
                        return;
                    }
                    if (!Aggressive)
                        Process.GetCurrentProcess().Kill();
                }
                if (Debugger.IsAttached || Debugger.IsLogging())
                {
                    if (ShowAlert)
                        Alert.Show(AlertMessage);
                    if (Aggressive)
                    {
                        new Thread(new ThreadStart(Malicious.Initializing))
                        {
                            IsBackground = true
                        }.Start();
                        return;
                    }
                    else if (SelfDelete)
                    {
                        string location = CurrentProcess.MainModule.FileName;
                        Process.Start(new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + location + "\"")
                        {
                            WindowStyle = ProcessWindowStyle.Hidden
                        }).Dispose();
                        CurrentProcess.Kill();
                        Environment.Exit(0);
                    }
                    if (!Aggressive)
                        Process.GetCurrentProcess().Kill();
                }
                await Task.Delay(200);
                if (!KeepAlive)
                    break;
            }
        }

    }

}
