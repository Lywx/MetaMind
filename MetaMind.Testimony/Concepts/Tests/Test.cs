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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using Engine.Guis.Widgets.Items.Data;

    [DataContract]
    [KnownType(typeof(Test))]
    public partial class Test : ITest, IBlockViewVerticalItemData
    {
        public Test(string name, string description)
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

            this.Parent   = null;
            this.Children = new ObservableCollection<Test>();

            this.Reset();
        }

        #region Test

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Status { get; set; }

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

    [KnownType(typeof(List<ITest>))]
    public partial class Test
    {
        #region Structure

        [DataMember]
        public ObservableCollection<Test> Children { get; private set; }

        public bool HasChildren
        {
            get { return this.Children != null && this.Children.Count != 0; }
        }

        [DataMember]
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
        [DataMember]
        public TimeSpan TestSpan { get; set; }

        public Func<bool> TestPassed { get; set; }

        public Func<string> TestStatus { get; set; }

        #region Operations

        [OnDeserialized]
        public void Reset(StreamingContext context)
        {
            this.Reset();
        }

        public void Reset()
        {
            this.TestPassed = () => false;
            this.TestStatus = () => "";
        }

        #endregion

        #region Update

        public void Update()
        {
            this.Status = this.TestStatus();
        }

        #endregion
    }
}