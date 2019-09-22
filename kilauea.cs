// ***********************************************************************
// Author           : AMERICAS\Luis_F_Aguirre
// Created          : 8/17/2019 8:54:00 AM
//
// Last Modified By : AMERICAS\Luis_F_Aguirre
// Last Modified On : 8/17/2019 8:54:00 AM
// ***********************************************************************
// <copyright file="Kilauea.cs" company="Dell">
//     Copyright (c) Dell 2019. All rights reserved.
// </copyright>
// <summary>Describe what is being tested in this test class here.</summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Dell.Adept.UI.Web.Testing.Framework;
using Dell.Adept.UI.Web.Testing.Framework.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ATLAS.PageObjects.Page_Objects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Text.RegularExpressions;
using ATLAS.Helper;

namespace ATLAS.TestLayer
{
    /// <summary>
    /// Summary description for Kilauea
    /// </summary>
    [TestClass]
    [TestCategory("Kilauea")]
    [SingleWebDriver(false)]
    public class Kilauea : WebUIMsTestBase
    {

        #region Initialization and clean up
        [TestInitialize]
        public void SetUp()
        {

            string tcName = TestContext.TestName;
            if (tcName == null) tcName = "TestNameNotFound";
            string location = CommonPageMethods.TestOpenLogFile(tcName);
            TestContext.AddResultFile(location);
        }
       

        [TestCleanup]
        public void Close()
        {
            Trace.Unindent();
            Trace.WriteLine(String.Format("{0:yyyy-MM-dd} {0:HH:mm:ss}", DateTime.Now) +
                " Test finished: " + TestContext.CurrentTestOutcome);
            Trace.Flush();
            Trace.Close();
            string tcName = TestContext.TestName;
            if (tcName == null) tcName = "TestNameNotFound";
            string location = CommonPageMethods.TestClose(tcName);
            TestContext.AddResultFile(location);

   
        }
        #endregion


        private HomePage homePage
        {
            get { return new HomePage(TestWebDriver); }
        }
        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestMethod]
        [Owner("Luis_F_Aguirre")]
        [Timeout(TestTimeout.Infinite)]
        [Priority(2)]
        [TestCategory("")]
        [Description("Describe the Test case here.")]
        public void Kilauea_Test()
        {
            int startIndex;
            int endIndex;
            string textTochange;

            string url = "https://volcanoes.usgs.gov/vhp/archive_search.html";
            NewMethod(url);

            SelectDropDown("//select[@id='typecd']", "Daily Update");
            SelectDropDown("//select[@id='vcd']", "Kilauea");

            List<string> dateList = GetDates();
            string startDate, endDate;
            foreach (string value in dateList)
            {
                startDate = value.Substring(0, 10);
                endDate = value.Substring(11, 10);

                TypeText("//input[@id='startdate']", startDate);
                TypeText("//input[@id='enddate']", endDate);
                TestWebDriver.FindElement(By.XPath("//input[@id='enddate']")).SendKeys(Keys.Tab);

                Thread.Sleep(3000);
                ClickElement("//input[@type='submit']");
                Thread.Sleep(3000);

                textTochange = GetText("//div[@id='results_DIV']");

                #region old
                //try
                //{
                //    startIndex = textTochange.IndexOf("class=\"text-center\"");
                //    endIndex = textTochange.IndexOf("<b>KILAUEA VOLCANO</b>");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("<b>KILAUEA VOLCANO</b>");
                //    endIndex = textTochange.IndexOf("name=\"summary\"");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("name=\"summary\"");
                //    endIndex = textTochange.IndexOf("Lava Flow Observations:");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("Lava Flow Observations:");
                //    endIndex = textTochange.IndexOf("Puʻu ʻŌʻō Observations:");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("Puʻu ʻŌʻō Observations:");
                //    endIndex = textTochange.IndexOf("Summit Observations:");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("Summit Observations:");
                //    endIndex = textTochange.IndexOf("Sulfur Dioxide Emission Rate estimation caveat:");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("Sulfur Dioxide Emission Rate estimation caveat:");
                //    endIndex = textTochange.IndexOf("Background:");
                //    RemoveTags(textTochange, startIndex, endIndex);

                //    startIndex = textTochange.IndexOf("Background:");
                //    endIndex = textTochange.IndexOf("Hazard Summary:");
                //    RemoveTags(textTochange, startIndex, endIndex);
                //    Trace.Write(Environment.NewLine);

                //}
                //catch (Exception e)
                //{
                //    Trace.Write(Environment.NewLine);
                //}
                #endregion
                textTochange = PrepareData(textTochange);
                Thread.Sleep(3000);
                ClickElement("//a[@class='search-control ssf']");
                Thread.Sleep(3000);
            }
        }

        private static string PrepareData(string textTochange)
        {
            textTochange = textTochange.Replace(";", ",");
            textTochange = textTochange.Replace("(1247 m)", "(1247 m);");
            textTochange = textTochange.Replace("U.S. Geological Survey", "U.S. Geological Survey;");
            textTochange = textTochange.Replace("UTC)", "UTC);");
            textTochange = textTochange.Replace("Activity Summary for past 24 hours:", ";Activity Summary for past 24 hours:");
            textTochange = textTochange.Replace("Past 24 hours at Kilauea summit:", ";Past 24 hours at Kilauea summit:");
            textTochange = textTochange.Replace("Past 24 hours at the middle east rift zone vents and flow field:", ";Past 24 hours at the middle east rift zone vents and flow field:");
            textTochange = textTochange.Replace("HAZARD ALERT:", ";HAZARD ALERT:");
            textTochange = textTochange.Replace("<b>", "").Replace("class=\"text-center\">", "").Replace("<span name=\"summary\">", "").Replace("</b>", "").Replace("</center>", "").Replace("<br>", "").Replace("<u>", "").Replace("<p>", "").Replace("</p>", "").Replace(@"\r\n?|\n", "").Replace("class=\"text - center\">", "").Trim();
            textTochange = textTochange.Replace("\r\n", "").Replace("</span><hr>", "").Replace("</u>", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
            Trace.Write(textTochange + ";");
            Trace.Write(Environment.NewLine);
            return textTochange;
        }

        private static List<string> GetDates()
        {
            //return new List<string>()
            List<string> ListtoChange = new List<string>()
            {
                "2015-01-01*2015-01-01",
                "2015-01-02*2015-01-02",
                "2015-01-03*2015-01-03",
                "2015-01-04*2015-01-04",
                "2015-01-05*2015-01-05",
                "2015-01-06*2015-01-06",
                "2015-01-07*2015-01-07",
                "2015-01-08*2015-01-08",
                "2015-01-09*2015-01-09",
                "2015-01-10*2015-01-10",
                "2015-01-11*2015-01-11",
                "2015-01-12*2015-01-12",
                "2015-01-13*2015-01-13",
                "2015-01-14*2015-01-14",
                "2015-01-15*2015-01-15",
                "2015-01-16*2015-01-16",
                "2015-01-17*2015-01-17",
                "2015-01-18*2015-01-18",
                "2015-01-19*2015-01-19",
                "2015-01-20*2015-01-20",
                "2015-01-21*2015-01-21",
                "2015-01-22*2015-01-22",
                "2015-01-23*2015-01-23",
                "2015-01-24*2015-01-24",
                "2015-01-25*2015-01-25",
                "2015-01-26*2015-01-26",
                "2015-01-27*2015-01-27",
                "2015-01-28*2015-01-28",
                "2015-01-29*2015-01-29",
                "2015-01-30*2015-01-30",
                "2015-01-31*2015-01-31",
                "2015-02-01*2015-02-01",
                "2015-02-02*2015-02-02",
                "2015-02-03*2015-02-03",
                "2015-02-04*2015-02-04",
                "2015-02-05*2015-02-05",
                "2015-02-06*2015-02-06",
                "2015-02-07*2015-02-07",
                "2015-02-08*2015-02-08",
                "2015-02-09*2015-02-09",
                "2015-02-10*2015-02-10",
                "2015-02-11*2015-02-11",
                "2015-02-12*2015-02-12",
                "2015-02-13*2015-02-13",
                "2015-02-14*2015-02-14",
                "2015-02-15*2015-02-15",
                "2015-02-16*2015-02-16",
                "2015-02-17*2015-02-17",
                "2015-02-18*2015-02-18",
                "2015-02-19*2015-02-19",
                "2015-02-20*2015-02-20",
                "2015-02-21*2015-02-21",
                "2015-02-22*2015-02-22",
                "2015-02-23*2015-02-23",
                "2015-02-24*2015-02-24",
                "2015-02-25*2015-02-25",
                "2015-02-26*2015-02-26",
                "2015-02-27*2015-02-27",
                "2015-02-28*2015-02-28",
                "2015-03-01*2015-03-01",
                "2015-03-02*2015-03-02",
                "2015-03-03*2015-03-03",
                "2015-03-04*2015-03-04",
                "2015-03-05*2015-03-05",
                "2015-03-06*2015-03-06",
                "2015-03-07*2015-03-07",
                "2015-03-08*2015-03-08",
                "2015-03-09*2015-03-09",
                "2015-03-10*2015-03-10",
                "2015-03-11*2015-03-11",
                "2015-03-12*2015-03-12",
                "2015-03-13*2015-03-13",
                "2015-03-14*2015-03-14",
                "2015-03-15*2015-03-15",
                "2015-03-16*2015-03-16",
                "2015-03-17*2015-03-17",
                "2015-03-18*2015-03-18",
                "2015-03-19*2015-03-19",
                "2015-03-20*2015-03-20",
                "2015-03-21*2015-03-21",
                "2015-03-22*2015-03-22",
                "2015-03-23*2015-03-23",
                "2015-03-24*2015-03-24",
                "2015-03-25*2015-03-25",
                "2015-03-26*2015-03-26",
                "2015-03-27*2015-03-27",
                "2015-03-28*2015-03-28",
                "2015-03-29*2015-03-29",
                "2015-03-30*2015-03-30",
                "2015-03-31*2015-03-31",
                "2015-04-01*2015-04-01",
                "2015-04-02*2015-04-02",
                "2015-04-03*2015-04-03",
                "2015-04-04*2015-04-04",
                "2015-04-05*2015-04-05",
                "2015-04-06*2015-04-06",
                "2015-04-07*2015-04-07",
                "2015-04-08*2015-04-08",
                "2015-04-09*2015-04-09",
                "2015-04-10*2015-04-10",
                "2015-04-11*2015-04-11",
                "2015-04-12*2015-04-12",
                "2015-04-13*2015-04-13",
                "2015-04-14*2015-04-14",
                "2015-04-15*2015-04-15",
                "2015-04-16*2015-04-16",
                "2015-04-17*2015-04-17",
                "2015-04-18*2015-04-18",
                "2015-04-19*2015-04-19",
                "2015-04-20*2015-04-20",
                "2015-04-21*2015-04-21",
                "2015-04-22*2015-04-22",
                "2015-04-23*2015-04-23",
                "2015-04-24*2015-04-24",
                "2015-04-25*2015-04-25",
                "2015-04-26*2015-04-26",
                "2015-04-27*2015-04-27",
                "2015-04-28*2015-04-28",
                "2015-04-29*2015-04-29",
                "2015-04-30*2015-04-30",
                "2015-05-01*2015-05-01",
                "2015-05-02*2015-05-02",
                "2015-05-03*2015-05-03",
                "2015-05-04*2015-05-04",
                "2015-05-05*2015-05-05",
                "2015-05-06*2015-05-06",
                "2015-05-07*2015-05-07",
                "2015-05-08*2015-05-08",
                "2015-05-09*2015-05-09",
                "2015-05-10*2015-05-10",
                "2015-05-11*2015-05-11",
                "2015-05-12*2015-05-12",
                "2015-05-13*2015-05-13",
                "2015-05-14*2015-05-14",
                "2015-05-15*2015-05-15",
                "2015-05-16*2015-05-16",
                "2015-05-17*2015-05-17",
                "2015-05-18*2015-05-18",
                "2015-05-19*2015-05-19",
                "2015-05-20*2015-05-20",
                "2015-05-21*2015-05-21",
                "2015-05-22*2015-05-22",
                "2015-05-23*2015-05-23",
                "2015-05-24*2015-05-24",
                "2015-05-25*2015-05-25",
                "2015-05-26*2015-05-26",
                "2015-05-27*2015-05-27",
                "2015-05-28*2015-05-28",
                "2015-05-29*2015-05-29",
                "2015-05-30*2015-05-30",
                "2015-05-31*2015-05-31",
                "2015-06-01*2015-06-01",
                "2015-06-02*2015-06-02",
                "2015-06-03*2015-06-03",
                "2015-06-04*2015-06-04",
                "2015-06-05*2015-06-05",
                "2015-06-06*2015-06-06",
                "2015-06-07*2015-06-07",
                "2015-06-08*2015-06-08",
                "2015-06-09*2015-06-09",
                "2015-06-10*2015-06-10",
                "2015-06-11*2015-06-11",
                "2015-06-12*2015-06-12",
                "2015-06-13*2015-06-13",
                "2015-06-14*2015-06-14",
                "2015-06-15*2015-06-15",
                "2015-06-16*2015-06-16",
                "2015-06-17*2015-06-17",
                "2015-06-18*2015-06-18",
                "2015-06-19*2015-06-19",
                "2015-06-20*2015-06-20",
                "2015-06-21*2015-06-21",
                "2015-06-22*2015-06-22",
                "2015-06-23*2015-06-23",
                "2015-06-24*2015-06-24",
                "2015-06-25*2015-06-25",
                "2015-06-26*2015-06-26",
                "2015-06-27*2015-06-27",
                "2015-06-28*2015-06-28",
                "2015-06-29*2015-06-29",
                "2015-06-30*2015-06-30",
                "2015-07-01*2015-07-01",
                "2015-07-02*2015-07-02",
                "2015-07-03*2015-07-03",
                "2015-07-04*2015-07-04",
                "2015-07-05*2015-07-05",
                "2015-07-06*2015-07-06",
                "2015-07-07*2015-07-07",
                "2015-07-08*2015-07-08",
                "2015-07-09*2015-07-09",
                "2015-07-10*2015-07-10",
                "2015-07-11*2015-07-11",
                "2015-07-12*2015-07-12",
                "2015-07-13*2015-07-13",
                "2015-07-14*2015-07-14",
                "2015-07-15*2015-07-15",
                "2015-07-16*2015-07-16",
                "2015-07-17*2015-07-17",
                "2015-07-18*2015-07-18",
                "2015-07-19*2015-07-19",
                "2015-07-20*2015-07-20",
                "2015-07-21*2015-07-21",
                "2015-07-22*2015-07-22",
                "2015-07-23*2015-07-23",
                "2015-07-24*2015-07-24",
                "2015-07-25*2015-07-25",
                "2015-07-26*2015-07-26",
                "2015-07-27*2015-07-27",
                "2015-07-28*2015-07-28",
                "2015-07-29*2015-07-29",
                "2015-07-30*2015-07-30",
                "2015-07-31*2015-07-31",
                "2015-08-01*2015-08-01",
                "2015-08-02*2015-08-02",
                "2015-08-03*2015-08-03",
                "2015-08-04*2015-08-04",
                "2015-08-05*2015-08-05",
                "2015-08-06*2015-08-06",
                "2015-08-07*2015-08-07",
                "2015-08-08*2015-08-08",
                "2015-08-09*2015-08-09",
                "2015-08-10*2015-08-10",
                "2015-08-11*2015-08-11",
                "2015-08-12*2015-08-12",
                "2015-08-13*2015-08-13",
                "2015-08-14*2015-08-14",
                "2015-08-15*2015-08-15",
                "2015-08-16*2015-08-16",
                "2015-08-17*2015-08-17",
                "2015-08-18*2015-08-18",
                "2015-08-19*2015-08-19",
                "2015-08-20*2015-08-20",
                "2015-08-21*2015-08-21",
                "2015-08-22*2015-08-22",
                "2015-08-23*2015-08-23",
                "2015-08-24*2015-08-24",
                "2015-08-25*2015-08-25",
                "2015-08-26*2015-08-26",
                "2015-08-27*2015-08-27",
                "2015-08-28*2015-08-28",
                "2015-08-29*2015-08-29",
                "2015-08-30*2015-08-30",
                "2015-08-31*2015-08-31",
                "2015-09-01*2015-09-01",
                "2015-09-02*2015-09-02",
                "2015-09-03*2015-09-03",
                "2015-09-04*2015-09-04",
                "2015-09-05*2015-09-05",
                "2015-09-06*2015-09-06",
                "2015-09-07*2015-09-07",
                "2015-09-08*2015-09-08",
                "2015-09-09*2015-09-09",
                "2015-09-10*2015-09-10",
                "2015-09-11*2015-09-11",
                "2015-09-12*2015-09-12",
                "2015-09-13*2015-09-13",
                "2015-09-14*2015-09-14",
                "2015-09-15*2015-09-15",
                "2015-09-16*2015-09-16",
                "2015-09-17*2015-09-17",
                "2015-09-18*2015-09-18",
                "2015-09-19*2015-09-19",
                "2015-09-20*2015-09-20",
                "2015-09-21*2015-09-21",
                "2015-09-22*2015-09-22",
                "2015-09-23*2015-09-23",
                "2015-09-24*2015-09-24",
                "2015-09-25*2015-09-25",
                "2015-09-26*2015-09-26",
                "2015-09-27*2015-09-27",
                "2015-09-28*2015-09-28",
                "2015-09-29*2015-09-29",
                "2015-09-30*2015-09-30",
                "2015-10-01*2015-10-01",
                "2015-10-02*2015-10-02",
                "2015-10-03*2015-10-03",
                "2015-10-04*2015-10-04",
                "2015-10-05*2015-10-05",
                "2015-10-06*2015-10-06",
                "2015-10-07*2015-10-07",
                "2015-10-08*2015-10-08",
                "2015-10-09*2015-10-09",
                "2015-10-10*2015-10-10",
                "2015-10-11*2015-10-11",
                "2015-10-12*2015-10-12",
                "2015-10-13*2015-10-13",
                "2015-10-14*2015-10-14",
                "2015-10-15*2015-10-15",
                "2015-10-16*2015-10-16",
                "2015-10-17*2015-10-17",
                "2015-10-18*2015-10-18",
                "2015-10-19*2015-10-19",
                "2015-10-20*2015-10-20",
                "2015-10-21*2015-10-21",
                "2015-10-22*2015-10-22",
                "2015-10-23*2015-10-23",
                "2015-10-24*2015-10-24",
                "2015-10-25*2015-10-25",
                "2015-10-26*2015-10-26",
                "2015-10-27*2015-10-27",
                "2015-10-28*2015-10-28",
                "2015-10-29*2015-10-29",
                "2015-10-30*2015-10-30",
                "2015-10-31*2015-10-31",
                "2015-11-01*2015-11-01",
                "2015-11-02*2015-11-02",
                "2015-11-03*2015-11-03",
                "2015-11-04*2015-11-04",
                "2015-11-05*2015-11-05",
                "2015-11-06*2015-11-06",
                "2015-11-07*2015-11-07",
                "2015-11-08*2015-11-08",
                "2015-11-09*2015-11-09",
                "2015-11-10*2015-11-10",
                "2015-11-11*2015-11-11",
                "2015-11-12*2015-11-12",
                "2015-11-13*2015-11-13",
                "2015-11-14*2015-11-14",
                "2015-11-15*2015-11-15",
                "2015-11-16*2015-11-16",
                "2015-11-17*2015-11-17",
                "2015-11-18*2015-11-18",
                "2015-11-19*2015-11-19",
                "2015-11-20*2015-11-20",
                "2015-11-21*2015-11-21",
                "2015-11-22*2015-11-22",
                "2015-11-23*2015-11-23",
                "2015-11-24*2015-11-24",
                "2015-11-25*2015-11-25",
                "2015-11-26*2015-11-26",
                "2015-11-27*2015-11-27",
                "2015-11-28*2015-11-28",
                "2015-11-29*2015-11-29",
                "2015-11-30*2015-11-30",
                "2015-12-01*2015-12-01",
                "2015-12-02*2015-12-02",
                "2015-12-03*2015-12-03",
                "2015-12-04*2015-12-04",
                "2015-12-05*2015-12-05",
                "2015-12-06*2015-12-06",
                "2015-12-07*2015-12-07",
                "2015-12-08*2015-12-08",
                "2015-12-09*2015-12-09",
                "2015-12-10*2015-12-10",
                "2015-12-11*2015-12-11",
                "2015-12-12*2015-12-12",
                "2015-12-13*2015-12-13",
                "2015-12-14*2015-12-14",
                "2015-12-15*2015-12-15",
                "2015-12-16*2015-12-16",
                "2015-12-17*2015-12-17",
                "2015-12-18*2015-12-18",
                "2015-12-19*2015-12-19",
                "2015-12-20*2015-12-20",
                "2015-12-21*2015-12-21",
                "2015-12-22*2015-12-22",
                "2015-12-23*2015-12-23",
                "2015-12-24*2015-12-24",
                "2015-12-25*2015-12-25",
                "2015-12-26*2015-12-26",
                "2015-12-27*2015-12-27",
                "2015-12-28*2015-12-28",
                "2015-12-29*2015-12-29",
                "2015-12-30*2015-12-30",
                "2015-12-31*2015-12-31",

};
            List<string> ListToReturn = new List<string>();
            List<string> yearsTogetValue = new List<string>()
            {
                "2010", "2011", "2012","2013","2014", "2015","2016","2017","2018"
            };
            foreach(string value in yearsTogetValue)
            {
                foreach(string valueListToChange in ListtoChange)
                {
                    ListToReturn.Add(valueListToChange.Replace("2015", value));
                }
            }
            return ListToReturn;
        }

        private static string lineToPrint = "";
        private static void RemoveTags(string textTochange, int startIndex, int endIndex)
        {
            lineToPrint = textTochange.Substring(startIndex, endIndex);
            lineToPrint = lineToPrint.Replace("<b>", "").Replace("class=\"text-center\">", "").Replace("<span name=\"summary\">", "").Replace("</b>", "").Replace("</center>", "").Replace("<br>", "").Replace("<u>", "").Replace(";", "").Replace("<p>", "").Replace("</p>", "").Replace(@"\r\n?|\n", "").Replace("class=\"text - center\">", "").Trim();
            lineToPrint = lineToPrint.Replace("\r\n", "").Replace("</span><hr>", "").Replace("</u>", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
            lineToPrint = lineToPrint.Replace("HAWAIIAN VOLCANO OBSERVATORY DAILY UPDATEU.S. Geological Survey", "");
            lineToPrint = lineToPrint.Replace("Thi report on the statu of Kilauea volcanic activity, in addition to maps, photos, and Webcam image (available at http://hvo.wr.usgs.gov/activity/kilaueastatus.php), wa prepared by the USGS Hawaiian Volcano Observatory (HVO). All time are Hawai`i Standard Time. KILAUEA VOLCANO(VNUM #332010)19Â°25'16\" N 155Â°17'13\" W,             Summit Elevation 4091 ft (1247 m)", "");
            lineToPrint = lineToPrint.Trim();
            lineToPrint = lineToPrint.Replace(Environment.NewLine, "");
            lineToPrint = Regex.Replace(lineToPrint, @"s +", " ").Trim();
            Trace.Write(lineToPrint);
            Trace.Write(";");
        }

        private static string RemovePhase(string text, string textToRemove)
        {
            return text.Replace(textToRemove, "");
        }

        private static string GetText(string xpath)
        {
            return TestWebDriver.FindElement(By.XPath(xpath)).GetAttribute("innerHTML");
        }

        private static void TypeText(string xpath, string value)
        {
            TestWebDriver.FindElement(By.XPath(xpath)).Clear();
            TestWebDriver.FindElement(By.XPath(xpath)).SendKeys(value);

        }

        private static void ClickElement(string xpath)
        {
            TestWebDriver.FindElement(By.XPath(xpath)).Click();
        }

        private static void NewMethod(string url)
        {
            TestWebDriver.Navigate().Refresh();
            TestWebDriver.Navigate().GoToUrl(url);
            TestWebDriver.Manage().Window.Maximize();
        }

        private static void SelectDropDown(string xpath, string value)
        {
            var education = TestWebDriver.FindElement(By.XPath(xpath));
            //create select element object 
            var selectElement = new SelectElement(education);

            //select by value
            selectElement.SelectByText(value);
        }
    }
}

