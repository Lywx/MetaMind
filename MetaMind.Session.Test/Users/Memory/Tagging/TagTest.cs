using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.SessionTest.Users.Memory.Tagging
{
    using NUnit.Framework;
    using Session.Users.Memory.Tagging;

    [TestFixture]
    public class TagTest
    {
        private Tag tag;

        private TagGroup tagGroup;

        [SetUp]
        public void Setup()
        {
            this.tag = new Tag("example");

            this.tagGroup = new TagGroup();
        }

        [Test]
        public void MatchTest()
        {
            this.tagGroup.Add(this.tag);

            if (this.tagGroup.Find("a"))
        }
    }
}
