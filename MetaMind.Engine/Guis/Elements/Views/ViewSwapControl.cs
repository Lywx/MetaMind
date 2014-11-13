namespace MetaMind.Engine.Guis.Elements.Views
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public interface IViewSwapControl
    {
        List<IView> Observors { get; }

        float Progress { get; set; }

        void AddObserver(IView view);

        void Initialize(Point origin, Point target);

        void ObserveSwapFrom(IViewItem draggingItem, IView view);

        Point RootCenterPoint();
    }

    public class ViewSwapControl : ViewComponent, IViewSwapControl
    {
        private bool initialized;

        public ViewSwapControl(IView view, ICloneable viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Observors = new List<IView> { view };
        }

        public List<IView> Observors { get; private set; }

        public Point Origin { get; set; }

        public float Progress { get; set; }

        public Point Target { get; set; }

        public void AddObserver(IView view)
        {
            this.Observors.Add(view);
        }

        public void Initialize(Point origin, Point target)
        {
            this.Origin = origin;
            this.Target = target;

            this.Progress = 0f;

            this.initialized = true;
        }

        public void ObserveSwapFrom(IViewItem draggingItem, IView view)
        {
            Predicate<IViewItem> touched = t => t.IsEnabled(ItemState.Item_Mouse_Over);
            Predicate<IViewItem> another = t => !ReferenceEquals(t, draggingItem);

            var active   = view.Items.FindAll(t => t.IsEnabled(ItemState.Item_Active));
            var swapping = active.FindAll(touched).Find(another);

            if (swapping != null && !swapping.IsEnabled(ItemState.Item_Swaping))
            {
                swapping.ItemControl.SwapIt(draggingItem);
            }
        }

        public Point RootCenterPoint()
        {
            Debug.Assert(this.initialized, "Swap was not initialized.");

            return new Point(
                this.Origin.X + (int)((this.Target.X - this.Origin.X) * this.Progress),
                this.Origin.Y + (int)((this.Target.Y - this.Origin.Y) * this.Progress));
        }
    }
}