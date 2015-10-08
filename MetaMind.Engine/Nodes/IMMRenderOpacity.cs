namespace MetaMind.Engine.Nodes
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

    internal interface IMMRenderOpacityInternal : IMMRenderOpacityOperationsInternal
    {
        byte Displayed { set; }

        byte Real { get; set; }

        event Action UpdateDisplayedInItselfStarted;

        event Action UpdateDisplayedInItselfEnded;
    }
}