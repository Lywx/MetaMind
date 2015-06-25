namespace MetaMind.Testimony.Concepts.Tests
{
    using System;

    public interface ITestComputation
    {
        string TestStatus { get; }

        bool TestPassed { get; }

        int ChildrenTestPassed { get; }

        int TestResultVariation { get; }

        bool TestResultChanged { get; }

        TimeSpan TestSpan { set; }

        Func<bool> TestMethod { set; }

        Func<string> TestStatusSelector { set; }
    }
}