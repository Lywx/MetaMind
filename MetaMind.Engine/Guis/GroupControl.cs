namespace MetaMind.Engine.Guis
{
    public class GroupControl<TGroup, TGroupSettings, TGroupControl> : GroupComponent<TGroup, TGroupSettings, TGroupControl>, IGroupControl
        where                 TGroup                                 : Group         <TGroupSettings>
        where                 TGroupControl                          : GroupControl  <TGroup, TGroupSettings, TGroupControl>
    {
        public GroupControl(TGroup group)
            : base(group)
        {
        }
    }
}