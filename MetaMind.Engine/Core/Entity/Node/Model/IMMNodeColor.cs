namespace MetaMind.Engine.Core.Entity.Node.Model
{
    using Microsoft.Xna.Framework;

    public interface IMMNodeColor
    {
        /// <summary>
        /// Raw color without processing.
        /// </summary>
        Color Raw { get; set; }

        /// <summary>
        /// Blend color which is a computed and processed version of raw and its 
        /// parent's color. You cannot change it in this interface.
        /// </summary>
        Color Blend { get; }

        bool CascadeEnabled { get; set; }
    }
}