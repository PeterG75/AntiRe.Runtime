using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;

namespace AntiRE.Runtime
{
    public class AntiSniff
    {
        /// <summary>
        /// Program will be closed and deleted if network monitored (Default = false)
        /// </summary>
        public static bool SelfDelete = false;
        /// <summary>
        /// Show a alert message in notepad if network monitored (Default = true)
        /// </summary>
        public static bool ShowAlert = true;
        /// <summary>
        /// Text of alert message
        /// </summary>
        public static string AlertMessage = "NETWORK CONNECTION ERROR, CHECK YOUR INTERNET CONNECTION OR CLOSE SNIFFER SOFTWARES";
        /// <summary>
        /// Validate network ssl certificate, you can use certificate validation on your config for more security
        /// example : request.ServerCertificateValidationCallback = ValidationCallback;
        /// </summary>
        public static bool ValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                return false;
            }
            if (chain.ChainPolicy.VerificationFlags == X509VerificationFlags.NoFlag &&
                chain.ChainPolicy.RevocationMode == X509RevocationMode.Online)
            {
                return true;
            }
            X509Chain newChain = new X509Chain();
            X509ChainElementCollection chainElements = chain.ChainElements;
            for (int i = 1; i < chainElements.Count - 1; i++)
            {
                newChain.ChainPolicy.ExtraStore.Add(chainElements[i].Certificate);
            }

            return newChain.Build(chainElements[0].Certificate);
        }
        /// <summary>
        /// Check for sniffers
        /// </summary>
        public static void Parse(Process CurrentProcess)
        {
            try
            {
                ServicePointManager.CheckCertificateRevocationList = true;
                HttpWebRequest request = WebRequest.Create("https://google.com") as HttpWebRequest;
                request.Timeout = 10000;
                request.ContinueTimeout = 10000;
                request.ReadWriteTimeout = 10000;
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
                request.Host = "www.google.com";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Method = "GET";
                request.ServerCertificateValidationCallback = ValidationCallback;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                        response.Close();
                    else
                    {
                        response.Close();
                        
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
            catch
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
