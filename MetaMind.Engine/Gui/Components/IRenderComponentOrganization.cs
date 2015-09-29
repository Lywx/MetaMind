// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderComponentOrganization.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Components
{
    public interface IRenderComponentOrganization
    {
        RenderComponent Parent { get; }

        RenderComponent Root { get; }

        RenderComponentCollection Children { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }
}