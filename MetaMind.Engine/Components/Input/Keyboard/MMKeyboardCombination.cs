namespace MetaMind.Engine.Components.Input
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Input;

    /// <remarks>
    ///     Index key is normal key. Value key is modifier.
    /// </remarks>>
    public class MMKeyboardCombination : Dictionary<Keys, List<Keys>> 
    {
    }
}