using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class TacticModuleSettings
    {
        //---------------------------------------------------------------------
        #region Tactic Window Settings

        public string TacticWindowName    = "Tactics";
        public Point  TacticWindowPosition = new Point(PlanningModuleSettings.Default.QuestionWindowX - 134, PlanningModuleSettings.Default.QuestionWindowY + 420);
        
        public int    TacticViewRowNumDisplay    = 3;
        public int    TacticViewColumnNumMax     = 8;
        public int    TacticViewColumnNumDisplay = 8;
        public int    TacticViewNameXMargin      = 0;

        #endregion Tactic Window Settings

        //---------------------------------------------------------------------
        #region Default

        public static TacticModuleSettings Default
        {
            get { return new TacticModuleSettings(); }
        }

        #endregion Default
    }

    public class TacticItemSettings : ItemSettings
    {
        public new static TacticItemSettings Default
        {
            get
            {
                var settings = new TacticItemSettings
                {
                    NameSize = 0.7f,
                    NameFrameSize = new Point(147, 48),
                    NameFrameRegularColor = new Color( 16, 32, 32, 2 ),
                };
                return settings;
            }
        }
    }

    //public class TacticViewSettings : ViewSettings
    //{
    //    public static TacticItemSettings Default
    //    {
    //        get { return new TacticItemSettings(); }
    //    }
    //}
}