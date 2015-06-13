// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Concepts.Tests
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;

    using Engine.Guis.Widgets.Items.Data;

    public partial class Test : ITest, IBlockViewVerticalItemData
    {
        public Test(string name, string description, string path)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            this.Name        = name;
            this.Description = description;
            this.Path        = path;

            this.Parent   = null;
            this.Children = new ObservableCollection<Test>();

            this.Reset();
        }

        #region Test

        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Path { get; set; }

        #endregion

        #region IBlockViewVerticalItemData

        public string BlockStringRaw
        {
            get { return this.Description; }
        }

        public string BlockLabel
        {
            get { return "DescriptionLabel"; }
        }

        public string BlockFrame
        {
            get { return "DescriptionFrame"; }
        }

        #endregion
    }

    public partial class Test
    {
        #region Structure

        public ObservableCollection<Test> Children { get; private set; }

        public bool HasChildren
        {
            get { return this.Children != null && this.Children.Count != 0; }
        }

        public Test Parent { get; private set; }

        public bool HasParent
        {
            get { return this.Parent != null; }
        }

        public IEnumerable AllTests()
        {
            yield return this;

            if (this.HasChildren)
            {
                foreach (var directChild in this.Children)
                {
                    yield return directChild;

                    if (directChild.HasChildren)
                    {
                        foreach (var child in directChild.AllTests())
                        {
                            yield return child;
                        }
                    }
                }
            }
        }

        public IEnumerable ChildrenTests()
        {
            if (this.HasChildren)
            {
                foreach (var child in this.Children)
                {
                    yield return child;
                }
            }
        }

        #endregion
    }

    public partial class Test
    {
        public TimeSpan TestSpan { get; set; }

        public Func<bool> TestPassed { get; set; }

        public Func<string> TestStatus { get; set; }

        #region Operations

        public void Reset()
        {
            this.TestPassed = () => false;
            this.TestStatus = () => this.TestPassed() ? "SUCCEEDED" : "FAILED";
        }

        #endregion

        #region Update

        public void Update()
        {
            this.Status = this.TestStatus();

            foreach (var child in this.Children)
            {
                child.Update();
            }
        }

        #endregion
    }
}