namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Engine.Extensions;
    using Extensions;

    public class TestOrganizer
    {
        public void Organize(IList<ITest> tests)
        {
            this.Organize(tests, tests.Count);
        }

        private void Organize(IList<ITest> tests, int count, int level = 1)
        {
            if (tests.Count < 2)
            {
                return;
            }

            var groups = new Dictionary<string, List<ITest>>();
            var groupNames =
                tests.Select(child => this.LeftTrim(child.Name, level))
                          .ToHashSet();

            foreach (var groupName in groupNames)
            {
                groups[groupName] = new List<ITest>();

                foreach (var t in tests)
                {
                    if (t.Name.StartsWith(groupName))
                    {
                        groups[groupName].Add(t);
                    }
                }

                // Recursive when result is more polished
                if (groups.Count < count)
                {
                    this.Organize(groups[groupName], groups.Count, level + 1);
                }
            }

            foreach (var group in groups)
            {
                var found = false;

                foreach (var t in group.Value)
                {
                    if (t.Name == group.Key)
                    {
                        var relocated = @group.Value.Where(test => t != test).ToList();

                        tests.RemoveRange(relocated);
                        t.Children.AddRange(relocated);

                        found = true;
                    }
                }

                // Auto injection of not found node
                if (!found)
                {
                    // Get the first path in group
                    var vacant = new Test(@group.Key, "", @group.Value[0].Path);

                    tests.RemoveRange(group.Value);
                    vacant.Children.AddRange(group.Value);

                    tests.Add(vacant);
                }
            }

            // Remove the not grouped member in the tail of recursion.
            // Since in the head recursion, "tests" is the not part of the "groups".
            // As a result, when calling tests.RemoveRange() on tests on recursive Organize, 
            // original "tests" is not affected. It is only possible to modify original "tests" 
            // at the tail of recursion.
            tests.RemoveRange(tests.Where(item => !groupNames.Contains(item.Name)));

            // Sort at last
            tests.Sort((test, other) => test.CompareTo(other));
        }

        internal string LeftTrim(string name, int level = 1)
        {
            var nameGroup = name.Split('.');
            var nameTrimmed = string.Join(".", nameGroup.Take(level));

            return nameTrimmed;
        }

        internal string RightTrim(string name, int level = 1)
        {
            var nameGroup = name.Split('.');
            var nameTrimmed = string.Join(".", nameGroup.Take(nameGroup.Length - level));

            return string.IsNullOrEmpty(nameTrimmed) ? "^" : nameTrimmed;
        }
    }
}