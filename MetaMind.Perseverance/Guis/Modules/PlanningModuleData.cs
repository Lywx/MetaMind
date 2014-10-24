using System.Collections.Generic;
using System.Linq;
using MetaMind.Perseverance.Concepts.TaskEntries;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class PlanningModuleData : IModuleData
    {
        public List<QuestionEntry>  QuestionData  { get; set; }
        public List<FutureEntry>    FutureData    { get; set; }
        public List<DirectionEntry> DirectionData { get; set; }

        public PlanningModuleData( Tasklist tasklist )
        {
            QuestionData  = tasklist.Questions .Where( data => !data.HasParent ).ToList();
            DirectionData = tasklist.Directions.Where( data => !data.HasParent ).ToList();
            FutureData    = tasklist.Futures   .Where( data => !data.HasParent ).ToList();
        }
    }
}