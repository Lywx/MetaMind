namespace MetaMind.Session.Model.Memory.Tagging
{
    internal static class TagGroupExample
    {
        private static void CreationExample()
        {
            var tag1 = new Tag("example1");
            var tag2 = new Tag("example2");
            var tag3 = new Tag("example3");
            var tag4 = new Tag("example4");
            var tag5 = new Tag("example5");

            var tagGroup = new TagGroup { tag1, tag2, tag3, tag4, tag5 };
        }

        private static void MatchExample()
        {
            var tag1 = new Tag("example1");
            var tag2 = new Tag("example2");
            var tag3 = new Tag("example1");

            var tagGroup = new TagGroup { tag1, tag2, tag3 };

            if (tag1 == tag2)
            {
                ;
            }

            var tagMatch = tagGroup.Find(tag => tag == tag1);
            if (tagMatch != null)
            {
                ;
            }
        }
    }
}