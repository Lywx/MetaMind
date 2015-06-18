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

    public partial class Test : GameEntity, ITest, IBlockViewVerticalItemData
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

        #region Test

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Path { get; private set; }

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

        public int ChildrenPassed
        {
            get
            {
                return this.Children.ToArray().Count(child => child.Passed);
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

        public Test Parent { get; private set; }

        public bool HasParent
        {
            get { return this.Parent != null; }
        }

        public int CompareTo(ITest other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }

        public bool Equals(ITest other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    public partial class Test
    {
        public static TestSession TestSession { get; set; }

        private readonly string failingCue = "Test Failure";

        private readonly string succeedingCue = "Test Success";

        private bool passed = true;

        public bool Passed
        {
            get { return this.passed; }
            private set
            {
                var failing    = this.passed && !value;
                var succeeding = !this.passed && value;

                this.passed = value;

                if (failing)
                {
                    this.OnFailing();
                }

                if (succeeding)
                {
                    this.OnSucceeding();
                }
            }
        }

        public string Status { get; private set; }

        public TimeSpan TestSpan { get; set; }

        public Func<bool> TestPassed { get; set; } 

        public Func<string> TestStatus { get; set; }

        #region Events

        public event EventHandler Succeeding;

        public event EventHandler Failing;
            
        private void OnFailing()
        {
            if (TestSession.NotificationEnabled)
            {
                var audio = this.GameInterop.Audio;
                audio.PlayCue(failingCue);
            }

            if (this.Failing != null)
            {
                this.Failing(this, EventArgs.Empty);
            }
        }

        private void OnSucceeding()
        {
            if (TestSession.NotificationEnabled)
            {
                var audio = this.GameInterop.Audio;
                audio.PlayCue(succeedingCue);
            }

            if (this.Succeeding != null)
            {
                this.Succeeding(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Update

        public void Update()
        {
            this.Passed = this.TestPassed();
            this.Status = this.TestStatus();

            foreach (var child in this.Children.ToArray())
            {
                child.Update();
            }
        }

        #endregion

        #region Operations

        public void Reset()
        {
            this.Parent = null;
            this.Children.Clear();

            this.TestPassed = () => this.ChildrenPassed == this.Children.Count;
            this.TestStatus = () => this.TestPassed() ? "SUCCEEDED" : "FAILED";
        }

        #endregion
    }
}