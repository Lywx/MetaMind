namespace MetaMind.Engine.Extensions
{
    using System.Windows.Forms;

    public static class MouseButtonsExt
    {
        public static Component.Input.MouseButtons Convert(this MouseButtons mouseButtons)
        {
            return (Component.Input.MouseButtons)mouseButtons;
        }
    }
}