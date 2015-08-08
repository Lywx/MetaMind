// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Test.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Concepts.Tests
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

            this.Reset();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Status { get; private set; }

        public string Path { get; private set; }
    }

    #endregion

    #region Test Structure

    public partial class Test
    {
        public IEnumerable AllCollection()
        {
            yield return this;

            if (this.HasChildren)
            {
                foreach (var directChild in this.Children)
                {
                    yield return directChild;

                    if (directChild.HasChildren)
                    {
                        foreach (var child in directChild.AllCollection())
                        {
                            yield return child;
                        }
                    }
                }
            }
        }

        public List<ITest> Children { get; private set; } = new List<ITest>(); 

        public int ChildrenPassedNum
        {
            get
            {
                return this.Children.ToArray().Count(child => child.IsPassed);
            }
        }

        public IEnumerable ChildrenCollection()
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

        public Test Parent { get; set; } = null;

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

        private readonly TimeSpan resultChangedTimeout = TimeSpan.FromMinutes(3);

        private int resultVariation;

        private DateTime resultChangedTime;

        private bool isPassed = true;

        public bool IsPassed
        {
            get { return this.isPassed; }
            private set
            {
                var failing    = this.isPassed && !value;
                var succeeding = !this.isPassed && value;

                this.isPassed = value;

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

        public int ResultVariationNum
        {
            get
            {
                return this.HasChildren
                           ? this.Children.Sum(test => test.ResultVariationNum)
                           : this.resultVariation;
            }
        }

        public bool IsResultChanged { get; private set; }

        #region Events

        public event EventHandler Succeed;

        public event EventHandler Failed;

        private void OnFail(bool isCause)
        {
            if (isCause)
            {
                if (Session.IsNotificationEnabled)
                {
                    var audio = this.GameInterop.Audio;
                    audio.PlayCue(this.failingCue);
                }
            }

            if (this.HasParent)
            {
                this.Parent.OnFail(false);
            }

            this.OnResultChange(-1);

            if (this.Failed != null)
            {
                this.Failed(this, EventArgs.Empty);
            }
        }

        private void OnSucceed(bool isCause)
        {
            if (isCause)
            {
                if (Session.IsNotificationEnabled)
                {
                    var audio = this.GameInterop.Audio;
                    audio.PlayCue(this.succeedingCue);
                }
            }

            if (this.HasParent)
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
            this.resultVariation = change;
            this.resultChangedTime = DateTime.Now;
        }

        #endregion

        #region Update

        public void Update()
        {
            this.IsPassed = this.ResultSelector.Invoke();
            this.Status = this.StatusSelector.Invoke();

            this.IsResultChanged = DateTime.Now - this.resultChangedTime < this.resultChangedTimeout;

            if (!this.IsResultChanged)
            {
                this.resultVariation = 0;
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

        public TimeSpan EvaluationSpan { get; set; }

        public Func<bool> ResultSelector { get; set; } 

        public Func<string> StatusSelector { get; set; }

        #endregion

        #region Operations

        public void Reset()
        {
            this.Parent = null;
            this.Children.Clear();

            this.resultChangedTime = DateTime.MinValue;
            this.resultVariation = 0;

            this.ResultSelector = () => this.ChildrenPassedNum == this.Children.Count;
            this.StatusSelector = () => this.ResultSelector() ? "PASSED" : "FAILED";
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
        public string BlockStringRaw => this.Description;

        public string BlockLabel => "DescriptionLabel"; 

        public string BlockFrame => "DescriptionFrame";
    }

    #endregion
}