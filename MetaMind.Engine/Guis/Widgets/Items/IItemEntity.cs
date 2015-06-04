namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    public interface IItemEntity : IGameControllableEntity
    {
        #region States

        bool[] States { get; }

        Func<bool> this[ItemState state] { get; set; }

        #endregion
    }
}