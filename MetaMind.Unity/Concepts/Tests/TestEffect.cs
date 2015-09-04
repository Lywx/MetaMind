namespace MetaMind.Unity.Concepts.Tests
{
    using Engine;
    using System;
    using Events;

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

        private string FailingNotification => $"{this.test.Name} failed.";

        private string SucceedingNotification => $"{this.test.Name} succeeded.";

        private void EvaluationFailed(object sender, TestEvaluationEventArgs e)
        {
            if (e.IsSource && Test.Session.IsNotificationEnabled)
            {
                var audio = this.Interop.Audio;
                audio.PlayCue(this.failingCue);

                Test.Speech.SpeakAsync(this.FailingNotification);
            }
        }

        private void EvaluationSucceeded(object sender, TestEvaluationEventArgs e)
        {
            if (e.IsSource && Test.Session.IsNotificationEnabled)
            {
                var audio = this.Interop.Audio;
                audio.PlayCue(this.succeedingCue);

                Test.Speech.SpeakAsync(this.SucceedingNotification);
            }
        }
    }
}