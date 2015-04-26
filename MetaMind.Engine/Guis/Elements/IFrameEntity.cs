namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;

    public interface IFrameEntity : IUpdateable, IDrawable, IInputable
    {
        bool[] States { get; }

        Func<bool> this[FrameState state] { get; set; }
    }
}