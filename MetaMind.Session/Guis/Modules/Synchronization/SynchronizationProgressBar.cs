namespace MetaMind.Session.Guis.Modules.Synchronization
{
    using System;
    using Engine.Entities;
    using Engine.Entities.Controls.Images;
    using Engine.Services;
    using Engine.Settings;
    using Microsoft.Xna.Framework;
    using Runtime.Attention;

    public class SynchronizationProgressBar : MMMVCEntityComponent<SynchronizationModule, SynchronizationSettings, SynchronizationController>
    {
        public SynchronizationProgressBar(SynchronizationModule module, ISynchronizationData synchronizationData)
            : base(module)
        {
            if (synchronizationData == null)
            {
                throw new ArgumentNullException("synchronizationData");
            }

            this.SynchronizationData = synchronizationData;

            this.Frame = new ImageBox(
                () => RectangleExtension.RectangleByCenter(
                    this.Settings.BarFrameCenterPosition,
                    this.Settings.BarFrameSize),
                () => this.Settings.BarFrameColor,
                () => true);

            this.Progress = new ImageBox(
                () => RectangleExtension.RectangleByCenter(
                    this.Settings.BarFrameCenterPosition,
                    this.Settings.BarFrameSize * new Vector2(this.SynchronizationData.Progress, 1)),
                () => this.SynchronizationData.Enabled ? MMPalette.LightBlue : MMPalette.LightPink,
                () => true);
        }

        public ImageBox Progress { get; set; }

        public ImageBox Frame { get; set; }

        private ISynchronizationData SynchronizationData { get; set; }

        public override void Draw(GameTime time)
        {
            this.Frame   .Draw(time);
            this.Progress.Draw(time);
        }
    }
}