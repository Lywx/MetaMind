namespace MetaMind.Testimony.Concepts.Tests
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

            this.IsNotificationEnabled = true;
        }

        public bool IsNotificationEnabled { get; private set; }

        public FsiSession FsiSession { get { return this.fsiSession; } }

        public void ToggleNotification()
        {
            this.IsNotificationEnabled = !this.IsNotificationEnabled;
        }
    }
}
