using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMind.Perseverance.Concepts
{
    [Serializable]
    public class Thing
    {
        public string Name;
        public string Desire;

        //public string ? 

        public List<Idea> Potential;
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Idea
    {
        public string Name;

    }

    [Serializable]
    public class Achievement
    {
        public string Name;
    }
}
