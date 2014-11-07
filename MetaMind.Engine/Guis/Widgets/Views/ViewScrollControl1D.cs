using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewScrollControl1D : ViewComponent, IViewScrollControlHorizontal
    {
        private int scroll;

        public ViewScrollControl1D(IView view, ViewSettings1D viewSettings, ItemSettings itemSettings)
            : base( view, viewSettings, itemSettings )
        {

        }

        public int XOffset { get { return scroll; } }

        private bool CanMoveLeft
        {
            get { return scroll > 0; }
        }

        private bool CanMoveRight
        {
            get { return ( ViewSettings.ColumnNumDisplay + scroll ) < View.Items.Count; }
        }

        public bool CanDisplay( int id )
        {
            return scroll <= id && id < ViewSettings.ColumnNumDisplay + scroll;
        }

        public bool IsLeftToDisplay( int column )
        {
            return column < scroll - 1;
        }

        public bool IsRightToDisplay( int column )
        {
            return column > ViewSettings.ColumnNumDisplay + scroll;
        }

        public void MoveLeft()
        {
            if ( CanMoveLeft )
                --scroll;
        }

        public void MoveRight()
        {
            if ( CanMoveRight )
                ++scroll;
        }

        public Point RootCenterPoint( int id )
        {
            return new Point(
                ViewSettings.Direction == ViewSettings1D.ScrollDirection.Right ?
                ViewSettings.StartPoint.X - ( scroll * ViewSettings.RootMargin.X ) + id * ViewSettings.RootMargin.X :
                ViewSettings.StartPoint.X + ( ( scroll + 1 - View.Items.Count ) * ViewSettings.RootMargin.X ) + id * ViewSettings.RootMargin.X,
                ViewSettings.StartPoint.Y );
        }
    }
}