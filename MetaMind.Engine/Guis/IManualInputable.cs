namespace MetaMind.Engine.Guis
{
    public interface IManualInputable : IInputable
    {
        /// <summary>
        /// Trigger to update the input part of updating.
        /// </summary>
        void HandleInput();
    }
}