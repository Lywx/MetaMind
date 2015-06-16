namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Engine.Extensions;
    using Extensions;

    public class TestOrganizer
    {
        public void Organize(ITest test)
        {
            this.Organize(test.Children);
        }

        public void Organize(IList<ITest> tests)
        {
            this.Organize(tests, tests);
        }

        /// <summary>
        /// Organize tests list into tree structure automatically inserting missing nodes.
        /// </summary>
        /// <param name="tests">Original tree structure</param>
        /// <param name="groupedTests">Recursively refined tree structure</param>
        /// <param name="groupedNames">Recursively refined group name</param>
        /// <param name="level">Depth of recursion</param>
        private void Organize(IList<ITest> tests, IList<ITest> groupedTests, HashSet<string> groupedNames = null, int level = 1)
        {
            var groups = new Dictionary<string, List<ITest>>();
            var groupNames = this.UniqueGroupName(groupedTests, level);

            // Return when group name is the same at the previous call
            if (groupedNames != null && groupNames.SequenceEqual(groupedNames))
            {
                return;
            }

            // Group the tests according to the test name prefix
            foreach (var groupName in groupNames)
            {
                groups[groupName] = new List<ITest>();

                foreach (var test in groupedTests)
                {
                    if (test.Name.StartsWith(groupName))
                    {
                        groups[groupName].Add(test);
                    }
                }
            }

            foreach (var groupName in groupNames)
            {
                this.Organize(tests, groups[groupName], groupNames, level + 1);
            }

            foreach (var group in groups)
            {
                var found = false;

                foreach (var existing in group.Value)
                {
                    if (existing.Name == group.Key)
                    {
                        // Find the other tests in the same group that is children of existing parent
                        var relocated = group.Value.Where(test => existing != test).ToList();
                        var relocatedChildren = existing.Children;

                        // Relocate tests in group
                        groupedTests.RemoveRange(relocated);
                        groupedTests.RemoveRange(relocatedChildren);

                        existing.Children.AddRange(relocated);

                        found = true;

                        break;
                    }
                }

                // Auto injection of not found node
                if (!found)
                {
                    // Get the first path in group
                    var vacant = new Test(@group.Key, "", @group.Value[0].Path);

                    // Relocate tests in group
                    groupedTests.RemoveRange(group.Value);
                    vacant.Children.AddRange(group.Value);

                    groupedTests.Add(vacant);
                }
            }

            // Sorting in not essential for this process
            groupedTests.Sort((test, other) => test.CompareTo(other));

            // Modify the tests according to the grouped tests
            if (level == 1)
            {
                tests.RemoveRange(groupedTests.Where(item => !groupNames.Contains(item.Name)));
                tests.AddRange(groupedTests.Where(item => groupNames.Contains(item.Name) && !tests.Contains(item)));

                tests.Sort((test, other) => test.CompareTo(other));
            }
        }

        private HashSet<string> UniqueGroupName(IList<ITest> groupedTests, int level)
        {
            return groupedTests.Select(child => this.LeftCrop(child.Name, level))
                               .ToHashSet();
        }

        internal string LeftCrop(string name, int level = 1)
        {
            var nameGroup = name.Split('.');
            var nameTrimmed = string.Join(".", nameGroup.Take(level));

            return nameTrimmed;
        }
    }
}