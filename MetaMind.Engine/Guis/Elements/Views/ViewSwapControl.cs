// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSwapControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Regions;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    using Microsoft.Xna.Framework;

    public interface IViewSwapControl
    {
        List<IView> Observors { get; }

        float Progress { get; set; }

        void AddObserver(IView view);

        void Initialize(Point origin, Point target);

        Point RootCenterPoint();

        void WatchExchangeIn(IViewItem draggingItem, IView targetView);

        void WatchSwapFrom(IViewItem draggingItem, IView targetView);
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

        public Point RootCenterPoint()
        {
            Debug.Assert(this.initialized, "Swap was not initialized.");

            return new Point(
                this.Origin.X + (int)((this.Target.X - this.Origin.X) * this.Progress),
                this.Origin.Y + (int)((this.Target.Y - this.Origin.Y) * this.Progress));
        }

        /// <summary>
        /// Watching possible dragging item exchange in target view.
        /// </summary>
        /// <remarks>
        /// Valid only for view has a region in view control.
        /// </remarks>
        /// <param name="draggingItem"></param>
        /// <param name="targetView"></param>
        public void WatchExchangeIn(IViewItem draggingItem, IView targetView)
        {
            Type control  = targetView.Control.GetType();
            var assertion = control.HasProperty("Region");

            Debug.Assert(assertion, "Target view does not have a Region property named 'Region'.");

            if (targetView.Control.Region.IsEnabled(RegionState.Region_Mouse_Over) &&
               !draggingItem.IsEnabled(ItemState.Item_Exchanging))
            {
                draggingItem.ItemControl.ExchangeIt(draggingItem, targetView);
            }
        }

        /// <summary>
        /// Watching possible dragging item swapping in target view.
        /// </summary>
        /// <remarks>
        /// Valid universally.
        /// </remarks>
        /// <param name="draggingItem"></param>
        /// <param name="targetView"></param>
        public void WatchSwapFrom(IViewItem draggingItem, IView targetView)
        {
            Predicate<IViewItem> touched = t => t.IsEnabled(ItemState.Item_Mouse_Over);
            Predicate<IViewItem> another = t => !ReferenceEquals(t, draggingItem);

            var active = targetView.Items.FindAll(t => t.IsEnabled(ItemState.Item_Active));
            var swapping = active.FindAll(touched).Find(another);

            if (swapping != null && 
               !swapping.IsEnabled(ItemState.Item_Swaping))
            {
                swapping.ItemControl.SwapIt(draggingItem);
            }
        }
    }
}