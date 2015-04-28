namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using System;

    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Settings.Colors;
    using MetaMind.Runtime.Concepts.Synchronizations;

    using Microsoft.Xna.Framework;

    public class SynchronizationProgressBar : ModuleComponent<SynchronizationModule, SynchronizationSettings, SynchronizationModuleLogicControl>
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
                    this.Settings.BarFrameSize * new Vector2(this.Synchronization.Progress, 0)),
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