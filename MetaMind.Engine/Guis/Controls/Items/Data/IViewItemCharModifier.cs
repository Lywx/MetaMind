namespace MetaMind.Engine.Guis.Controls.Items.Data
{
    using System;
    using Events;

    public interface IViewItemCharModifier : IInputable, IDrawable, IDisposable
    {
        event EventHandler<ViewItemDataEventArgs> ModificationEnded;

        event EventHandler<ViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Initialize(string originalString, bool showCursor);

        void Release();
    }
}