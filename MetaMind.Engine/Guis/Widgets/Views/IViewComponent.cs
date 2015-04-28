namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public interface IViewComponent : IUpdateable
    {
        dynamic ViewLogic { get; }

        IView View { get; }

        dynamic ViewSettings { get; }

        dynamic ItemSettings { get; }
    }
}