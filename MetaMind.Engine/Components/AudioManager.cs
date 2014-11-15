// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudioManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
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
    public class AudioManager : GameComponent
    {
        #region Singleton

        /// <summary>
        /// The singleton for this type.
        /// </summary>
        private static AudioManager singleton;

        #endregion Singleton

        #region Audio Data

        /// <summary>
        /// The audio engine used to play all cues.
        /// </summary>
        private AudioEngine audioEngine;

        /// <summary>
        /// The soundbank that contains all cues.
        /// </summary>
        private SoundBank soundBank;

        /// <summary>
        /// The wavebank with all wave files for this game.
        /// </summary>
        private WaveBank waveBank;

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
                audioEngine = new AudioEngine(settingsFile);
                waveBank    = new WaveBank(audioEngine, waveBankFile);
                soundBank   = new SoundBank(audioEngine, soundBankFile);
            }
            catch (NoAudioHardwareException)
            {
                // silently fall back to silence
                audioEngine = null;
                waveBank    = null;
                soundBank   = null;
            }
            catch (InvalidOperationException)
            {
                // no audio prepared
                // silently fall back to silence
                audioEngine = null;
                waveBank    = null;
                soundBank   = null;
            }
            catch (NotImplementedException)
            {
                audioEngine = null;
                waveBank    = null;
                soundBank   = null;
            }
        }

        /// <summary>
        /// Initialize the static AudioManager functionality.
        /// </summary>
        /// <param name="game">The game that this component will be attached to.</param>
        /// <param name="settingsFile">The filename of the XACT settings file.</param>
        /// <param name="waveBankFile">The filename of the XACT wavebank file.</param>
        /// <param name="soundBankFile">The filename of the XACT soundbank file.</param>
        public static AudioManager GetInstance(
            Game game, 
            string settingsFile, 
            string waveBankFile, 
            string soundBankFile)
        {
            if (singleton == null)
            {
                singleton = new AudioManager(game, settingsFile, waveBankFile, soundBankFile);
            }

            if (game != null)
            {
                game.Components.Add(singleton);
            }

            return singleton;
        }

        #endregion Initialization Methods

        #region Cue Methods

        /// <summary>
        /// Retrieve a cue by name.
        /// </summary>
        /// <param name="cueName">The name of the cue requested.</param>
        /// <returns>The cue corresponding to the name provided.</returns>
        public static Cue GetCue(string cueName)
        {
            if (string.IsNullOrEmpty(cueName) || (singleton == null) || (singleton.audioEngine == null)
                || (singleton.soundBank == null) || (singleton.waveBank == null))
            {
                return null;
            }

            return singleton.soundBank.GetCue(cueName);
        }

        /// <summary>
        /// Plays a cue by name.
        /// </summary>
        /// <param name="cueName">The name of the cue to play.</param>
        public static void PlayCue(string cueName)
        {
            if ((singleton != null) && (singleton.audioEngine != null) && (singleton.soundBank != null)
                && (singleton.waveBank != null))
            {
                singleton.soundBank.PlayCue(cueName);
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
            if (singleton != null)
            {
                singleton.musicCueNameStack.Clear();
                PushMusic(cueName);
            }
        }

        /// <summary>
        /// Plays the music for this game, adding it to the music stack.
        /// </summary>
        /// <param name="cueName">The name of the music cue to play.</param>
        public static void PushMusic(string cueName)
        {
            // start the new music cue
            if ((singleton != null) && (singleton.audioEngine != null) && (singleton.soundBank != null)
                && (singleton.waveBank != null))
            {
                singleton.musicCueNameStack.Push(cueName);
                if ((singleton.musicCue == null) || (singleton.musicCue.Name != cueName))
                {
                    if (singleton.musicCue != null)
                    {
                        singleton.musicCue.Stop(AudioStopOptions.AsAuthored);
                        singleton.musicCue.Dispose();
                        singleton.musicCue = null;
                    }

                    singleton.musicCue = GetCue(cueName);
                    if (singleton.musicCue != null)
                    {
                        singleton.musicCue.Play();
                    }
                }
            }
        }

        /// <summary>
        /// Stops the current music and plays the previous music on the stack.
        /// </summary>
        public static void PopMusic()
        {
            // start the new music cue
            if ((singleton != null) && (singleton.audioEngine != null) && (singleton.soundBank != null)
                && (singleton.waveBank != null))
            {
                string cueName = null;
                if (singleton.musicCueNameStack.Count > 0)
                {
                    singleton.musicCueNameStack.Pop();
                    if (singleton.musicCueNameStack.Count > 0)
                    {
                        cueName = singleton.musicCueNameStack.Peek();
                    }
                }

                if ((singleton.musicCue == null) || (singleton.musicCue.Name != cueName))
                {
                    if (singleton.musicCue != null)
                    {
                        singleton.musicCue.Stop(AudioStopOptions.AsAuthored);
                        singleton.musicCue.Dispose();
                        singleton.musicCue = null;
                    }

                    if (!string.IsNullOrEmpty(cueName))
                    {
                        singleton.musicCue = GetCue(cueName);
                        if (singleton.musicCue != null)
                        {
                            singleton.musicCue.Play();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stop music playback, clearing the cue.
        /// </summary>
        public static void StopMusic()
        {
            if (singleton != null)
            {
                singleton.musicCueNameStack.Clear();
                if (singleton.musicCue != null)
                {
                    singleton.musicCue.Stop(AudioStopOptions.AsAuthored);
                    singleton.musicCue.Dispose();
                    singleton.musicCue = null;
                }
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
            if (audioEngine != null)
            {
                audioEngine.Update();
            }

            if ((musicCue != null) && musicCue.IsStopped)
            {
                PopMusic();
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
                    soundBank = null;
                    waveBank = null;
                    audioEngine = null;
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