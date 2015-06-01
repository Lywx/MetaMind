// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Runtime.Serialization;

    using MetaMind.Testimony.Concepts.Synchronizations;

    [DataContract]
    public class Test : ITest
    {
        public Test(string name)
        {
            this.Name = name;

            this.SynchronizationData = new SynchronizationData();
        }

        public string Name { get; set; }

        public string SynchronizationName
        {
            get
            {
                return this.Name;
            }
        }

        public ISynchronizationData SynchronizationData { get; set; }
    }

    public interface ITest : ISynchronizable
    {
        string Name { get; set; }
    }
}