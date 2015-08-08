namespace MetaMind.Unity.Concepts.Tests
{
    using System;

    public interface ITestComputation
    {
        string Status { get; }

        Func<string> StatusSelector { set; }

        bool IsPassed { get; }

        bool IsResultChanged { get; }

        int ChildrenPassedNum { get; }

        int ResultVariationNum { get; }

        Func<bool> ResultSelector { set; }

        TimeSpan EvaluationSpan { set; }
    }
}