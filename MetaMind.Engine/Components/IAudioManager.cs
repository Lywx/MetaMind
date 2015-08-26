namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public interface IAudioManager : IGameComponent, IDisposable
    {
        /// <summary>
        /// Retrieve a cue by name.
        /// </summary>
        /// <param name="cueName">The name of the cue requested.</param>
        /// <returns>The cue corresponding to the name provided.</returns>
        Cue GetCue(string cueName);

        /// <summary>
        /// Plays a cue by name.
        /// </summary>
        /// <param name="cueName">The name of the cue to play.</param>
        void PlayCue(string cueName);

        /// <summary>
        /// Plays the desired music, clearing the stack of music cues.
        /// </summary>
        /// <param name="cueName">The name of the music cue to play.</param>
        void PlayMusic(string cueName);

        /// <summary>
        /// Plays the music for this game, adding it to the music stack.
        /// </summary>
        /// <param name="cueName">The name of the music cue to play.</param>
        void PushMusic(string cueName);

        /// <summary>
        /// Stops the current music and plays the previous music on the stack.
        /// </summary>
        void PopMusic();

        /// <summary>
        /// Stop music playback, clearing the cue.
        /// </summary>
        void StopMusic();
    }
}