namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
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

        public override void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            if (this.sleepStoppedEventListener == null)
            {
                this.sleepStoppedEventListener = new SummaryModuleSleepStoppedEventListener();
            }

            gameInterop.Event.AddListener(this.sleepStoppedEventListener);
        }

        public override void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            if (this.sleepStoppedEventListener != null)
            {
                gameInterop.Event.RemoveListener(this.sleepStoppedEventListener);
            }

            this.sleepStoppedEventListener = null;
        }

        #endregion

        #region Update

        /// <summary>
        /// A must implementation for widget compatible module.
        /// </summary>
        /// <param name="gameInput"></param>
        /// <param name="gameTime"></param>
        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.ForceReset))
            {
                this.synchronization.ResetTomorrow();
            }
        }

        public override void Update(GameTime gameTime)
        {
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (this.cognition.Awake)
            {
                return;
            }
            
            this.DrawSummaryTitle(gameGraphics, Color.White, "Summary");
            
            // daily statistics
            this.DrawSummaryEntry(gameGraphics, 1, Color.White, "Hours in Synchronization:" , this.synchronization.SynchronizedHourYesterday.ToSummary());
            this.DrawSummaryBlank(gameGraphics, 2);
            this.DrawSummaryEntry(gameGraphics, 3, Color.Red,   "Hours in Good Profession:" , -this.Settings.GoodPrefessionHour);
            this.DrawSummaryEntry(gameGraphics, 4, Color.Red,   "Hours in Lofty Profession:", -this.Settings.LoftyProfessionHour);
            this.DrawSummarySplit(gameGraphics, 5, Color.Red);

            var dailyLeftHour = this.synchronization.SynchronizedHourYesterday - this.Settings.GoodPrefessionHour - this.Settings.LoftyProfessionHour;
            this.DrawSummaryResult(gameGraphics, 6, Color.White, Color.Red, string.Empty, dailyLeftHour);
            
            this.DrawSummaryBlank(gameGraphics, 7);

            // weekly statistics
            var weeklyHour = (int)this.synchronization.SynchronizedTimeRecentWeek.TotalHours;
            var weeklyLeftHour = weeklyHour - this.Settings.WorldRecordHour;

            this.DrawSummaryEntry(gameGraphics, 8, Color.White, "Hours in Synchronization In Recent 7 Days:", weeklyHour.ToSummary());
            this.DrawSummaryBlank(gameGraphics, 9);
            this.DrawSummaryEntry(gameGraphics, 10, Color.Red,  "Len Bosack's Records in 7 Days:", -this.Settings.WorldRecordHour);
            this.DrawSummarySplit(gameGraphics, 11, Color.Red);

            this.DrawSummaryResult(gameGraphics, 12, Color.Gold, Color.Red, string.Empty, weeklyLeftHour);
        }

        private void DrawSummaryBlank(IGameGraphics gameGraphics, int line)
        {
            this.DrawSummaryEntry(gameGraphics, line, Color.White, string.Empty, string.Empty);
        }

        private void DrawSummaryEntry(IGameGraphics gameGraphics, int line, Color color, string caption, string presentation)
        {
            var captionPosition = new Vector2(gameGraphics.Settings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(gameGraphics.Settings.Width / 2f + 260, 150 + line * Settings.LineHeight);

            gameGraphics.FontDrawer.DrawString(this.Settings.EntityFont, caption     , captionPosition, color, this.Settings.EntitySize);
            gameGraphics.FontDrawer.DrawString(this.Settings.EntityFont, presentation, contentPosition, color, this.Settings.EntitySize);
        }

        private void DrawSummaryEntry(IGameGraphics gameGraphics, int line, Color color, string caption, object presentedData)
        {
            var captionPosition = new Vector2(gameGraphics.Settings.Width / 2f - 300, 150 + line * Settings.LineHeight);
            var contentPosition = new Vector2(gameGraphics.Settings.Width / 2f + 260, 150 + line * Settings.LineHeight);
            var contentString   = string.Format("{0}", presentedData);

            gameGraphics.FontDrawer.DrawString(this.Settings.EntityFont, caption      , captionPosition, color, this.Settings.EntitySize);
            gameGraphics.FontDrawer.DrawString(this.Settings.EntityFont, contentString, contentPosition, color, this.Settings.EntitySize);
        }

        private void DrawSummaryResult(IGameGraphics gameGraphics, int line, Color goodColor, Color badColor, string caption, int computation)
        {
            this.DrawSummaryEntry(gameGraphics, line, computation >= 0 ? goodColor : badColor, caption, computation.ToSummary());
        }

        private void DrawSummarySplit(IGameGraphics gameGraphics, int line, Color color)
        {
            var splitStart = new Vector2(gameGraphics.Settings.Width / 2f - 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);
            var splitEnd   = new Vector2(gameGraphics.Settings.Width / 2f + 300, 150 + line * Settings.LineHeight + Settings.LineHeight / 2);

            var spriteBatch = gameGraphics.Screen.SpriteBatch;
            spriteBatch.DrawLine(splitStart, splitEnd, color, this.Settings.EntitySize);
        }

        private void DrawSummaryTitle(IGameGraphics gameGraphics, Color color, string title)
        {
            gameGraphics.FontDrawer.DrawStringCenteredHV(this.Settings.TitleFont, title, this.Settings.TitleCenter, color, this.Settings.TitleSize);
        }

        #endregion 
    }
}