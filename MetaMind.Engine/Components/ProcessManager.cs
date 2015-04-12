// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Process.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Processes;

    using Microsoft.Xna.Framework;

    public class ProcessManager : GameComponent
    {
        #region Singleton

        private static ProcessManager Singleton { get; set; }

        public static ProcessManager GetInstance(GameEngine gameEngine)
        {
            if (Singleton == null)
            {
                Singleton = new ProcessManager(gameEngine);
            }

            if (gameEngine != null)
            {
                gameEngine.Components.Add(Singleton);
            }

            return Singleton;
        }

        #endregion Singleton

        #region Process Data

        private readonly List<IProcess> processes;

        #endregion Process Data

        #region Engine Data

        private IGameFile GameFile { get; set; }

        private IGameInterop GameInterop { get; set; }

        private IGameSound GameSound { get; set; }

        #endregion

        #region Constructors

        private ProcessManager(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.processes = new List<IProcess>();

            this.UpdateOrder = 1;
            
            this.GameFile    = new GameEngineFile(gameEngine);
            this.GameInterop = new GameEngineInterop(gameEngine);
            this.GameSound   = new GameEngineSound(gameEngine);
        }

        #endregion Constructors

        #region Deconstruction

        ~ProcessManager()
        {
            processes.Clear();
        }

        #endregion Deconstruction

        #region Update 

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var i = 0;
            while (i < this.processes.Count)
            {
                var process = this.processes[i];

                if (process.State == ProcessState.Uninitilized)
                {
                    process.OnInit();
                }

                if (process.State == ProcessState.Running)
                {
                    process.Update(gameTime);
                    process.Update(this.GameFile, gameTime);
                    process.Update(this.GameInterop, gameTime);
                    process.Update(this.GameSound, gameTime);
                }

                if (process.IsDead)
                {
                    switch (process.State)
                    {
                        case ProcessState.Succeeded:
                            {
                                process.OnSuccess();
                                var child = process.RemoveChild();
                                if (child != null)
                                {
                                    this.processes.Add(child);
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

                    this.processes.Remove(process);
                }
                else
                {
                    ++i;
                }
            }
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