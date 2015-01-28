// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProcess.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Processes
{
    using Microsoft.Xna.Framework;

    public interface IProcess
    {
        #region Process Data

        IProcess Child { get; }

        bool IsAlive { get; }

        bool IsDead { get; }

        bool IsPaused { get; }

        bool IsRemoved { get; }

        ProcessState State { get; }

        #endregion Process Data

        #region Transition

        void OnAbort();

        void OnFail();

        void OnInit();

        void OnSuccess();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        #endregion Transition

        #region Operations

        void Abort();

        void AttachChild(IProcess process);

        void Fail();

        void Pause();

        IProcess RemoveChild();

        void Resume();

        void Succeed();

        #endregion Operations
    }
}