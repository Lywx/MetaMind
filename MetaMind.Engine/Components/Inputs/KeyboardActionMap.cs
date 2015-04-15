namespace MetaMind.Engine.Components.Inputs
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Input;

    public class KeyboardActionMap
    {
        /// <summary>
        /// Dictionary of key, modifier pair to be mapped to a given action.
        /// </summary>
        public Dictionary<Keys, List<Keys>> Bindings = new Dictionary<Keys, List<Keys>>();
    }
}