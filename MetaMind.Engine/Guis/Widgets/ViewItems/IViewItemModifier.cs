using System;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemModifier
    {
        event EventHandler<ViewItemDataEventArgs> ModificationEnded;

        event EventHandler<ViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Draw(GameTime gameTime);

        void Initialize(string oldString);

        void Release();

        void Update(GameTime gameTime);
    }
}