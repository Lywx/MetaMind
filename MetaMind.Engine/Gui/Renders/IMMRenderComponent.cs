namespace MetaMind.Engine.Gui.Renders
{
    using System;
    using Elements;
    using Entities;
    using Node;
    using Shapes;

    /// <summary>
    /// Render reactor is used for classes that enable the advanced rendering 
    /// and post-processing effect on render target. For example, screen-wise 
    /// pixel clone or distortion, Blinds Wipe screen transition along with 
    /// other technique in http://content.gpwiki.org/Screen_Transition_Effects.
    /// </summary>
    /// <remarks>
    /// Render reactor can be used to achieve texture clipping in Gui.
    /// </remarks>
    public interface IMMRenderComponent : IMMRenderComponentOperations, IMMRenderComponenetOrganization, IMMRenderEntity, IMMReactor, IMMShape, IMMEntity
    {
        IMMRenderOpacity Opacity { get; set; }

        #region Events

        event EventHandler<MMRenderComponentDrawEventArgs> BeginDrawStarted;

        event EventHandler<MMRenderComponentDrawEventArgs> EndDrawStarted;

        event EventHandler ParentChanged;

        #endregion
    }

    internal interface IMMRenderComponentInternal : IMMRenderComponenetOrganizationInternal 
    {
        #region Event Ons

        void OnOpacityChanged(object sender, EventArgs e);

        void OnParentResize(object sender, MMElementEventArgs e);

        void OnParentChanged(object sender, EventArgs e);

        void OnBeginDrawStarted(object sender, MMRenderComponentDrawEventArgs e);

        void OnEndDrawStarted(object sender, MMRenderComponentDrawEventArgs e);

        #endregion
    }
}