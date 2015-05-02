namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using Microsoft.Xna.Framework;

    public interface IViewItemFrameControl : IViewItemComponent, IUpdateable, IInputable 
    {
        IItemRootFrame RootFrame { get; }
    }
}