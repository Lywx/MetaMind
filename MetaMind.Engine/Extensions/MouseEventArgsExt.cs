namespace MetaMind.Engine.Extensions
{
    using System.Windows.Forms;

    public static class MouseEventArgsExt
    {
        public static Components.Input.MouseEventArgs Migrate(this MouseEventArgs args)
        {
            return new Components.Input.MouseEventArgs(args.Button.Migrate(), args.Clicks, args.X, args.Y, args.Delta);
        }
    }
}
