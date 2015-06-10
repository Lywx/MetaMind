// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Process.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Processes;

    using Microsoft.Xna.Framework;

    public class ProcessManager : GameComponent, IProcessManager
    {
        #region Process Data

        private readonly List<IProcess> processes;

        #endregion Process Data

        #region Constructors

        public ProcessManager(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            engine.Components.Add(this);
            
            this.processes = new List<IProcess>();
        }

        #endregion Constructors

        #region Deconstruction

        ~ProcessManager()
        {
            this.processes.Clear();
        }

        #endregion Deconstruction

        #region Update 

        public override void Update(GameTime gameTime)
        {
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
                }

                if (process.IsDead)
                {
                    switch (process.State)
                    {
                        case ProcessState.Succeeded:
                            {
                                process.OnSuccess();

                                // Continue child processes when succeeded
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

                    // Totally remove process's existing implication
                    process.Dispose();
                }
                else
                {
                    i += 1;
                }
            }
        }

        #endregion Update

        #region Operations

        public void AbortProcesses(bool immediate)
        {
            var i = 0;
            while (i < this.processes.Count)
            {
                var process = this.processes[i];

                process.Abort();

                if (immediate)
                {
                    process.OnAbort();

                    this.processes.Remove(process);
                }
                else
                {
                    i += 1;
                }
            }
        }

        public void AttachProcess(IProcess process)
        {
            this.processes.Add(process);
        }

        #endregion Operations
    }
}