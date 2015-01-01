using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaMind.AcutanceUnitTest
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Guis.Widgets;

    [TestClass]
    public class CommandParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string CommandString = "[Mon AM 10:2:0] CMD: Are you okay?";
            const string PathString    = "TestPath.txt";

            var commandEntry = CommandParser.ParseLine(CommandString, PathString);

            Assert.AreEqual(commandEntry.Name, "Are you okey?");
            Assert.AreEqual(commandEntry.Path, PathString);
            Assert.AreEqual(commandEntry.Mode, CommandMode.TriggeredByDate);
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string CommandString = "[Eve PM 2:2:0] CMD: Are you okay?";
            const string PathString    = "TestPath.txt";

            var commandEntry = CommandParser.ParseLine(CommandString, PathString);

            Assert.AreEqual(commandEntry.Name, "Are you okey?");
            Assert.AreEqual(commandEntry.Path, PathString);
            Assert.AreEqual(commandEntry.Mode, CommandMode.TriggeredByDate);
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string CommandString = "[Eve PM 13:2:0] CMD: Are you okay?";
            const string PathString    = "TestPath.txt";

            var commandEntry = CommandParser.ParseLine(CommandString, PathString);

            Assert.AreEqual(commandEntry.Name, "Are you okey?");
            Assert.AreEqual(commandEntry.Path, PathString);
            Assert.AreEqual(commandEntry.Mode, CommandMode.TriggeredByDate);
        }
    }
}
