namespace MetaMind.Engine.Guis
{
    public class GroupLogic<TGroup, TGroupSettings, TGroupLogic> : GroupComponent<TGroup, TGroupSettings, TGroupLogic>, IGroupLogic
        where                      TGroup                        : Group<TGroupSettings>
        where                      TGroupLogic                   : GroupLogic<TGroup, TGroupSettings, TGroupLogic>
    {
        public GroupLogic(TGroup group)
            : base(group)
        {
        }
    }
}