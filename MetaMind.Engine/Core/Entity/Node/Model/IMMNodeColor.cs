namespace MetaMind.Engine.Core.Entity.Node.Model
{
    using Microsoft.Xna.Framework;

    public interface __IMMNodeColorOperations
    {
    }

    internal interface __IMMNodeColorOperationsInternal : __IMMNodeColorOperations
    {
        #region Cascade

        void DisableCascade();

        void UpdateCascade();

        #endregion

        void UpdateBlend();
    }

    public interface IMMNodeColor : __IMMNodeColorOperations
    {
        /// <summary>
        /// Raw color without processing.
        /// </summary>
        Color Raw { get; set; }

        /// <summary>
        /// Blend color which is a computed and processed version of raw and its 
        /// parent's color. You cannot change it in this interface.
        /// </summary>
        Color Blend { get; }

        bool CascadeEnabled { get; set; }
    }

    internal interface IMMNodeColorInternal : IMMNodeColor, __IMMNodeColorOperationsInternal 
    {
        IMMNode Target { get; }
    }
}