namespace MetaMind.Engine
{
    using System;

    public interface IMMEntity : IMMUpdateable, IMMBufferUpdateable, IDisposable, IMMInteroperableOperations  
    {
    }
}