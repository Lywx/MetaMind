namespace MetaMind.Engine.Core.Services.Console.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    internal class CommandHistory : List<string>
    {
        public CommandHistory()
        {
        }

        public int Index { get; private set; }

        #region Operations

        public new void Add(string command)
        {
            var parts = command.Split('\n');

            foreach (var part in parts.Where(part => !string.IsNullOrEmpty(part)))
            {
                base.Add(part);
            }

            this.Reset();
        }

        /// <summary>
        /// Reset the current position of the history list.
        /// </summary>
        public void Reset()
        {
            this.Index = this.Count;
        }

        public string Previous()
        {
            return this.Count == 0
                       ? string.Empty
                       : this.Index - 1 < 0 ? this[0] : this[--this.Index];
        }

        public string Next()
        {
            return this.Count == 0
                       ? string.Empty
                       : this.Index + 1 > this.Count - 1
                             ? this[this.Count - 1]
                             : this[++this.Index];
        }

        #endregion
    }
}
