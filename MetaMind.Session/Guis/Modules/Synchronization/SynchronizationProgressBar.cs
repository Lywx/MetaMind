namespace MetaMind.Session.Guis.Modules.Synchronization
{
    using System;
    using Concepts.Synchronizations;
    using Engine.Entities;
    using Engine.Gui.Controls.Images;
    using Engine.Services;
    using Engine.Settings.Color;
    using Microsoft.Xna.Framework;

    public class SynchronizationProgressBar : MMMvcEntityComponent<SynchronizationModule, SynchronizationSettings, SynchronizationLogic>
    {
        public SynchronizationProgressBar(SynchronizationModule module, ISynchronization synchronization)
            : base(module)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Synchronization = synchronization;

            this.Frame = new ImageBox(
                () => RectangleExtension.RectangleByCenter(
                    this.Settings.BarFrameCenterPosition,
                    this.Settings.BarFrameSize),
                () => this.Settings.BarFrameColor,
                () => true);

            this.Progress = new ImageBox(
                () => RectangleExtension.RectangleByCenter(
                    this.Settings.BarFrameCenterPosition,
                    this.Settings.BarFrameSize * new Vector2(this.Synchronization.Progress, 1)),
                () => this.Synchronization.Enabled ? MMPalette.LightBlue : MMPalette.LightPink,
                () => true);
        }

        public ImageBox Progress { get; set; }

        public ImageBox Frame { get; set; }

        private ISynchronization Synchronization { get; set; }

        public override void Draw(GameTime time)
        {
            this.Frame   .Draw(time);
            this.Progress.Draw(time);
        }
    }
}