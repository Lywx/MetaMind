namespace MetaMind.Engine.Extensions
{
    public static class TExt
    {
        public static void Swap<T>(ref T first, ref T second)
        {
            var temp = first;

            first = second;
            second = temp;
        }
    }
}