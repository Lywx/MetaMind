namespace MetaMind.Engine.Node
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IMMNodeColor : IMMNodeColorOperations
    {
        IMMNode Target { get; }

        Color Standalone { get; set; }

        Color Displayed { get; }

        bool IsCascaded { get; set; }
    }

    public interface IMMNodeColorOperations
    {
    }

    internal interface IMMNodeColorInternal : IMMNodeColorOperationsInternal 
    {
        Color Displayed { set; }

        Color Real { get; set; }

        event Action UpdateDisplayedInItselfStarted;

        event Action UpdateDisplayedInItselfEnded;
    }

    internal interface IMMNodeColorOperationsInternal
    {
        #region Cascade

        void DisableCascade();

        void UpdateCascade(IMMNodeColor parent);

        #endregion

        void UpdateDisplayed(IMMNodeColor parent);
    }
}