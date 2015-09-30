// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderReactorOperations.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Reactors
{
    public interface IRenderReactorOperations
    {
        void Add(RenderReactor reactor);

        void Remove(RenderReactor reactor);

        bool Contains(RenderReactor reactor, bool recursive);
    }
}