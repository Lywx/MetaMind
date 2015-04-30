namespace MetaMind.Engine.Guis.Widgets.Items.ItemFrames
{
    using Microsoft.Xna.Framework;

    public interface IViewItemFrameControl : IUpdateable, IInputable
    {
        IItemRootFrame RootFrame { get; }
    }
}