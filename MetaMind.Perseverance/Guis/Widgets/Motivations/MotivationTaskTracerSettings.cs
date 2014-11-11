using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;
using System;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations
{
    public class MotivationTaskTracerSettings : ICloneable
    {
        public readonly TaskViewFactory  ViewFactory  = new TaskViewFactory();

        public readonly TaskItemSettings ItemSettings = new TaskItemSettings();
        public readonly TaskViewSettings ViewSettings = new TaskViewSettings
        {
            ColumnNumDisplay = 1, 
            ColumnNumMax     = 1, 
            RowNumDisplay    = 9, 
            RowNumMax        = 100,
        };
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}