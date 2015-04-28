// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewSwapControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public class PointViewSwapControl : ViewComponent, IPointViewSwapControl
    {
        private bool initialized;

        public PointViewSwapControl(IView view, ICloneable viewSettings, ItemSettings itemSettings)
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
        public void WatchExchangeIn(IViewItem draggingItem, IView targetView)
        {
            Type control  = targetView.Logic.GetType();
            var assertion = control.HasProperty("Region");

            Debug.Assert(assertion, "Target view does not have a Region property named 'Region'.");

            if (targetView.Logic.Region[RegionState.Mouse_Is_Over]()  &&
               !draggingItem[ItemState.Item_Is_Transiting]() )
            {
                draggingItem.ItemLogic.ExchangeIt(draggingItem, targetView);
            }
        }

        /// <summary>
        /// Watching possible dragging item swapping in target view.
        /// </summary>
        /// <remarks>
        /// Valid universally.
        /// </remarks>
        public void WatchSwapFrom(IViewItem draggingItem, IView targetView)
        {
            Predicate<IViewItem> touched = t => t[ItemState.Item_Is_Mouse_Over]() ;
            Predicate<IViewItem> another = t => !ReferenceEquals(t, draggingItem);

            var active = targetView.Items.FindAll(t => t[ItemState.Item_Is_Active]() );
            var swapping = active.FindAll(touched).Find(another);

            if (swapping != null && 
               !swapping[ItemState.Item_Is_Swaping]() )
            {
                swapping.ItemLogic.SwapIt(draggingItem);
            }
        }
    }
}