namespace MetaMind.Session.Components.Input
{
    using Engine.Components.Input;

    internal class SessionInputActions : MMInputActions
    {
        public static new bool TryParse(string value, out MMInputAction result)
        {
            return MMInputActionParser.TryParse<SessionInputActions>(value, out result);
        }

        public static new MMInputAction Parse(string value)
        {
            return MMInputActionParser.Parse<SessionInputActions>(value);
        }
    }
}