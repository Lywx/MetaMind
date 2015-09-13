﻿namespace MetaMind.Engine.Debugging.Screens
{
    using System;
    using Engine.Screens;
    using Microsoft.Xna.Framework;
    using Services;

    public class CpuWhiteNoiseSreen : GameScreen
    {
        private Color[] randomData;

        private Random Random => this.Numerical.Random;

        public override void EndDraw(IGameGraphicsService graphics, GameTime time)
        {
            this.randomData = new Color[this.Width * this.Height];

            for (var i = 0; i < this.Width * this.Height; ++i)
            {
                var random = this.Random.Next(255);

                this.randomData[i] = new Color(random, random, random);
            }

            this.RenderTarget.SetData(this.randomData);

            base.EndDraw(graphics, time);
        }
    }
}
