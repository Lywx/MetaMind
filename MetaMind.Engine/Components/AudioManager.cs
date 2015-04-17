﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Audio.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    /// <summary>
    /// Component that manages audio playback for all cues.
    /// </summary>
    /// <remarks>
    /// Similar to a class found in the Net Rumble starter kit on the
    /// XNA Creators Club Online website (http://creators.xna.com).
    /// </remarks>
    public class AudioManager : GameComponent, IAudioManager
    {
        #region Singleton

        /// <summary>
        /// The singleton for this type.
        /// </summary>
        private static AudioManager Singleton { get; set; }

        public static AudioManager GetComponent(GameEngine game, int updateOrder)
        {
            var settingsFile  = @"Content\Audio\Audio.xgs";
            var waveBankFile  = @"Content\Audio\Wave Bank.xwb";
            var soundBankFile = @"Content\Audio\Sound Bank.xsb";

            return GetComponent(game, settingsFile, waveBankFile, soundBankFile, updateOrder);
        }

        /// <summary>
        /// Initialize the static Audio functionality.
        /// </summary>
        /// <param name="game">The game that this component will be attached to.</param>
        /// <param name="settingsFile">The filename of the XACT settings file.</param>
        /// <param name="waveBankFile">The filename of the XACT wavebank file.</param>
        /// <param name="soundBankFile">The filename of the XACT soundbank file.</param>
        /// <param name="updateOrder"></param>
        public static AudioManager GetComponent(GameEngine game, string settingsFile, string waveBankFile, string soundBankFile, int updateOrder)
        {
            if (Singleton == null)
            {
                Singleton = new AudioManager(game, settingsFile, waveBankFile, soundBankFile);
            }

            if (game != null)
            {
                game.Components.Add(Singleton);
            }

            return Singleton;
        }

        #endregion Singleton

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

        #region Initialization Methods

        /// <summary>
        /// Constructs the manager for audio playback of all cues.
        /// </summary>
        /// <param name="game">The game that this component will be attached to.</param>
        /// <param name="settingsFile">The filename of the XACT settings file.</param>
        /// <param name="waveBankFile">The filename of the XACT wavebank file.</param>
        /// <param name="soundBankFile">The filename of the XACT soundbank file.</param>
        private AudioManager(Game game, string settingsFile, string waveBankFile, string soundBankFile)
            : base(game)
        {
            try
            {
                this.AudioEngine = new AudioEngine(settingsFile);
                this.WaveBank    = new WaveBank(this.AudioEngine, waveBankFile);
                this.SoundBank   = new SoundBank(this.AudioEngine, soundBankFile);
            }
            catch (NoAudioHardwareException)
            {
                // silently fall back to silence
                this.AudioEngine = null;
                this.WaveBank    = null;
                this.SoundBank   = null;
            }
            catch (InvalidOperationException)
            {
                // no audio prepared
                // silently fall back to silence
                this.AudioEngine = null;
                this.WaveBank    = null;
                this.SoundBank   = null;
            }
            catch (NotImplementedException)
            {
                this.AudioEngine = null;
                this.WaveBank    = null;
                this.SoundBank   = null;
            }
        }

        #endregion Initialization Methods

        #region Cue Methods

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

        #endregion Cue Methods

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

                    this.musicCue = GetCue(cueName);
                    if (this.musicCue != null)
                    {
                        this.musicCue.Play();
                    }
                }
            }
        }

        /// <summary>
        /// Stops the current music and plays the previous music on the stack.
        /// </summary>
        public void PopMusic()
        {
            // start the new music cue
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
                        this.musicCue = GetCue(cueName);
                        if (this.musicCue != null)
                        {
                            this.musicCue.Play();
                        }
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
            // update the audio engine
            if (this.AudioEngine != null)
            {
                this.AudioEngine.Update();
            }

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
                    StopMusic();
                    this.SoundBank = null;
                    this.WaveBank = null;
                    this.AudioEngine = null;
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