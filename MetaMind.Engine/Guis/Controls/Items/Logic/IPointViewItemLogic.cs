namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Layouts;

    public interface IPointViewItemLogic : IViewItemLogic
    {
        new IPointViewItemLayout ItemLayout { get; }
    }
}