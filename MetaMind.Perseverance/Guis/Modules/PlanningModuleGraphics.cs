using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class PlanningModuleGraphics : ModuleGraphics<PlanningModule, PlanningModuleSettings, PlanningModuleControl>
    {
        public PlanningModuleGraphics( PlanningModule module )
            : base( module )
        {
        }

        public override void Draw( GameTime gameTime )
        {
            Control.Windows.ForEach( window => window.Draw( gameTime ) );
        }

        public override void Update( GameTime gameTime )
        {
        }
    }
}