namespace MetaMind.Engine.Core.Entity.Control.Item.Data
{
    using System;
    using Entity.Common;

    public interface IMMViewItemCharModifier : IMMInputtable, IMMDrawable, IDisposable
    {
        event EventHandler<MMViewItemDataEventArgs> ModificationEnded;

        event EventHandler<MMViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Initialize(string originalString, bool showCursor);

        void Release();
    }
}