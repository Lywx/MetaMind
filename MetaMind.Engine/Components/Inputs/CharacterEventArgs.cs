using System;

namespace MetaMind.Engine.Components.Inputs
{
    public class CharacterEventArgs : EventArgs
    {
        private readonly byte[] character;
        private readonly int lParam;

        public CharacterEventArgs(byte[] character, int lParam)
        {
            this.character = character;
            this.lParam = lParam;
        }

        public bool AltPressed
        {
            get { return (lParam & (1 << 29)) > 0; }
        }
        public byte[] Character
        {
            get { return character; }
        }

        public bool ExtendedKey
        {
            get { return (lParam & (1 << 24)) > 0; }
        }
        public int Param
        {
            get { return lParam; }
        }

        public bool PreviousState
        {
            get { return (lParam & (1 << 30)) > 0; }
        }
        public int RepeatCount
        {
            get { return lParam & 0xffff; }
        }
        public bool TransitionState
        {
            get { return (lParam & (1 << 31)) > 0; }
        }
    }
}