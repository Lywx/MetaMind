namespace MetaMind.Engine
{
    public interface IMMBufferUpdateable
    {
        void UpdateForwardBuffer();

        void UpdateBackwardBuffer();
    }
}