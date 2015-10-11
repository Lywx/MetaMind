namespace MetaMind.Engine.Entities.Controls.Views.Visuals
{
    using Graphics;

    public abstract class MMViewRenderComponent : MMRenderComponent, IMMViewRenderComponent
    {
        protected MMViewRenderComponent(IMMView view)
        {
        }
    }
}