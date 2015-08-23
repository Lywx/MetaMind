using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MetaMind.Unity.Concepts.Tests
{
    public class TestOrganization : ITestOrganization
    {
        private readonly Test test;

        public TestOrganization(Test test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;
        }

        public List<ITest> Children { get; private set; } = new List<ITest>();

        public bool HasChildren => this.Children != null && this.Children.Count != 0;

        public bool HasParent => this.Parent != null;

        public Test Parent { get; set; } = null;

        public IEnumerable<ITest> AllCollection
        {
            get
            {
                yield return this.test;

                if (this.HasChildren)
                {
                    foreach (var directChild in this.Children.ToArray())
                    {
                        yield return directChild;

                        if (directChild.Organization.HasChildren)
                        {
                            foreach (var child in directChild.Organization.AllCollection)
                            {
                                yield return child;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<ITest> ChildrenCollection
        {
            get
            {
                if (this.HasChildren)
                {
                    foreach (var child in this.Children.ToArray())
                    {
                        yield return child;
                    }
                }
            }
        }

        #region Operations

        public void Reset()
        {
            this.Parent = null;
            this.Children.Clear();
        }

        public void Update()
        {
            foreach (var child in this.test.Organization.Children.ToArray())
            {
                ((TestOrganization)child.Organization).Parent = this.test;
            }
        }

        #endregion Operations
    }
}