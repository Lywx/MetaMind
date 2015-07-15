namespace MetaMind.Testimony.Concepts.Operations
{
    using System.Collections.Generic;
    using System.Linq;
    using Engine.Extensions;
    using Extensions;

    /// <remakrs>
    /// Copied from the TestOrganizer
    /// </remakrs>
    public class OperationOrganizer
    {
        public void Organize(IOperationDescription operation)
        {
            this.Organize(operation.Children);
        }

        public void Organize(IList<IOperationDescription> operations)
        {
            this.Organize(operations, operations);
        }

        /// <summary>
        /// Organize operations list into tree structure automatically inserting missing nodes.
        /// </summary>
        /// <param name="operations">Original tree structure</param>
        /// <param name="groupedoperations">Recursively refined tree structure</param>
        /// <param name="groupedNames">Recursively refined group name</param>
        /// <param name="level">Depth of recursion</param>
        private void Organize(IList<IOperationDescription> operations, IList<IOperationDescription> groupedoperations, HashSet<string> groupedNames = null, int level = 1)
        {
            var groups = new Dictionary<string, List<IOperationDescription>>();
            var groupNames = this.GroupNamesUnique(groupedoperations, level);

            // Return when group name is the same at the previous call
            if (groupedNames != null && groupNames.SequenceEqual(groupedNames))
            {
                return;
            }

            // Group the operations according to the operation name prefix

            var linkedoperations = new LinkedList<IOperationDescription>(groupedoperations);

            // Reverse is to make A.A before A to reduce iteration afterwards
            foreach (var groupName in groupNames.Reverse())
            {
                groups[groupName] = new List<IOperationDescription>();

                var current = linkedoperations.First;
                while (current != null)
                {
                    var operation = current.Value;

                    if (operation.Name.StartsWith(groupName))
                    {
                        groups[groupName].Add(operation);

                        // Remove added operations
                        var removal = current;
                        current = current.Next;

                        // Avoid duplicate operations under different group name
                        linkedoperations.Remove(removal);
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
            }

            foreach (var groupName in groupNames)
            {
                this.Organize(operations, groups[groupName], groupNames, level + 1);
            }

            foreach (var group in groups)
            {
                var found = false;

                foreach (var existing in @group.Value)
                {
                    if (existing.Name == @group.Key)
                    {
                        // Find the other operations in the same group that is children of existing parent
                        var relocated = @group.Value
                                             .Where(operation => existing != operation && existing.Name != operation.Name)
                                             .ToList();

                        // Relocate operations in group
                        groupedoperations.RemoveRange(relocated);
                        existing.Children.AddRange(relocated);

                        found = true;

                        break;
                    }
                }

                // Auto injection of not found node
                if (!found)
                {
                    // Get the first path in group
                    var vacant = new OperationDescription(@group.Key, "", @group.Value.Count != 0 ? @group.Value[0].Path : "");

                    // Relocate operations in group
                    groupedoperations.RemoveRange(@group.Value);
                    vacant.Children.AddRange(@group.Value);

                    groupedoperations.Add(vacant);
                }
            }

            // Modify the grouped operations according to the grouped name
            groupedoperations.RemoveRange(groupedoperations.Where(item => !groupNames.Contains(item.Name)));
            groupedoperations.AddRange(groupedoperations.Where(item => groupNames.Contains(item.Name) && !groupedoperations.Contains(item)));

            // Sorting in not essential for this process
            groupedoperations.Sort((operation, other) => operation.CompareTo(other));

            // Modify the operations according to the grouped operations
            if (level == 1)
            {
                operations.RemoveRange(groupedoperations.Where(item => !groupNames.Contains(item.Name)));
                operations.AddRange(groupedoperations.Where(item => groupNames.Contains(item.Name) && !operations.Contains(item)));

                operations.Sort((operation, other) => operation.CompareTo(other));
            }
        }

        private HashSet<string> GroupNamesUnique(IList<IOperationDescription> groupedoperations, int level)
        {
            return groupedoperations.Select(child => this.CropStart(child.Name, level))
                                    .ToHashSet();
        }

        internal string CropStart(string name, int level = 1)
        {
            var nameGroup = name.Split('.');
            var nameTrimmed = string.Join(".", nameGroup.Take(level));

            return nameTrimmed;
        }
    }
}