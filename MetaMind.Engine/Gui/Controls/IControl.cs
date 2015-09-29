namespace MetaMind.Engine.Gui.Controls
{
    using Components;

    public interface IControl : IControlOrganization, IControlOperations, IRenderComponent
    {
        #region Control Data

        new Control Parent { get; }

        new ControlCollection Children { get; }

        new Control Root { get; }

        new bool IsChild { get; }

        new bool IsParent { get; }

        #endregion

        #region Render Data

        bool IsRenderChild { get; }

        bool IsRenderParent { get; }

        #endregion

        #region Input Data 

        /// <summary>
        /// Similar design to Game.Component in MonoGame.
        /// </summary>
        GameEntityCollection<IGameEntity> Minions { get; }

        #endregion
    }
}