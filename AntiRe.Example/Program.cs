using System;
using AntiRE.Runtime;
using System.Diagnostics;

namespace AntiRe.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "AntiRE.Example";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"

                 _   _ _____  ______   _____             _   _                
     /\         | | (_)  __ \|  ____| |  __ \           | | (_)               
    /  \   _ __ | |_ _| |__) | |__    | |__) |   _ _ __ | |_ _ _ __ ___   ___ 
   / /\ \ | '_ \| __| |  _  /|  __|   |  _  / | | | '_ \| __| | '_ ` _ \ / _ \
  / ____ \| | | | |_| | | \ \| |____ _| | \ \ |_| | | | | |_| | | | | | |  __/
 /_/    \_\_| |_|\__|_|_|  \_\______(_)_|  \_\__,_|_| |_|\__|_|_| |_| |_|\___|
                                                                              ");
            var CurrentProcess = Process.GetCurrentProcess();
            bool SelfDelete = false;
            bool ShowAlert = true;
            bool Aggressive = false;
            //Alert settings
            Alert.NotepadStyle = true;
            Alert.AutoClose = false;
            Alert.AutoCloseTime = 2;
            Alert.NotepadPath = "readme.txt";
            //Prevent assembly being dumped from memory
            AntiDump.Parse(typeof(Program /* or this.GetType() */));
            //Prevent application start under sandbox tools
            AntiSandBox.SelfDelete = SelfDelete;
            AntiSandBox.ShowAlert = ShowAlert;
            AntiSandBox.Parse(CurrentProcess);
            //Prevent application start under virtual machine
            AntiVirtualMachine.SelfDelete = SelfDelete;
            AntiVirtualMachine.ShowAlert = ShowAlert;
            AntiVirtualMachine.Parse(CurrentProcess);
            //Prevent from network being monitored
            AntiSniff.SelfDelete = SelfDelete;
            AntiSniff.ShowAlert = ShowAlert;
            AntiSniff.Parse(CurrentProcess);
            //Prevent reverse engineering tools from started on system
            AntiReverserTools.SelfDelete = SelfDelete;
            AntiReverserTools.ShowAlert = ShowAlert;
            AntiReverserTools.Aggressive = Aggressive;
            AntiReverserTools.IgnoreCase = true;
            AntiReverserTools.KeepAlive = true;
            AntiReverserTools.WhiteList.Add("notepad");
            AntiReverserTools.Start(CurrentProcess);
            //Anti debugger
            AntiDebugger.SelfDelete = SelfDelete;
            AntiDebugger.ShowAlert = ShowAlert;
            AntiDebugger.Aggressive = Aggressive;
            AntiDebugger.KeepAlive = true;
            AntiDebugger.Start(CurrentProcess);
            //Detect if dnspy installed on system
            AntiDnspy.SelfDelete = SelfDelete;
            AntiDnspy.ShowAlert = ShowAlert;
            AntiDnspy.SelfDelete = true;
            AntiDnspy.Parse(CurrentProcess);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\r\n [#] Application started successfully\r\n [#] Press any key to exit...");
            Console.ReadKey();
        }
    }
}
