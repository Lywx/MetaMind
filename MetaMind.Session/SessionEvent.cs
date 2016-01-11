namespace MetaMind.Session.Sessions
{
    public enum SessionEvent
    {
        GameStarted  = 0xf3d8,
        GameEnded    = 0xb21a,

        SleepStarted = 0xd5e3,
        SleepStopped = 0x7bc6,
        
        SyncAlerted  = 0xe4e1,
        SyncStarted  = 0x35e3,
        SyncStopped  = 0x95db,
    }
}
