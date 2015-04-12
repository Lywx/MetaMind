namespace MetaMind.Acutance.Guis.Modules
{
    using System;
    using System.ServiceModel;

    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class SynchronizationGroup : Group<SynchronizationGroupSettings>
    {
        private ISynchronization       synchronization;
        private TimeSpan               synchronizationTimer     = TimeSpan.Zero;
        private TimeSpan               synchronizationRetryTime = TimeSpan.FromSeconds(5);

        public SynchronizationGroup(SynchronizationGroupSettings settings)
            : base(settings)
        {
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
            get { return this.SynchronizationTaskInfoCenter + new Vector2(0, 30); }
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (this.synchronization != null)
            {
                this.DrawSynchronizationProgress(this.synchronization);
                this.DrawSynchronizationStateInformation(this.synchronization);
                this.DrawSynchronizationTaskInformation(this.synchronization);
                this.DrawSynchronizationTime(this.synchronization);
            }
        }

        public void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
        }

        public void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
        }

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (this.synchronizationTimer < TimeSpan.FromMilliseconds(10))
            {
                try
                {
                    this.synchronization = Acutance.Synchronization.Fetch() as ISynchronization;
                }
                catch (TimeoutException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
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
                catch (CommunicationObjectAbortedException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
                catch (CommunicationException)
                {
                    this.synchronizationTimer += gameTime.ElapsedGameTime;
                    this.synchronization = null;
                }
            }
            else
            {
                this.synchronizationTimer += gameTime.ElapsedGameTime;
                if (this.synchronizationTimer > this.synchronizationRetryTime)
                {
                    this.synchronizationTimer = TimeSpan.Zero;
                }
            }
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
            FontManager.DrawStringCenteredHV(
                this.Settings.StateFont,
                validSynchronization.Enabled ? SynchronizationModule.SyncTrueInfo : SynchronizationModule.SyncFalseInfo,
                this.SynchronizationStateInfoCenter,
                this.Settings.StateColor,
                this.Settings.StateSize);
        }

        private void DrawSynchronizationTaskInformation(ISynchronization validSynchronization)
        {
            var task = validSynchronization.SynchronizedData;
            if (task != null)
            {
                var text = task.SynchronizationName;
                FontManager.DrawStringCenteredHV(
                    text.IsAscii() ? this.Settings.StateFont : this.Settings.TaskFont,
                    text,
                    this.SynchronizationTaskInfoCenter,
                    this.Settings.TaskColor,
                    this.Settings.TaskSize);
            }
        }

        private void DrawSynchronizationTime(ISynchronization validSynchronization)
        {
            FontManager.DrawStringCenteredHV(
                this.Settings.SynchronizationTimeFont,
                string.Format("{0:hh\\:mm\\:ss}:{1}", validSynchronization.ElapsedTimeSinceTransition, validSynchronization.ElapsedTimeSinceTransition.Milliseconds.ToString("000")),
                this.SynchronizationTimeCenter,
                this.Settings.SynchronizationTimeColor,
                this.Settings.SynchronizationTimeSize);
        }

        private Rectangle SynchronizationProgressBarRectangle(ISynchronization validSynchronization)
        {
            return new Rectangle(
                this.Settings.BarFrameXC - this.Settings.BarFrameSize.X / 2,
                this.Settings.BarFrameYC - this.Settings.BarFrameSize.Y / 2,
                (int)(validSynchronization.ProgressPercent * this.Settings.BarFrameSize.X),
                this.Settings.BarFrameSize.Y);
        }
    }
}