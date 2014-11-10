using System.Collections.Generic;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Guis.Widgets.Cameras;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class TacticModuleControl : ModuleControl<TacticModule, TacticModuleSettings, TacticModuleControl>
    {
        //---------------------------------------------------------------------
        private IMarkovCamera camera;
        private TacticWindow tacticWindow;

        //---------------------------------------------------------------------
        public List<TacticWindow> Windows { get; set; }

        //---------------------------------------------------------------------

        #region Contructors

        public TacticModuleControl( TacticModule module )
            : base( module )
        {
            camera = new MarkovCamera( CameraSettings.Default );

            Windows = new List<TacticWindow>();
        }

        #endregion Contructors

        //---------------------------------------------------------------------

        #region Load and Unload

        public override void Load( IModuleData data )
        {
            LoadGui();
            LoadEvents();
            LoadData( ( TacticModuleData ) data );
        }

        public override void Unload()
        {
            UnloadGui();
            UnloadEvents();
        }

        private void LoadData( TacticModuleData data )
        {
            data.TacticData.ForEach( tactic => tacticWindow.AddItem( tactic ) );
        }
        private void LoadEvents()
        {
        }

        private void LoadGui()
        {
            var tileViewSetting = new TileViewSettings
            {
                TileViewRowNumDisplay = ModuleSettings.TacticViewRowNumDisplay,
                TileViewColumnNumMax = ModuleSettings.TacticViewColumnNumMax,
                TileViewColumnNumDisplay = ModuleSettings.TacticViewColumnNumDisplay,
                TileViewNameXMargin = ModuleSettings.TacticViewNameXMargin
            };
            var tileItemSetting = TacticItemSettings.Default;

            tacticWindow = new TacticWindow(
                ModuleSettings.TacticWindowName,
                ModuleSettings.TacticWindowPosition.X,
                ModuleSettings.TacticWindowPosition.Y,
                tileViewSetting,
                tileItemSetting
                );
            
            Windows.Add( tacticWindow );
        }

        private void UnloadEvents()
        {
            
        }
        private void UnloadGui()
        {
            tacticWindow.Close();
        }

        #endregion Load and Unload

        #region Update

        public override void HandleInput()
        {
            Windows.ForEach( window => window.HandleInput() );
            camera.HandleInput();
        }

        public override void UpdateInput( GameTime gameTime )
        {
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            camera.Update( gameTime );

            foreach ( var window in Windows )
            {
                window.TileWindowX += ( int ) camera.Movement.X;
                window.Update( gameTime );
            }
            Windows.RemoveAll( window => !window.IsActive );
        }

        #endregion Update
    }
}