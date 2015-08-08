namespace MetaMind.Unity.Scripting
{
    using System;
    using System.Collections.Generic;
    using Engine;

    public class ScriptRunner : GameEntity
    {
        private readonly ScriptSearcher searcher;

        private List<string> searchResult;

        private readonly FsiSession fsiSession;

        public ScriptRunner(ScriptSearcher searcher, FsiSession fsiSession)
        {
            if (searcher == null)
            {
                throw new ArgumentNullException("searcher");
            }

            this.searcher = searcher;

            if (fsiSession == null)
            {
                throw new ArgumentNullException("fsiSession");
            }

            this.fsiSession = fsiSession;
        }

        public void Search()
        {
            this.searchResult = this.searcher.SearchScriptPaths();
        }

        public void Run()
        {
            if (this.searchResult != null)
            {
                foreach (var path in this.searchResult)
                {
                    var script = new Script(path);
                    script.Run(this.fsiSession);
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
