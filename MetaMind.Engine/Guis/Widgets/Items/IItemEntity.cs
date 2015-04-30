namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    public interface IItemEntity : IInputable, Engine.IDrawable
    {
        #region States

        bool[] States { get; }

        Func<bool> this[ItemState state] { get; set; }

        #endregion

        dynamic ItemSettings { get; }
    }
}