namespace MetaMind.Unity.Concepts.Tests
{
    using System;
    using Scripting;

    public class TestSession
    {
        private readonly FsiSession fsiSession;

        public TestSession(FsiSession fsiSession)
        {
            if (fsiSession == null)
            {
                throw new ArgumentNullException("fsiSession");
            }

            this.fsiSession = fsiSession;
        }

        public bool IsNotificationEnabled { get; private set; } = false;

        public FsiSession FsiSession => this.fsiSession;

        public void ToggleNotification()
        {
            this.IsNotificationEnabled = !this.IsNotificationEnabled;
        }
    }
}
