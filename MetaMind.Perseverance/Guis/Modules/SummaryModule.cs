namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Extensions;

    using Microsoft.Xna.Framework;

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

        public override void Load(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio)
        {
            if (this.sleepStoppedEventListener == null)
            {
                this.sleepStoppedEventListener = new SummaryModuleSleepStoppedEventListener();
            }

            interop.Event.AddListener(this.sleepStoppedEventListener);
        }

        public override void Unload(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio)
        {
            if (this.sleepStoppedEventListener != null)
            {
                interop.Event.RemoveListener(this.sleepStoppedEventListener);
            }

            this.sleepStoppedEventListener = null;
        }

        #endregion

        #region Update

        /// <summary>
        /// A must implementation for widget compatible module.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="gameTime"></param>
        public override void UpdateInput(IGameInputService input, GameTime gameTime)
        {
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.ForceReset))
            {
                this.synchronization.ResetTomorrow();
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.cognition.Awake)
            {
                return;
            }
            
            this.DrawSummaryTitle(graphics, Color.White, "Summary");
            
            // daily statistics
            this.DrawSummaryEntry(graphics, 1, Color.White, "Hours in Synchronization:" , this.synchronization.SynchronizedHourYesterday.ToSummary());
            this.DrawSummaryBlank(graphics, 2);
            this.DrawSummaryEntry(graphics, 3, Color.Red,   "Hours in Good Profession:" , -this.Settings.GoodPrefessionHour);
            this.DrawSummaryEntry(graphics, 4, Color.Red,   "Hours in Lofty Profession:", -this.Settings.LoftyProfessionHour);
            this.DrawSummarySplit(graphics, 5, Color.Red);

            var dailyLeftHour = this.synchronization.SynchronizedHourYesterday - this.Settings.GoodPrefessionHour - this.Settings.LoftyProfessionHour;
            this.DrawSummaryResult(graphics, 6, Color.White, Color.Red, string.Empty, dailyLeftHour);
            
            this.DrawSummaryBlank(graphics, 7);

            // weekly statistics
            var weeklyHour = (int)this.synchronization.SynchronizedTimeRecentWeek.TotalHours;
            var weeklyLeftHour = weeklyHour - this.Settings.WorldRecordHour;

            this.DrawSummaryEntry(graphics, 8, Color.White, "Hours in Synchronization In Recent 7 Days:", weeklyHour.ToSummary());
            this.DrawSummaryBlank(graphics, 9);
            this.DrawSummaryEntry(graphics, 10, Color.Red,  "Len Bosack's Records in 7 Days:", -this.Settings.WorldRecordHour);
            this.DrawSummarySplit(graphics, 11, Color.Red);

            this.DrawSummaryResult(graphics, 12, Color.Gold, Color.Red, string.Empty, weeklyLeftHour);
        }

        private void DrawSummaryBlank(IGameGraphicsService graphics, int line)
        {
            this.DrawSummaryEntry(graphics, line, Color.White, string.Empty, string.Empty);
        }

        private void DrawSummaryEntry(IGameGraphicsService graphics, int line, Color color, string caption, string presentation)
        {
            var captionPosition = new Vector2(graphics.Settings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(graphics.Settings.Width / 2f + 260, 150 + line * Settings.LineHeight);

            graphics.StringDrawer.DrawString(this.Settings.EntityFont, caption     , captionPosition, color, this.Settings.EntitySize);
            graphics.StringDrawer.DrawString(this.Settings.EntityFont, presentation, contentPosition, color, this.Settings.EntitySize);
        }

        private void DrawSummaryEntry(IGameGraphicsService graphics, int line, Color color, string caption, object presentedData)
        {
            var captionPosition = new Vector2(graphics.Settings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(graphics.Settings.Width / 2f + 260, 150 + line * Settings.LineHeight);
            var contentString   = string.Format("{0}", presentedData);

            graphics.StringDrawer.DrawString(this.Settings.EntityFont, caption      , captionPosition, color, this.Settings.EntitySize);
            graphics.StringDrawer.DrawString(this.Settings.EntityFont, contentString, contentPosition, color, this.Settings.EntitySize);
        }

        private void DrawSummaryResult(IGameGraphicsService graphics, int line, Color goodColor, Color badColor, string caption, int computation)
        {
            this.DrawSummaryEntry(graphics, line, computation >= 0 ? goodColor : badColor, caption, computation.ToSummary());
        }

        private void DrawSummarySplit(IGameGraphicsService graphics, int line, Color color)
        {
            var splitStart = new Vector2(graphics.Settings.Width / 2f - 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);
            var splitEnd   = new Vector2(graphics.Settings.Width / 2f + 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);

            var spriteBatch = graphics.SpriteBatch;
            spriteBatch.DrawLine(splitStart, splitEnd, color, this.Settings.EntitySize);
        }

        private void DrawSummaryTitle(IGameGraphicsService graphics, Color color, string title)
        {
            graphics.StringDrawer.DrawStringCenteredHV(this.Settings.TitleFont, title, this.Settings.TitleCenter, color, this.Settings.TitleSize);
        }

        #endregion 
    }
}