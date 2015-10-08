// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemInteraction.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using Services;

    /// <summary>
    ///     Item interaction with view, other items.
    /// </summary>
    public interface IViewItemInteraction : IViewItemComponent
    {
        /// <summary>
        /// Customized action for item which would be triggered after selection.
        /// </summary>
        void ItemSelect();

        /// <summary>
        /// Customized action for view which would be triggered after selection.
        /// </summary>
        void ViewSelect();
        
        /// <summary>
        /// Customized action for view which would be triggered after unselection.
        /// </summary>
        void ViewUnselect();

        void ViewSwap(IMMEngineInteropService interop, IViewItem draggingItem);
    }
}