namespace MetaMind.TestimonyTest.Concepts.Tests
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Testimony.Concepts.Tests;

    [TestFixture]
    public class TestOrganizerTest
    {
        [Test]
        public void SortTest()
        {
            var test = new Test("Root", "", "");

            test.Children.Add(new Test("A.A", "", ""));
            test.Children.Add(new Test("A.B", "", ""));
            test.Children.Add(new Test("A.A.A", "", ""));
            test.Children.Add(new Test("A.A.C", "", ""));
            test.Children.Add(new Test("A.Z.A", "", ""));
            test.Children.Add(new Test("A.K.A", "", ""));

            var organizer = new TestOrganizer();
            organizer.Organize(test.Children);

            Action<ITest> childrenWriteLine = t =>
            {
                Console.WriteLine(t.Name);

                foreach (var child in t.Children)
                {
                    Console.WriteLine(child.Name);
                }
            };

            foreach (var child in test.Children)
            {
                foreach (var c in child.Children)
                {
                    childrenWriteLine(c);
                }
            } 
        }

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
    }
}
