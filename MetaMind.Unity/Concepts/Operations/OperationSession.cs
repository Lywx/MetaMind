namespace MetaMind.Unity.Concepts.Operations
{
    using System;
    using Scripting;

    public class OperationSession
    {
        #region Lock and Unlock

        private readonly TimeSpan unlockedTimeout = TimeSpan.FromSeconds(1);

        private DateTime unlockedMoment;

        private DateTime lockedMoment;

        private bool isLocked;

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

        public void Lock()
        {
            this.isLocked = true;

            this.lockedMoment = DateTime.Now;
        }

        public void Unlock()
        {
            this.unlockedMoment = DateTime.Now;
        }

        #endregion

        #region Notification

        public bool IsNotificationEnabled { get; set; } = false;

        public void ToggleNotification()
        {
            this.IsNotificationEnabled = !this.IsNotificationEnabled;
        }

        #endregion

        #region Fsi Session

        private readonly FsiSession fsiSession;

        public FsiSession FsiSession => this.fsiSession;

        #endregion

        public OperationSession(FsiSession fsiSession)
        {
            if (fsiSession == null)
            {
                throw new ArgumentNullException(nameof(fsiSession));
            }

            this.fsiSession = fsiSession;
        }
    }
}