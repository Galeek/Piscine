using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RESSOURCE.CONFIGS;
using RESSOURCE.PROVIDERS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ANDROID
{
    public class Launcher : Fixtures
    {
        [TestCase(TestName = "Android Test")]
        //Height : 1776 Width : 1080
        public void AndroidOpenning()
        {
            //-----ANDROID-APP-URL-----
            //Actions a = new Actions(_driverANDROID);
            /*
            _driverANDROID.FindElement(By.ClassName("connexion__bloc_insciption")).Click();
            _driverANDROID.FindElement(By.Name("ctl00$ContentPlaceHolder1$Login$tbLogin")).Clear();
            _driverANDROID.FindElement(By.Name("ctl00$ContentPlaceHolder1$Login$tbLogin")).SendKeys("abdelkader@testingdigital.com");
            _driverANDROID.FindElement(By.Name("ctl00$ContentPlaceHolder1$Login$tbPass")).Clear();
            _driverANDROID.FindElement(By.Name("ctl00$ContentPlaceHolder1$Login$tbPass")).SendKeys("byron123");
            a.MoveToElement(_driverANDROID.FindElement(By.Id("ContentPlaceHolder1_Login_LienLogin"))).Click().Build().Perform();
            _driverANDROID.FindElement(By.ClassName("header_home__lien_menu")).Click();
            a.MoveToElement(_driverANDROID.FindElement(By.ClassName("icon-avatar"))).Click().Build().Perform();
            _driverANDROID.FindElement(By.XPath("//*[@id=\"ctl01\"]/div[3]/ul/li[1]")).Click();
            Thread.Sleep(2000);
            */

            //AndroidDriver<AndroidElement> driver;
            //_driverANDROID.Navigate().GoToUrl("http://www.showroomprive.com/");
            _driverANDROID.Navigate().GoToUrl("http://demo.automationtesting.in/Register.html");

            _driverANDROID.FindElement(By.CssSelector("[ng-model*='FirstName']")).SendKeys("Test");
            string truc = _driverANDROID.FindElement(By.CssSelector("[ng-model*='FirstName']")).GetAttribute("placeholder");
            _driverANDROID.FindElement(By.CssSelector("[value*='Male']")).Click();
            _driverANDROID.FindElement(By.CssSelector("[ng-model*='LastName']")).SendKeys(truc);

            _driverANDROID.FindElement(By.XPath("//input[@type='email']")).SendKeys("faux@test.fr");
            _driverANDROID.FindElement(By.XPath("//input[@type='tel']")).SendKeys("0123456789");

            _driverANDROID.FindElement(By.Id("checkbox1")).Click();
            _driverANDROID.FindElement(By.Id("checkbox2")).Click();
            _driverANDROID.FindElement(By.Id("checkbox3")).Click();

            int n = 1;
            bool pays = true;
            string value = _driverANDROID.FindElement(By.Id("countries")).Text;
            Char delimiter = '\n';
            String[] substrings = value.Split(delimiter);

            while (pays)
            {
                _driverANDROID.FindElement(By.Id("countries")).SendKeys(Keys.ArrowDown);
                n++;

                if (substrings[n - 1].Contains("France"))
                    pays = false;
            }

            _driverANDROID.FindElement(By.Id("yearbox")).SendKeys(Keys.ArrowDown);
            _driverANDROID.FindElement(By.CssSelector("[ng-model*='monthbox']")).SendKeys(Keys.ArrowDown);
            _driverANDROID.FindElement(By.Id("daybox")).SendKeys(Keys.ArrowDown);

            _driverANDROID.FindElement(By.CssSelector("[ng-model*='Password']")).SendKeys("MDPmdp0");
            _driverANDROID.FindElement(By.CssSelector("[ng-model*='CPassword']")).SendKeys("MDPmdp0");

            //_driver.FindElement(By.Id("submitbtn")).Click();
            //_driver.FindElement(By.Id("Button1")).Click();

            Thread.Sleep(5000);
        }

        public bool RetryingFindClick(By by)
        {
            Actions a = new Actions(_driverANDROID);
            bool result = false;
            int attempts = 0;
            while (attempts < 2)
            {
                try
                {
                    a.MoveToElement(_driverANDROID.FindElement(by)).Click().Build().Perform();
                    //_driver.FindElement(by).Click();
                    result = true;
                    break;
                }
                catch (Exception e)
                {

                }
                attempts++;
            }
            return result;
        }
        public void ScrollAndClick(By by)
        {
            int scrollStart = (int)(_driverANDROID.Manage().Window.Size.Height*0.2);
            int i = 0;
            bool found = false;

            while (i < 10 && !found)
            {
                _driverANDROID.Swipe(0, scrollStart, 0, 10, 350);
                Thread.Sleep(1000);
                try
                {
                    _driverANDROID.FindElement(by).Click();
                    found = true;
                }
                catch (NoSuchElementException ex)
                {

                }
                i++;
            }
        }
    }
    
}