﻿namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Gui.Controls;
    using Microsoft.Xna.Framework;
    using NUnit.Framework;

    [TestFixture]
    public class Test_ControlSettings
    {
        private GameSettings settings;

        [SetUp]
        public void Test_Creation()
        {
            this.settings = new GameSettings { { "Size", new Point(5, 5) }, { "Color", Color.White } };
        }

        [Test]
        public void Test_LowLevelUse()
        {
            Assert.AreEqual(((Point)this.settings["Size"]).X, 5);
            Assert.AreEqual(((Point)this.settings["Size"]).Y, 5);

            Assert.AreEqual(((Color)this.settings["Color"]).A, 255);
            Assert.AreEqual(((Color)this.settings["Color"]).B, 255);
            Assert.AreEqual(((Color)this.settings["Color"]).G, 255);
        }

        [Test]
        public void Test_HighLevelUse()
        {
            Assert.AreEqual(this.settings.Get<Point>("Size").X, 5);
            Assert.AreEqual(this.settings.Get<Point>("Size").Y, 5);
        }

        [Test]
        public void Test_FrameVisualUse()
        {
            this.settings.Add("NameFrameSize", new Point(5, 5));

            var nameFrameSize = this.settings.Get<Point>("NameFrameSize");

            Assert.AreEqual(nameFrameSize.X, 5);
            Assert.AreEqual(nameFrameSize.Y, 5);
        }
    }
}