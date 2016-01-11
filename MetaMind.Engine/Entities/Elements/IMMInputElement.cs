namespace MetaMind.Engine.Entities.Elements
{
    using Bases;
    using Shapes;
    using System;
    
    public interface IMMInputElementDebug : IMMInputEntity, IMMShape
    { 
        Func<bool> this[MMInputElementDebugState debugState] { get; }
    }

    public interface IMMInputElement : IMMInputEntity, IMMShape 
    {
    }
}