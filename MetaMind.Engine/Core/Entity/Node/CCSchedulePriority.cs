namespace MetaMind.Engine.Core.Entity.Node
{
    /// <remarks>
    /// We will define this as a static class since we can not define an Enum
    /// with the way uint.MaxValue is represented.
    /// </remarks>
    public static class CCSchedulePriority
    {
        public const uint RepeatForever = uint.MaxValue - 1;

        public const int System = int.MinValue;

        public const int User = System + 1;
    }
}