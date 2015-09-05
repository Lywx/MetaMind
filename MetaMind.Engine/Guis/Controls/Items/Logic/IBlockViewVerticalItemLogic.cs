namespace MetaMind.Engine.Guis.Controls.Items.Logic
{
    using Layouts;

    public interface IBlockViewVerticalItemLogic : IPointViewVerticalItemLogic
    {
        new IBlockViewVerticalItemLayout ItemLayout { get; }
    }
}