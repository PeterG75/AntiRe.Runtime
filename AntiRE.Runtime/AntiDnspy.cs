using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiRE.Runtime
{
    public class AntiDnspy
    {
        /// <summary>
        /// Program will be closed and deleted if detected dnspy on disk (Default = false)
        /// </summary>
        public static bool SelfDelete = false;
        /// <summary>
        /// Show a alert message in notepad if detected dnspy on disk (Default = true)
        /// </summary>
        public static bool ShowAlert = true;
        /// <summary>
        /// Text of alert message
        /// </summary>
        public static string AlertMessage = "DNSPY DETECTED ON DISK, APPLICATION CANNOT BE STARTED";
        /// <summary>
        /// Check for dnspy
        /// </summary>
        public static void Parse(Process CurrentProcess)
        {
            if (File.Exists(Environment.ExpandEnvironmentVariables("%appdata%") + "\\dnSpy\\dnSpy.xml"))
            {
                if (ShowAlert)
                    Alert.Show(AlertMessage);
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
