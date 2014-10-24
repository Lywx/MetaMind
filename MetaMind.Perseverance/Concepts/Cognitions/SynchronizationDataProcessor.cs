using System;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [Serializable]
    public class SynchronizationDataProcessor
    {
        private TaskEntry Target { get; set; } // ILivingData

        public void Accept( TaskEntry target ) // ILivingData
        {
            Target = target;
            Target.IsRunning = true;
            Target.Experience += new Experience();
        }

        public void Release( out TimeSpan timePassed )
        {
            // merge end and isrunning?
            timePassed = Target.Experience.End();

            Target.IsRunning = false;
            Target = null;
        }

        public void Update( GameTime gameTime, bool enabled, double acceleration )
        {
            if ( enabled )
                Target.Experience.Accelaration = acceleration;
        }
    }
}