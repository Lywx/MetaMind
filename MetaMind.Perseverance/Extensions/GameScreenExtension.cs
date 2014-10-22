using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Extensions
{
    public static class GameScreenExtension
    {
        public static Adventure GetAdventure( this GameScreen gameScreen )
        {
            return Perseverance.Adventure;
        }
        public static void SetAdventure( this GameScreen screen, Adventure adventure )
        {
            Perseverance.Adventure = adventure;
        }
    }
}