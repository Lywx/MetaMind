namespace MetaMind.Engine.Components.Inputs
{
    using System;

    public class CharEnteredEventArgs : EventArgs
    {
        private readonly char character;


        public CharEnteredEventArgs(char character)
        {
            this.character = character;
        }

        public char Character
        {
            get
            {
                return this.character;
            }
        }
    }
}