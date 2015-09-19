namespace MetaMind.Engine.Gui.Control.Item.Logic
{
    using Layouts;

    public interface IPointViewItemLogic : IViewItemLogic
    {
        new IPointViewItemLayout ItemLayout { get; }
    }
}