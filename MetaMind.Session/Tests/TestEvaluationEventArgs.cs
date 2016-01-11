namespace MetaMind.Session.Tests
{
    public class TestEvaluationEventArgs
    {
        private readonly bool isSource;

        public TestEvaluationEventArgs(bool isSource)
        {
            this.isSource = isSource;
        }

        public bool IsSource => this.isSource;
    }
}