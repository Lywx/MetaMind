namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using Microsoft.Xna.Framework;

    public interface IViewItemDataModel : IViewItemComponent, IUpdateable, IInputable
    {
        void EditString(string targetName);

        void EditInt(string targetName);
    }
}