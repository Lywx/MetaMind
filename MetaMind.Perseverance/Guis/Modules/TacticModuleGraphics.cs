using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class TacticModuleGraphics : ModuleGraphics<TacticModule, TacticModuleSettings, TacticModuleControl>
    {
        public TacticModuleGraphics(TacticModule module) : base(module)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Control.Windows.ForEach( window => window.Draw( gameTime ) );
        }

        public override void Update( GameTime gameTime )
        {
            
        }
    }
}