using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Testimony
{
    using Engine;
    using Guis.Console.Commands;

    class TestimonyConfigurer : GameEngineConfigurer
    {
        public override void Configure(GameEngine engine)
        {
            base.Configure(engine);

            engine.Interop.Console.AddCommand(new VerboseCommand());
            engine.Interop.Console.AddCommand(new DebugCommand());
        }
    }
}
