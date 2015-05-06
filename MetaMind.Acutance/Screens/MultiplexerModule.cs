// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiplexerModule.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Acutance.Screens
{
    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<object>
    {
        private MultiplexerGroup multiplexer;

        public MultiplexerModule()
            : base(null)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.multiplexer.Draw(graphics, time, alpha);
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.multiplexer = new MultiplexerGroup(new MultiplexerGroupSettings());
            this.multiplexer.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.multiplexer.UnloadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.multiplexer.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.multiplexer.Update(time);
        }
    }
}