namespace MetaMind.Engine.Entities.Controls.Item.Layouts
{
    /// <summary>
    /// Item layout for all point view item including point view 1d or 2d
    /// </summary>
    public interface IMMPointViewItemLayout : IMMViewItemLayout
    {
        int Row { get; }

        int Column { get; }
    }
}