// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSwap.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Swaps;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewSwapControl : ViewComponent, IViewSwapControl
    {
        #region Constructors

        public ViewSwapControl(IView view)
            : base(view)
        {
            this.Observors = new List<IView>();
        }

        #endregion

        #region Cross View 

        private ICrossViewSwapObservor CrossSwap { get; set; }

        private List<IView> Observors { get; set; }

        public void AddObserver(IView view)
        {
            this.Observors.Add(view);
        }

        public void RemoveObserver(IView view)
        {
            this.Observors.Remove(view);
        }

        #endregion

        #region States

        protected bool HasStarted { get; set; }

        public float Progress { get; set; }

        protected Vector2 End { get; set; }

        protected Vector2 Start { get; set; }

        /// <summary>
        /// Linear straight line movement between start and end.
        /// </summary>
        /// <returns></returns>
        public Vector2 Position
        {
            get
            {
                Debug.Assert(this.HasStarted, "Process has not be started yet.");

                return this.Start + (this.End - this.Start) * this.Progress;
            }
        }

        #endregion

        public virtual void StartProcess(IGameInteropService interop, IViewItem touchedItem, IViewItem draggingItem, IViewLogic draggingViewLogic)
        {
            this.HasStarted = true;
            this.Progress   = 0f;

            touchedItem[ItemState.Item_Is_Swaping] = () => true;

            // Set start point
            this.Start = this.View.ViewLogic.ViewScroll.Position(touchedItem.ItemLogic.ItemLayout.Id);

            // Set end point
            this.End = draggingViewLogic.ViewScroll.Position(draggingItem.ItemLogic.ItemLayout.Id);

            // launch process
            interop.Process.AttachProcess(new ViewItemSwapProcess<>(
                draggingItem,
                draggingItem.ItemLogic,
                draggingViewLogic,
                touchedItem,
                touchedItem.ItemLogic,
                this.View.ViewLogic,
                source));
        }

        public void WatchProcess(IViewItem item)
        {
            this.CrossSwap.WatchSwapFrom(item, this.View, this.View.ViewLogic);
        }
    }
}