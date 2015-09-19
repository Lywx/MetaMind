namespace MetaMind.Engine.Gui.Control.Item.Data
{
    using System;

    public interface IViewItemCharModifier : IInputable, IDrawable, IDisposable
    {
        event EventHandler<ViewItemDataEventArgs> ModificationEnded;

        event EventHandler<ViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Initialize(string originalString, bool showCursor);

        void Release();
    }
}