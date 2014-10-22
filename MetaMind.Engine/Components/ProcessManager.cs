using System.Collections.Generic;
using MetaMind.Engine.Components.Processes;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components
{
    public class ProcessManager : GameComponent
    {
        #region Process Data

        private readonly List<IProcess> processes;

        #endregion Process Data

        #region Singleton

        private static ProcessManager singleton;

        public static ProcessManager GetInstance(Game game)
        {
            if (singleton == null)
                singleton = new ProcessManager(game);
            if (game != null)
                game.Components.Add(singleton);
            return singleton;
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

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var i = 0;
            while (i < processes.Count)
            {
                var process = processes[i];

                if (process.State == ProcessState.Uninitilized)
                {
                    process.OnInit();
                }

                if (process.State == ProcessState.Running)
                {
                    process.OnUpdate(gameTime);
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
                                    processes.Add(child);
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

        #endregion Update

        #region Operations

        public void AbortAllProcesses(bool immediate)
        {
            var i = 0;

            while (i < processes.Count)
            {
                var process = processes[i];
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