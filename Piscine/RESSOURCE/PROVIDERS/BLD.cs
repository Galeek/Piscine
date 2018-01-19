using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using RESSOURCE.CONFIGS;
using RESSOURCE.MODELS;
using RESSOURCE.MODELS.FUNCTIONAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.PROVIDERS
{
    //Enum for browserType.
    public enum DriverType
    {
        Android,
        Ios,
        Desktop
    }
    public class BLD : BASE
    {
        #region Credentials
        string pathDLL, path;
        private DriverType _driverType;
        public string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        HNDLR _hndlr = new HNDLR();
        #endregion

        #region Builder Mobile ||
        public void BuilderMobileParallel(List<Configuration> _configs, string cmpId, string url, int dataId)
        {
            bool ProcessesON = false;

            List<Process[]> listProcessNunitAgent = new List<Process[]>();
            foreach (var _config in _configs)
            {
                Process _proc = new Process();
                Task.Delay(250).Wait();

                path = ChoosePath(_config.device_os);
                _proc.StartInfo.FileName = documents + @"\Source\Repos\automate_v2\packages\NUnit.ConsoleRunner.3.7.0\tools\nunit3-console.exe";
                _proc.StartInfo.Arguments = "--params:Url=" + _hndlr.CompleteUrl(url) + ";DataID=" + dataId.ToString() +
                                                    ";ConfigurationID=" + _config.id + "--dispose-runners --process=Multiple" + path;
                _proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(_config.device_model + " - " + _config.device_marker + " - " + _config.device_serial_number + "[Session] Appium En cours de creation...");
                Task.Factory.StartNew(() => _proc.Start(), TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness | TaskCreationOptions.HideScheduler);
                listProcessNunitAgent.Add(Process.GetProcessesByName("nunit3-console"));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(_config.device_model + " - " + _config.device_marker + " - " + _config.device_serial_number + "[Session] Appium OK.");
            }
            #region check Process
            ProcessesON = true;
            //Appium c'est de la cacahuète, faut qu'il respire!, temps moyen 80sec ont donne 90sec.
            Task.Delay(5000).Wait();
            Process[] listProcessNUnit = Process.GetProcessesByName("nunit3-console");
            foreach (var nunitAgent in listProcessNUnit)
            {
                nunitAgent.WaitForExit();
            }
            while (ProcessesON)
            {
                ProcessesON = _hndlr.NextProcesses(listProcessNunitAgent);
            }
            Task.Delay(500).Wait();

            Process[] listProcessAppiumNode = Process.GetProcessesByName("node");
            foreach (var node in listProcessAppiumNode)
            {
                node.Kill();
                Task.Delay(1000).Wait();
            }
            foreach (var _config in _configs)
            {
                if (_config.connector == "ROG-B")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(_config.device_model + " - " + _config.device_marker + " - " + _config.device_serial_number + "[Session] Appium Terminé");
                }
            }
            _hndlr.ClearADB();
            _hndlr.NewADB();
            #endregion
        }
        #endregion

        #region Root / Path
        public string ChoosePath(string platform)
        {
            if (platform.ToUpper() == "ANDROID")
            {
                pathDLL = documents + @"\Source\Repos\automate_v2\ANDROID\bin\Debug\ANDROID.dll";
            }
            else if (platform.ToUpper() == "IOS")
            {
                pathDLL = documents + @"\Source\Repos\automate_v2\IOS\bin\Debug\IOS.dll";
            }
            else if (platform.ToUpper() == "DESKTOP")
            {
                pathDLL = @documents + @"\Source\Repos\automate_v2\DESKTOP\bin\Debug\DESKTOP.dll";
            }
            return pathDLL;
        }
        #endregion

        #region Builder Script ANDROID

        public void ScriptBuilder(ActionEvent _aE, AndroidDriver<AndroidElement> _driverANDROID, SCR _ssb,
                                   string udid, string userName, string userLName,
                                   string os, bool pgR)
        {
            if (_aE.elementPropertyValue == null && _aE.elementPropertyType == null && _aE.elementValue == null)
            {
                switch (_aE.actionType)
                {
                    case "Screenshot":
                        Task.Delay(2500).Wait();
                        _ssb.TakeScreenshot(_driverANDROID, udid, userLName, userLName, os, pgR);
                        break;
                    case "ScreenshotLarge":
                        Task.Delay(1500).Wait();
                        _ssb.GetEntireScreenshotMobile(_driverANDROID, udid);
                        break;
                    case "GoToUrl":
                        _driverANDROID.Navigate().GoToUrl(url);
                        break;
                    case "Refresh":
                        _driverANDROID.Navigate().Refresh();
                        break;
                    case "Forward":
                        _driverANDROID.Navigate().Forward();
                        break;
                    case "Back":
                        _driverANDROID.Navigate().Back();
                        break;
                    default: break;
                }
            }
            else if (_aE.elementValue == null)
            {
                switch (_aE.actionType)
                {
                    case "Click":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverANDROID).Click();
                        break;
                    case "Clear":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverANDROID).Clear();
                        break;
                    default: break;
                }
            }
            else if (_aE.elementPropertyValue == null && _aE.elementPropertyType == null)
            {
                switch (_aE.actionType)
                {
                    case "GoToUrl":
                        _driverANDROID.Navigate().GoToUrl(_aE.elementValue);
                        break;
                    default: break;
                }
            }
            else
            {
                switch (_aE.actionType)
                {
                    case "SendKey":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverANDROID).Click();
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverANDROID).SendKeys(_aE.elementValue);
                        break;
                    default: break;
                }
            }
        }
        #endregion

        #region Builder Script IOS

        public void ScriptBuilder(ActionEvent _aE, IOSDriver<IOSElement> _driverIOS, SCR _ssb,
                                   string udid, string userName, string userLName,
                                   string os, bool pgR)
        {
            if (_aE.elementPropertyValue == null && _aE.elementPropertyType == null && _aE.elementValue == null)
            {
                switch (_aE.actionType)
                {
                    case "Screenshot":
                        Task.Delay(2500).Wait();
                        _ssb.TakeScreenshot(_driverIOS, udid, userLName, userLName, os, pgR);
                        break;
                    case "ScreenshotLarge":
                        Task.Delay(1500).Wait();
                        _ssb.GetEntireScreenshotMobile(_driverIOS, udid);
                        break;
                    case "GoToUrl":
                        _driverIOS.Navigate().GoToUrl(url);
                        break;
                    case "Refresh":
                        _driverIOS.Navigate().Refresh();
                        break;
                    case "Forward":
                        _driverIOS.Navigate().Forward();
                        break;
                    case "Back":
                        _driverIOS.Navigate().Back();
                        break;
                    default: break;
                }
            }
            else if (_aE.elementValue == null)
            {
                switch (_aE.actionType)
                {
                    case "Click":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverIOS).Click();
                        break;
                    case "Clear":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverIOS).Clear();
                        break;
                    default: break;
                }
            }
            else if (_aE.elementPropertyValue == null && _aE.elementPropertyType == null)
            {
                switch (_aE.actionType)
                {
                    case "GoToUrl":
                        _driverIOS.Navigate().GoToUrl(_aE.elementValue);
                        break;
                    default: break;
                }
            }
            else
            {
                switch (_aE.actionType)
                {
                    case "SendKey":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverIOS).Click();
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverIOS).SendKeys(_aE.elementValue);
                        break;
                    default: break;
                }
            }
        }
        #endregion

        #region Builder Script DESKTOP

        public void ScriptBuilderDESKTOP(ActionEvent _aE, IWebDriver _driverDESKTOP, SCR _ssb,
                                   string udid, string userName, string userLName,
                                   string os, bool pgR)
        {
            if (_aE.elementPropertyValue == null && _aE.elementPropertyType == null && _aE.elementValue == null)
            {
                switch (_aE.actionType)
                {
                    case "Screenshot":
                        Task.Delay(2500).Wait();
                        _ssb.TakeScreenshotDESKTOP(_driverDESKTOP, udid, userLName, userLName, os, pgR);
                        break;
                    case "ScreenshotLarge":
                        Task.Delay(1500).Wait();
                        _ssb.TakeScreenshotLarge(_driverDESKTOP.Url, udid, userLName, userLName, os, pgR);
                        break;
                    case "GoToUrl":
                        _driverDESKTOP.Navigate().GoToUrl(url);
                        break;
                    case "Refresh":
                        _driverDESKTOP.Navigate().Refresh();
                        break;
                    case "Forward":
                        _driverDESKTOP.Navigate().Forward();
                        break;
                    case "Back":
                        _driverDESKTOP.Navigate().Back();
                        break;
                    default: break;
                }
            }
            else if (_aE.elementValue == null)
            {
                switch (_aE.actionType)
                {
                    case "Click":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverDESKTOP).Click();
                        break;
                    case "Clear":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverDESKTOP).Clear();
                        break;
                    default: break;
                }
            }
            else if (_aE.elementPropertyValue == null && _aE.elementPropertyType == null)
            {
                switch (_aE.actionType)
                {
                    case "GoToUrl":
                        _driverDESKTOP.Navigate().GoToUrl(_aE.elementValue);
                        break;
                    default: break;
                }
            }
            else
            {
                switch (_aE.actionType)
                {
                    case "SendKey":
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverDESKTOP).Click();
                        TDFinder(_aE.elementPropertyType, _aE.elementPropertyValue, _driverDESKTOP).SendKeys(_aE.elementValue);
                        break;
                    default: break;
                }
            }
        }
        #endregion

        #region Finder
        // Android Finder
        public AndroidElement  TDFinder(string propType, string propValue, AndroidDriver<AndroidElement> _driverANDROID)
        {
            AndroidElement eToUse = null;
            switch (propType)
            {
                case "Id":
                    eToUse = _driverANDROID.FindElement(By.Id(propValue));
                    break;
                case "Name":
                    eToUse = _driverANDROID.FindElement(By.Name(propValue));
                    break;
                case "Class":
                    eToUse = _driverANDROID.FindElement(By.ClassName(propValue));
                    break;
                case "CssSelector":
                    eToUse = _driverANDROID.FindElement(By.CssSelector(propValue));
                    break;
                case "LinkText":
                    eToUse = _driverANDROID.FindElement(By.LinkText(propValue));
                    break;
                case "XPath":
                    eToUse = _driverANDROID.FindElement(By.XPath(propValue));
                    break;
            }
            return eToUse;
        }

        // IOS Finder
        public IOSElement TDFinder(string propType, string propValue, IOSDriver<IOSElement> _driverIOS)
        {
            IOSElement eToUse = null;
            switch (propType)
            {
                case "Id":
                    eToUse = _driverIOS.FindElement(By.Id(propValue));
                    break;
                case "Name":
                    eToUse = _driverIOS.FindElement(By.Name(propValue));
                    break;
                case "Class":
                    eToUse = _driverIOS.FindElement(By.ClassName(propValue));
                    break;
                case "CssSelector":
                    eToUse = _driverIOS.FindElement(By.CssSelector(propValue));
                    break;
                case "LinkText":
                    eToUse = _driverIOS.FindElement(By.LinkText(propValue));
                    break;
                case "XPath":
                    eToUse = _driverIOS.FindElement(By.XPath(propValue));
                    break;
            }
            return eToUse;
        }

        // DESKTOP Finder
        public IWebElement TDFinder(string propType, string propValue, IWebDriver _driverDESKTOP)
        {
            IWebElement eToUse = null;
            switch (propType)
            {
                case "Id":
                    eToUse = _driverDESKTOP.FindElement(By.Id(propValue));
                    break;
                case "Name":
                    eToUse = _driverDESKTOP.FindElement(By.Name(propValue));
                    break;
                case "Class":
                    eToUse = _driverDESKTOP.FindElement(By.ClassName(propValue));
                    break;
                case "CssSelector":
                    eToUse = _driverDESKTOP.FindElement(By.CssSelector(propValue));
                    break;
                case "LinkText":
                    eToUse = _driverDESKTOP.FindElement(By.LinkText(propValue));
                    break;
                case "XPath":
                    eToUse = _driverDESKTOP.FindElement(By.XPath(propValue));
                    break;
            }
            return eToUse;
        }
        #endregion
    }
}