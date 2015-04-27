namespace MetaMind.Runtime.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ExperienceModule : Module<ExperienceSettings>
    {
        public ExperienceModule(ExperienceSettings settings)
            : base(settings)
        {
            this.Entities = new GameControllableEntityCollection<GameControllableEntity>();
            new View();
            this.Entities.Add();
        }

        private GameControllableEntityCollection<GameControllableEntity> Entities { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
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
