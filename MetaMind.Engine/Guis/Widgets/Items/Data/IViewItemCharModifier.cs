namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System;

    using MetaMind.Engine.Events;

    public interface IViewItemCharModifier : IInputable, Engine.IDrawable, IDisposable
    {
        event EventHandler<ViewItemDataEventArgs> ModificationEnded;

        event EventHandler<ViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Initialize(string prevString, bool showCursor);

        void Release();
    }
}