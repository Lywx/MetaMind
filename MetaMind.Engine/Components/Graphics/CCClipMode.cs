namespace MetaMind.Engine.Components.Graphics
{
    public enum CCClipMode
    {
        None,                   // No clipping of children

        Bounds,                 // Clipping with a ScissorRect

        BoundsWithRenderTarget  // Clipping with the ScissorRect and in a RenderTarget
    }
}