namespace MetaMind.Engine.Core.Backend.Audio
{
    using System;
    using Content.Audio;
    using Microsoft.Xna.Framework;

    public interface IMMAudioController : IGameComponent, IDisposable
    {
        /// <summary>
        /// Plays the music, clearing the music stack.
        /// </summary>
        void Start(MMAudio audio);

        void Stop();

        /// <summary>
        /// Plays the music.
        /// </summary>
        void Play(MMAudio audio);

        /// <summary>
        /// Plays the music, adding it to the music stack.
        /// </summary>
        void Push(MMAudio audio);

        void Pop();
    }
}