namespace MetaMind.Engine.Guis
{
    public class GroupComponent<TGroup, TGroupSettings, TGroupControl> : GameControllableEntity, IUpdateable, IDrawable, IInputable
        where                   TGroup        : Group<TGroupSettings>
        where                   TGroupControl : GroupControl<TGroup, TGroupSettings, TGroupControl>
    {
        private readonly TGroup group;

        protected GroupComponent(TGroup group)
        {
            this.group = group;
        }

        public TGroupSettings GroupSettings { get { return this.@group.Settings; } }

        protected TGroupControl Control { get { return (TGroupControl)this.@group.Control; } }

        protected TGroup Group { get { return this.@group; } }
    }
}