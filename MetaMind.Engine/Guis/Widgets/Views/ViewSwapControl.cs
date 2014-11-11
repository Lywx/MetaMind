using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewSwapControl
    {
        IEnumerable<IView> Observors { get; }

        void AddObserver( IView view );

        float Progress { get; set; }

        void Initialize( Point origin, Point target );

        void ObserveSwapFrom( IViewItem draggingItem, IView view );

        Point RootCenterPoint();
    }

    public class ViewSwapControl : ViewComponent, IViewSwapControl
    {
        private List<IView> observors;

        private float progress;
        private Point origin;
        private Point target;
        private bool isInitialized;

        public ViewSwapControl( IView view, ICloneable viewSettings, ItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            observors = new List<IView> { view };
        }

        public Point Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public IEnumerable<IView> Observors { get { return observors; } }

        public void AddObserver( IView view )
        {
            observors.Add( view );
        }

        public float Progress
        {
            get { return progress; }
            set { progress = value; }
        }
        public Point Target
        {
            get { return target; }
            set { target = value; }
        }

        public void Initialize( Point origin, Point target )
        {
            this.origin = origin;
            this.target = target;

            progress = 0f;

            isInitialized = true;
        }

        public Point RootCenterPoint()
        {
            Debug.Assert( isInitialized );

            return new Point(
                origin.X + ( int ) ( ( target.X - origin.X ) * progress ),
                origin.Y + ( int ) ( ( target.Y - origin.Y ) * progress ) );
        }

        public void ObserveSwapFrom( IViewItem draggingItem, IView view )
        {
            Predicate<IViewItem> touched = ( t => t.IsEnabled( ItemState.Item_Mouse_Over ) );
            Predicate<IViewItem> another = ( t => !ReferenceEquals( t, draggingItem ) );

            var active = view.Items.FindAll( t => t.IsEnabled( ItemState.Item_Active ) );
            var swapping = active.FindAll( touched ).Find( another );

            if ( swapping != null && !swapping.IsEnabled( ItemState.Item_Swaping ) )
            {
                swapping.ItemControl.SwapIt( draggingItem );
            }
        }
    }
}