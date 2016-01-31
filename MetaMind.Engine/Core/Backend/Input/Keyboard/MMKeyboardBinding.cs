namespace MetaMind.Engine.Core.Backend.Input.Keyboard
{
    using System.Collections.Generic;

    public class MMKeyboardBinding<TAction> : Dictionary<TAction, MMKeyboardCombination> {}
}