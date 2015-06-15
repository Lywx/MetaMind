namespace MetaMind.Testimony.Concepts.Tests
{
    public class TestSession
    {
        public bool NotificationEnabled { get; private set; }

        public TestSession()
        {
            this.NotificationEnabled = true;
        }

        public void ToggleNotification()
        {
            this.NotificationEnabled = !this.NotificationEnabled;
        }
    }
}
