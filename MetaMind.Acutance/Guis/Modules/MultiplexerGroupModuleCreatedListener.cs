namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupModuleCreatedListener : ListenerBase
    {
        private readonly IView moduleView;

        public MultiplexerGroupModuleCreatedListener(IView moduleView)
        {
            this.moduleView = moduleView;

            this.RegisteredEvents.Add((int)SessionEventType.ModuleCreated);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var eventArgs = @event.Data as ModuleCreatedEventArgs;

            if (eventArgs != null)
            {
                var moduleEntry = moduleView.Control.ItemFactory.CreateData(eventArgs.Buffer);

                this.moduleView.Control.AddItem(moduleEntry);
                this.moduleView.Control.SortItems(ViewSortMode.Name);
            }

            return true;
        }
    }
}