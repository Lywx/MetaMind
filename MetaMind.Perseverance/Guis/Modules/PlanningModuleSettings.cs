using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class PlanningModuleSettings
    {
        //---------------------------------------------------------------------
        public Point WindowMargin                    = new Point(24, 0);
        public Point WindowPositionMargin            = new Point(ItemSettings.Default.IdFrameSize.X + ItemSettings.Default.ExperienceFrameSize.X + ItemSettings.Default.NameFrameSize.X + TileViewSettings.Default.ScrollBarWidth + ItemSettings.Default.ExperienceFrameSize.X * 2 + ItemSettings.Default.IdFrameMargin.X * 2, 0);
        public float WindowSlidingElasticCoefficient = 0.2f;

        //---------------------------------------------------------------------
        public Point ViewPositionMargion  = new Point(ItemSettings.Default.IdFrameSize.X + ItemSettings.Default.ExperienceFrameSize.X + ItemSettings.Default.ExperienceFrameMargin.X * 2 + ItemSettings.Default.IdFrameMargin.X * 2, 0);
        
        //---------------------------------------------------------------------
        public int QuestionWindowX;
        public int QuestionWindowY;
        public int DirectionWindowX;
        public int DirectionWindowY;
        public int FutureWindowX;
        public int FutureWindowY;

        //---------------------------------------------------------------------
        
        public static PlanningModuleSettings Default
        {
            get
            {
                var settings = new PlanningModuleSettings();

                settings.QuestionWindowX  = 185;
                settings.DirectionWindowX = settings.QuestionWindowX  + settings.WindowPositionMargin.X;
                settings.FutureWindowX    = settings.DirectionWindowX + settings.WindowPositionMargin.X;
                settings.QuestionWindowY  = 140;
                settings.DirectionWindowY = settings.QuestionWindowY  + settings.WindowPositionMargin.Y;
                settings.FutureWindowY    = settings.QuestionWindowY  + settings.WindowPositionMargin.Y * 2;

                return settings;
            }
        }
    }
}