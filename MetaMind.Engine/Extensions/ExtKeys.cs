namespace System.Windows.Forms
{
    public static class ExtKeys
    {
        public static Microsoft.Xna.Framework.Input.Keys Convert(this Keys keys) => (Microsoft.Xna.Framework.Input.Keys)keys;
    }
}