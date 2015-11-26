// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItemInteraction.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using Services;

    /// <summary>
    ///     Item interaction with view, other items.
    /// </summary>
    public interface IMMViewItemInteraction : IMMViewItemControllerComponent
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

        void ViewSwap(IMMEngineInteropService interop, IMMViewItem draggingItem);
    }
}