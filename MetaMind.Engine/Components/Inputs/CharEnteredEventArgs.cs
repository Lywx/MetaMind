namespace MetaMind.Engine.Components.Inputs
{
    using System;

    public class CharEnteredEventArgs : EventArgs
    {
        private readonly byte[] character;

        private readonly int lParam;

        public CharEnteredEventArgs(byte[] character, int lParam)
        {
            this.character = character;
            this.lParam = lParam;
        }

        public bool AltPressed
        {
            get
            {
                return (this.lParam & (1 << 29)) > 0;
            }
        }

        public byte[] Character
        {
            get
            {
                return this.character;
            }
        }

        public bool ExtendedKey
        {
            get
            {
                return (this.lParam & (1 << 24)) > 0;
            }
        }

        public int Param
        {
            get
            {
                return this.lParam;
            }
        }

        public bool PreviousState
        {
            get
            {
                return (this.lParam & (1 << 30)) > 0;
            }
        }

        public int RepeatCount
        {
            get
            {
                return this.lParam & 0xffff;
            }
        }

        public bool TransitionState
        {
            get
            {
                return (this.lParam & (1 << 31)) > 0;
            }
        }
    }
}