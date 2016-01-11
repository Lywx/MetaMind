namespace MetaMind.Session.Model.Runtime.Story
{
    using System.Collections.Generic;

    public interface IObjective
    {
        string Name { get; set; }

        List<int> Data { get; set; }

        bool IsComplete { get; set; }
    }

    public class Objective : IObjective
    {
    }
}
