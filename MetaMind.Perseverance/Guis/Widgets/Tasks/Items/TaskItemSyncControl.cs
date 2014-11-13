// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskItemSyncControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Elements.ViewItems;
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
                (int)AdventureEventType.SyncStopped, 
                new SynchronizationStoppedEventArgs(ItemData));
            EventManager.QueueEvent(syncStoppedEvent);
        }

        public void StartSync()
        {
            var syncStartEvent = new EventBase(
                (int)AdventureEventType.SyncStarted, 
                new SynchronizationStartedEventArgs(ItemData));
            EventManager.QueueEvent(syncStartEvent);
        }

        public void SwitchSync()
        {
            if (!ItemData.Synchronizing)
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