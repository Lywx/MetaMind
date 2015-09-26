namespace MetaMind.Engine.Gui.Controls.Item
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