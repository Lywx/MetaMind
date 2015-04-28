namespace MetaMind.Engine.Guis
{
    public class GroupLogicControl<TGroup, TGroupSettings, TGroupLogic> : GroupComponent<TGroup, TGroupSettings, TGroupLogic>, IGroupLogicControl
        where                      TGroup                               : Group         <TGroupSettings>
        where                      TGroupLogic                          : GroupLogicControl<TGroup, TGroupSettings, TGroupLogic>
    {
        public GroupLogicControl(TGroup group)
            : base(group)
        {
        }
    }
}