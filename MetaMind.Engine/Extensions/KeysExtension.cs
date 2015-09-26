namespace System.Windows.Forms
{
    public static class KeysExtension
    {
        public static Microsoft.Xna.Framework.Input.Keys Migrate(this Keys keys) => (Microsoft.Xna.Framework.Input.Keys)keys;
    }
}