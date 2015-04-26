namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IItemEntity : IInputable, IDrawable
    {
        #region States

        bool[] States { get; }

        Func<bool> this[ItemState state] { get; set; }

        #endregion

        ICloneable ItemSettings { get; }
    }
}