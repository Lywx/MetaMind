namespace MetaMind.Engine.Gui.Reactors
{
    using System;
    using Elements;
    using Entities;

    public interface IMMRenderReactor : IRenderReactorOperations, IRenderReactorOrganization, IMMRenderEntity, IMMReactor, IElement
    {
        #region State

        new bool Active { get; set; }

        #endregion

        #region Lookup

        string Name { get; }

        #endregion

        #region Events

        event EventHandler<RenderReactorDrawEventArgs> BeginDrawStarted;

        event EventHandler<RenderReactorDrawEventArgs> EndDrawStarted;

        event EventHandler ParentChanged;

        #endregion

    }
}