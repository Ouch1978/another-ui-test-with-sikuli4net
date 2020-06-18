using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Sikuli4Net.sikuli_REST;
using Sikuli4Net.sikuli_UTIL;

namespace AnotherUITestWithSikuli4Net
{
    [TestFixture]
    public class TestClass
    {
        static IWebDriver _webDriver;

        static APILauncher _launcher;

        private const string GoogleUrl = "https://www.google.com.tw/imghp?hl=zh-TW&tab=wi&ogbl";

        [SetUp]
        public static void TestSetup()
        {
            _launcher = new APILauncher( false );

            _webDriver = new ChromeDriver();

            ServicePointManager.SecurityProtocol = ( SecurityProtocolType ) 3072;

            _launcher.VerifyJarExists();

            _launcher.Start();

            _webDriver.Manage().Window.Maximize();

            _webDriver.Navigate().GoToUrl( GoogleUrl );
        }


        [Test]
        public void TestFindImagesWithSikuli()
        {
            Screen mainPage = new Screen();

            Pattern googleHeroImage = new Pattern( Path.Combine( AppDomain.CurrentDomain.BaseDirectory + @"\Images\Google-Image-Hero.png" ) );
            Pattern googleImageSearchBar = new Pattern( Path.Combine( AppDomain.CurrentDomain.BaseDirectory + @"\Images\Google-Image-Search-Bar.jpg" ) );
            Pattern taipei_101_01 = new Pattern( Path.Combine( AppDomain.CurrentDomain.BaseDirectory + @"\Images\Taipei-101-01.jpg" ) , 0.95 );
            Pattern taipei_101_02 = new Pattern( Path.Combine( AppDomain.CurrentDomain.BaseDirectory + @"\Images\Taipei-101-02.jpg" ) , 0.95 );

            mainPage.Wait( googleHeroImage );

            mainPage.Type( googleImageSearchBar , "101" + Key.ENTER );


            Thread.Sleep( 3000 );

            Assert.IsTrue( mainPage.Exists( taipei_101_01 ) );

            Assert.IsFalse( mainPage.Exists( taipei_101_02 ) );

        }

        [TearDown]
        public static void TestTearDown()
        {
            _webDriver.Quit();

            _launcher.Stop();
        }
    }
}