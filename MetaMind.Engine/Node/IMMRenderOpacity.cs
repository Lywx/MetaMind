namespace MetaMind.Engine.Node
{
    using System;
    using Gui.Renders;

    public interface IMMRenderOpacity : IMMRenderOpacityOperations 
    {
        IMMRenderComponent Target { get; }

        byte Standalone { get; set; }

        byte Displayed { get; }

        bool IsCascaded { get; set; }
    }

    public interface IMMRenderOpacityOperations
    {
    }

    internal interface IMMRenderOpacityInternal : IMMRenderOpacityOperationsInternal
    {
        byte Displayed { set; }

        byte Real { get; set; }

        event Action UpdateDisplayedInItselfStarted;

        event Action UpdateDisplayedInItselfEnded;
    }

    internal interface IMMRenderOpacityOperationsInternal
    {
        void DisableCascade();

        void UpdateCascade(IMMRenderOpacity parent);

        void UpdateDisplayed(IMMRenderOpacity parent);
    }
}