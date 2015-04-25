// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSyncControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

    public class ViewItemSyncControl : ViewItemComponent
    {
        public ViewItemSyncControl(IViewItem item)
            : base(item)
        {
            this.Synchronizable = this.ItemData as ISynchronizable;
            if (this.Synchronizable == null)
            {
                throw new InvalidOperationException("Item data is not synchronizable.");
            }

            this.SynchronizationData = this.Synchronizable.SynchronizationData;
        }

        protected ISynchronizable Synchronizable { get; private set; }

        protected ISynchronizationData SynchronizationData { get; private set; }

        public void StartSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStarted, new SynchronizationStartedEventArgs(this.ItemData)));
        }

        public void StopSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStopped, new SynchronizationStoppedEventArgs(this.ItemData)));
        }

        public void SwitchSync()
        {
            if (!this.SynchronizationData.IsSynchronizing)
            {
                this.StartSync();
            }
            else
            {
                this.StopSync();
            }
        }
    }
}