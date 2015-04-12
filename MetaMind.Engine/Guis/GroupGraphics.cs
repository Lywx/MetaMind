namespace MetaMind.Engine.Guis
{
    public class GroupGraphics<TGroup, TGroupSettings, TGroupControl> : GroupComponent<TGroup, TGroupSettings, TGroupControl>, IGroupGraphics
        where                  TGroup                                 : Group         <TGroupSettings>
        where                  TGroupControl                          : GroupControl  <TGroup, TGroupSettings, TGroupControl>
    {
        public GroupGraphics(TGroup group)
            : base(group)
        {
        }
    }
}