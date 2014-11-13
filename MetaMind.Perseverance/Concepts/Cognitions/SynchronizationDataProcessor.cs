// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationDataProcessor.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;

    using MetaMind.Engine.Concepts;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    using Microsoft.Xna.Framework;

    [Serializable]
    public class SynchronizationDataProcessor
    {
        private TaskEntry target;

        public void Accept(TaskEntry task)
        {
            this.target = task;
            this.target.Synchronizing = true;
            this.target.Experience += new Experience();
        }

        public void Release(out TimeSpan timePassed)
        {
            timePassed = this.target.Experience.End();

            this.target.Synchronizing = false;
            this.target = null;
        }

        public void Update(GameTime gameTime, bool enabled, double acceleration)
        {
            if (enabled)
            {
                this.target.Experience.Accelaration = acceleration;
            }
        }
    }
}