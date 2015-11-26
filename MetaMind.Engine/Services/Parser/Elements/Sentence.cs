namespace MetaMind.Engine.Services.Parser.Elements
{
    using System.Collections.Generic;

    public class Sentence
    {
        public Sentence(List<string> words)
        {
            this.Words = words;
        }

        public List<string> Words { get; }

        public override string ToString()
        {
            return string.Join(" ", this.Words);
        }

        public override int GetHashCode()
        {
            return this.Words?.GetHashCode() ?? 0;
        }

        protected bool Equals(Sentence other)
        {
            return Equals(this.Words, other.Words);
        }
    }
}
