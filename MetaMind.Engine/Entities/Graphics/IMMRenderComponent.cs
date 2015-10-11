namespace MetaMind.Engine.Entities.Graphics
{
    using System;
    using Elements;
    using Entities;
    using Shapes;

    public interface IMMRenderComponentBase : 
        IMMEntity,

        // Need initialization
        IMMReactor,
        IMMRenderEntity,

        // Support common shape operations
        IMMShape,

        // Support double buffer
        IMMBufferable,

        IComparable<IMMRenderComponent>
    {
        
    }

    public interface IMMRenderComponentOperations : IMMDrawOperations
    {
        void Add(IMMRenderComponent component);

        void Remove(IMMRenderComponent component);

        bool Contains(IMMRenderComponent component, bool recursive);
    }

    public interface IMMRenderComponentOrganization
    {
        IMMRenderComponent Parent { get; }

        IMMRenderComponent Root { get; }

        MMRenderComponenetCollection Children { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }

    public interface IMMRenderComponentOrganizationInternal
    {
        IMMRenderComponent Parent { get; set; }

        IMMRenderComponent Root { get; set; }
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
    public interface IMMRenderComponent : IMMRenderComponentBase, IMMRenderComponentOperations, IMMRenderComponentOrganization 
    {
        IMMRenderOpacity Opacity { get; }

        int ZOrder { get; set; }

        #region Events

        event EventHandler<MMRenderComponentDrawEventArgs> CascadedBeginDrawStarted;

        event EventHandler<MMRenderComponentDrawEventArgs> CascadedEndDrawStarted;

        event EventHandler ParentChanged;

        #endregion
    }

    public interface IMMRenderComponentInternal : IMMRenderComponentBase, IMMRenderComponentOrganizationInternal
    {
        #region Event Ons

        void OnOpacityChanged(object sender, EventArgs e);

        void OnParentResize(object sender, MMElementEventArgs e);

        void OnParentChanged(object sender, EventArgs e);

        void OnCascadedBeginDrawStarted(object sender, MMRenderComponentDrawEventArgs e);

        void OnCascadedEndDrawStarted(object sender, MMRenderComponentDrawEventArgs e);

        #endregion
    }
}