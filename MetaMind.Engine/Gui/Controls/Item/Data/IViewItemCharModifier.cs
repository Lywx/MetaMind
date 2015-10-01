namespace MetaMind.Engine.Gui.Controls.Item.Data
{
    using System;
    using Entities;

    public interface IViewItemCharModifier : IMMInputable, IMMDrawable, IDisposable
    {
        event EventHandler<ViewItemDataEventArgs> ModificationEnded;

        event EventHandler<ViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Initialize(string originalString, bool showCursor);

        void Release();
    }
}