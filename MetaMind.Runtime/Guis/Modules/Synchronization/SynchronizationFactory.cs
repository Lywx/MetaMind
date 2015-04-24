namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using System;

    using Microsoft.Xna.Framework;

    public class SynchronizationFactory 
    {
        private SynchronizationSettings Settings { get; set; }


        public SynchronizationFactory(SynchronizationSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.Settings = settings;
        }

        public SynchronizationPoint CreatePoint(Vector2 origin, int index, string caption, Func<Color> frameColor, SynchronizationPointSide side)
        {
            return new SynchronizationPoint(
                caption,
                () => Color.White,
                frameColor,
                () => this.SynchronizationDotBounds(origin, index, side));
        }

        public SynchronizationPoint CreatePointFrame(Vector2 origin, int index, SynchronizationPointSide size)
        {
            return this.CreatePoint(
                origin,
                index,
                ((index + 1) % 10).ToString("F0"),
                () => this.Settings.SynchronizationPointFrameColor,
                size);
        }

        private Rectangle SynchronizationDotBounds(Vector2 center, int index, SynchronizationPointSide side)
        {
            return
                ExtRectangle.RectangleByCenter(
                    side == SynchronizationPointSide.Left
                        ? center + index * new Vector2(15, 0) + new Vector2(275, -1)
                        : center - index * new Vector2(15, 0) + new Vector2(-275, -1),
                    this.Settings.SynchronizationDotFrameSize);
        }
    }
}