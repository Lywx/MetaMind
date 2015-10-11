namespace MetaMind.Engine.Entities.Elements
{
    using System;
    using Entities;
    using Shapes;

    public interface IMMElement : IMMShape, IMMInputEntity
    {
        Func<bool> this[MMElementState state] { get; }
    }
}
