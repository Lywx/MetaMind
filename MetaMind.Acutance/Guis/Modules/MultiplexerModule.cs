namespace MetaMind.Acutance.Guis.Modules
{
    using System;
    using System.ServiceModel;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<MultiplexerModuleSettings>
    {
        private readonly IView knowledgeView;
        private readonly IView traceView;

        private ISynchronization       synchronization;
        private TimeSpan               synchronizationTimer = TimeSpan.Zero;
        private SynchronizationMonitor monitor;

        private MultiplexerModuleSynchronizationAlertedListener synchronizationAlertedListener;

        public MultiplexerModule(MultiplexerModuleSettings settings)
            : base(settings)
        {
            this.traceView     = new View(this.Settings.TraceViewSettings, this.Settings.TraceItemSettings, this.Settings.TraceViewFactory);

            // TODO: strategy loading and alternative implementation
            this.knowledgeView = new View(this.Settings.KnowledgeViewSettings, this.Settings.KnowledgeItemSettings, this.Settings.KnowledgeViewFactory);
        }

        private Rectangle SynchronizationFrameRectangle
        {
            get
            {
                return new Rectangle(
                    this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                    this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                    this.Settings.BarFrameSize.X,
                    this.Settings.BarFrameSize.Y);
            }
        }

        private Vector2 SynchronizationStateInfoCenter
        {
            get
            {
                return new Vector2(
                    this.Settings.BarFrameXC,
                    this.Settings.BarFrameYC + this.Settings.StateMargin.Y);
            }
        }

        private Vector2 SynchronizationTaskInfoCenter
        {
            get
            {
                return new Vector2(
                    this.Settings.BarFrameXC,
                    this.Settings.BarFrameYC + this.Settings.TaskMargin.Y);
            }
        }

        private Vector2 SynchronizationTimeCenter
        {
            get { return this.SynchronizationTaskInfoCenter + new Vector2(0, 20); }
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (this.synchronization != null)
            {
                this.DrawSynchronizationProgress(this.synchronization);
                this.DrawSynchronizationStateInformation(this.synchronization);
                this.DrawSynchronizationTaskInformation(this.synchronization);
                this.DrawSynchronizationTime(this.synchronization);
            }

            this.DrawTrace(gameTime, alpha);
            this.DrawKnowledge(gameTime, alpha);
        }

        public override void Load()
        {
            foreach (var trace in Acutance.Adventure.Tracelist.Traces.ToArray())
            {
                this.traceView.Control.AddItem(trace);
            }

            if (this.synchronizationAlertedListener == null)
            {
                this.synchronizationAlertedListener = new MultiplexerModuleSynchronizationAlertedListener(this.traceView);
            }

            EventManager.AddListener(this.synchronizationAlertedListener);
        }

        public override void Unload()
        {
            if (this.synchronizationAlertedListener != null)
            {
                EventManager.RemoveListener(this.synchronizationAlertedListener);
            }

            this.synchronizationAlertedListener = null;
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.traceView.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.TryUpdateSynchronization(gameTime);

            this.traceView.UpdateStructure(gameTime);
            this.knowledgeView.UpdateStructure(gameTime);
        }

        private void DrawKnowledge(GameTime gameTime, byte alpha)
        {
            this.knowledgeView.Draw(gameTime, alpha);
        }

        private void DrawSynchronizationProgress(ISynchronization validSynchronization)
        {
            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.SynchronizationFrameRectangle,
                this.Settings.BarFrameBackgroundColor);

            Primitives2D.FillRectangle(
                ScreenManager.SpriteBatch,
                this.SynchronizationProgressBarRectangle(validSynchronization),
                this.synchronization.Enabled ? this.Settings.BarFrameAscendColor : this.Settings.BarFrameDescendColor);
        }

        private void DrawSynchronizationStateInformation(ISynchronization validSynchronization)
        {
            FontManager.DrawCenteredText(
                this.Settings.StateFont,
                validSynchronization.Enabled ? SynchronizationModule.SyncTrueInfo : SynchronizationModule.SyncFalseInfo,
                this.SynchronizationStateInfoCenter,
                this.Settings.StateColor,
                this.Settings.StateSize);
        }

        private void DrawSynchronizationTaskInformation(ISynchronization synchronization)
        {
            var task = synchronization.SynchronizedTask;
            if (task != null)
            {
                var text = task.Name;
                FontManager.DrawCenteredText(
                    text.IsAscii() ? this.Settings.StateFont : this.Settings.TaskFont,
                    text,
                    this.SynchronizationTaskInfoCenter,
                    this.Settings.TaskColor,
                    this.Settings.TaskSize);
            }
        }

        private void DrawSynchronizationTime(ISynchronization validSynchronization)
        {
            FontManager.DrawCenteredText(
                this.Settings.SynchronizationTimeFont,
                string.Format("{0:hh\\:mm\\:ss}:{1}", validSynchronization.ElapsedTimeSinceTransition, validSynchronization.ElapsedTimeSinceTransition.Milliseconds.ToString("000")),
                this.SynchronizationTimeCenter,
                this.Settings.SynchronizationTimeColor,
                this.Settings.SynchronizationTimeSize);
        }

        private void DrawTrace(GameTime gameTime, byte alpha)
        {
            this.traceView.Draw(gameTime, alpha);
        }

        private Rectangle SynchronizationProgressBarRectangle(ISynchronization validSynchronization)
        {
            return new Rectangle(
                this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                (int)(validSynchronization.ProgressPercent * this.Settings.BarFrameSize.X),
                this.Settings.BarFrameSize.Y);
        }

        private void TryUpdateSynchronization(GameTime gameTime)
        {
            if (this.synchronizationTimer < TimeSpan.FromMilliseconds(100))
            {
                try
                {
                    this.synchronization = Acutance.Synchronization.Fetch() as ISynchronization;

                    if (this.monitor == null)
                    {
                        this.monitor = new SynchronizationMonitor(ScreenManager.Game, this.synchronization, false)
                                           {
                                               AttentionSpan = TimeSpan.FromSeconds(20),

                                               SynchronizingCue    = "Hit Point Restoring",
                                               NotSynchronizingCue = "Magic Returning",
                                           };
                    }

                    this.monitor.Synchronization = this.synchronization;
                    this.monitor.TryStart();
                }
                catch (ServerTooBusyException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
                catch (EndpointNotFoundException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
                catch (TimeoutException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
                catch (CommunicationObjectAbortedException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
            }
            else
            {
                this.synchronizationTimer += gameTime.ElapsedGameTime;
                if (this.synchronizationTimer > TimeSpan.FromSeconds(5))
                {
                    this.synchronizationTimer = TimeSpan.Zero;
                }
            }
        }
    }
}