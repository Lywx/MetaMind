namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Layouts;

    public interface IBlockViewVerticalItemLogic : IPointViewVerticalItemLogic
    {
        new IBlockViewVerticalItemLayout ItemLayout { get; }
    }
}