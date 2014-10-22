using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components.Inputs
{
    public class ActionMap
    {
        /// <summary>
        /// List of Keyboard controls to be mapped to a given action.
        /// </summary>
        public List<Keys> Keys = new List<Keys>();
    }
}
