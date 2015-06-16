namespace MetaMind.TestimonyTest.Concepts.Tests
{
    using System;
    using NUnit.Framework;
    using Testimony.Concepts.Tests;

    [TestFixture]
    public class TestOrganizerTest
    {
        private TestOrganizer organizer;

        [TestFixtureSetUp]
        private void SetupTest()
        {
            this.organizer = new TestOrganizer();
        }

        [Test]
        public void SortTest1()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A", "", ""));
            test.Children.Add(new Test("A.B", "", ""));
            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.Z.A", "", ""));
            test.Children.Add(new Test("A.K.A", "", ""));

            this.organizer.Organize(test);

            this.PrintChildrenTests(test);
        }

        [Test]
        public void SortTest2()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.Z.A", "", ""));
            test.Children.Add(new Test("A.K.A", "", ""));

            this.organizer.Organize(test);

            this.PrintChildrenTests(test);
        }

        [Test]
        public void SortTest3()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));

            this.organizer.Organize(test);

            this.PrintChildrenTests(test);
        }

        [Test]
        public void LeftCropTest()
        {
            // Third level
            Assert.AreEqual("A", this.organizer.LeftCrop("A.B.C"));

            // Second level
            Assert.AreEqual("A", this.organizer.LeftCrop("A.B"));

            // Top level 
            Assert.AreEqual("A", this.organizer.LeftCrop("A"));
        }

        private void PrintChildrenTests(ITest test)
        {
            foreach (var child in test.Children)
            {
                this.PrintTest(child);

                this.PrintChildrenTests(child);
            } 
        }

        private void PrintTest(ITest t)
        {
            Console.WriteLine(t.Name);
        }
    }
}
