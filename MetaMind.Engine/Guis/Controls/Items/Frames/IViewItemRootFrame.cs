namespace MetaMind.Engine.Guis.Controls.Items.Frames
{
    using Elements;

    public interface IViewItemRootFrame : IPickableFrame
    {
        bool IsActive { get; set; }
    }
}