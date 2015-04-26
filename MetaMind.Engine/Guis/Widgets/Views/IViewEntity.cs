namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IViewEntity : IInputable, IDrawable, IUpdateable
    {
        dynamic ItemSettings { get; }

        dynamic ViewSettings { get; }

        #region States

        bool[] States { get; }

        Func<bool> this[ViewState state] { get; set; }

        #endregion
    }
}