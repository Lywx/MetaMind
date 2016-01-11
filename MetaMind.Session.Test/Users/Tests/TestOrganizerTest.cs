namespace MetaMind.UnityTest.Concepts.Tests
{
    using System;
    using NUnit.Framework;
    using Session.Tests;

    [TestFixture]
    public class TestOrganizerTest
    {
        private TestOrganizer organizer;

        [SetUp]
        public void Setup()
        {
            this.organizer = new TestOrganizer();
        }

        [Test]
        public void CropStartTest1()
        {
            // Top level 
            Assert.AreEqual("A", this.organizer.CropStart("A"));
        }

        [Test]
        public void CropStartTest2()
        {
            // Second level
            Assert.AreEqual("A", this.organizer.CropStart("A.B"));
        }

        [Test]
        public void CropStartTest3()
        {
            // Third level
            Assert.AreEqual("A", this.organizer.CropStart("A.B.C"));
        }

        [Test]
        public void OrganizeTest1()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A", "", ""));
            test.Children.Add(new Test("A.B", "", ""));
            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.Z.A", "", ""));
            test.Children.Add(new Test("A.K.A", "", ""));

            this.organizer.Organize(test);

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
        public void OrganizeTest2()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.Z.A", "", ""));
            test.Children.Add(new Test("A.K.A", "", ""));

            this.organizer.Organize(test);

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
        public void OrganizeTest3()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));

            this.organizer.Organize(test);

            Assert.AreEqual("A",     test.Children[0].Name);
            Assert.AreEqual("A.A",   test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.C", test.Children[0].Children[0].Children[1].Name);

            // Extra check
            PrintChildrenTests(test);
        }

        [Test]
        public void OrganizeTest4()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.A", "", ""));

            this.organizer.Organize(test);

            Assert.AreEqual("A", test.Children[0].Name);
            Assert.AreEqual("A.A", test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A", test.Children[0].Children[0].Children[1].Name);

            // Extra check
            PrintChildrenTests(test);
        }

        [Test]
        public void OrganizeTest5()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A", "", ""));
            test.Children.Add(new Test("A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.A.A.C", "", ""));

            this.organizer.Organize(test);

            Assert.AreEqual("A",       test.Children[0].Name);
            Assert.AreEqual("A.A",     test.Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A",   test.Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.A.C", test.Children[0].Children[0].Children[0].Children[0].Name);
            Assert.AreEqual("A.A.C",   test.Children[0].Children[0].Children[1].Name);
            Assert.AreEqual("A.A.A",   test.Children[0].Children[0].Children[0].Name);

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
