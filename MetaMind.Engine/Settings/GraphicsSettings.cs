namespace MetaMind.Engine.Settings
{
    using System.Linq;
    using System.Windows.Forms;

    public static class GraphicsSettings
    {
        public static Screen Screen     = Screen.AllScreens.First(e => e.Primary);

        public static bool   Fullscreen = false;
        
        public static int Width
        {
            get { return Fullscreen ? Screen.Bounds.Width : 1280; }
        }

        public static int Height
        {
            get { return Fullscreen ? Screen.Bounds.Height : 720; }         
        } 
    }
}