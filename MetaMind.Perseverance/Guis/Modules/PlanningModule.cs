namespace MetaMind.Perseverance.Guis.Modules
{
    public class PlanningModule : Module<PlanningModuleSettings>
    {
        #region Constructors

        public PlanningModule( PlanningModuleSettings settings )
        {
            Settings = settings;

            Control = new PlanningModuleControl( this );
            Graphics = new PlanningModuleGraphics( this );
        }

        #endregion Constructors
    }
}