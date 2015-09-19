namespace MetaMind.Engine.Extensions
{
    using System.Windows.Forms;

    public static class MouseEventArgsExt
    {
        public static Component.Input.MouseEventArgs Convert(this MouseEventArgs args)
        {
            return new Component.Input.MouseEventArgs(args.Button.Convert(), args.Clicks, args.X, args.Y, args.Delta);
        }
    }
}
