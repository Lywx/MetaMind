namespace MetaMind.Engine.Entities.Graphics
{
    using System;

    public interface IMMRendererOpacityOperations
    {
        
    }

    internal interface IMMRendererOpacityOperationsInternal
    {
        void DisableCascade();

        void UpdateCascade(IMMRendererOpacity parent);

        void UpdateDisplayed(IMMRendererOpacity parent);
    }

    public interface IMMRendererOpacity : IMMRendererOpacityOperations 
    {
        IMMRendererComponent Target { get; }

        byte Standard { get; set; }

        byte Displayed { get; }

        bool IsCascaded { get; set; }
    }

    internal interface IMMRenderOpacityInternal : IMMRendererOpacityOperationsInternal
    {
        byte Displayed { set; }

        byte Real { get; set; }

        event Action UpdateDisplayedInItselfStarted;

        event Action UpdateDisplayedInItselfEnded;
    }
}