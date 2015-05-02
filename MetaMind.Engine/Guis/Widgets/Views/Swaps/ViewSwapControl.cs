// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSwapControl.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public class ViewSwapControl : ViewComponent, IViewSwapControl
    {
        private Point end;
        private Point start;

        private bool isStarted;

        #region Constructors

        public ViewSwapControl(IView view)
            : base(view)
        {
            this.Observors = new List<IView> { view };
        }

        #endregion

        #region Observors

        public List<IView> Observors { get; private set; }

        public void AddObserver(IView view)
        {
            this.Observors.Add(view);
        }

        #endregion

        public float Progress { get; set; }

        /// <summary>
        /// Linear straight line movement between start and end.
        /// </summary>
        /// <returns></returns>
        public Point RootCenterPosition
        {
            get
            {
                Debug.Assert(this.isStarted, "Swapping has not be started.");

                return new Point(
                    this.start.X + (int)((this.end.X - this.start.X) * this.Progress),
                    this.start.Y + (int)((this.end.Y - this.start.Y) * this.Progress));
            }
        }

        // TODO: MAY I SHOULD LANUCH THE PROCESS FROM HERE
        public void StartProcess(Point start, Point end)
        {
            this.start = start;
            this.end   = end;

            this.Progress = 0f;

            this.isStarted = true;
        }

        /// <summary>
        /// Watching possible dragging item exchange in target view.
        /// </summary>
        /// <remarks>
        /// Valid only for view has a region in view control.
        /// </remarks>
        public void WatchTrasitIn(IViewItem draggingItem, IView targetView)
        {
            Type viewLogic = targetView.ViewLogic.GetType();
            Debug.Assert(viewLogic.HasProperty("Region"), "Target view does not have a Region property named 'Region'.");

            if (targetView.ViewLogic.Region[RegionState.Mouse_Is_Over]() && 
               !draggingItem[ItemState.Item_Is_Transiting]())
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
            Predicate<IViewItem> touched = t => t[ItemState.Item_Is_Mouse_Over]();
            Predicate<IViewItem> another = t => !ReferenceEquals(t, draggingItem);

            var active = targetView.ViewItems.FindAll(t => t[ItemState.Item_Is_Active]());
            var swapping = active.FindAll(touched).Find(another);

            if (swapping != null && !swapping[ItemState.Item_Is_Swaping]())
            {
                var itemLogic = (IViewSwapSupport)swapping.ItemLogic;
                (itemLogic.ViewSwap).SwapIt(draggingItem);
            }
        }
    }
}