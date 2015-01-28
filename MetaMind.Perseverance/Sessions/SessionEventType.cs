namespace MetaMind.Perseverance.Sessions
{
    public enum SessionEventType
    {
        GameStarted  = 0xf3d8,

        SleepStarted = 0xd5e3,
        SleepStopped = 0x7bc6,
        
        SyncStarted  = 0x35e3,
        SyncStopped  = 0x95db,

        SyncAlerted  = 0xe4e1,
    }
}
