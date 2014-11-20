using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Settings;

    public class MotivationExchangeSettings : ICloneable
    {
        public readonly MotivationItemSettings ItemSettings = new MotivationItemSettings();
        public readonly MotivationViewFactory  ViewFactory  = new MotivationViewFactory();

        public MotivationExchangeSettings()
        {
            this.PastViewSettings   = new MotivationViewSettings();
            this.NowViewSettings    = new MotivationViewSettings();
            this.FutureViewSettings = new MotivationViewSettings();

            this.FutureViewSettings.Space            = MotivationSpace.Future;

            this.NowViewSettings.Space               = MotivationSpace.Now;
            
            this.PastViewSettings.Direction          = ViewSettings1D.ScrollDirection.Left;
            this.PastViewSettings.Space              = MotivationSpace.Past;

            if (GraphicsSettings.Fullscreen)
            {
                this.PastViewSettings  .ColumnNumDisplay = 9;
                this.NowViewSettings   .ColumnNumDisplay = 1;
                this.FutureViewSettings.ColumnNumDisplay = 9;

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
            return Perseverance.Adventure.Motivationlist.FutureMotivations;
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
            return Perseverance.Adventure.Motivationlist.NowMotivations;
        }

        public static List<MotivationEntry> GetPastMotivations()
        {
            return Perseverance.Adventure.Motivationlist.PastMotivations;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}