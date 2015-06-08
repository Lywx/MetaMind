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
    using System.Runtime.Serialization;
    using Engine.Guis.Widgets.Items.Data;

    [DataContract]
    [KnownType(typeof(Test))]
    public class Test : ITest, IBlockViewVerticalItemData
    {
        public Test(string name, string description, string status = "SUCCESS")
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            if (status == null)
            {
                throw new ArgumentNullException("status");
            }

            this.Name        = name;
            this.Description = description;
            this.Status      = status;

            // Strucure 
            this.Parent   = null;
            this.Children = new List<ITest>();
        }

        #region Test 

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Status { get; set; }

        #endregion

        #region Structure

        [DataMember]
        public IList<ITest> Children { get; private set; }

        public bool HasChildren
        {
            get { return this.Children != null && this.Children.Count != 0; }
        }

        [DataMember]
        public ITest Parent { get; private set; }

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

        #region Update

        public void Update()
        {
        }

        #endregion
    }
}