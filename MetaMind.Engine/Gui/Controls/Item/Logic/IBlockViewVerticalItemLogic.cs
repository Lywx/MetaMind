namespace MetaMind.Engine.Gui.Controls.Item.Logic
{
    using Layouts;

    public interface IBlockViewVerticalItemLogic : IPointViewVerticalItemLogic
    {
        new IBlockViewVerticalItemLayout ItemLayout { get; }
    }
}