﻿namespace MetaMind.Runtime.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Testers;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class ExperienceModule : Module<ExperienceSettings>
    {
        private Region pickableFrame;

        public ExperienceModule(ExperienceSettings settings)
            : base(settings)
        {
            this.Entities = new GameControllableEntityCollection<GameControllableEntity>();
            this.pickableFrame = new Region(new Rectangle(50, 50, 50, 50));
            this.Entities.Add(this.pickableFrame);
        }

        private GameControllableEntityCollection<GameControllableEntity> Entities { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            Primitives2D.DrawRectangle(graphics.SpriteBatch, this.pickableFrame.Rectangle, Color.Red);
        }

        public override void Update(GameTime time)
        {
            this.Entities.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);
        }
    }
}
