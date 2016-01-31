namespace MetaMind.Session.View.Dialogue
{
    using Engine.Core.Settings;

    public class MMDialogueViewSettings : MMSessionPlainConfigurationRoot, IMMParameter
    {
        public float DialogueWidth { get; set; }

        public float DialogueHeight { get; set; }
    }
}
