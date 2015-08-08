namespace MetaMind.Unity.Scripting
{
    using System;

    public class Script : IScript
    {
        public Script(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            this.Path = path;
        }

        public string Path { get; set; }

        public void Run(FsiSession session)
        {
            session.EvalScript(this.Path);
        }
    }
}