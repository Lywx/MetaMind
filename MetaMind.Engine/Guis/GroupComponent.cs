namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public class GroupComponent<TGroup, TGroupSettings, TGroupLogic> : GameControllableEntity, IUpdateable, IDrawable, IInputable
        where                   TGroup      : Group<TGroupSettings>
        where                   TGroupLogic : GroupLogicControl<TGroup, TGroupSettings, TGroupLogic>
    {
        private readonly TGroup group;

        protected GroupComponent(TGroup group)
        {
            this.group = group;
        }

        public TGroupSettings GroupSettings { get { return this.group.Settings; } }

        protected TGroupLogic Control { get { return (TGroupLogic)this.group.Logic; } }

        protected TGroup Group { get { return this.group; } }
    }
}