using RESSOURCE.CONFIGS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.PROVIDERS
{
    public class HNDLR
    {
        public string myIp = ConfigurationManager.AppSettings["IPADDRESS"];
        public string serverResponse;
        BASE _base = new BASE();

        #region RESPONSE SERVER(S)
        public bool PageStatut(string url)
        {
            bool etatSite = false;
            HttpWebRequest task; 
            HttpWebResponse taskresponse = null; 
            task = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                taskresponse = (HttpWebResponse)task.GetResponse();
                etatSite = true;
                Console.WriteLine(etatSite);
            }
            catch (Exception)
            {
                etatSite = false;
                Console.WriteLine(etatSite);
            }
            return etatSite;
        }

        public string serverCodeResponse(bool pageStaut)
        {
            if (pageStaut)
            {
                serverResponse = "200";
            }
            return serverResponse;
        }
        #endregion

        #region ENCRYPTAGE
        public string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
        #endregion

        #region VERIFICATION URL SAISIE
        // -- Reception  https://  --  http://
        string urlAfterCheck;
        bool statusUri = false;
        public string CheckURL(string url)
        {
            if (url.Contains("http"))
            {
                urlAfterCheck = url.Remove(0, 7);
            }
            else if (url.Contains("https"))
            {
                urlAfterCheck = url.Remove(0, 8);
            }
            else
            {
                urlAfterCheck = url;
            }
            return urlAfterCheck;
        }

        // --- Check 
        public bool CheckURI(string uri)
        {
            if (uri.Contains("http") || uri.Contains("https"))
            {
                statusUri = true; //valide
            }
            return statusUri;
        }

        public string CompleteUrl(string url)
        {
            if (!url.Contains("http"))
            {
                urlAfterCheck = "http://" + url;
            }
            else
            {
                urlAfterCheck = url;
            }
            return urlAfterCheck;
        }
        #endregion

        #region ROOT
        public string PathProject()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            return projectPath;
        }
        #endregion

        #region ADB Handler
        public void ClearADB()
        {
            ConnectionOptions con = new ConnectionOptions();
            //con.Username = "";
            //con.Password = "";
            var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", _base.computerName));
            var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());

            var processToRun = new[] { "cmd.exe /c adb kill-server" };
            wmiProcess.InvokeMethod("Create", processToRun);
            Task.Delay(5000).Wait();
        }

        public void NewADB()
        {
            var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", _base.computerName));
            var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());

            var processToRun = new[] { "cmd.exe /c adb start-server" };
            wmiProcess.InvokeMethod("Create", processToRun);
            //Task.Delay(5000).Wait();
        }
        #endregion

        #region PROCESS Handler
        public bool NextProcesses(List<Process[]> _processes)
        {
            foreach (var _process in _processes)
            {
                if (_process.All(x => x.HasExited == true))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region APPIUM Handler
        public void StartRemoteAppiumNode(string portMobile, string deviceUDID)
        {
            ConnectionOptions con = new ConnectionOptions();
            var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", _base.computerName));
            var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());

            var processToRun = new[] { "cmd.exe /c appium  -a "+ myIp +" -p "+portMobile+
                                     " -bp "+(Int32.Parse(portMobile)-2472)+
                                     " --udid " +deviceUDID+
                                     " --chromedriver-port "+(Int32.Parse(portMobile)+4795)+
                                     " --command-timeout " +180+" --local-timezone --session-override"};

            wmiProcess.InvokeMethod("Create", processToRun);
            Task.Delay(6600).Wait();
        }
        #endregion

        #region APPIUM Server Checker Handler

        #endregion

    }
}
