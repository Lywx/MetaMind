namespace MetaMind.Engine.Extensions
{
    using System.Windows.Forms;

    public static class ExtMouseEventArgs
    {
        public static Components.Inputs.MouseEventArgs Convert(this MouseEventArgs args)
        {
            return new Components.Inputs.MouseEventArgs(args.Button.Convert(), args.Clicks, args.X, args.Y, args.Delta);
        }
    }
}
