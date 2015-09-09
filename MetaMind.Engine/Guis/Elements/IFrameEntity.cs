namespace MetaMind.Engine.Guis.Elements
{
    using System;

    public interface IFrameEntity : IInputable
    {
        Func<bool> this[FrameState state] { get; }
    }
}