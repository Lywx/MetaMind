namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupModuleCreatedListener : Listener
    {
        private readonly IView moduleView;

        public MultiplexerGroupModuleCreatedListener(IView moduleView)
        {
            this.moduleView = moduleView;

            this.RegisteredEvents.Add((int)SessionEventType.ModuleCreated);
        }

        public override bool HandleEvent(IEvent e)
        {
            var eventArgs = e.EventData as ModuleCreatedEventArgs;

            if (eventArgs != null)
            {
                var moduleEntry = moduleView.ViewLogic.ItemFactory.CreateData(eventArgs.Buffer);

                this.moduleView.ViewLogic.AddItem(moduleEntry);
                this.moduleView.ViewLogic.SortItems(PointViewSortMode.Name);
            }

            return true;
        }
    }
}