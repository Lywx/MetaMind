namespace MetaMind.Engine.Guis.Elements
{
    using System;

    public interface IFrameEntity : IInputable
    {
        bool[] States { get; }

        Func<bool> this[FrameState state] { get; }
    }
}