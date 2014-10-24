using System.Collections.Generic;
using MetaMind.Perseverance.Sessions;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class TacticModule : Module<TacticModuleSettings>
    {
        public TacticModule( TacticModuleSettings settings )
        {
            Settings = settings;

            Control = new TacticModuleControl( this );
            Graphics = new TacticModuleGraphics( this );
        }
    }
}