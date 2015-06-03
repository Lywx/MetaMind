// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Testimony.Concepts.Synchronizations;

    [DataContract]
    public class Test : ITest, IBlockViewVerticalItemData
    {
        public Test(string name)
        {
            this.Name = name;

            this.Status = "READY";

            // Strucure 
            this.Parent   = null;
            this.Children = new List<ITest>();

            // Synchronization
            this.SynchronizationData = new SynchronizationData();
        }

        #region Test 

        public string Name { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        #endregion

        #region Structure

        public IList<ITest> Children { get; private set; }

        public ITest Parent { get; private set; }

        #endregion

        #region Update

        public void Update()
        {
        }

        #endregion

        public string SynchronizationName { get { return this.Name; } }

        public ISynchronizationData SynchronizationData { get; set; }

        public string BlockText { get { return this.Name; } }
    }
}