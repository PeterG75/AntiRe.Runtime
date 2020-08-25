using System.Diagnostics;
using System;

namespace AntiRE.Runtime
{
    public class AntiVirtualMachine
    {
        /// <summary>
        /// Program will be closed and deleted if virtual machine detected (Default = false)
        /// </summary>
        public static bool SelfDelete = false;
        /// <summary>
        /// Text of alert message
        /// </summary>
        public static string AlertMessage = "APPLICATION CANNOT BE STARTED UNDER VIRTUAL MACHINE";
        /// <summary>
        /// Show a alert message in notepad if virtual machine detected (Default = true)
        /// </summary>
        public static bool ShowAlert = true;
        /// <summary>
        /// Check application running under virtual machine
        /// </summary>
        public static void Parse(Process CurrentProcess)
        {
            try
            {
                if (VirtualMachineDetector.Assert())
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
            catch
            {

            }

        }
    }
}
