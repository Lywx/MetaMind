namespace MetaMind.Engine.Core.Entity.Graphics
{
    using Entity.Common;
    using Entity.Shape;
    using System;

    public interface __IMMRendererComponentBaseOrganizational
    {
        IMMRendererComponent Parent { get; }

        IMMRendererComponent Root { get; }

        MMRendererComponenetCollection Children { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }

    public interface __IMMRendererComponentBaseOrganizationalInternal : __IMMRendererComponentBaseOrganizational
    {
        new IMMRendererComponent Parent { get; set; }

        new IMMRendererComponent Root { get; set; }
    }

    public interface __IMMRendererComponentBase : __IMMRendererComponentBaseOrganizational,

        IMMVisualEntity,

        // Need initialization
        IMMReactor,

        IMMRendererEntity,

        // Support common shape operations
        IMMShape,

        // Support double buffer
        IMMBufferable,

        IComparable<IMMRendererComponent>
    {
        
    }

    public interface __IMMRendererComponentOperations : IMMDrawableOperations
    {
        void Add(IMMRendererComponent component);

        void Remove(IMMRendererComponent component);

        bool Contains(IMMRendererComponent component, bool recursive);
    }

    /// <summary>
    /// Render reactor is used for classes that enable the advanced rendering 
    /// and post-processing effect on render target. For example, screen-wise 
    /// pixel clone or distortion, Blinds Wipe screen transition along with 
    /// other technique in http://content.gpwiki.org/Screen_Transition_Effects.
    /// </summary>
    /// <remarks>
    /// Render reactor can be used to achieve texture clipping in Gui.
    /// </remarks>
    public interface IMMRendererComponent : __IMMRendererComponentBase, __IMMRendererComponentOperations, __IMMRendererComponentBaseOrganizational 
    {
        IMMRendererOpacity Opacity { get; }

        #region Events

        event EventHandler<MMRendererComponentDrawEventArgs> CascadedBeginDrawStarted;

        event EventHandler<MMRendererComponentDrawEventArgs> CascadedEndDrawStarted;

        event EventHandler ParentChanged;

        #endregion
    }

    public interface IMMRendererComponentInternal : IMMRendererComponent, __IMMRendererComponentBaseOrganizationalInternal
    {
        #region Event Ons

        void OnOpacityChanged(object sender, EventArgs e);

        void OnParentResize(object sender, EventArgs e);

        void OnParentChanged(object sender, EventArgs e);

        void OnCascadedBeginDrawStarted(object sender, MMRendererComponentDrawEventArgs e);

        void OnCascadedEndDrawStarted(object sender, MMRendererComponentDrawEventArgs e);

        #endregion
    }
}