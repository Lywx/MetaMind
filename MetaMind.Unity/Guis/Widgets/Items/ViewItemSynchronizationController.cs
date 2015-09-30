// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSynchronizationController.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Guis.Widgets.Items
{
    using Concepts.Synchronizations;
    using Engine.Components.Interop.Event;
    using Engine.Gui.Controls.Item;
    using Events;
    using Sessions;

    public class ViewItemSynchronizationController : ViewItemComponent, ISynchronizationController 
    {
        public ViewItemSynchronizationController(IViewItem item)
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
            @event.QueueEvent(new Event((int)SessionEvent.SyncStarted, new SynchronizationStartedEventArgs(this.Item.ItemData)));
        }

        public void StopSynchronization()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new Event((int)SessionEvent.SyncStopped, new SynchronizationStoppedEventArgs(this.Item.ItemData)));
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