namespace MetaMind.Engine.Guis
{
    public class GroupVisual<TGroup, TGroupSettings, TGroupLogic> : GroupComponent<TGroup, TGroupSettings, TGroupLogic>, IGroupVisual
        where                       TGroup                        : Group<TGroupSettings>
        where                       TGroupLogic                   : GroupLogic<TGroup, TGroupSettings, TGroupLogic>
    {
        public GroupVisual(TGroup group)
            : base(group)
        {
        }
    }
}