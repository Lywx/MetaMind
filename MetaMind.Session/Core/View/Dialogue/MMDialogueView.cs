namespace MetaMind.Session.View.Dialogue
{
    using System;
    using Engine.Core.Settings;

    public class MMDialogueView : IMMParameterDependant<MMDialogueViewSettings>
    {
        public MMDialogueView(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            this.Text = text;
        }

        public string Text { get; }

        public string 

        #region Parameter Loading

        public void LoadParameter(MMDialogueViewSettings parameter)
        {
            parameter.DialogueHeight;
        }

        #endregion
    }
}