using System.Collections.Generic;
using System.Linq;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewSelectionControl1D
    {
        bool HasSelected { get; }

        void Clear();

        /// <summary>
        /// Whether specific id is selected.
        /// </summary>
        bool IsSelected( int id );

        void MoveLeft();

        void MoveRight();

        /// <summary>
        /// Selects the specified id.
        /// </summary>
        /// <remarks>
        /// All the selection has to be done by this function to ensure uniformity.
        /// </remarks>
        void Select( int id );
    }

    public class ViewSelectionControl1D : ViewComponent, IViewSelectionControl1D
    {
        private int? currentColumn;
        private int? previousColumn;

        public ViewSelectionControl1D( IView view, ViewSettings1D viewSettings, ItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public bool HasSelected
        {
            get { return currentColumn != null; }
        }

        public void Clear()
        {
            previousColumn = currentColumn;
            currentColumn = null;
        }

        public bool IsSelected( int id )
        {
            return currentColumn == id;
        }

        public void MoveLeft()
        {
            if ( !currentColumn.HasValue )
                Select( previousColumn.HasValue ? previousColumn.Value : 0 );
            else if ( !IsLeftmost( currentColumn.Value ) )
            {
                Select( currentColumn.Value - 1 );
                if ( Control.Scroll.IsLeftToDisplay( currentColumn.Value - 1 ) )
                    Control.Scroll.MoveLeft();
            }
            else
                Select( currentColumn.Value );
        }

        public void MoveRight()
        {
            if ( !currentColumn.HasValue )
                Select( previousColumn.HasValue ? previousColumn.Value : 0 );
            else if ( !IsRightmost( currentColumn.Value ) )
            {
                Select( currentColumn.Value + 1 );
                if ( Control.Scroll.IsRightToDisplay( currentColumn.Value + 1 ) )
                    Control.Scroll.MoveRight();
            }
            else
                Select( currentColumn.Value );
        }

        public void Select( int id )
        {
            previousColumn = currentColumn;
            currentColumn = id;
        }

        private bool IsLeftmost( int column )
        {
            return column <= 0;
        }

        private bool IsRightmost( int column )
        {
            return column >= View.Items.Count - 1;
        }
    }
}