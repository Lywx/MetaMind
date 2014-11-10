using System;
using System.Collections;
using System.Collections.Generic;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Concepts.MotivationEntries;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class MotivationExchangeSettings : ICloneable
    {
        public readonly MotivationItemSettings ItemSettings = new MotivationItemSettings();
        public readonly MotivationItemFactory  ItemFactory  = new MotivationItemFactory();
        public readonly MotivationViewFactory  ViewFactory  = new MotivationViewFactory();

        public readonly MotivationViewSettings PastViewSettings = new MotivationViewSettings
        {
            ColumnNumDisplay = 1,
            StartPoint       = new Point( 160, 160 ),
            Direction        = ViewSettings1D.ScrollDirection.Left,
            Space            = MotivationSpace.Past,
        };

        public readonly MotivationViewSettings NowViewSettings = new MotivationViewSettings
        {
            ColumnNumDisplay = 1,
            StartPoint       = new Point( 160 + 270, 160 ),
            Space            = MotivationSpace.Now,
        };

        public readonly MotivationViewSettings FutureViewSettings = new MotivationViewSettings
        {
            ColumnNumDisplay = 9,
            StartPoint       = new Point( 160 + 270 * 2, 160 ),
            Space            = MotivationSpace.Future,
        };

        public List<MotivationEntry> GetPastMotivations()
        {
            return Perseverance.Adventure.Motivationlist.PastMotivations;
        }

        public List<MotivationEntry> GetNowMotivations()
        {
            return Perseverance.Adventure.Motivationlist.NowMotivations;
        }

        public List<MotivationEntry> GetFutureMotivations()
        {
            return Perseverance.Adventure.Motivationlist.FutureMotivations;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}