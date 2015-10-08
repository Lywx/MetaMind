namespace MetaMind.Engine.Services.Script.FSharp
{
    using System;

    public class FsScript : IFsScript
    {
        public FsScript(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.Path = path;
        }

        public string Path { get; set; }

        public void Run(FsiSession session)
        {
            session.EvalScriptAsync(this.Path);
        }
    }
}