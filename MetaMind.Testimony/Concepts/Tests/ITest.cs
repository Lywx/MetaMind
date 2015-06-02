namespace MetaMind.Testimony.Concepts.Tests
{
    using System.Collections.Generic;
    using Synchronizations;

    public interface ITest : ISynchronizable
    {
        #region Test

        string Name { get; set; }

        string Code { get; set; }

        #endregion

        #region Structure

        IList<ITest> Children { get; }

        ITest Parent { get; }

        #endregion

        #region Update

        void Update();

        #endregion
    }
}