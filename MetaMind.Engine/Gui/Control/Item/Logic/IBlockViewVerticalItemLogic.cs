namespace MetaMind.Engine.Gui.Control.Item.Logic
{
    using Layouts;

    public interface IBlockViewVerticalItemLogic : IPointViewVerticalItemLogic
    {
        new IBlockViewVerticalItemLayout ItemLayout { get; }
    }
}