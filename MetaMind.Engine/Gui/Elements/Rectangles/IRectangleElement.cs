namespace MetaMind.Engine.Gui.Elements.Rectangles
{
    using System;

    public interface IRectangleElement : IElement, IInputable
    {
        Func<bool> this[ElementState state] { get; }
    }
}