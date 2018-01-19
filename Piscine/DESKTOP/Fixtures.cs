using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using RESSOURCE.CONFIGS;
using RESSOURCE.PROVIDERS;
using System;
using System.Diagnostics;
namespace DESKTOP
{
    //Enum for browserType.
    public enum BrowserType
    {
        Chrome,
        Firefox,
        IE,
        Edge,
        Opera,
        HeadLess,
        Android
    }
    public enum TypeDeTest
    {
        Ergonomique,
        Fonctionnel
    }
    public class Fixtures : BASE
    {
        private BrowserType _browserType;
        private TypeDeTest _testType;
        private Int32 vTStampDebut, vTstampFin;
        public SCR _ssb = new SCR();
        HNDLR _hndlr = new HNDLR();

        [SetUp]
        public void SetupTest()
        {
            _gppd = new GPPD();
            _bldr = new BLD();
            _awsS3 = new AWSS3();
            _contexte = TestContext.CurrentContext;
            vTStampDebut = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            #region Parametres entrant
            url = TestContext.Parameters.Get("Url", "");
            deviceOsV = TestContext.Parameters.Get("DeviceOs", "Windows10");
            browserType = TestContext.Parameters.Get("Browser", "Chrome");
            dataId = TestContext.Parameters.Get("DataID", "1");
            idConfig = TestContext.Parameters.Get("IDConfiguration", "1");
            userName = TestContext.Parameters.Get("PortMobile", "Lorem");
            userLName = TestContext.Parameters.Get("IDConfiguration", "Ipsum");
            cmpName = TestContext.Parameters.Get("CampaignName", "1");
            testType = TestContext.Parameters.Get("TestType", "Fonctionnel");
            #endregion

            // Parser typeBrowser
            _browserType = (BrowserType)Enum.Parse(typeof(BrowserType), browserType);
            // Le passer a notre methode
            ChooseDriverInstance(_browserType);
            // Visiter la page
            _driver.Navigate().GoToUrl(url);
            // Tester la page
            _hndlr.serverResponse = _hndlr.serverCodeResponse(pageEtat);

            // Parser testType
            _testType = (TypeDeTest)Enum.Parse(typeof(TypeDeTest), testType);

            #region JS ~Metrics
            //-----------------------------
            // JS recuperation données HTML
            //-----------------------------
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            loadEventEnd = (long)js.ExecuteScript("return window.performance.timing.loadEventEnd");
            navigationStart = (long)js.ExecuteScript("return window.performance.timing.navigationStart");
            title = (string)js.ExecuteScript("return document.title");
            charset = (string)js.ExecuteScript("return document.charset");
            _tagImg = js.ExecuteScript("return document.images");
            _tagAComplet = js.ExecuteScript("return document.anchors");
            _tagA = js.ExecuteScript("return document.applets");
            _tagBody = js.ExecuteScript("return document.body");
            _docMode = js.ExecuteScript("return document.documentMode");
            _scripts = js.ExecuteScript("return document.scripts");
            readyStateComplete = ((String)js.ExecuteScript("return document.readyState")).Equals("complete");
            #endregion

            /*
            if (_testType == TypeDeTest.Fonctionnel)
            {
                //------------------------
                // TestCase RECUPERRATION
                //------------------------
                TestCase Script = _gppd.ChargerTestCase();

                foreach (var action in Script.Action)
                {
                    _bldr.ScriptBuilderDESKTOP(action, _driver, _ssb, deviceUDID, userName, userLName, "DESKTOP", pageEtat);
                }
            }*/
            //temps de chargement de page.
            tempsDeChargement = loadEventEnd - navigationStart;
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {

                //Quitter/Terminer l'instance.
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Quit();

                Process[] listProcessBrowser = Process.GetProcessesByName("opera");
                if (listProcessBrowser.Length != 0)
                {
                    foreach (var _process in listProcessBrowser)
                    {
                        _process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region WEBDRIVER Handler
        private void ChooseDriverInstance(BrowserType browserType)
        {
            if (browserType == BrowserType.Chrome)
            {
                int lrg = 1920;  // Longeur ICI
                int lng = 1080; // Largeur ICI

                int lngForScreenShot = lng + (Int32)127;
                int lrgForScreenShot = lrg + 37;

                ChromeOptions options = new ChromeOptions();
                //options.AddArgument("--headless");
                options.AddArgument("--window-size=" + lrg + "," + lngForScreenShot);
                //options.EnableMobileEmulation("iPhone 6 Plus");
                _driver = new ChromeDriver(@"C:/WEBDRIVER", options);
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == BrowserType.Firefox)
            {
                FirefoxBinary binary = new FirefoxBinary(@"C:/Program Files/Mozilla Firefox/firefox.exe");
                FirefoxProfile firefoxProfile = new FirefoxProfile();
                //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                //service.FirefoxBinaryPath = @"C:\\Program Files\\Mozilla Firefox";
                //service.HideCommandPromptWindow = true;
                //service.SuppressInitialDiagnosticInformation = true;
                //_driver = new FirefoxDriver();
                _driver = new FirefoxDriver(binary, firefoxProfile);
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == BrowserType.IE)
            {
                _driver = new InternetExplorerDriver(@"C:/WEBDRIVER");
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == BrowserType.Edge)
            {
                _driver = new EdgeDriver(@"C:/WEBDRIVER");
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == BrowserType.Opera)
            {
                _driver = new OperaDriver("C:/WEBDRIVER");
                _driver.Manage().Window.Maximize();
            }
            else if (browserType == BrowserType.HeadLess)
            {
                _driver = new PhantomJSDriver("C:/WEBDRIVER");
                _driver.Manage().Window.Maximize();
            }
        }
        #endregion
    }
}