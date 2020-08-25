# AntiRE.Runtime
About :
-------
A open source .net library for prevent from assembly being reverse engineered

Tip :
-------
Obfuscate dll and merge it with main assembly for more security

Features :
-------
[+] Anti debugger

[+] Anti sandbox

[+] Anti virtual machine

[+] Anti reverse engineering tools

[+] Anti memory dumping

[+] Anti dnspy

[+] Anti sniff (prevent network being monitored)

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

Anti sniff request to server

```C#
  HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://google.com");
  req.ContinueTimeout = 10000;
  req.ReadWriteTimeout = 10000;
  req.Timeout = 10000;
  req.KeepAlive = true;
  req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/28.0.1500.63 Safari/537.36";
  req.Accept = "*/*";
  req.Method = "GET";
  req.Headers.Add("Accept-Language", "en-US,en;q=0.9,fa;q=0.8");
  req.Headers.Add("Accept-Encoding", "gzip, deflate");
  req.AutomaticDecompression = DecompressionMethods.GZip;
  req.ServerCertificateValidationCallback = AntiSniff.ValidationCallback;
  req.ServicePoint.Expect100Continue = false;
  using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
  {
  if (response.StatusCode != HttpStatusCode.OK)
  {
  Alert.Show("NETWORK CONNECTION ERROR, CHECK YOUR INTERNET CONNECTION OR CLOSE SNIFFER SOFTWARES");
  Environment.Exit(0);
  return;
  }
  }
```

Alert message settings

```C#
  Alert.NotepadStyle = true; //True == Show message in notepad & False == MessageBox
  Alert.AutoClose = false; //Auto close message box after preferred time
  Alert.AutoCloseTime = 2; //Time (seconds) to close the messagebox
  Alert.NotepadPath = "readme.txt"; //Notepad file path
```

![](https://user-images.githubusercontent.com/53654076/91207148-8df60580-e71d-11ea-8333-c554b37b628d.png)

Contacts :
-------
Website : www.CodeStrikers.org

Telegram : [@SychicBoy](https://t.me/SychicBoy)

Softwares Channel : [@CSSOFT](https://t.me/CSSOFT)

Reverse engineering Channel : [@CodeStrikers](https://t.me/CodeStrikers)

Email : SychicBoy@CodeStrikers.org

Credits :
-------
[robsonfelix](https://github.com/robsonfelix) for vm detcetor


