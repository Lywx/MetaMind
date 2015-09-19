namespace MetaMind.Engine.Gui.Element.Rectangles
{
    using System;

    public interface IRectangleElement : IElement, IInputable
    {
        Func<bool> this[ElementState state] { get; }
    }
}