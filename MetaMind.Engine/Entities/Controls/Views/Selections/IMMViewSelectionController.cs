// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewSelectionController.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities.Controls.Views.Selections
{
    public interface IMMViewSelectionController : IMMViewComponent
    {
        bool HasPreviouslySelected { get; }

        bool HasSelected { get; }

        int? PreviousSelectedId { get; }

        int? CurrentSelectedId { get; }

        void Cancel();

        bool IsSelected(int id);

        /// <summary>
        ///     Selects the specified id.
        /// </summary>
        /// <remarks>
        ///     All the selection has to be done by this function to enforce uniformity.
        /// </remarks>
        void Select(int id);
    }
}