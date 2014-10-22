using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewScrollControl2D : ViewComponent, IViewScrollControlHorizontal, IViewScrollControlVertical
    {
        private int xOffset;
        private int yOffset;

        public ViewScrollControl2D( IView view, ViewSettings2D viewSettings, ItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        public int XOffset
        {
            get { return xOffset; }
        }

        public int YOffset
        {
            get { return yOffset; }
        }

        private bool CanMoveDown
        {
            get
            {
                return ( ViewSettings.ColumnNumDisplay * ( ViewSettings.RowNumDisplay + YOffset ) < ViewSettings.ColumnNumDisplay * ViewSettings.RowNumMax ) &&
                        ( ViewSettings.ColumnNumMax * ( ViewSettings.RowNumDisplay + YOffset ) < View.Items.Count );
            }
        }

        private bool CanMoveLeft
        {
            get { return XOffset > 0; }
        }

        private bool CanMoveRight
        {
            get
            {
                return ( ViewSettings.RowNumDisplay * ( ViewSettings.ColumnNumDisplay + XOffset ) < ViewSettings.RowNumDisplay * ViewSettings.ColumnNumMax );
            }
        }

        private bool CanMoveUp
        {
            get { return YOffset > 0; }
        }

        public bool CanDisplay( int id )
        {
            var row = Control.RowFrom( id );
            var column = Control.ColumnFrom( id );
            return XOffset <= column && column < ViewSettings.ColumnNumDisplay + XOffset &&
                   YOffset <= row && row < ViewSettings.RowNumDisplay + YOffset;
        }

        public bool IsDownToDisplay( int row )
        {
            return row > ViewSettings.RowNumDisplay + YOffset - 1;
        }

        public bool IsLeftToDisplay( int column )
        {
            return column < XOffset;
        }

        public bool IsRightToDisplay( int column )
        {
            return column > ViewSettings.ColumnNumDisplay + XOffset - 1;
        }

        public bool IsUpToDisplay( int row )
        {
            return row < YOffset;
        }

        public void MoveDown()
        {
            if ( CanMoveDown )
                yOffset = YOffset + 1;
        }

        public void MoveLeft()
        {
            if ( CanMoveLeft )
                xOffset = XOffset - 1;
        }

        public void MoveRight()
        {
            if ( CanMoveRight )
                xOffset = XOffset + 1;
        }

        public void MoveUp()
        {
            if ( CanMoveUp )
                yOffset = YOffset - 1;
        }

        public Point RootCenterPoint( int id )
        {
            var row = Control.RowFrom( id );
            var column = Control.ColumnFrom( id );
            return new Point(
                ViewSettings.StartPoint.X - XOffset * ViewSettings.RootMargin.X + column * ViewSettings.RootMargin.X,
                ViewSettings.StartPoint.Y - YOffset * ViewSettings.RootMargin.Y + row * ViewSettings.RootMargin.Y );
        }
    }
}