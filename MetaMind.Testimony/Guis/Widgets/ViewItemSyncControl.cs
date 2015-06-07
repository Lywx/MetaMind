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
    using Testimony.Concepts.Synchronizations;
    using Testimony.Events;
    using Testimony.Sessions;

    public class ViewItemSyncControl : ViewItemComponent
    {
        public ViewItemSyncControl(IViewItem item)
            : base(item)
        {
            this.Synchronizable      = (ISynchronizable)this.Item.ItemData;
            this.SynchronizationData = this.Synchronizable.SynchronizationData;
        }

        protected ISynchronizable Synchronizable { get; private set; }

        protected ISynchronizationData SynchronizationData { get; private set; }

        public void StartSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStarted, new SynchronizationStartedEventArgs(this.Item.ItemData)));
        }

        public void StopSync()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStopped, new SynchronizationStoppedEventArgs(this.Item.ItemData)));
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