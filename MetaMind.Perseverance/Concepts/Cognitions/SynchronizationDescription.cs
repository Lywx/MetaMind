using System;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [Serializable]
    public class SynchronizationDescription
    {
        
        public readonly double[] LevelAcceleration =
        {
            0.1f, // level 0 unrecognizable
            0.3f, // level 1 perceivable
            0.6f, // level 2 sensible
            0.9f, // level 3 intelligible
            1.2f, // level 4 tangible
            1.5f, // level 5 illusional
            2.0f  // level 6 the beyond
        };

        public readonly int[] LevelSeconds =
        {
            0,      // level 0 LevelSeconds[0]
            10,     // level 1 LevelSeconds[1]
            100,    // level 2 LevelSeconds[2]
            1000,   // level 3
            10000,  // level 4
            100000, // level 5
            1000000 // level 6
        };

        public readonly string[] LevelStates =
        {
            "unrecognizable", // level 0
            "perceivable",    // level 1
            "sensible",       // level 2
            "intelligible",   // level 3
            "tangible",       // level 4
            "illusional",     // level 5
            "the beyond",     // level 6
        };
    }
}