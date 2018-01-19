using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using RESSOURCE.MODELS;
using RESSOURCE.PROVIDERS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESSOURCE.CONFIGS
{
    public class BASE
    {
        public string computerName = Environment.MachineName;
        //Drivers
        public IWebDriver _driver { get; set; }
        public AndroidDriver<AndroidElement> _driverANDROID { get; set; }
        public IOSDriver<IOSElement> _driverIOS { get; set; }
        
        //FNC
        public TestContext _contexte;

        //Variété
        public string url, browserType, deviceUDID, deviceName,
                      deviceOsV, userName, userLName, cmpName,
                      dataId, resultOutcome, lienScreenShotPortrait,
                      lienScreenShotLandScape, mobileOS, portMobile,
                      idConfig, testType, script, inputSearch, browser;

        public bool etatAppium = false, readyStateComplete;

        public long tempsDeChargement;

        //Responses
        public bool pageEtat = true;

        //GPPD
        public GPPD _gppd;

        //Dates
        public DateTime _dateStartTest;
        public DateTime _dateEndTest;

        //JS Declaration & Objects
        public long loadEventEnd, navigationStart;
        public string title, charset;
        public object _tagImg, _tagAComplet, _tagA, _tagBody, _scripts, _docMode;

        //Result & relations
        public Result _result = new Result();
        public ToS3 _pathS3 = new ToS3();

        public AWSS3 _awsS3;
        public BLD _bldr;

    }
}