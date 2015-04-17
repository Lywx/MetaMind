namespace MetaMind.Engine.Guis.Elements
{
    public interface IFrameEntity : IUpdateable, IDrawable, IInputable
    {
        bool[] States { get; }

        void Disable(FrameState state);

        void Enable(FrameState state);

        bool IsEnabled(FrameState state);
    }
}