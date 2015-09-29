// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderComponentOperations.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Components
{
    public interface IRenderComponentOperations
    {
        void Add(RenderComponent component);

        void Remove(RenderComponent component);

        bool Contains(RenderComponent component, bool recursive);
    }
}