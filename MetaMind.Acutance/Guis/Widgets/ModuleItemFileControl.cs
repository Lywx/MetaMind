namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class ModuleItemFileControl : ViewItemComponent
    {
        public ModuleItemFileControl(IViewItem item)
            : base(item)
        {
            ((Module)ItemData).Reloaded += this.ReloadModule;
        }

        ~ModuleItemFileControl()
        {
            this.Dispose();
        }

        private void ReloadModule(object sender, EventArgs e)
        {
            this.ReloadModule();
        }

        private void ReloadModule()
        {
            var moduleReloadedEvent = new EventBase(
                (int)SessionEventType.ModuleReloaded,
                new KnowledgeReloadedEventArgs(this.ItemData.Path));

            GameEngine.EventManager.QueueEvent(moduleReloadedEvent);

            this.ItemControl.DeleteIt();
        }
    }
}