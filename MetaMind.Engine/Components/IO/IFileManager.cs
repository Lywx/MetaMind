// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.IO
{
    using System;

    public interface IFileManager : IDisposable
    {
        #region Directory

        void DeleteSaveDirectory();

        #endregion
    }
}