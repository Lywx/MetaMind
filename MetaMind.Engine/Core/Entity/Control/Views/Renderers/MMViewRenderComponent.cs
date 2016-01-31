namespace MetaMind.Engine.Core.Entity.Control.Views.Renderers
{
    using Entity.Graphics;

    public abstract class MMViewRenderComponent : MMRendererComponent, IMMViewRenderComponent
    {
        protected MMViewRenderComponent(IMMView view)
        {
        }
    }
}