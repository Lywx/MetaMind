using MetaMind.Engine.Concepts;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [Serializable]
    public class SynchronizationDataProcessor
    {
        private TaskEntry target; 

        public void Accept( TaskEntry task )
        {
            target = task;
            target.Experience += new Experience();
        }

        public void Release( out TimeSpan timePassed )
        {
            timePassed = target.Experience.End();
            target = null;
        }

        public void Update( GameTime gameTime, bool enabled, double acceleration )
        {
            if ( enabled )
                target.Experience.Accelaration = acceleration;
        }
    }
}