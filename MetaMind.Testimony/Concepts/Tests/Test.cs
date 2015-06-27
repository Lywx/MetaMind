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
    using System.Linq;
    using Engine;
    using Engine.Guis.Widgets.Items.Data;

    #region Test Session

    public partial class Test
    {
        public static TestSession Session { get; set; }
    }

    #endregion

    #region Test

    public partial class Test : ITest
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
            this.Children = new List<ITest>();

            this.Reset();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string TestStatus { get; private set; }

        public string Path { get; private set; }
    }

    #endregion

    #region Test Structure

    public partial class Test
    {
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

        public List<ITest> Children { get; private set; }

        public int ChildrenTestPassed
        {
            get
            {
                return this.Children.ToArray().Count(child => child.TestPassed);
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

        public bool HasChildren
        {
            get { return this.Children != null && this.Children.Count != 0; }
        }

        public Test Parent { get; set; }

        public bool HasParent
        {
            get { return this.Parent != null; }
        }
    }

    #endregion

    #region Test Computations

    public partial class Test : GameEntity 
    {
        private readonly string failingCue = "Test Failure";

        private readonly string succeedingCue = "Test Success";

        private readonly TimeSpan testResultChangedTimeout = TimeSpan.FromMinutes(3);

        private int testResultVariation;

        private DateTime testResultChangedTime;

        private bool testPassed = true;

        public bool TestPassed
        {
            get { return this.testPassed; }
            private set
            {
                var failing    = this.testPassed && !value;
                var succeeding = !this.testPassed && value;

                this.testPassed = value;

                if (failing)
                {
                    this.OnFail(true);
                }

                if (succeeding)
                {
                    this.OnSucceed(true);
                }
            }
        }

        public int TestResultVariation
        {
            get
            {
                return this.HasChildren
                           ? this.Children.Sum(test => test.TestResultVariation)
                           : this.testResultVariation;
            }
        }

        public bool TestResultChanged { get; private set; }

        #region Events

        public event EventHandler Succeed;

        public event EventHandler Fail;

        private void OnFail(bool cause)
        {
            if (cause)
            {
                if (Session.NotificationEnabled)
                {
                    var audio = this.GameInterop.Audio;
                    audio.PlayCue(failingCue);
                }
            }

            if (HasParent)
            {
                this.Parent.OnFail(false);
            }

            this.OnResultChange(-1);

            if (this.Fail != null)
            {
                this.Fail(this, EventArgs.Empty);
            }
        }

        private void OnSucceed(bool cause)
        {
            if (cause)
            {
                if (Session.NotificationEnabled)
                {
                    var audio = this.GameInterop.Audio;
                    audio.PlayCue(succeedingCue);
                }
            }

            if (HasParent)
            {
                this.Parent.OnFail(false);
            }

            this.OnResultChange(1);

            if (this.Succeed != null)
            {
                this.Succeed(this, EventArgs.Empty);
            }
        }

        private void OnResultChange(int change)
        {
            this.testResultVariation = change;
            this.testResultChangedTime = DateTime.Now;
        }

        #endregion

        #region Update

        public void Update()
        {
            this.TestPassed = this.TestMethod();
            this.TestStatus = this.TestStatusSelector();

            this.TestResultChanged = DateTime.Now - this.testResultChangedTime < this.testResultChangedTimeout;

            if (!this.TestResultChanged)
            {
                this.testResultVariation = 0;
            }

            this.UpdateChildrenTests();
        }

        private void UpdateChildrenTests()
        {
            foreach (var child in this.Children.ToArray())
            {
                child.Parent = this;
                child.Update();
            }
        }

        #endregion

        #region Evaluation

        public TimeSpan TestSpan { get; set; }

        public Func<bool> TestMethod { get; set; } 

        public Func<string> TestStatusSelector { get; set; }

        #endregion

        #region Operations

        public void Reset()
        {
            // Structure
            this.Parent = null;
            this.Children.Clear();

            this.testResultChangedTime = DateTime.MinValue;
            this.testResultVariation = 0;

            this.TestMethod = () => this.ChildrenTestPassed == this.Children.Count;
            this.TestStatusSelector = () => this.TestMethod() ? "PASSED" : "FAILED";
        }

        #endregion
    }

    #endregion

    #region IComparable


    public partial class Test
    {
        public int CompareTo(ITest other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }
    }

    #endregion

    #region IBlockViewItemData

    public partial class Test : IBlockViewItemData 
    {
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

    #endregion
}