﻿namespace MetaMind.Unity.Concepts.Tests
{
    using Engine;
    using System.Collections;
    using System.Collections.Generic;

    public interface ITestOrganization : IMMFreeUpdatable, ITestOperations
    {
        List<ITest> Children { get; }

        Test Parent { get; }

        bool HasParent { get; }

        bool HasChildren { get; }

        IEnumerable<ITest> AllCollection { get; }

        IEnumerable<ITest> ChildrenCollection { get; }
    }
}