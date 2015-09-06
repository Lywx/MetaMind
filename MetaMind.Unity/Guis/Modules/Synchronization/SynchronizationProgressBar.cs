namespace MetaMind.Unity.Guis.Modules.Synchronization
{
    using System;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Controls.Visuals;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;

    public class SynchronizationProgressBar : GameEntityModuleComponent<SynchronizationModule, SynchronizationSettings, SynchronizationModuleLogic>
    {
        public SynchronizationProgressBar(SynchronizationModule module, ISynchronization synchronization)
            : base(module)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Synchronization = synchronization;

            this.Frame = new Box(
                () => ExtRectangle.RectangleByCenter(
                    this.Settings.BarFrameCenterPosition,
                    this.Settings.BarFrameSize),
                () => this.Settings.BarFrameColor,
                () => true);

            this.Progress = new Box(
                () => ExtRectangle.RectangleByCenter(
                    this.Settings.BarFrameCenterPosition,
                    this.Settings.BarFrameSize * new Vector2(this.Synchronization.Progress, 1)),
                () => this.Synchronization.Enabled ? Palette.LightBlue : Palette.LightPink,
                () => true);
        }

        public Box Progress { get; set; }

        public Box Frame { get; set; }

        private ISynchronization Synchronization { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Frame   .Draw(graphics, time, alpha);
            this.Progress.Draw(graphics, time, alpha);
        }
    }
}