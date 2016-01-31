namespace MetaMind.Session.Model.Memory
{
    using Story;
    using System;
    using Tagging;

    public class MMMemoryPiece : IMMDialogueConvertible
    {
        public MMMemoryPiece()
        {
        }

        public string Author { get; set; }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public TagGroup Tags { get; set; }

        public IMMDialogue ToDialogue()
        {
            throw new System.NotImplementedException();
        }
    }
}