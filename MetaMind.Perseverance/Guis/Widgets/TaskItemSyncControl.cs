// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskItemSyncControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Sessions;

    public class TaskItemSyncControl : ViewItemComponent
    {
        public TaskItemSyncControl(IViewItem item)
            : base(item)
        {
        }

        public void StopSync()
        {
            var syncStoppedEvent = new EventBase(
                (int)SessionEventType.SyncStopped, 
                new SynchronizationStoppedEventArgs(this.ItemData));
            EventManager.QueueEvent(syncStoppedEvent);
        }

        public void StartSync()
        {
            var syncStartEvent = new EventBase(
                (int)SessionEventType.SyncStarted, 
                new SynchronizationStartedEventArgs(this.ItemData));
            EventManager.QueueEvent(syncStartEvent);
        }

        public void SwitchSync()
        {
            if (!this.ItemData.Synchronizing)
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