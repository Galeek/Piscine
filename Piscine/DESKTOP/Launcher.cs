using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium.Interactions.Internal;

namespace DESKTOP
{
    public class Launcher : Fixtures
    {
        [TestCase(TestName = "Web Test")]
        public void WebTest()
        {
            // _driver.Navigate().GoToUrl("http://www.free.fr");
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Actions a = new Actions(_driver);
            #region Register
            //_driver.Navigate().GoToUrl("http://demo.automationtesting.in/Register.html");

            //// IWebElement example = _driver.FindElement(By.CssSelector("FirstName"));

            //// _driver.FindElement(By.CssSelector("[ng-model*='FirstName']"));

            //_driver.FindElement(By.CssSelector("[ng-model*='FirstName']")).SendKeys("Test");
            //// Thread.Sleep(5000);
            //string truc = _driver.FindElement(By.CssSelector("[ng-model*='FirstName']")).GetAttribute("placeholder");
            //// Console.Write(truc);
            //_driver.FindElement(By.CssSelector("[value*='Male']")).Click();
            //// Thread.Sleep(5000);
            //_driver.FindElement(By.CssSelector("[ng-model*='LastName']")).SendKeys(truc);
            //// Thread.Sleep(5000);

            //_driver.FindElement(By.XPath("//textarea")).SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            ////Thread.Sleep(20000);

            //// _driver.FindElement(By.XPath("/html/body/section/div[@class='container center']/div[@class='row']/div[@class='col-sm-6 col-md-6 col-xs-12']/form/div[2]/div[0]/input")).SendKeys("faux");

            //// _driver.FindElement(By.XPath("/html/body/section/div[0]/div[0]/div[1]/form/div[2]/div[0]/input")).SendKeys("faux");

            //_driver.FindElement(By.XPath("//input[@type='email']")).SendKeys("faux@test.fr");
            //_driver.FindElement(By.XPath("//input[@type='tel']")).SendKeys("0123456789");

            //_driver.FindElement(By.Id("checkbox1")).Click();
            //_driver.FindElement(By.Id("checkbox2")).Click();
            //_driver.FindElement(By.Id("checkbox3")).Click();

            ////Debug.Assert(_driver.FindElement(By.Id("checkbox1")).Selected, "Case non cochée");

            ////_driver.FindElement(By.Id("msdd")).Click(); // que faire ensuite pour sélectionner une valeur (impossible de arrowdown) ?
            ////_driver.FindElement(By.Id("msdd")).SendKeys(Keys.ArrowDown);

            ////_driver.FindElement(By.ClassName("ui-front")).SendKeys(Keys.ArrowDown);

            ////string occurences = _driver.FindElement(By.LinkText("French")).ToString(); //erreur
            ////Console.Write(occurences);

            ////_driver.FindElement(By.ClassName("ui-corner-all")).Click();
            ///*
            //_driver.FindElement(By.PartialLinkText("French")).Click();
            //_driver.FindElement(By.PartialLinkText("Dutch")).Click();
            //_driver.FindElement(By.PartialLinkText("English")).Click();
            //*/

            ////Thread.Sleep(2000);

            ////Debug.Assert(_driver.FindElement(By.PartialLinkText("French")).Enabled);
            ///*
            //_driver.FindElement(By.ClassName("ui-icon")).Click();
            //_driver.FindElement(By.ClassName("ui-icon")).Click();
            //_driver.FindElement(By.ClassName("ui-icon")).Click();
            //*/
            ////Thread.Sleep(2000);

            ////_driver.FindElement(By.PartialLinkText("French")).Click();

            ////Thread.Sleep(20000);

            ///*
            //for(int i = 0; i < 20; i++)
            //{
            //    _driver.FindElement(By.Id("countries")).SendKeys(Keys.ArrowDown);
            //}
            //*/

            //int n = 1;
            //bool pays = true;
            //string france = _driver.FindElement(By.XPath("//option[@value='France']")).GetAttribute("value");
            ////Console.Write(france);

            //string type = _driver.FindElement(By.Id("countries")).GetType().ToString();
            ////Console.Write(type);
            //string taille = _driver.FindElement(By.Id("countries")).Text.GetType().ToString();
            ////Console.Write(taille);

            //string value = _driver.FindElement(By.Id("countries")).Text;
            //Char delimiter = '\n';
            //String[] substrings = value.Split(delimiter);
            ///*
            //foreach (var substring in substrings)
            //    Console.WriteLine(substring);
            //    */

            ////Console.Write(substrings.Length);

            //while (pays)
            //{
            //    /*
            //    if (substrings[n - 1].Contains("France"))
            //        Console.Write(n - 1);
            //        */

            //    _driver.FindElement(By.Id("countries")).SendKeys(Keys.ArrowDown);
            //    n++;

            //    if (substrings[n - 1].Contains("France"))
            //        pays = false;
            //    //n = 5000;
            //}

            //_driver.FindElement(By.Id("yearbox")).SendKeys(Keys.ArrowDown);
            //_driver.FindElement(By.CssSelector("[ng-model*='monthbox']")).SendKeys(Keys.ArrowDown);
            //_driver.FindElement(By.Id("daybox")).SendKeys(Keys.ArrowDown);

            //_driver.FindElement(By.CssSelector("[ng-model*='Password']")).SendKeys("MDPmdp0");
            //_driver.FindElement(By.CssSelector("[ng-model*='CPassword']")).SendKeys("MDPmdp0");

            ////_driver.FindElement(By.LinkText("Automation Testing")).Click();
            ////_driver.FindElement(By.ClassName("facebook")).Click();

            ////_driver.FindElement(By.Id("imagesrc")).Click();
            //_driver.FindElement(By.Id("imagesrc")).SendKeys("C:/Users/PrestaConsole/Desktop/ALEX/bebe-dragon.jpg");

            ////Thread.Sleep(10000);

            ////_driver.FindElement(By.Id("submitbtn")).Click();
            ////_driver.FindElement(By.Id("Button1")).Click();

            //Thread.Sleep(5000);
            #endregion

            #region Web Table
            // _driver.FindElement(By.Id("1514560859110-uiGrid-0005-header-text")).Click(); // erreur !

            //_driver.Navigate().GoToUrl("http://demo.automationtesting.in/WebTable.html");

            //_driver.FindElement(By.Id("1515405249403-uiGrid-0006-header-text")).Click();
            //_driver.FindElement(By.ClassName("ui-grid-header-cell-label ng-binding")).Click();

            /*
            string adresse = "http://demo.automationtesting.in/WebTable.html";
            System.Net.WebClient WC = new System.Net.WebClient();
            System.IO.Stream s = WC.OpenRead(adresse);
            System.IO.StreamReader sr = new System.IO.StreamReader(s);
            string CodeSource = sr.ReadToEnd();

            //Console.Write(CodeSource);

            string ex = @"<\s*span[^>]*>(?<valeur>([^<]*))</span>";
            string source = CodeSource;
            Regex regex = new Regex(ex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var resultats = regex.Matches(source);
            //Console.Write(resultats.ToString());
            foreach (Match resultat in resultats)
            {
                Console.WriteLine(resultat.Groups["valeur"].Value);
            }
            */

            //_driver.FindElement(By.TagName("span[ui-grid-one-bind-id-grid*='col.uid + '-header-text'']")).Click();
            //_driver.FindElement(By.CssSelector("[aria-labelledby*='1515406463918-uiGrid-0006-header-text']")).Click();
            //Thread.Sleep(2000);
            //a.MoveToElement(_driver.FindElement(By.XPath("//*[@id=\"1515406463918 - uiGrid - 0006 - header - text\"]"))).Click();
            //var xx = _driver.FindElement(By.XPath("//*[@id=\"1515406463918 - 1 - uiGrid - 0005 - cell\"]/div"));
            //Thread.Sleep(5000);
            #endregion

            #region PHPTRAVELS
            _driver.Navigate().GoToUrl("http://www.phptravels.net/");
            Thread.Sleep(2000);

            //_driver.FindElement(By.Id("citiesInput")).Click();
            _driver.FindElement(By.Id("citiesInput")).Clear();
            _driver.FindElement(By.Id("citiesInput")).SendKeys("tetqsdq");
            //_driver.FindElement(By.Id("citiesInput")).Clear();

            //_driver.FindElement(By.ClassName("select2-input")).Clear();
            //_driver.FindElement(By.ClassName("select2-input")).SendKeys("djkcbdjc");

            //_driver.FindElement(By.Id("select2-drop")).Clear();
            //_driver.FindElement(By.XPath("//*[@id=\"select2 - drop\"]/div/input")).Clear();

            _driver.FindElement(By.CssSelector("[placeholder*='Check in']")).Clear();
            _driver.FindElement(By.CssSelector("[placeholder*='Check in']")).SendKeys("01/24/2018");
            _driver.FindElement(By.CssSelector("[placeholder*='Check in']")).SendKeys(Keys.Tab);
            //_driver.FindElement(By.CssSelector("[placeholder*='Check in']")).SendKeys(Keys.Enter);

            //_driver.FindElement(By.CssSelector("[type*='submit']")).Click();

            _driver.FindElement(By.ClassName("glyphicon-chevron-right")).Click();

            //_driver.FindElement(By.PartialLinkText("Flights")).Click();
            //*[@id="body-section"]/div[2]/div[2]/div/ul/li[3]/a/span
            //_driver.FindElement(By.XPath("//*[@id=\"body - section\"]/div[2]/div[2]/div/ul/li[3]/a/span")).Click();
            //_driver.FindElement(By.XPath("//*[@id='body - section']/div[2]/div[2]/div/ul/li[3]/a/span")).Click();
            _driver.FindElement(By.CssSelector("[href*='#TRAVELPAYOUTS']")).Click();

            Thread.Sleep(3000);

            _driver.FindElement(By.ClassName("mewtwo-swap_button")).Click();
            _driver.FindElement(By.Name("origin_name")).SendKeys("Paris");
            //_driver.FindElement(By.Name("origin_name")).SendKeys(Keys.Enter);

            //_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper")).Click();
            //_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper")).Click();
            //_driver.FindElement(By.ClassName("mewtwo-popup-ages-counter__plus")).Click();
            Actions action = new Actions(_driver);
            IAction maintien = action.ClickAndHold(_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper"))).Release(_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper"))).Build();
            maintien.Perform();
            //IAction doubleclic = action.DoubleClick(_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper"))).Build();
            Thread.Sleep(2000);
            string display = _driver.FindElement(By.CssSelector("body > div.mewtwo-modal-wrapper.mewtwo-modal--max")).GetAttribute("style");
            Console.Write("Avant : " + display + "\n");

            //for(int n = 0; n < 10; n++)
            //{
            //    maintien.Perform();
            //    Thread.Sleep(3000);
            //}

            //if(_driver.FindElement(By.ClassName("mewtwo-popup-ages-counter__plus")).Displayed)
            //{
            //    _driver.FindElement(By.ClassName("mewtwo-popup-ages-counter__plus")).Click();
            //    _driver.FindElement(By.XPath("//div[@role='children']/span[@role='increase']")).Click();
            //}
            //else
            //{
            //    maintien.Perform();
            //    _driver.FindElement(By.ClassName("mewtwo-popup-ages-counter__plus")).Click();
            //    _driver.FindElement(By.XPath("//div[@role='children']/span[@role='increase']")).Click();
            //}

            Passenger();

            Console.Write("Après : " + display + "\n");

            //_driver.FindElement(By.CssSelector("[role*='flights_submit']")).Click();

            _driver.FindElement(By.CssSelector("[href*='#TOURS']")).Click();

            Thread.Sleep(5000);
            #endregion
        }

        public void Passenger()
        {
            if (_driver.FindElement(By.ClassName("mewtwo-popup-ages-counter__plus")).Displayed)
            {
                _driver.FindElement(By.ClassName("mewtwo-popup-ages-counter__plus")).Click();
                _driver.FindElement(By.XPath("//div[@role='children']/span[@role='increase']")).Click();
                _driver.FindElement(By.XPath("//div[@role='infants']/span[@role='increase']")).Click();
                _driver.FindElement(By.Id("flight_type__checkboxe823caac1a24a0172d4ee792e5dad7d9")).Click();
                _driver.FindElement(By.CssSelector("[role*='ready_button']")).Click();
            }
            else
            {
                Actions actionP = new Actions(_driver);
                IAction maintienP = actionP.ClickAndHold(_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper"))).Release(_driver.FindElement(By.ClassName("mewtwo-flights-trip_class-wrapper"))).Build();
                maintienP.Perform();
                Passenger();
            }
        }

    }

}
