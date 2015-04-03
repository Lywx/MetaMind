namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;
    using System.IO;

    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class ModuleItemFileControl : ViewItemComponent
    {
        private List<FileSystemWatcher> watchers;

        public ModuleItemFileControl(IViewItem item)
            : base(item)
        {
            this.watchers = new List<FileSystemWatcher>();

            //foreach (var VARIABLE in ItemData.)
            //{
                
            //}

            //watcher.Changed += this.ReloadModule;
        }

        ~ModuleItemFileControl()
        {
            //this.watcher.Dispose();

            //this.watcher = null;
        }

        private void ReloadModule(object sender, FileSystemEventArgs e)
        {
            var moduleReloadedEvent = new EventBase(
                (int)SessionEventType.ModuleReloaded,
                new KnowledgeReloadedEventArgs(this.ItemData.Path));

            GameEngine.EventManager.QueueEvent(moduleReloadedEvent);

            this.ItemControl.DeleteIt();
        }
    }
}