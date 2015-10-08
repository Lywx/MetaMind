namespace MetaMind.Session.Concepts.Synchronizations
{
    using System;

    [Serializable]
    public class SynchronizationDescription
    {
        public readonly double[] LevelAcceleration =
        {
            0.1f, // level 0 
            0.3f, // level 1
            0.6f, // level 2 
            0.9f, // level 3 
            1.2f, // level 4 
            1.5f, // level 5 
            2.0f  // level 6 
        };

        public readonly int[] LevelSeconds =
        {
            0,      // level 0 
            10,     // level 1 
            100,    // level 2 
            1000,   // level 3
            10000,  // level 4
            100000, // level 5
            1000000 // level 6
        };

        public readonly string[] LevelStates =
        {
            "Unrecognizable", // level 0
            "Perceivable",    // level 1
            "Sensible",       // level 2
            "Intelligible",   // level 3
            "Tangible",       // level 4
            "Illusional",     // level 5
            "Impossible",     // level 6
        };
    }
}