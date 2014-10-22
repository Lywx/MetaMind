using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewSelectionControl2D : IViewSelectionControl1D
    {
        void MoveDown();

        void MoveUp();
    }

    public class ViewSelectionControl2D : ViewComponent, IViewSelectionControl2D
    {
        private int? currentId;
        private int? previousId;

        public ViewSelectionControl2D( IView view, ViewSettings2D viewSettings, ItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public bool HasSelected
        {
            get { return currentId != null; }
        }

        public void Clear()
        {
            previousId = currentId;
            currentId = null;
        }

        public bool IsSelected( int id )
        {
            return currentId == id;
        }

        public void MoveDown()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsBottommost( Control.RowFrom( currentId.Value ) ) )
            {
                var row = Control.RowFrom( currentId.Value ) + 1;
                var column = Control.ColumnFrom( currentId.Value );
                Select( row, column );
                if ( Control.Scroll.IsDownToDisplay( row ) )
                    Control.Scroll.MoveDown();
            }
        }

        public void MoveLeft()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsLeftmost( Control.ColumnFrom( currentId.Value ) ) )
            {
                var row = Control.RowFrom( currentId.Value );
                var column = Control.ColumnFrom( currentId.Value ) - 1;
                Select( row, column );
                if ( Control.Scroll.IsLeftToDisplay( column ) )
                    Control.Scroll.MoveLeft();
            }
        }

        public void MoveRight()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsRightmost( Control.ColumnFrom( currentId.Value ) ) )
            {
                var row = Control.RowFrom( currentId.Value );
                var column = Control.ColumnFrom( currentId.Value ) + 1;
                Select( row, column );
                if ( Control.Scroll.IsRightToDisplay( column ) )
                    Control.Scroll.MoveRight();
            }
        }

        public void MoveUp()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsTopmost( Control.RowFrom( currentId.Value ) ) )
            {
                var row = Control.RowFrom( currentId.Value ) - 1;
                var column = Control.ColumnFrom( currentId.Value );
                Select( row, column );
                if ( Control.Scroll.IsUpToDisplay( row ) )
                    Control.Scroll.MoveUp();
            }
        }

        public void Select( int id )
        {
            previousId = currentId;
            currentId = id;
        }

        private void Select( int row, int column )
        {
            previousId = currentId;
            currentId = Control.IdFrom( row, column );
        }

        private bool IsBottommost( int row )
        {
            return row >= Control.RowNum - 1;
        }

        private bool IsLeftmost( int column )
        {
            return column <= 0;
        }

        private bool IsRightmost( int column )
        {
            return column >= Control.ColumnNum - 1;
        }

        private bool IsTopmost( int row )
        {
            return row <= 0;
        }

        private void SelectInit()
        {
            Select( previousId.HasValue ? previousId.Value : 0 );
        }
    }
}