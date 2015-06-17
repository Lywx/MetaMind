namespace MetaMind.TestimonyTest.Concepts.Tests
{
    using System;
    using NUnit.Framework;
    using Testimony.Concepts.Tests;

    [TestFixture]
    public class TestOrganizerTest
    {
        [Test]
        public void LeftCropTest()
        {
            var organizer = new TestOrganizer();

            // Third level
            Assert.AreEqual("A", organizer.LeftCrop("A.B.C"));

            // Second level
            Assert.AreEqual("A", organizer.LeftCrop("A.B"));

            // Top level 
            Assert.AreEqual("A", organizer.LeftCrop("A"));
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

            var organizer = new TestOrganizer();
            organizer.Organize(test);

            Assert.AreEqual("A",     test.Children[0].Name);
            Assert.AreEqual("A.A",   test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.C", test.Children[0].Children[0].Children[1].Name);
            Assert.AreEqual("A.B",   test.Children[0].Children[1].Name);
            Assert.AreEqual("A.K",   test.Children[0].Children[2].Name);
            Assert.AreEqual("A.K.A", test.Children[0].Children[2].Children[0].Name);
            Assert.AreEqual("A.Z",   test.Children[0].Children[3].Name);
            Assert.AreEqual("A.Z.A", test.Children[0].Children[3].Children[0].Name);

            // Extra check
            PrintChildrenTests(test);
        }

        [Test]
        public void SortTest2()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.Z.A", "", ""));
            test.Children.Add(new Test("A.K.A", "", ""));

            var organizer = new TestOrganizer();
            organizer.Organize(test);

            Assert.AreEqual("A",     test.Children[0].Name);
            Assert.AreEqual("A.A",   test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.C", test.Children[0].Children[0].Children[1].Name);
            Assert.AreEqual("A.K",   test.Children[0].Children[1].Name);
            Assert.AreEqual("A.K.A", test.Children[0].Children[1].Children[0].Name);
            Assert.AreEqual("A.Z",   test.Children[0].Children[2].Name);
            Assert.AreEqual("A.Z.A", test.Children[0].Children[2].Children[0].Name);

            // Extra check
            PrintChildrenTests(test);
        }

        [Test]
        public void SortTest3()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));

            var organizer = new TestOrganizer();
            organizer.Organize(test);

            Assert.AreEqual("A",     test.Children[0].Name);
            Assert.AreEqual("A.A",   test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.C", test.Children[0].Children[0].Children[1].Name);

            // Extra check
            PrintChildrenTests(test);
        }

        [Test]
        public void SortTest4()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A", "", ""));
            test.Children.Add(new Test("A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.A.A.C", "", ""));

            var organizer = new TestOrganizer();
            organizer.Organize(test);

            Assert.AreEqual("A",       test.Children[0].Name);
            Assert.AreEqual("A.A",     test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A",   test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A.C", test.Children[0].Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.C",   test.Children[0].Children[0].Children[1].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[0].Name);

            // Extra check
            PrintChildrenTests(test);
        }

        private static void PrintChildrenTests(ITest test)
        {
            foreach (var child in test.Children)
            {
                PrintTest(child);

                PrintChildrenTests(child);
            } 
        }

        private static void PrintTest(ITest t)
        {
            Console.WriteLine(t.Name);
        }
    }
}
