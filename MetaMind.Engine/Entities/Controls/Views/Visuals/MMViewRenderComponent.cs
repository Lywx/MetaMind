namespace MetaMind.Engine.Entities.Controls.Views.Visuals
{
    using Graphics;

    public abstract class MMViewRenderComponent : MMRendererComponent, IMMViewRenderComponent
    {
        protected MMViewRenderComponent(IMMView view)
        {
        }
    }
}