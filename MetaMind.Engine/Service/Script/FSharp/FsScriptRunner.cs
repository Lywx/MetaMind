namespace MetaMind.Engine.Service.Scripting.FSharp
{
    using System;
    using System.Collections.Generic;

    public class FsScriptRunner : GameEntity
    {
        private readonly FsScriptSearcher searcher;

        private List<string> searchResult;

        private readonly FsiSession fsiSession;

        public FsScriptRunner(FsScriptSearcher searcher, FsiSession fsiSession)
        {
            if (searcher == null)
            {
                throw new ArgumentNullException(nameof(searcher));
            }

            this.searcher = searcher;

            if (fsiSession == null)
            {
                throw new ArgumentNullException(nameof(fsiSession));
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
                    var script = new FsScript(path);
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
