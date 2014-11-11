namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    using Microsoft.Xna.Framework;

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
            this.observors = new List<IView> { view };
        }

        public Point Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public IEnumerable<IView> Observors { get { return this.observors; } }

        public void AddObserver( IView view )
        {
            this.observors.Add( view );
        }

        public float Progress
        {
            get { return this.progress; }
            set { this.progress = value; }
        }
        public Point Target
        {
            get { return this.target; }
            set { this.target = value; }
        }

        public void Initialize( Point origin, Point target )
        {
            this.origin = origin;
            this.target = target;

            this.progress = 0f;

            this.isInitialized = true;
        }

        public Point RootCenterPoint()
        {
            Debug.Assert( this.isInitialized );

            return new Point(
                this.origin.X + ( int ) ( ( this.target.X - this.origin.X ) * this.progress ),
                this.origin.Y + ( int ) ( ( this.target.Y - this.origin.Y ) * this.progress ) );
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