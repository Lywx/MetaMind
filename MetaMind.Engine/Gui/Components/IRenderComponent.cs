namespace MetaMind.Engine.Gui.Components
{
    using System;
    using Elements;

    public interface IRenderComponent : IRenderComponentOperations, IRenderComponentOrganization, IRenderTarget, IComponent, IElement
    {
        #region State

        new bool Active { get; set; }

        #endregion

        #region Lookup

        string Name { get; }

        #endregion

        #region Events

        event EventHandler<RenderComponentDrawEventArgs> BeginDrawStarted;

        event EventHandler<RenderComponentDrawEventArgs> EndDrawStarted;

        event EventHandler ParentChanged;

        #endregion

    }
}