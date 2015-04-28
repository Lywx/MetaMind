namespace MetaMind.Engine.Guis
{
    public class GroupVisualControl<TGroup, TGroupSettings, TGroupLogic> : GroupComponent<TGroup, TGroupSettings, TGroupLogic>, IGroupVisualControl
        where                       TGroup                               : Group         <TGroupSettings>
        where                       TGroupLogic                          : GroupLogicControl<TGroup, TGroupSettings, TGroupLogic>
    {
        public GroupVisualControl(TGroup group)
            : base(group)
        {
        }
    }
}