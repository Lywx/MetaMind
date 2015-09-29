// --------------------------------------------------------------------------------------------------------------------
// <copyright file="" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Elements
{
    using System;

    public interface IInputableElement : IElement, IInputable
    {
        Func<bool> this[ElementState state] { get; }
    }
}