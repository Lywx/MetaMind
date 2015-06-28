namespace MetaMind.Testimony.Concepts.Operations
{
    using System;
    using Scripting;

    public class OperationSession
    {
        private readonly TimeSpan unlockedTimeout = TimeSpan.FromSeconds(3);

        private DateTime unlockedMoment;

        private DateTime lockedMoment;

        private bool isLocked;

        private readonly FsiSession fsiSession;

        public OperationSession(FsiSession fsiSession)
        {
            if (fsiSession == null)
            {
                throw new ArgumentNullException("fsiSession");
            }

            this.fsiSession = fsiSession;

            this.IsNotificationEnabled = true;
        }

        public bool IsLocked
        {
            get
            {
                // Introduced timeout rather than directly control isLocked 
                // to lock to only one operation when multiple operation is 
                // trying to send option screens

                // Make sure the unlocked moment is initialized
                if (this.unlockedMoment - this.lockedMoment > TimeSpan.Zero && 

                    // Unlock when timeout
                    DateTime.Now - this.unlockedMoment > this.unlockedTimeout)
                {
                    this.isLocked = false;
                }

                return this.isLocked;
            }
        }

        public bool IsNotificationEnabled { get; set; }

        public FsiSession FsiSession { get { return this.fsiSession; } }

        public void ToggleNotification()
        {
            this.IsNotificationEnabled = !this.IsNotificationEnabled;
        }

        public void Unlock()
        {
            this.unlockedMoment = DateTime.Now;
        }

        public void Lock()
        {
            this.isLocked = true;

            this.lockedMoment = DateTime.Now;
        }
    }
}