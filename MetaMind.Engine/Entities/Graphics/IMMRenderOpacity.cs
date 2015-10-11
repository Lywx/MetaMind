namespace MetaMind.Engine.Entities.Graphics
{
    using System;

    public interface IMMRenderOpacity : IMMRenderOpacityOperations 
    {
        IMMRenderComponent Target { get; }

        byte Standard { get; set; }

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