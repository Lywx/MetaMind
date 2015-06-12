namespace MetaMind.Testimony.Scripting
{
    using System;

    public class ScriptRunner
    {
        private readonly ScriptSearcher searcher;

        public ScriptRunner(ScriptSearcher searcher)
        {
            if (searcher == null)
            {
                throw new ArgumentNullException("searcher");
            }

            this.searcher = searcher;
        }

        public void Run()
        {
            foreach (var path in this.searcher.SearchScriptPaths())
            {
                var script = new Script(path);

                script.Run(Testimony.FsiSession);
            }
        }
    }
}
