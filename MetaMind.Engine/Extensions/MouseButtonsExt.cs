namespace MetaMind.Engine.Extensions
{
    using System.Windows.Forms;

    public static class MouseButtonsExt
    {
        public static Components.Input.MouseButtons Migrate(this MouseButtons mouseButtons)
        {
            return (Components.Input.MouseButtons)mouseButtons;
        }
    }
}