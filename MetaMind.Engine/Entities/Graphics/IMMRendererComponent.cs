namespace MetaMind.Engine.Entities.Graphics
{
    using System;
    using Bases;
    using Elements;
    using Entities;
    using Shapes;

    public interface IMMRendererComponentBase : 
        IMMEntity,

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

    public interface IMMRenderComponentOperations : IMMDrawOperations
    {
        void Add(IMMRendererComponent component);

        void Remove(IMMRendererComponent component);

        bool Contains(IMMRendererComponent component, bool recursive);
    }

    public interface IMMRenderComponentOrganization
    {
        IMMRendererComponent Parent { get; }

        IMMRendererComponent Root { get; }

        MMRendererComponenetCollection Children { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }

    public interface IMMRenderComponentOrganizationInternal
    {
        IMMRendererComponent Parent { get; set; }

        IMMRendererComponent Root { get; set; }
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
    public interface IMMRendererComponent : IMMRendererComponentBase, IMMRenderComponentOperations, IMMRenderComponentOrganization 
    {
        IMMRendererOpacity Opacity { get; }

        int ZOrder { get; set; }

        #region Events

        event EventHandler<MMRendererComponentDrawEventArgs> CascadedBeginDrawStarted;

        event EventHandler<MMRendererComponentDrawEventArgs> CascadedEndDrawStarted;

        event EventHandler ParentChanged;

        #endregion
    }

    public interface IMMRendererComponentInternal : IMMRendererComponentBase, IMMRenderComponentOrganizationInternal
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