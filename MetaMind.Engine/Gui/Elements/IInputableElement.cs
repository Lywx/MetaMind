// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui.Elements
{
    using System;
    using Entities;

    public interface IInputableElement : IElement, IMMInputable
    {
        Func<bool> this[ElementState state] { get; }
    }
}