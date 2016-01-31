namespace MetaMind.Engine.Core.Entity.Control.Item
{
    using System;
    using Entity.Node.Model;
    using Layers;
    using Microsoft.Xna.Framework;
    using Settings;
    using Views;

    public interface IMMViewItemBase : IMMNode, IMMReactor, IMMBufferable
    {
        
    }

    public interface IMMViewItemOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IMMViewItemLayer;

        #endregion

        #region Update

        /// <summary>
        /// Update logic related with IView.
        /// </summary>
        /// <param name="time"></param>
        void UpdateView(GameTime time);

        #endregion
    }

    public interface IMMViewItem : IMMViewItemBase, IMMViewItemOperations 
    {
        #region Events

        event EventHandler<EventArgs> Selected;

        event EventHandler<EventArgs> Unselected;

        event EventHandler<EventArgs> Swapped;

        event EventHandler<EventArgs> Swapping;

        event EventHandler<EventArgs> Transited;

        #endregion

        #region Item Data

        Func<bool> this[MMViewItemState state] { get; set; }

        ItemSettings ItemSettings { get; set; }
        
        /// <summary>
        /// Data that is to be presented.
        /// </summary>
        dynamic ItemData { get; set; }

        IMMViewItemController ItemLogic { get; }

        IMMViewItemLayer ItemLayer { get; }

        #endregion

        #region View Data

        IMMView View { get; }

        #endregion
    }

    public interface IMMViewItemInternal : IMMViewItemBase
    {
        bool[] ItemStates { get; }
    }
}