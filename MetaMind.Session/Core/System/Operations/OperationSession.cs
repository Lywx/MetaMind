namespace MetaMind.Session.Operations
{
    using System;
    using Engine.Core.Services.Script.FSharp;
    using Runtime;

    public class OperationSession
    {
        private readonly ICognition cognition;

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

        public bool IsNotificationEnabled => this.cognition.SynchronizationData.Enabled;

        #endregion

        #region Fsi Session

        private readonly FsiSession fsiSession;

        public FsiSession FsiSession => this.fsiSession;

        #endregion

        public OperationSession(FsiSession fsiSession, ICognition cognition)
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
    }
}