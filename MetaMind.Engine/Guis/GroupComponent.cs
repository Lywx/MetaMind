namespace MetaMind.Engine.Guis
{
    public class GroupComponent<TGroup, TGroupSettings, TGroupControl>
        where TGroup : Group<TGroupSettings>
        where TGroupControl : GroupControl<TGroup, TGroupSettings, TGroupControl>
    {
        private readonly TGroup group;

        protected GroupComponent(TGroup group)
        {
            this.group = group;
        }

        protected TGroup Group { get { return this.@group; } }

        protected TGroupControl Control { get { return ( TGroupControl ) this.@group.Control; } }
        public TGroupSettings GroupSettings { get { return this.@group.Settings; } }
        
    }
}