namespace MetaMind.Engine.Entities.Controls.Views.Renderers
{
    using Graphics;

    public abstract class MMViewRenderComponent : MMRendererComponent, IMMViewRenderComponent
    {
        protected MMViewRenderComponent(IMMView view)
        {
        }
    }
}