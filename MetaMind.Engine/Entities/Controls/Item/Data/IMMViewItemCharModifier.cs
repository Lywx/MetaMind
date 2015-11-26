namespace MetaMind.Engine.Entities.Controls.Item.Data
{
    using System;
    using Entities;

    public interface IMMViewItemCharModifier : IMMInputable, IMMDrawable, IDisposable
    {
        event EventHandler<MMViewItemDataEventArgs> ModificationEnded;

        event EventHandler<MMViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Initialize(string originalString, bool showCursor);

        void Release();
    }
}