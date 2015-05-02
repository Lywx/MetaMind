namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using MetaMind.Engine.Guis.Widgets.Items.Factories;

    public interface IViewLogic : IViewComponent, IInputable
    {
        IViewItemFactory ItemFactory { get; }
    }
}