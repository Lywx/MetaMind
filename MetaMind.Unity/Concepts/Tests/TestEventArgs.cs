namespace MetaMind.Unity.Concepts.Tests
{
    public class TestEventArgs
    {
        private readonly bool isCause;

        public TestEventArgs(bool isCause)
        {
            this.isCause = isCause;
        }

        public bool IsCause
        {
            get
            {
                return isCause;
            }
        }
    }
}