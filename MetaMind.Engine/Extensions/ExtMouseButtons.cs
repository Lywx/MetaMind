namespace MetaMind.Engine.Extensions
{
    using System.Windows.Forms;

    public static class ExtMouseButtons
    {
        public static Components.Inputs.MouseButtons Convert(this MouseButtons mouseButtons)
        {
            return (Components.Inputs.MouseButtons)mouseButtons;
        }
    }
}