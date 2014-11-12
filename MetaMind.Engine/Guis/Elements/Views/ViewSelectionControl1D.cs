// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSelectionControl1D.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.Views
{
    using MetaMind.Engine.Guis.Elements.Items;

    public interface IViewSelectionControl1D
    {
        bool HasSelected { get; }

        void Clear();

        /// <summary>
        /// Whether specific id is selected.
        /// </summary>
        bool IsSelected(int id);

        void MoveLeft();

        void MoveRight();

        /// <summary>
        /// Selects the specified id.
        /// </summary>
        /// <remarks>
        /// All the selection has to be done by this function to ensure uniformity.
        /// </remarks>
        void Select(int id);
    }

    public class ViewSelectionControl1D : ViewComponent, IViewSelectionControl1D
    {
        private int? currentColumn;

        private int? previousColumn;

        public ViewSelectionControl1D(IView view, ViewSettings1D viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        #region IViewSelectionControl1D Members

        public bool HasSelected
        {
            get { return this.currentColumn != null; }
        }

        public void Clear()
        {
            this.previousColumn = this.currentColumn;
            this.currentColumn = null;
            
            View.Disable(ViewState.View_Has_Selection);
        }

        public bool IsSelected(int id)
        {
            return this.currentColumn == id;
        }

        public void MoveLeft()
        {
            if (!this.currentColumn.HasValue)
            {
                this.Select(this.previousColumn.HasValue ? this.previousColumn.Value : 0);
            }
            else if (!this.IsLeftmost(this.currentColumn.Value))
            {
                this.Select(this.currentColumn.Value - 1);
                if (this.ViewControl.Scroll.IsLeftToDisplay(this.currentColumn.Value - 1))
                {
                    this.ViewControl.Scroll.MoveLeft();
                }
            }
            else
            {
                this.Select(this.currentColumn.Value);
            }
        }

        public void MoveRight()
        {
            if (!this.currentColumn.HasValue)
            {
                this.Select(this.previousColumn.HasValue ? this.previousColumn.Value : 0);
            }
            else if (!this.IsRightmost(this.currentColumn.Value))
            {
                this.Select(this.currentColumn.Value + 1);
                if (this.ViewControl.Scroll.IsRightToDisplay(this.currentColumn.Value + 1))
                {
                    this.ViewControl.Scroll.MoveRight();
                }
            }
            else
            {
                this.Select(this.currentColumn.Value);
            }
        }

        public void Select(int id)
        {
            this.previousColumn = this.currentColumn;
            this.currentColumn = id;
        }

        #endregion

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.View.Items.Count - 1;
        }
    }
}