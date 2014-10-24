using System.Collections.Generic;
using MetaMind.Perseverance.Concepts.TaskEntries;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class TacticModuleData : IModuleData
    {
        public List<TacticData> TacticData { get; set; }
        public TacticModuleData(Tasklist data)
        {
            TacticData = data.TacticData;
        }
    }
}