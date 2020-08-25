using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AntiRE.Runtime
{
    public class Alert
    {
        /// <summary>
		/// Style of alert message (true = Notepad, false = MessageBox : NotSecure)
		/// </summary>
        public static bool NotepadStyle = true;
        /// <summary>
		/// Auto close message box after preferred time (Default = false)
		/// </summary>
        public static bool AutoClose = false;
        /// <summary>
        /// Time (seconds) to close the message (Default = 2)
        /// </summary>
        public static int AutoCloseTime = 2;
        /// <summary>
        /// Notepad file path
        /// </summary>
        public static string NotepadPath = "readme.txt";
        public static void Show(string content)
        {
            if (NotepadStyle)
            {
                File.WriteAllText(Environment.CurrentDirectory + "\\" + NotepadPath, content);
                Process.Start(Environment.CurrentDirectory + "\\" + NotepadPath);
            }
            else
            {
                if (!AutoClose)
                    MessageBox.Show(content, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    AutoClosingMessageBox.Show(content, "", AutoCloseTime * 1000);
            }
        }
    }
}
