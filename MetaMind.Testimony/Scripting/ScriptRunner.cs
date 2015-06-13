namespace MetaMind.Testimony.Scripting
{
    using System;
    using System.Collections.Generic;
    using Engine;

    public class ScriptRunner : GameEntity
    {
        private readonly ScriptSearcher searcher;

        private List<string> searchResult;

        public ScriptRunner(ScriptSearcher searcher)
        {
            if (searcher == null)
            {
                throw new ArgumentNullException("searcher");
            }

            this.searcher = searcher;
        }

        public void Search()
        {
            this.searchResult = this.searcher.SearchScriptPaths();
        }

        public void Run()
        {
            if (this.searchResult != null)
            {
                var console = this.GameInterop.Console;
                console.WriteLine("MESSAGE: Script evaluation started");

                foreach (var path in this.searchResult)
                {
                    var script = new Script(path);

                    script.Run(Testimony.FsiSession);
                }
            }
            else
            {
                this.Search();
                this.Run();
            }
        }

        public void Rerun()
        {
            this.Search();
            this.Run(); 
        }
    }
}
