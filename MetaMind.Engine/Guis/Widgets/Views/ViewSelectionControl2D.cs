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
            else if ( !IsBottommost( ViewControl.RowFrom( currentId.Value ) ) )
            {
                var row = ViewControl.RowFrom( currentId.Value ) + 1;
                var column = ViewControl.ColumnFrom( currentId.Value );
                Select( row, column );
                if ( ViewControl.Scroll.IsDownToDisplay( row ) )
                    ViewControl.Scroll.MoveDown();
            }
        }

        public void MoveLeft()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsLeftmost( ViewControl.ColumnFrom( currentId.Value ) ) )
            {
                var row = ViewControl.RowFrom( currentId.Value );
                var column = ViewControl.ColumnFrom( currentId.Value ) - 1;
                Select( row, column );
                if ( ViewControl.Scroll.IsLeftToDisplay( column ) )
                    ViewControl.Scroll.MoveLeft();
            }
        }

        public void MoveRight()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsRightmost( ViewControl.ColumnFrom( currentId.Value ) ) )
            {
                var row = ViewControl.RowFrom( currentId.Value );
                var column = ViewControl.ColumnFrom( currentId.Value ) + 1;
                Select( row, column );
                if ( ViewControl.Scroll.IsRightToDisplay( column ) )
                    ViewControl.Scroll.MoveRight();
            }
        }

        public void MoveUp()
        {
            if ( !currentId.HasValue )
                SelectInit();
            else if ( !IsTopmost( ViewControl.RowFrom( currentId.Value ) ) )
            {
                var row = ViewControl.RowFrom( currentId.Value ) - 1;
                var column = ViewControl.ColumnFrom( currentId.Value );
                Select( row, column );
                if ( ViewControl.Scroll.IsUpToDisplay( row ) )
                    ViewControl.Scroll.MoveUp();
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
            currentId = ViewControl.IdFrom( row, column );
        }

        private bool IsBottommost( int row )
        {
            return row >= ViewControl.RowNum - 1;
        }

        private bool IsLeftmost( int column )
        {
            return column <= 0;
        }

        private bool IsRightmost( int column )
        {
            return column >= ViewControl.ColumnNum - 1;
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