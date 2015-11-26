namespace MetaMind.Engine.Components.Content.Texture
{
    public struct MMImageDesign
    {
        public MMImageDesign(int screenWidth, int screenHeight)
        {
            this.ScreenWidth  = screenWidth;
            this.ScreenHeight = screenHeight;
        }

        public int ScreenWidth { get; private set; }

        public int ScreenHeight { get; private set; }
    }
}