namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Concepts.Cognitions;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using Primtives2D;

    public class SummaryModule : Module<SummaryModuleSettings>
    {
        private ICognition       cognition;
        private ISynchronization synchronization;

        private SummaryModuleSleepStoppedEventListener sleepStoppedEventListener;

        #region Constructors

        public SummaryModule(ICognition cognition, SummaryModuleSettings settings)
            : base(settings)
        {
            this.cognition       = cognition;
            this.synchronization = cognition.Synchronization;
        }

        #endregion Constructors

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
            if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ForceReset))
            {
                this.synchronization.ResetForTomorrow();
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (this.cognition.Awake)
            {
                return;
            }
            
            this.DrawSummaryTitle (   Color.White, "Summary");
            
            // daily statistics
            this.DrawSummaryEntity(1, Color.White, "Hours in Synchronization:" , this.synchronization.SynchronizedHourYesterday.ToSummary());
            this.DrawSummaryBlank (2);
            this.DrawSummaryEntity(3, Color.Red,   "Hours in Good Profession:" , -this.Settings.GoodPrefessionHour);
            this.DrawSummaryEntity(4, Color.Red,   "Hours in Lofty Profession:", -this.Settings.LoftyProfessionHour);
            this.DrawSummarySplit (5, Color.Red);

            var dailyLeftHour = this.synchronization.SynchronizedHourYesterday - this.Settings.GoodPrefessionHour - this.Settings.LoftyProfessionHour;
            this.DrawSummaryResult(6, Color.White, Color.Red, string.Empty, dailyLeftHour);
            
            this.DrawSummaryBlank (7);

            // weekly statistics
            var weeklyHour = (int)this.synchronization.SynchronizedTimeRecentWeek.TotalHours;
            var weeklyLeftHour = weeklyHour - this.Settings.WorldRecordHour;

            this.DrawSummaryEntity(8, Color.White, "Hours in Synchronization In Recent 7 Days:", weeklyHour.ToSummary());
            this.DrawSummaryBlank (9);
            this.DrawSummaryEntity(10, Color.Red,   "Len Bosack's Records in 7 Days:", -this.Settings.WorldRecordHour);
            this.DrawSummarySplit (11, Color.Red);

            this.DrawSummaryResult(12, Color.Gold, Color.Red, string.Empty, weeklyLeftHour);
        }

        private void DrawSummaryBlank(int line)
        {
            this.DrawSummaryEntity(line, Color.White, string.Empty, string.Empty);
        }

        private void DrawSummaryEntity(int line, Color color, string caption, string presentation)
        {
            var captionPosition = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(GraphicsSettings.Width / 2f + 260, 150 + line * Settings.LineHeight);

            FontManager.DrawText(Settings.EntityFont, caption     , captionPosition, color, Settings.EntitySize);
            FontManager.DrawText(Settings.EntityFont, presentation, contentPosition, color, Settings.EntitySize);
        }

        private void DrawSummaryEntity(int line, Color color, string caption, object presentedData)
        {
            var captionPosition = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(GraphicsSettings.Width / 2f + 260, 150 + line * Settings.LineHeight);
            var contentString   = string.Format("{0}", presentedData);

            FontManager.DrawText(Settings.EntityFont, caption      , captionPosition, color, Settings.EntitySize);
            FontManager.DrawText(Settings.EntityFont, contentString, contentPosition, color, Settings.EntitySize);
        }

        private void DrawSummaryResult(int line, Color goodColor, Color badColor, string caption, int computation)
        {
            this.DrawSummaryEntity(line, computation >= 0 ? goodColor : badColor, caption, computation.ToSummary());
        }

        private void DrawSummarySplit(int line, Color color)
        {
            var splitStart = new Vector2(GraphicsSettings.Width / 2f - 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);
            var splitEnd   = new Vector2(GraphicsSettings.Width / 2f + 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);

            Primitives2D.DrawLine(ScreenManager.SpriteBatch, splitStart, splitEnd, color, this.Settings.EntitySize);
        }

        private void DrawSummaryTitle(Color color, string title)
        {
            FontManager.DrawCenteredText(Settings.TitleFont, title, this.Settings.TitleCenter, color, Settings.TitleSize);
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