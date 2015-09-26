// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Audio.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Audio
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    /// <summary>
    ///     Component that manages audio playback for all cues.
    /// </summary>
    /// <remarks>
    ///     Similar to a class found in the Net Rumble starter kit on the
    ///     XNA Creators Club Online website (http://creators.xna.com).
    ///
    ///     It does not need a finalizer, for it does not directly own
    ///     unmanaged resource.
    /// </remarks>
    public class AudioManager : GameComponent, IAudioManager
    {
        public AudioManager(GameEngine engine, AudioEngine audioEngine, WaveBank waveBank, SoundBank soundBank)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (audioEngine == null)
            {
                throw new ArgumentNullException(nameof(audioEngine));
            }

            if (waveBank == null)
            {
                throw new ArgumentNullException(nameof(waveBank));
            }

            if (soundBank == null)
            {
                throw new ArgumentNullException(nameof(soundBank));
            }

            this.AudioEngine = audioEngine;
            this.WaveBank    = waveBank;
            this.SoundBank   = soundBank;
        }

        #region Audio Data

        /// <summary>
        /// The audio engine used to play all cues.
        /// </summary>
        private AudioEngine AudioEngine { get; set; }

        /// <summary>
        /// The soundbank that contains all cues.
        /// </summary>
        private SoundBank SoundBank { get; set; }

        /// <summary>
        /// The wavebank with all wave files for this game.
        /// </summary>
        private WaveBank WaveBank { get; set; }

        #endregion Audio Data

        #region Cue 

        /// <summary>
        /// Retrieve a cue by name.
        /// </summary>
        /// <param name="cueName">The name of the cue requested.</param>
        /// <returns>The cue corresponding to the name provided.</returns>
        public Cue GetCue(string cueName)
        {
            if (string.IsNullOrEmpty(cueName) ||
                this.AudioEngine == null ||
                this.SoundBank == null ||
                this.WaveBank == null)
            {
                return null;
            }

            return this.SoundBank.GetCue(cueName);
        }

        /// <summary>
        /// Plays a cue by name.
        /// </summary>
        /// <param name="cueName">The name of the cue to play.</param>
        public void PlayCue(string cueName)
        {
            if (this.AudioEngine != null && 
                this.SoundBank != null && 
                this.WaveBank != null)
            {
                this.SoundBank.PlayCue(cueName);
            }
        }

        #endregion Cue

        #region Music

        /// <summary>
        /// Stack of music cue names, for layered music playback.
        /// </summary>
        private readonly Stack<string> musicCueNameStack = new Stack<string>();

        /// <summary>
        /// The cue for the music currently playing, if any.
        /// </summary>
        private Cue musicCue;

        /// <summary>
        /// Plays the desired music, clearing the stack of music cues.
        /// </summary>
        /// <param name="cueName">The name of the music cue to play.</param>
        public void PlayMusic(string cueName)
        {
            // start the new music cue
            this.musicCueNameStack.Clear();
            this.PushMusic(cueName);
        }

        /// <summary>
        /// Plays the music for this game, adding it to the music stack.
        /// </summary>
        /// <param name="cueName">The name of the music cue to play.</param>
        public void PushMusic(string cueName)
        {
            // start the new music cue
            if (!(this.AudioEngine == null || 
                this.SoundBank == null || 
                this.WaveBank == null))
            {
                this.musicCueNameStack.Push(cueName);
                if (this.musicCue == null || 
                    this.musicCue.Name != cueName)
                {
                    if (this.musicCue != null)
                    {
                        this.musicCue.Stop(AudioStopOptions.AsAuthored);
                        this.musicCue.Dispose();
                        this.musicCue = null;
                    }

                    this.musicCue = this.GetCue(cueName);
                    this.musicCue?.Play();
                }
            }
        }

        /// <summary>
        /// Stops the current music and plays the previous music on the stack.
        /// </summary>
        public void PopMusic()
        {
            // Start the new music cue
            if (this.AudioEngine != null && 
                this.SoundBank != null && 
                this.WaveBank != null)
            {
                string cueName = null;
                if (this.musicCueNameStack.Count > 0)
                {
                    this.musicCueNameStack.Pop();
                    if (this.musicCueNameStack.Count > 0)
                    {
                        cueName = this.musicCueNameStack.Peek();
                    }
                }

                if ((this.musicCue == null) || (this.musicCue.Name != cueName))
                {
                    if (this.musicCue != null)
                    {
                        this.musicCue.Stop(AudioStopOptions.AsAuthored);
                        this.musicCue.Dispose();
                        this.musicCue = null;
                    }

                    if (!string.IsNullOrEmpty(cueName))
                    {
                        this.musicCue = this.GetCue(cueName);
                        this.musicCue?.Play();
                    }
                }
            }
        }

        /// <summary>
        /// Stop music playback, clearing the cue.
        /// </summary>
        public void StopMusic()
        {
            this.musicCueNameStack.Clear();
            if (this.musicCue != null)
            {
                this.musicCue.Stop(AudioStopOptions.AsAuthored);
                this.musicCue.Dispose();
                this.musicCue = null;
            }
        }

        #endregion Music

        #region Updating Methods

        /// <summary>
        /// Update the audio manager, particularly the engine.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update the audio engine
            this.AudioEngine?.Update();

            if (this.musicCue != null && 
                this.musicCue.IsStopped)
            {
                this.PopMusic();
            }

            base.Update(gameTime);
        }

        #endregion Updating Methods

        #region Instance Disposal Methods

        /// <summary>
        /// Clean up the component when it is disposing.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Dispose music cue
                    this.StopMusic();

                    this.AudioEngine?.Dispose();
                    this.SoundBank?  .Dispose();
                    this.WaveBank?   .Dispose();

                    this.AudioEngine = null;
                    this.WaveBank    = null;
                    this.SoundBank   = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion Instance Disposal Methods
    }
}