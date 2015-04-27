namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IFrameEntity : IUpdateable, Engine.IDrawable, IInputable
    {
        bool[] States { get; }

        Func<bool> this[FrameState state] { get; }
    }
}