namespace MetaMind.Engine.Guis.Controls.Items.Logic
{
    using Layouts;

    public interface IPointViewItemLogic : IViewItemLogic
    {
        new IPointViewItemLayout ItemLayout { get; }
    }
}