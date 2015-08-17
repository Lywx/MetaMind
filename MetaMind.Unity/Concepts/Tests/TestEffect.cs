namespace MetaMind.Unity.Concepts.Tests
{
    using Engine;
    using System;

    public class TestEffect : GameEntity
    {
        private readonly string failingCue = "Test Failure";

        private readonly string succeedingCue = "Test Success";

        private readonly Test test;

        public TestEffect(Test test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;

            this.test.Succeeded += this.EvaluationSucceeded;
            this.test.Failed    += this.EvaluationFailed;
        }

        private void EvaluationFailed(object sender, TestEventArgs e)
        {
            if (e.IsCause && Test.Session.IsNotificationEnabled)
            {
                var audio = this.GameInterop.Audio;
                audio.PlayCue(this.failingCue);
            }
        }

        private void EvaluationSucceeded(object sender, TestEventArgs e)
        {
            if (e.IsCause && Test.Session.IsNotificationEnabled)
            {
                var audio = this.GameInterop.Audio;
                audio.PlayCue(this.succeedingCue);
            }
        }
    }
}