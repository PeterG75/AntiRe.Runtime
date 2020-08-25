# AntiRE.Runtime
About :
-------
A open source .net library for prevent from assembly being reverse engineered

Usage :
-------
Namespaces :

```C#
    using System;
    using AntiRE.Runtime;
    using System.Diagnostics;
```

Prevent assembly being dumped from memory

```C#
  AntiDump.Parse(typeof(this.GetType()));
```

Get current process

```C#
     var CurrentProcess = Process.GetCurrentProcess();
```

Prevent application start under sandbox tools

```C#
  AntiSandBox.SelfDelete = false; //Delete application executable file
  AntiSandBox.ShowAlert = true; //Show a message
  AntiSandBox.Parse(CurrentProcess);
```

Prevent application start on virtual machine

```C#
  AntiVirtualMachine.SelfDelete = false;
  AntiVirtualMachine.ShowAlert = true;
  AntiVirtualMachine.Parse(CurrentProcess);
```

Prevent from network being monitored

```C#
  AntiSniff.SelfDelete = false;
  AntiSniff.ShowAlert = true;
  AntiSniff.Parse(CurrentProcess);
```

Prevents reverse engineering tools from running in the system

```C#
  AntiReverserTools.SelfDelete = false;
  AntiReverserTools.ShowAlert = true;
  AntiReverserTools.Aggressive = false; //Execute malicious codes (not recommended)
  AntiReverserTools.IgnoreCase = true; //Ignore case in process name
  AntiReverserTools.KeepAlive = true; //Keep anti reverse engineering service alive in background
  AntiReverserTools.WhiteList.Add("notepad"); //Add a process name to whitelist
  AntiReverserTools.BlackList.Add("dnspy"); //Add a process name to blacklist
  AntiReverserTools.Start(CurrentProcess);
```

Anti debugger

```C#
  AntiDebugger.SelfDelete = false;
  AntiDebugger.ShowAlert = true;
  AntiDebugger.Aggressive = false;
  AntiDebugger.KeepAlive = true; //Keep anti debugger service alive in background
  AntiDebugger.Start(CurrentProcess);
```

Detect if dnspy installed on system

```C#
  AntiDnspy.SelfDelete = false;
  AntiDnspy.ShowAlert = true;
  AntiDnspy.Parse(CurrentProcess);
```

![](https://user-images.githubusercontent.com/53654076/91207148-8df60580-e71d-11ea-8333-c554b37b628d.png)

Contacts :
-------
Website : www.CodeStrikers.org

Telegram : @SychicBoy

Softwares Channel : https://t.me/CSSOFT

Reverse engineering Channel : https://t.me/CodeStrikers

Email : SychicBoy@CodeStrikers.org

Credits :
-------
[robsonfelix](https://github.com/robsonfelix) for vm detcetor


