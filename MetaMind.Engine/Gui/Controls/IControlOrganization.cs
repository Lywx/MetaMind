// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IControlOrganization.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Controls
{
    public interface IControlOrganization
    {
        ControlCollection Children { get; }

        Control Parent { get; }

        Control Root { get; }

        bool IsChild { get; }

        bool IsParent { get; }
    }
}