namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;

    public interface IViewItemEntity : IMMInputableEntity
    {
        #region States

        bool[] ItemStates { get; }

        Func<bool> this[ViewItemState state] { get; set; }

        #endregion
    }
}