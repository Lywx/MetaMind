namespace MetaMind.Engine.Entities
{
    using System;

    public interface IMMEntity : IMMUpdateable, IMMInteropOperations, IDisposable  
    {
    }
}