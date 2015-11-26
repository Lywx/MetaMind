namespace MetaMind.Engine.Components.Input
{
    using System.Collections.Generic;

    public class MMKeyboardBinding<TAction> : Dictionary<TAction, MMKeyboardCombination> {}
}