namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using Microsoft.Xna.Framework;

    public interface IViewItemFrame : IViewItemComponent, IUpdateable, IInputable 
    {
        IViewItemRootFrame RootFrame { get; }
    }
}