namespace MetaMind.Engine
{
    public interface IBufferDoubleUpdateable
    {
        void UpdateForwardBuffer();

        void UpdateBackwardBuffer();
    }
}