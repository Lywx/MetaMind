namespace MetaMind.Engine.Guis.WidgetGroups
{
    public class WidgetGroupComponent<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl>
        where TWidgetGroup : WidgetGroup<TWidgetGroupSettings>
        where TWidgetGroupControl : WidgetGroupControl<TWidgetGroup, TWidgetGroupSettings, TWidgetGroupControl>
    {
        private readonly TWidgetGroup group;

        public WidgetGroupComponent(TWidgetGroup group)
        {
            this.group = group;
        }

        protected TWidgetGroup Group { get { return group; } }

        protected TWidgetGroupControl Control { get { return ( TWidgetGroupControl ) group.Control; } }
        public TWidgetGroupSettings GroupSettings { get { return group.Settings; } }
        
    }
}