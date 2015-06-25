namespace MetaMind.Engine
{
    using System;

    public interface IGameEntity : IOuterUpdateable, IBufferDoubleUpdateable, IDisposable, IInteroperableOperations  
    {
    }
}