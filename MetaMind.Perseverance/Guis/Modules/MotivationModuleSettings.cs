namespace MetaMind.Perseverance.Guis.Modules
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Concepts.MotivationEntries;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MotivationModuleSettings : ICloneable
    {
        public readonly MotivationItemSettings ItemSettings = new MotivationItemSettings();
        public readonly MotivationViewFactory  ViewFactory  = new MotivationViewFactory();

        public MotivationModuleSettings()
        {
            this.PastViewSettings   = new MotivationViewSettings
                                          {
                                              Space     = MotivationSpace.Past,

                                              Direction = ViewSettings1D.ScrollDirection.Left
                                          };
            this.NowViewSettings    = new MotivationViewSettings { Space = MotivationSpace.Now };
            this.FutureViewSettings = new MotivationViewSettings { Space = MotivationSpace.Future };

            if (GraphicsSettings.IsFullscreen)
            {
                this.PastViewSettings  .ColumnNumDisplay = 8;
                this.NowViewSettings   .ColumnNumDisplay = 1;
                this.FutureViewSettings.ColumnNumDisplay = 8;

                this.PastViewSettings  .StartPoint = new Point(GraphicsSettings.Width / 2 - 270, 160);
                this.NowViewSettings   .StartPoint = new Point(GraphicsSettings.Width / 2, 160);
                this.FutureViewSettings.StartPoint = new Point(GraphicsSettings.Width / 2 + 270, 160);
            }
            else
            {
                this.PastViewSettings  .ColumnNumDisplay = 1;
                this.NowViewSettings   .ColumnNumDisplay = 1;
                this.FutureViewSettings.ColumnNumDisplay = 9;

                this.PastViewSettings  .StartPoint = new Point(160, 160);
                this.NowViewSettings   .StartPoint = new Point(160 + 270, 160);
                this.FutureViewSettings.StartPoint = new Point(160 + 270 * 2, 160);
            }
        }

        public MotivationViewSettings FutureViewSettings { get; private set; }

        public MotivationViewSettings NowViewSettings { get; private set; }

        public MotivationViewSettings PastViewSettings { get; private set; }

        public static List<MotivationEntry> GetFutureMotivations()
        {
            return Perseverance.Session.Motivationlist.FutureMotivations;
        }

        public static List<MotivationEntry> GetMotivationSource(MotivationSpace space)
        {
            switch (space)
            {
                case MotivationSpace.Past:
                    {
                        return GetPastMotivations();
                    }

                case MotivationSpace.Now:
                    {
                        return GetNowMotivations();
                    }

                case MotivationSpace.Future:
                    {
                        return GetFutureMotivations();
                    }

                default:
                    return null;
            }
        }

        public static List<MotivationEntry> GetNowMotivations()
        {
            return Perseverance.Session.Motivationlist.NowMotivations;
        }

        public static List<MotivationEntry> GetPastMotivations()
        {
            return Perseverance.Session.Motivationlist.PastMotivations;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}