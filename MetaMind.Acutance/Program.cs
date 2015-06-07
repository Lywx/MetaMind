namespace MetaMind.Acutance
{
    using System;
    using Engine;

    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (var engine = new GameEngine())
            {
                var runner = new Acutance(engine);

                runner.Run();
            }
        }
    }
}