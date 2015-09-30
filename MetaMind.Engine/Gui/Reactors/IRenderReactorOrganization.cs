// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderReactorOrganization.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Reactors
{
    public interface IRenderReactorOrganization
    {
        RenderReactor Parent { get; }

        RenderReactor Root { get; }

        RenderReactorCollection Children { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }
}