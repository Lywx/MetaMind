// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Process.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Processes;

    using Microsoft.Xna.Framework;

    public class ProcessManager : DrawableGameComponent
    {
        #region Process Data

        private readonly List<IProcess> processes;

        #endregion Process Data

        #region Singleton

        private static ProcessManager Singleton { get; set; }

        public static ProcessManager GetInstance(Game game)
        {
            if (Singleton == null)
            {
                Singleton = new ProcessManager(game);
            }

            if (game != null)
            {
                game.Components.Add(Singleton);
            }

            return Singleton;
        }

        #endregion Singleton

        #region Constructors

        private ProcessManager(Game game)
            : base(game)
        {
            processes = new List<IProcess>();
        }

        #endregion Constructors

        #region Deconstruction

        ~ProcessManager()
        {
            processes.Clear();
        }

        #endregion Deconstruction

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            int i = 0;
            while (i < processes.Count)
            {
                IProcess process = processes[i];

                if (process.State == ProcessState.Uninitilized)
                {
                    process.OnInit();
                }

                if (process.State == ProcessState.Running)
                {
                    process.Update(gameTime);
                }

                if (process.IsDead)
                {
                    switch (process.State)
                    {
                        case ProcessState.Succeeded:
                            {
                                process.OnSuccess();
                                IProcess child = process.RemoveChild();
                                if (child != null)
                                {
                                    processes.Add(child);
                                }

                                break;
                            }

                        case ProcessState.Failed:
                            {
                                process.OnFail();
                                break;
                            }

                        case ProcessState.Aborted:
                            {
                                process.OnAbort();
                                break;
                            }
                    }

                    processes.Remove(process);
                }
                else
                {
                    ++i;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GameEngine.ScreenManager.SpriteBatch.Begin();

            foreach (var process in processes)
            {
                if (process.State == ProcessState.Running)
                {
                    process.Draw(gameTime);
                }
            }

            GameEngine.ScreenManager.SpriteBatch.End();
        }

        #endregion Update

        #region Operations

        public void AbortProcesses(bool immediate)
        {
            int i = 0;

            while (i < processes.Count)
            {
                IProcess process = processes[i];
                process.Abort();

                if (immediate)
                {
                    process.OnAbort();
                    processes.Remove(process);
                }
                else
                {
                    ++i;
                }
            }
        }

        public void AttachProcess(IProcess process)
        {
            processes.Add(process);
        }

        #endregion Operations
    }
}