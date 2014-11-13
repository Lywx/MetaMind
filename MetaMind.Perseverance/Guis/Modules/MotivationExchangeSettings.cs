using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Guis.Elements.Views;

    public class MotivationExchangeSettings : ICloneable
    {
        public readonly MotivationItemSettings ItemSettings = new MotivationItemSettings();
        public readonly MotivationItemFactory  ItemFactory  = new MotivationItemFactory();
        public readonly MotivationViewFactory  ViewFactory  = new MotivationViewFactory();

        public readonly MotivationViewSettings PastViewSettings = new MotivationViewSettings
        {
            ColumnNumDisplay = 1,
            StartPoint       = new Point(160, 160),
            Direction        = ViewSettings1D.ScrollDirection.Left,
            Space            = MotivationSpace.Past,
        };

        public readonly MotivationViewSettings NowViewSettings = new MotivationViewSettings
        {
            ColumnNumDisplay = 1,
            StartPoint       = new Point(160 + 270, 160),
            Space            = MotivationSpace.Now,
        };

        public readonly MotivationViewSettings FutureViewSettings = new MotivationViewSettings
        {
            ColumnNumDisplay = 9,
            StartPoint       = new Point(160 + 270 * 2, 160),
            Space            = MotivationSpace.Future,
        };

        public static List<MotivationEntry> GetPastMotivations()
        {
            return Perseverance.Adventure.Motivationlist.PastMotivations;
        }

        public static List<MotivationEntry> GetNowMotivations()
        {
            return Perseverance.Adventure.Motivationlist.NowMotivations;
        }

        public static List<MotivationEntry> GetFutureMotivations()
        {
            return Perseverance.Adventure.Motivationlist.FutureMotivations;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}