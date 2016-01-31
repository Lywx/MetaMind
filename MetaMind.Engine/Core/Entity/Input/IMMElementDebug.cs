namespace MetaMind.Engine.Core.Entity.Input
{
    using System;

    internal interface IMMElementDebug : IMMElement
    { 
        Func<bool> this[MMElementState state] { get; }
    }
}