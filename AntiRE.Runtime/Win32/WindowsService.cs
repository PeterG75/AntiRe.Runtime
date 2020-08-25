using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;

class WindowsService
{
    static Regex _fileNameRegex = new Regex(@"((?<name>[A-Z]:.+?(\.exe|\Z))|(?:""(?<name>.+?)""))", RegexOptions.IgnoreCase);

    public string DisplayName { get; private set; }
    public string Name { get; private set; }
    public string CommandLine { get; private set; }
    public FileInfo Executable { get; private set; }

    public FileVersionInfo Version { get; private set; }

    public WindowsService(ServiceController service)
    {
        Name = service.ServiceName.ToLower();
        DisplayName = service.DisplayName.ToLower();

        try
        {
            var registryPath = @"SYSTEM\CurrentControlSet\Services\" + service.ServiceName;
            var key = Registry.LocalMachine.OpenSubKey(registryPath);
            var value = key.GetValue("ImagePath").ToString();
            key.Close();
            CommandLine = Environment.ExpandEnvironmentVariables(value).ToLower();
            var m = _fileNameRegex.Match(CommandLine);
            if (m.Success)
            {
                var fileName = m.Groups["name"].Value;
                if (!File.Exists(fileName))
                    fileName += ".exe";

                if (File.Exists(fileName))
                {
                    Executable = new FileInfo(fileName);
                    if (Executable != null && Executable.Exists)
                        Version = FileVersionInfo.GetVersionInfo(fileName);
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("{0} ({1})", DisplayName, Name);
        if (Executable == null)
        {
            sb.AppendFormat(" - {0}", CommandLine);
        }
        else
        {
            if (Executable != null)
                sb.AppendFormat(" - {0}", Executable.ToString());
            if (Version != null)
                sb.AppendFormat(" (v{0})", Version.ProductVersion);
        }
        return sb.ToString();
    }
}
