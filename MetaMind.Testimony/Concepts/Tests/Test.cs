// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Concepts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Engine.Guis.Widgets.Items.Data;
    using Synchronizations;

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

            // Synchronization
            this.SynchronizationData = new SynchronizationData();
        }

        #region Test 

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Status { get; set; }

        #endregion

        #region Structure

        [DataMember]
        public IList<ITest> Children { get; private set; }

        [DataMember]
        public ITest Parent { get; private set; }

        #endregion

        #region Update

        public void Update()
        {
        }

        #endregion

        public string SynchronizationName { get { return this.Name; } }

        public ISynchronizationData SynchronizationData { get; set; }

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
    }
}