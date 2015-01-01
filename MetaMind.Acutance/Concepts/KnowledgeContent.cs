namespace MetaMind.Acutance.Concepts
{
    public class KnowledgeContent
    {
        public KnowledgeContent(string name, CommandMode mode)
        {
            this.Name = name;

            switch (mode)
            {
                case CommandMode.TriggeredByDate:
                    {
                        this.TriggeredByTimeout = false;
                        this.TriggeredByTime    = true;
                    }

                    break;

                case CommandMode.TriggeredByTimeout:
                    {
                        this.TriggeredByTimeout = true;
                        this.TriggeredByTime    = false;
                    }

                    break;
            }
        }

        public string Name { get; set; }

        public bool TriggeredByTime { get; private set; }

        public bool TriggeredByTimeout { get; private set; }
    }
}