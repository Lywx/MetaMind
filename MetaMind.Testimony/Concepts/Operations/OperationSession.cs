namespace MetaMind.Testimony.Concepts.Operations
{
    using System;
    using Scripting;

    public class OperationSession
    {
        private readonly FsiSession fsiSession;

        public OperationSession(FsiSession fsiSession)
        {
            if (fsiSession == null)
            {
                throw new ArgumentNullException("fsiSession");
            }

            this.fsiSession = fsiSession;
        }

        public bool IsLocked { get; private set; }

        public FsiSession FsiSession { get { return this.fsiSession; } }

        public void Unlock()
        {
            this.IsLocked = false;
        }

        public void Lock()
        {
            this.IsLocked = true;
        }
    }
}