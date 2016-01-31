namespace MetaMind.Engine.Core
{
    /// <summary>
    /// Interface for nodes that can be focused. 
    /// </summary>
    public interface IMMFocusable
    {
        bool CanFocus { get; }

        bool HasFocus { get; set; }
    }
}