// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSynchronizationController.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Guis.Widgets.Items
{
    using Concepts.Synchronizations;
    using Engine.Components.Interop.Event;
    using Engine.Entities.Controls.Item;
    using Session.Sessions;

    public class MMViewItemSynchronizationController : MMViewItemControllerComponent, ISynchronizationController 
    {
        public MMViewItemSynchronizationController(IMMViewItem item)
            : base(item)
        {
            this.Synchronizable      = (ISynchronizable)this.Item.ItemData;
            this.SynchronizationData = this.Synchronizable.SynchronizationData;
        }

        protected ISynchronizable Synchronizable { get; private set; }

        protected ISynchronizationData SynchronizationData { get; private set; }

        public void StartSynchronization()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new MMEvent((int)SessionEvent.SyncStarted, new SynchronizationStartedEventArgs(this.Item.ItemData)));
        }

        public void StopSynchronization()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new MMEvent((int)SessionEvent.SyncStopped, new SynchronizationStoppedEventArgs(this.Item.ItemData)));
        }

        public void ToggleSynchronization()
        {
            if (!this.SynchronizationData.IsSynchronizing)
            {
                this.StartSynchronization();
            }
            else
            {
                this.StopSynchronization();
            }
        }
    }
}