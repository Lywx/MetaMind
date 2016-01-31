namespace MetaMind.Engine.Core.Entity.Common
{
    using System.Collections.Generic;

    public interface __IMMEntityCollectionBase<T> : ICollection<T>, IMMDrawableOperations, IMMUpdateableOperations, IMMBufferOperations, IMMLoadableOperations 
    {
        
    }

    public interface __IMMEntityCollectionOperation
    {
        
    }

    public interface IMMEntityCollection<T> : __IMMEntityCollectionBase<T>, __IMMEntityCollectionOperation 
    {
        
    }
}