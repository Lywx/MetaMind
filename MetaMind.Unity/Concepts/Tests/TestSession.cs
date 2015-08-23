namespace MetaMind.Unity.Concepts.Tests
{
    using System;
    using Cognitions;
    using Scripting;

    public class TestSession
    {
        private readonly FsiSession fsiSession;

        private readonly ICognition cognition;

        public TestSession(FsiSession fsiSession, ICognition cognition)
        {
            if (fsiSession == null)
            {
                throw new ArgumentNullException(nameof(fsiSession));
            }

            if (cognition == null)
            {
                throw new ArgumentNullException(nameof(cognition));
            }

            this.fsiSession = fsiSession;
            this.cognition  = cognition;
        }

        public bool IsNotificationEnabled => this.cognition.Synchronization.Enabled;

        public FsiSession FsiSession => this.fsiSession;
    }
}
