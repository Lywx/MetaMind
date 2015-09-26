namespace MetaMind.Engine.Gui.Controls.Item.Logic
{
    using Layouts;

    public interface IPointViewItemLogic : IViewItemLogic
    {
        new IPointViewItemLayout ItemLayout { get; }
    }
}