// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Concepts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Speech.Synthesis;
    using Engine.Gui.Controls.Item.Data;
    using Events;

    /// <summary>
    /// Test is a functional programming interface for F#. Test is following 
    /// functional programming paradigm.
    /// </summary>
    public class Test : ITest, IBlockViewItemData 
    {
        #region Static Dependency

        public static TestSession Session { get; set; }

        public static SpeechSynthesizer Speech { get; set; }

        #endregion

        #region Dependency

        private ITestEvaluation evaluation;

        public ITestEvaluation Evaluation
        {
            get
            {
                if (this.evaluation == null)
                {
                    this.evaluation = new TestEvaluation(this, DateTime.Now, TimeSpan.FromSeconds(1.0));
                    this.RegisterEvaluationHandlers();
                }

                return this.evaluation;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.evaluation?.Dispose();

                this.evaluation = value;
                this.RegisterEvaluationHandlers();
            }
        }

        public TestEffect Effect { get; private set; }

        public ITestOrganization Organization { get; private set; }

        #endregion

        #region Constructors and Finalizer

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

        #endregion

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Path { get; private set; }

        #region Interface ITestOrganization

        public List<ITest> Children => this.Organization.Children;

        public Test Parent => this.Organization.Parent;

        public bool HasParent => this.Organization.HasParent;

        public bool HasChildren => this.Organization.HasChildren;

        public IEnumerable<ITest> AllCollection => this.Organization.AllCollection;

        public IEnumerable<ITest> ChildrenCollection => this.Organization.ChildrenCollection;

        #endregion ITestOrganization

        #region Interface IComparable

        public int CompareTo(ITest other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }

        #endregion 

        #region Interface IBlockViewItemData

        public string BlockStringRaw => this.Description;

        public string BlockLabel => "DescriptionLabel";

        public string BlockFrame => "DescriptionFrame";

        #endregion 

        #region Events

        public event EventHandler<TestEvaluationEventArgs> Succeeded = delegate { };

        public event EventHandler<TestEvaluationEventArgs> Failed = delegate { };

        #endregion Events

        #region Event Registration

        private void RegisterEvaluationHandlers()
        {
            // It safely adds the event handler without duplication.

            // There wouldn't be thread safety problem because the 
            // evaluation is only succeeded or failed in Update method.

            this.Evaluation.Succeeded -= this.EvaluationSucceeded;
            this.Evaluation.Succeeded += this.EvaluationSucceeded;

            this.Evaluation.Failed -= this.EvaluationFailed;
            this.Evaluation.Failed += this.EvaluationFailed;
        }

        #endregion

        #region Event Handlers

        private void EvaluationFailed(object e, TestEvaluationEventArgs a)
        {
            this.Failed(this, a);
        }

        private void EvaluationSucceeded(object e, TestEvaluationEventArgs a)
        {
            this.Succeeded(this, a);
        }

        #endregion

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
}