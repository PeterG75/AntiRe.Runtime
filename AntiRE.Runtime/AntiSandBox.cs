using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System;

namespace AntiRE.Runtime
{
    public class AntiSandBox
    {
        /// <summary>
        /// Program will be closed and deleted if application started under sandbox (Default = false)
        /// </summary>
        public static bool SelfDelete = false;
        /// <summary>
        /// Show a alert message in notepad if application started under sandbox (Default = true)
        /// </summary>
        public static bool ShowAlert = true;
        /// <summary>
        /// Text of alert message
        /// </summary>
        public static string AlertMessage = "APPLICATION CANNOT BE STARTED UNDER SANDBOX";
        /// <summary>
        /// Check application running under sandbox
        /// </summary>
        public static void Parse(Process CurrentProcess)
        {
            try
            {
                File.WriteAllText(Application.StartupPath + "\\sandbox.txt", "isWriten");
                string input = File.ReadAllText(Application.StartupPath + "\\sandbox.txt");
                File.Delete(Application.StartupPath + "\\sandbox.txt");
                if (input != "isWriten")
                {
                    if (ShowAlert)
                        AutoClosingMessageBox.Show(AlertMessage,"",2000);
                    if (SelfDelete)
                    {
                        string location = CurrentProcess.MainModule.FileName;
                        Process.Start(new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + location + "\"")
                        {
                            WindowStyle = ProcessWindowStyle.Hidden
                        }).Dispose();
                        CurrentProcess.Kill();
                        Environment.Exit(0);
                    }
                    CurrentProcess.Kill();
                }
            }
            catch
            {
                if (ShowAlert)
                    AutoClosingMessageBox.Show(AlertMessage, "", 2000);
                if (SelfDelete)
                {
                    string location = CurrentProcess.MainModule.FileName;
                    Process.Start(new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" + location + "\"")
                    {
                        WindowStyle = ProcessWindowStyle.Hidden
                    }).Dispose();
                    CurrentProcess.Kill();
                    Environment.Exit(0);
                }
                CurrentProcess.Kill();
            }
        }
        
    }
}
