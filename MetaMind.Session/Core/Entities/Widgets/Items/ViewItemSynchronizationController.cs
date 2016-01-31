// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemSynchronizationController.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Guis.Widgets.Items
{
    using Engine.Core.Backend.Interop.Event;
    using Engine.Core.Entity.Control.Item;
    using Runtime.Attention;

    public class MMViewItemSynchronizationController : MMViewItemControllerComponent, ISynchronizationController 
    {
        public MMViewItemSynchronizationController(IMMViewItem item)
            : base(item)
        {
            this.Synchronizable      = (ISynchronizable)this.Item.ItemData;
            this.JobSynchronizationData = this.Synchronizable.SynchronizationJob;
        }

        protected ISynchronizable Synchronizable { get; private set; }

        protected IJobSynchronizationData JobSynchronizationData { get; private set; }

        public void BeginSync()
        {
            var @event = this.GlobalInterop.Event;
            @event.QueueEvent(new MMEvent((int)MMSessionGameEvent.SyncStarted, new SynchronizationStartedEventArgs(this.Item.ItemData)));
        }

        public void EndSync()
        {
            var @event = this.GlobalInterop.Event;
            @event.QueueEvent(new MMEvent((int)MMSessionGameEvent.SyncStopped, new SynchronizationStoppedEventArgs(this.Item.ItemData)));
        }

        public void ToggleSync()
        {
            if (!this.JobSynchronizationData.IsSynchronizing)
            {
                this.BeginSync();
            }
            else
            {
                this.EndSync();
            }
        }
    }
}