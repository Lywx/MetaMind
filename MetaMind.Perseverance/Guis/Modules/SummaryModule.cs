using C3.Primtive2DXna;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Perseverance.Concepts.Cognitions;

    public class SummaryModule : Module<SummaryModuleSettings>
    {
        private IConsciousness   consciousness;
        private ISynchronization synchronization;

        private SummaryModuleSleepStoppedEventListener sleepStoppedEventListener;

        #region Constructors

        public SummaryModule(Cognition cognition, SummaryModuleSettings settings)
            : base(settings)
        {
            this.synchronization = cognition.Synchronization;
            this.consciousness   = cognition.Consciousness;
        }

        #endregion Constructors

        #region Graphical Position

        private Vector2 TitleCenter
        {
            get { return new Vector2(GraphicsSettings.Width / 2f, 100); }
        }

        #endregion

        #region Load and Unload

        public override void Load()
        {
            if (this.sleepStoppedEventListener == null)
            {
                this.sleepStoppedEventListener = new SummaryModuleSleepStoppedEventListener();
            }

            EventManager.AddListener(this.sleepStoppedEventListener);
        }

        public override void Unload()
        {
            if (this.sleepStoppedEventListener != null)
            {
                EventManager.RemoveListener(this.sleepStoppedEventListener);
            }

            this.sleepStoppedEventListener = null;
        }

        #endregion

        #region Update

        /// <summary>
        /// A must implementation for widget compatible module.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void UpdateInput(GameTime gameTime)
        {
        }

        public override void UpdateStructure(GameTime gameTime)
        {
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (this.consciousness.AwakeCondition)
            {
                return;
            }
            
            this.DrawSummaryTitle (   Color.White, "Summary");
            this.DrawSummaryEntity(1, Color.White, "Hours in Synchronization:" , this.synchronization.SynchronizedHourYesterday.ToSummary());
            this.DrawSummaryBlank (2);
            this.DrawSummaryEntity(3, Color.Red  , "Hours in Good Profession:" , -this.Settings.GoodPrefessionHour);
            this.DrawSummaryEntity(4, Color.Red  , "Hours in Lofty Profession:", -this.Settings.LoftyProfessionHour);
            this.DrawSummarySplit (5, Color.Red);
            this.DrawSummaryResult(6, Color.White, Color.Red, string.Empty);
        }

        private void DrawSummaryBlank(int line)
        {
            this.DrawSummaryEntity(line, Color.White, string.Empty, string.Empty);
        }

        private void DrawSummaryEntity(int line, Color color, string caption, object presentedData)
        {
            var captionPosition = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(GraphicsSettings.Width / 2f + 260, 150 + line * Settings.LineHeight);
            var contentString   = string.Format("{0}", presentedData);

            FontManager.DrawText(Settings.EntityFont, caption      , captionPosition, color, Settings.EntitySize);
            FontManager.DrawText(Settings.EntityFont, contentString, contentPosition, color, Settings.EntitySize);
        }

        private void DrawSummaryResult(int line, Color goodColor, Color badColor, string caption)
        {
            var leftHour = this.synchronization.SynchronizedHourYesterday - Settings.GoodPrefessionHour - Settings.LoftyProfessionHour;
            this.DrawSummaryEntity(line, leftHour >= 0 ? goodColor : badColor, caption, leftHour.ToSummary());
        }

        private void DrawSummarySplit(int line, Color color)
        {
            var splitStart = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);
            var splitEnd   = new Vector2(GraphicsSettings.Width / 2f + 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);

            Primitives2D.DrawLine(ScreenManager.SpriteBatch, splitStart, splitEnd, color, this.Settings.EntitySize);
        }

        private void DrawSummaryTitle(Color color, string title)
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