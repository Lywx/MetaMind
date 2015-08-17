// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Concepts.Tests
{
    using Engine.Guis.Widgets.Items.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    #region Test Session

    public partial class Test
    {
        public static TestSession Session { get; set; }
    }

    #endregion Test Session

    #region Test

    public partial class Test : ITest
    {
        private ITestEvaluation evaluation;

        public Test(string name, string description, string path)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Name          = name;
            this.Description   = description;
            this.Path          = path;

            this.Effect       = new TestEffect(this);
            this.Organization = new TestOrganization(this);

            this.Reset();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Path { get; private set; }

        public ITestEvaluation Evaluation
        {
            get
            {
                if (this.evaluation == null)
                {
                    this.evaluation = new TestEvaluation(this, DateTime.Now, TimeSpan.FromSeconds(1.0));
                    this.EvaluationSubscribe();
                }

                return this.evaluation;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.evaluation != null)
                {
                    this.evaluation.Dispose();
                }

                this.evaluation = value;
                this.EvaluationSubscribe();
            }
        }

        private void EvaluationSubscribe()
        {
            // It safely adds the event handler without dulplication.

            // There wouldn't be thread safety problem because the 
            // evaluation is only succeeded or failed in Update method.

            this.Evaluation.Succeeded -= this.EvaluationSucceeded;
            this.Evaluation.Succeeded += this.EvaluationSucceeded;

            this.Evaluation.Failed -= this.EvaluationFailed;
            this.Evaluation.Failed += this.EvaluationFailed;
        }

        private void EvaluationFailed(object e, TestEventArgs a)
        {
            this.Failed(this, a);
        }

        private void EvaluationSucceeded(object e, TestEventArgs a)
        {
            this.Succeeded(this, a);
        }

        public ITestOrganization Organization { get; private set; }

        public TestEffect Effect { get; private set; }

        #region Events

        public event EventHandler<TestEventArgs> Succeeded = delegate { };

        public event EventHandler<TestEventArgs> Failed = delegate { };

        #endregion Events

        #region Operations

        public void Reset()
        {
            this.Organization.Reset();
            this.Evaluation  .Reset();
        }

        #endregion Operations

        #region Update

        public void Update()
        {
            this.Organization.Update();
            this.Evaluation  .Update();
        }

        #endregion Update
    }

    #endregion Test

    #region ITestOrganization

    public partial class Test
    {
        public List<ITest> Children => this.Organization.Children;

        public Test Parent => this.Organization.Parent;

        public bool HasParent => this.Organization.HasParent;

        public bool HasChildren => this.Organization.HasChildren;

        public IEnumerable AllCollection => this.Organization.AllCollection;

        public IEnumerable ChildrenCollection => this.Organization.ChildrenCollection;
    }

    #endregion ITestOrganization

    #region IComparable

    public partial class Test
    {
        public int CompareTo(ITest other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }
    }

    #endregion IComparable

    #region IBlockViewItemData

    public partial class Test : IBlockViewItemData
    {
        public string BlockStringRaw => this.Description;

        public string BlockLabel => "DescriptionLabel";

        public string BlockFrame => "DescriptionFrame";
    }

    #endregion IBlockViewItemData
}