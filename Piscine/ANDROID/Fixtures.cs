using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using RESSOURCE.CONFIGS;
using RESSOURCE.MODELS;
using RESSOURCE.PROVIDERS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDROID
{
    public enum TypeDeTest
    {
        Ergonomique,
        Fonctionnel
    }
    public class Fixtures : BASE
    {
        //MesTemps Debut et fin de chargement d'une page. 
        private TypeDeTest _testType;
        private Int32 vTStampDebut, vTstampFin;
        public SCR _ssb = new SCR();
        HNDLR _hndlr = new HNDLR();
        //Process _processActuel;
        public string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        [SetUp]
        public void SetupTest()
        {
            _bldr = new BLD();
            _gppd = new GPPD();
            _contexte = TestContext.CurrentContext;
            vTStampDebut = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            #region Parametres entrant
            deviceUDID = TestContext.Parameters.Get("DeviceUDID", "9885b635494d373655");
            deviceName = TestContext.Parameters.Get("DeviceName", "SM-G930F");
            deviceOsV = TestContext.Parameters.Get("TestCaseName", "7.0.0");
            dataId = TestContext.Parameters.Get("DataID", "1");
            portMobile = TestContext.Parameters.Get("PortMobile", "4723");
            idConfig = TestContext.Parameters.Get("IDConfiguration", "1");
            userName = TestContext.Parameters.Get("PortMobile", "4723");
            userLName = TestContext.Parameters.Get("IDConfiguration", "1");
            cmpName = TestContext.Parameters.Get("CampaignName", "1");
            testType = TestContext.Parameters.Get("TestType", "Fonctionnel");
            #endregion

            #region Capabilitées DRIVER
            DesiredCapabilities _cap = new DesiredCapabilities();
            _cap.SetCapability("autoGrantPermissions", "true");
            //_cap.SetCapability(MobileCapabilityType.App, @"C:\Users\User\Downloads\ucbrowser.apk");

            _cap.SetCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
            //_cap.SetCapability(AndroidMobileCapabilityType.AppPackage, "fr.francetv.apps.info");
            //_cap.SetCapability(AndroidMobileCapabilityType.AppActivity, "fr.francetv.apps.info.activity.home.HomeActivity");
            //_cap.SetCapability(MobileCapabilityType.FullReset, true);

            _cap.SetCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
            _cap.SetCapability(MobileCapabilityType.NewCommandTimeout, 18000);
            _cap.SetCapability(MobileCapabilityType.Udid, deviceUDID);
            _cap.SetCapability(MobileCapabilityType.DeviceName, deviceName);
            _cap.SetCapability(MobileCapabilityType.PlatformVersion, deviceOsV);
            _cap.SetCapability(AndroidMobileCapabilityType.UnicodeKeyboard, true);
            _cap.SetCapability(AndroidMobileCapabilityType.ResetKeyboard, true);
            #endregion

            //Etat Appium?! When DEBUG MODE
            _hndlr.StartRemoteAppiumNode(portMobile, deviceUDID);
            // Parser testType
            _testType = (TypeDeTest)Enum.Parse(typeof(TypeDeTest), testType);

            try
            {
                //string xx = "http://" + ConfigurationManager.AppSettings["IPADDRESS"] + ":" + portMobile + "/wd/hub";
                _driverANDROID = new AndroidDriver<AndroidElement>(new Uri("http://" + ConfigurationManager.AppSettings["IPADDRESS"] + ":" + portMobile + "/wd/hub"), _cap);
                //_processActuel = AndroidRecordStart();
                //BuilderRecord("record");
                //_hndlr.serverResponse = _hndlr.serverCodeResponse(pageEtat);
                /*
                #region JS ~Metrics
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driverANDROID;
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
                        _bldr.ScriptBuilder(action, _driverANDROID, _ssb, deviceUDID, userName, userLName, "Android", pageEtat);
                    }
                }*/
                //tempsDeChargement = loadEventEnd - navigationStart;
            }
            catch (Exception ex)
            {

            }
        }

        [TearDown]
        public void TeardownTest()
        {
            //_awsS3 = new AWSS3();

            try
            {
                /*var img = _ssb.TakeScreenshot(_driverANDROID, deviceUDID, userName, userLName, "Android", pageEtat);
                var imgLarge = _ssb.GetEntireScreenshotMobile(_driverANDROID, deviceUDID);

                Tuple<string, string> pathScreenShotLarge = _ssb.TakeScreenshotACTION(imgLarge, deviceUDID, userName, userLName, "Android");
                Tuple<string, string> pathScreenShot = _ssb.TakeScreenshotACTION(img, deviceUDID, userName, userLName, "Android");

                List<Tuple<string, string>> allScreenShotPaths = new List<Tuple<string, string>>();
                allScreenShotPaths.Add(pathScreenShotLarge);
                allScreenShotPaths.Add(pathScreenShot);

                // POST S3.
                foreach (var pathObject in allScreenShotPaths)
                {   //upload
                    _awsS3.UploadFile(false, pathObject, cmpName, userName, userLName, "ANDROID");
                }
                // Date Fin test
                vTstampFin = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                // Collecte, Traitement informations, et Resultats. 
                resultOutcome = TestContext.CurrentContext.Result.Outcome.Status.ToString();
                //Fin des tests.
                _dateEndTest = DateTime.Now;

                //Setter l'Objet ScreenShotPath avant le PUT
                _pathS3.pathLandScape = pathScreenShotLarge.Item1;
                _pathS3.pathPortrait = pathScreenShot.Item1;

                // Lien urls ScreenShots
                lienScreenShotLandScape = _awsS3.ImagesURL(pathScreenShotLarge, cmpName, userName, userLName, "ANDROID");
                lienScreenShotPortrait = _awsS3.ImagesURL(pathScreenShot, cmpName, userName, userLName, "ANDROID");

                //Setter l'Objet Resultat avant le POST.
                _result.FillUpResult(dataId, title, _dateStartTest, _dateEndTest, _hndlr.serverResponse,
                                    tempsDeChargement, resultOutcome, lienScreenShotLandScape,
                                    lienScreenShotPortrait, vTStampDebut, vTstampFin);*/
                //_gppd.PostResults(dataId, title, _hndlr.serverResponse, tempsDeChargement.ToString(),
                //                    "HTML", _contexte.Result.Outcome.ToString(), lienScreenShotLandScape,
                //                    lienScreenShotPortrait, vTStampDebut.ToString(), vTstampFin.ToString(), idConfig).Wait();

                //Quitter/Terminer l'instance.
                //_driverANDROID.Manage().Cookies.DeleteAllCookies();
                //_processActuel.Kill();
                //_processActuel.Close();
                //AndroidPullVideo(deviceUDID+ vTStampDebut);
                //AndroidRM(deviceUDID + vTStampDebut);
                //First pull
                //BuilderRecord("pull");
                //Safety pull
                //BuilderRecord("pull");
                //rm
                //BuilderRecord("rm");
                _driverANDROID.CloseApp();
            }
            catch (Exception ex)
            {
                
            }
        }
        /*
         *   LD 240p 3G Mobile @ H.264 baseline profile 350 kbps (3 MB/minute)
         *   LD 360p 4G Mobile @ H.264 main profile 700 kbps (6 MB/minute)
         *   SD 480p WiFi @ H.264 main profile 1200 kbps (10 MB/minute)
         *   HD 720p @ H.264 high profile 2500 kbps (20 MB/minute)
         *   HD 1080p @ H.264 high profile 5000 kbps (35 MB/minute)
         *   default value --bit-rate 4000000
         */
        //Le builder fonctionne
        public void BuilderRecord(string cmd)
        {
            Process p = new Process();
            string vName = deviceUDID + vTStampDebut;
            string arg = "";
            switch (cmd)
            {
                case "record":
                    //_processActuel = p;
                    arg = "shell screenrecord --bit-rate 2500000 " + "/sdcard/" + vName + ".avi";
                    break;
                case "pull":
                    arg = "pull /sdcard/" + vName + ".avi " + @"C:\Users\User\Documents\Source\Repos\Piscine\DESKTOP\DESKTOPSCR";
                    break;
                case "rm":
                    arg = "shell rm /sdcard/" + vName + ".avi";
                    break;
                default:
                    break;
            }
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = "adb.exe",
                    Arguments = arg
                };
                p.StartInfo = startInfo;
                p.Start();
                if (!cmd.Equals("record"))
                {
                    p.WaitForExit();
                    Task.Delay(2000);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public Process AndroidRecordStart()
        {
            // --------------Try to record--------------
            Process record = new Process();
            string vName = deviceUDID + vTStampDebut;
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "adb.exe",
                Arguments = "shell screenrecord /sdcard/" + vName + ".avi"
            };
            record.StartInfo = startInfo;
            record.Start();
            return record;
        }

        public void AndroidPullVideo(string vName)
        {
            try
            {
                Process pull = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    Arguments = "/c adb pull /sdcard/" + vName + ".avi"
                };
                pull.StartInfo = startInfo;
                pull.Start();
                
            }
            catch (Exception ex)
            {
            }

        }

        public void AndroidRM(string vName)
        {
            Process remove;
            remove= new Process();
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = "adb.exe",
                    Arguments = "shell rm -f /sdcard/" + vName + ".mp4"
                };
                remove.StartInfo = startInfo;
                remove.Start();
                remove.WaitForExit();
            }
            catch (Exception ex)
            {
            }

        }
    }
}