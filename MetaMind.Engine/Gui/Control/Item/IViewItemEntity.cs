namespace MetaMind.Engine.Gui.Control.Item
{
    using System;

    public interface IViewItemEntity : IGameControllableEntity
    {
        #region States

        bool[] ItemStates { get; }

        Func<bool> this[ItemState state] { get; set; }

        #endregion
    }
}