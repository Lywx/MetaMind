namespace MetaMind.AcutanceUnitTest.Parsers
{
    using System;
    using System.IO;
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Parsers.Grammars;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sprache;

    [TestClass]
    public class ScheduleParserTest
    {
        [TestMethod]
        public void ScheduleDayVerbose()
        {
            var input = "Monday";
            var parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Monday, parsed);
            
            input = "Tuesday";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Tuesday, parsed);

            input = "Wednesday";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Wednesday, parsed);

            input = "Thursday";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Thursday, parsed);

            input = "Friday";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Friday, parsed);

            input = "Saturday";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Saturday, parsed);

            input = "Sunday";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Sunday, parsed);
        }

        [TestMethod]
        public void ScheduleDayConcise()
        {
            var input = "Mon";
            var parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Monday, parsed);

            input = "Tue";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Tuesday, parsed);

            input = "Wed";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Wednesday, parsed);

            input = "Thu";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Thursday, parsed);
            
            input = "Fri";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Friday, parsed);

            input = "Sat";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Saturday, parsed);

            input = "Sun";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Sunday, parsed);

            input = "-";
            parsed = ScheduleGrammar.DayParser.Parse(input);
            Assert.AreEqual(DayTag.Unspecified, parsed);
        }


        [TestMethod]
        public void ScheduleRepeativity()
        {
            var input = "Everyday";
            var parsed = ScheduleGrammar.RepeativityParser.Parse(input);
            Assert.AreEqual(RepeativityTag.EveryDay, parsed);
            
            input = "EveryWeek";
            parsed = ScheduleGrammar.RepeativityParser.Parse(input);
            Assert.AreEqual(RepeativityTag.EveryWeek, parsed);

            // TODO: Disabled for safety issue
            // input = "EveryMonth";
            // parsed = ScheduleGrammar.RepeativityParser.Parse(input);
            // Assert.AreEqual(RepeativityTag.EveryMonth, parsed);

            input = "-";
            parsed = ScheduleGrammar.RepeativityParser.Parse(input);
            Assert.AreEqual(RepeativityTag.Unspecified, parsed);
        }

        [TestMethod]
        public void ScheduleDate()
        {
            var input = "Everyday - 8:0:0";

            var elems = input.Split(' ');

            var repeativity = ScheduleGrammar.RepeativityParser .Parse(elems[0]);
            var day         = ScheduleGrammar.DayParser         .Parse(elems[1]);
            var time        = KnowledgeGrammar.TimeTagFullParser.Parse(elems[2]);

            Assert.AreEqual(RepeativityTag.EveryDay, repeativity);
            Assert.AreEqual(DayTag.Unspecified, day);
            Assert.AreEqual(8, time.Hours);
            Assert.AreEqual(0, time.Minutes);
            Assert.AreEqual(0, time.Seconds);
        }


        [TestMethod]
        public void ScheduleASchedule()
        {
            var input = TestResources.AScheduleSample;

            var parsed = ScheduleGrammar.ScheduleParser.Parse(input);

            Assert.AreEqual(DayTag.Unspecified, parsed.Date.Day);
            Assert.AreEqual(8, parsed.Date.Time.Hours);
            Assert.AreEqual(0, parsed.Date.Time.Minutes);
            Assert.AreEqual(0, parsed.Date.Time.Seconds);
            Assert.AreEqual(RepeativityTag.EveryDay, parsed.Date.Repeativity);
            Assert.AreEqual(TestResources.AScheduleSampleScheduleContent, parsed.Content);
        }

        [TestMethod]
        public void ScheduleTwoSchedule()
        {
            var input = TestResources.TwoScheduleSample;

            var parsed = ScheduleGrammar.ScheduleFileParser.Parse(input);

            Assert.AreEqual(2, parsed.Schedules.Count);
        }

        [TestMethod]
        public void ScheduleCommandExtraction()
        {
            var input = TestResources.AScheduleSampleScheduleContent;
            
            var parsed = ScheduleGrammar.CommandUnitParser.Parse(input);
            
            Assert.AreEqual(TestResources.AScheduleSampleCommandContent, parsed);
        }

    }
}
