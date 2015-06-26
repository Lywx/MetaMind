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

        public FsiSession FsiSession { get { return this.fsiSession; } }
    }
}