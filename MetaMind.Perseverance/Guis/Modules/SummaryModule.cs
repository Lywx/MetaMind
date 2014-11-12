using C3.Primtive2DXna;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class SummaryModule : Module<SummaryModuleSettings>
    {
        #region Constructors

        public SummaryModule(SummaryModuleSettings settings)
            : base(settings)
        {
        }

        #endregion Constructors

        #region Graphical Position

        private Vector2 TitleCenter
        {
            get { return new Vector2(GraphicsSettings.Width / 2f, 100); }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (Settings.Consciousness.AwakeCondition)
            {
                return;
            }

            this.DrawSummaryTitle ("Summary",                                                                                            Color.White);
            this.DrawSummaryEntity("Hours in Synchronization:" , 0, this.Settings.Synchronization.SynchronizedHourYesterday.ToSummary(), Color.White);
            this.DrawSummaryEntity("Hours in Good Profession:" , 1, -this.Settings.GoodPrefessionHour,                                   Color.Red);
            this.DrawSummaryEntity("Hours in Lofty Profession:", 2, -this.Settings.LoftyProfessionHour,                                  Color.Red);
            this.DrawSummarySplit (                              3,                                                                      Color.Red);
            this.DrawSummaryResult(string.Empty                , 4,                                                                      Color.White, Color.Red);
        }

        private void DrawSummaryEntity(string caption, int line, object presentedData, Color color)
        {
            var captionPosition = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(GraphicsSettings.Width / 2f + 270, 150 + line * Settings.LineHeight);
            var contentString   = string.Format("{0}", presentedData);

            FontManager.DrawText(Settings.EntityFont, caption      , captionPosition, color, Settings.EntitySize);
            FontManager.DrawText(Settings.EntityFont, contentString, contentPosition, color, Settings.EntitySize);
        }

        private void DrawSummaryResult(string caption, int line, Color goodColor, Color badColor)
        {
            var leftHour = Settings.Synchronization.SynchronizedHourYesterday - Settings.GoodPrefessionHour - Settings.LoftyProfessionHour;
            DrawSummaryEntity(caption, line, leftHour.ToSummary(), leftHour >= 0 ? goodColor : badColor);
        }

        private void DrawSummarySplit(int line, Color color)
        {
            var splitStart = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);
            var splitEnd   = new Vector2(GraphicsSettings.Width / 2f + 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);

            Primitives2D.DrawLine(ScreenManager.SpriteBatch, splitStart, splitEnd, color, this.Settings.EntitySize);
        }

        private void DrawSummaryTitle(string title, Color color)
        {
            FontManager.DrawCenteredText(Settings.TitleFont, title, TitleCenter, color, Settings.TitleSize);
        }

        #endregion 
    }

    internal static class Int32Ext
    {
        public static string ToSummary(this int hour)
        {
            return hour.ToString("+#;-#;+0");
        }
    }
}