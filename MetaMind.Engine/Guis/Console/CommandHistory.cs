namespace MetaMind.Engine.Guis.Console
{
    using System.Collections.Generic;

    internal class CommandHistory : List<string>
    {
        public int Index { get; private set; }

        public void Reset()
        {
            this.Index = this.Count;
        }

        public string Next()
        {
            return this.Count == 0 ? "" : this.Index + 1 > this.Count - 1 ? this[this.Count - 1] : this[++this.Index];
        }

        public string Previous()
        {
            return this.Count == 0 ? "" : this.Index - 1 < 0 ? this[0] : this[--this.Index];
        }

        public new void Add(string command)
        {
            var parts = command.Split('\n');
            foreach (var part in parts)
            {
                if (part != "")
                {
                    base.Add(part);
                }
            }

            this.Reset();
        }
    }
}