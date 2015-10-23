namespace MetaMind.Engine.Components.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MMRendererManager : MMInputableComponent
    {
        public MMRendererManager(MMEngine engine) : base(engine)
        {
        }

        #region Render Target

        private RenderTarget2D currentRenderTarget;

        private RenderTarget2D previousRenderTarget;

        internal RenderTarget2D CurrentRenderTarget
        {
            get { return this.currentRenderTarget; }
            set
            {
                this.previousRenderTarget = this.currentRenderTarget;
                this.currentRenderTarget = value;

                if (this.Graphics.Device.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal)
                {
                    this.Graphics.Device.SetRenderTarget(this.currentRenderTarget);
                }
            }
        }

        public void SetRenderTarget(RenderTarget2D target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.CurrentRenderTarget = target;
        }

        public void RestoreRenderTarget()
        {
            this.CurrentRenderTarget = this.previousRenderTarget;
        }
        #endregion

        #region Scissor Test

        private List<RasterizerState> rasterizerStatesCache = new List<RasterizerState>();

        internal Rectangle ScissorRectangle
        {
            get { return this.Graphics.Device.ScissorRectangle; }
            set
            {
                this.Graphics.Device.ScissorRectangle = new Rectangle(
                    value.X,
                    value.Y,
                    value.Width,
                    value.Height);
            }
        }

        public bool ScissorRectangleEnabled
        {
            get { return this.Graphics.Device.RasterizerState.ScissorTestEnable; }
            set
            {
                var changed = this.Graphics.Device.RasterizerState.ScissorTestEnable != value;
                if (changed)
                {
                    this.Graphics.Device.RasterizerState = this.GetScissorRasterizerState(value);
                }
            }
        }

        private RasterizerState GetScissorRasterizerState(bool scissorEnabled)
        {
            var currentState = this.Graphics.Device.RasterizerState;

            for (var i = 0; i < this.rasterizerStatesCache.Count; i++)
            {
                var state = this.rasterizerStatesCache[i];
                if (
                    state.ScissorTestEnable == scissorEnabled &&
                    currentState.CullMode == state.CullMode &&

                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    currentState.DepthBias == state.DepthBias &&
                    currentState.FillMode == state.FillMode &&
                    currentState.MultiSampleAntiAlias == state.MultiSampleAntiAlias &&

                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    currentState.SlopeScaleDepthBias == state.SlopeScaleDepthBias
                )
                {
                    return state;
                }
            }

            var newState = new RasterizerState
            {
                ScissorTestEnable    = scissorEnabled,
                CullMode             = currentState.CullMode,
                DepthBias            = currentState.DepthBias,
                FillMode             = currentState.FillMode,
                MultiSampleAntiAlias = currentState.MultiSampleAntiAlias,
                SlopeScaleDepthBias  = currentState.SlopeScaleDepthBias
            };

            this.rasterizerStatesCache.Add(newState);

            return newState;
        }

        #endregion

        #region Effect Texture

        private bool vertexColorEnabled;

        public bool VertexColorEnabled
        {
            get { return this.vertexColorEnabled; }
            set
            {
                if (this.vertexColorEnabled != value)
                {
                    this.vertexColorEnabled = value;
                    this.textureChanged = true;
                }
            }
        }

        private Texture2D textureCurrent;

        private bool textureChanged;

        private bool textureEnabled;

        public bool TextureEnabled
        {
            get { return this.textureEnabled; }
            set
            {
                if (this.textureEnabled != value)
                {
                    this.textureEnabled = value;
                    this.textureChanged = true;
                }
            }
        }

        // TODO(Critical): Wrong the curent effect's settigs is overriden
        private void ApplyTextureEffect()
        {
            if (this.effectCurrent is BasicEffect)
            {
                var basicEffect = (BasicEffect)this.effectCurrent;

                basicEffect.Texture = this.textureCurrent;
                basicEffect.TextureEnabled = this.textureEnabled;
                basicEffect.VertexColorEnabled = this.vertexColorEnabled;
            }
            else if (this.effectCurrent is AlphaTestEffect)
            {
                var alphaTestEffect = (AlphaTestEffect)this.effectCurrent;

                alphaTestEffect.Texture = this.textureCurrent;
                alphaTestEffect.VertexColorEnabled = this.vertexColorEnabled;
            }
            else
            {
                throw new Exception(
                    $"Effect {this.effectCurrent.GetType().Name} is not supported");
            }
        }

        public void SetTexture(Texture2D texture)
        {
            if (!this.Graphics.Device.IsDisposed
                && this.Graphics.Device.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal)
            {
                if (texture == null)
                {
                    this.Graphics.Device.SamplerStates[0] = SamplerState.LinearClamp;
                    this.TextureEnabled = false;
                }
                else
                {
                    // TODO(Improvement): Not supported for texture customized sampler state.
                    // this.Graphics.Device.SamplerStates[0] = texture.SamplerState;

                    this.TextureEnabled = true;
                }

                if (this.textureCurrent != texture)
                {
                    this.textureCurrent = texture;
                    this.textureChanged = true;
                }
            }
        }

        #endregion

        #region Effect Operations

        private bool effectChanged;

        private Effect effectCurrent;

        private BasicEffect effectDefault;

        private readonly Stack<Effect> effectStack;

        internal void PushEffect(Effect effect)
        {
            this.effectStack.Push(this.effectCurrent);
            this.effectCurrent = effect;
            this.effectChanged = true;
        }

        internal void PopEffect()
        {
            this.effectCurrent = this.effectStack.Pop();
            this.effectChanged = true;
        }

        private void ApplyEffect()
        {
            if (this.effectChanged)
            {
                var matrices = this.effectCurrent as IEffectMatrices;

                if (matrices != null)
                {
                    matrices.World      = this.matrixWorld;
                    matrices.View       = this.matrixView;
                    matrices.Projection = this.matrixProjection;
                }

                this.ApplyTextureEffect();
            }
            else
            {
                if (this.matrixWorldChanged
                    || this.matrixProjectionChanged
                    || this.matrixViewChanged)
                {
                    var matrices = this.effectCurrent as IEffectMatrices;
                    if (matrices != null)
                    {
                        if (this.matrixWorldChanged)
                        {
                            matrices.World = this.matrixWorld;
                        }

                        if (this.matrixViewChanged)
                        {
                            matrices.View = this.matrixView;
                        }

                        if (this.matrixProjectionChanged)
                        {
                            matrices.Projection = this.matrixProjection;
                        }
                    }
                }

                if (this.textureChanged)
                {
                    this.ApplyTextureEffect();
                }
            }

            this.effectChanged  = false;
            this.textureChanged = false;

            this.matrixWorldChanged      = false;
            this.matrixViewChanged       = false;
            this.matrixProjectionChanged = false;
        }

        #endregion

        #region Effect Matrix

        private Stack<Matrix> matrixStack = new Stack<Matrix>();

        private Matrix matrixWorld = Matrix.Identity;

        private Matrix matrixView = Matrix.Identity;

        private Matrix matrixProjection = Matrix.Identity;

        private Matrix matrixCurrent = Matrix.Identity;

        private bool matrixWorldChanged;

        private bool matrixProjectionChanged;

        private bool matrixViewChanged;

        public void SetIdentityMatrix()
        {
            this.matrixCurrent = Matrix.Identity;
            this.matrixWorldChanged = true;
        }

        public void PushMatrix()
        {
            this.matrixStack.Push(this.matrixCurrent);
        }

        public void PopMatrix()
        {
            this.matrixCurrent = this.matrixStack.Pop();
            this.matrixWorldChanged = true;
        }

        public void MultiplyMatrix(Matrix matrixIn)
        {
            this.matrixCurrent = Matrix.Multiply(this.matrixCurrent, matrixIn);
            this.matrixWorldChanged = true;
        }

        #endregion

        #region Initialization

        public SpriteBatch SpriteBatch { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            this.effectDefault = new BasicEffect(this.Graphics.Device);
            this.SpriteBatch   = new SpriteBatch(this.Graphics.Device);

            this.InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            this.matrixProjection = Matrix.Identity;
            this.matrixView = Matrix.Identity;
            this.matrixWorld = Matrix.Identity;

            this.matrixWorldChanged = true;
            this.matrixViewChanged = true;
            this.matrixProjectionChanged = true;
        }

        #endregion

        #region Reset

        internal void Reset()
        {
            this.ResetDevice();
            this.ResetDefaultEffect();

            this.ResetMatrix();
            this.ResetTexture();

            this.ResetEffect();
        }

        private void ResetMatrix()
        {
            this.matrixWorldChanged      = false;
            this.matrixViewChanged       = false;
            this.matrixProjectionChanged = false;
        }

        private void ResetTexture()
        {
            this.vertexColorEnabled = true;

            this.textureChanged = false;
            this.textureCurrent = null;
        }

        private void ResetDevice()
        {
            // Reset states
            this.Graphics.Device.RasterizerState  = RasterizerState.CullNone;
            this.Graphics.Device.BlendState       = BlendState.AlphaBlend;
            this.Graphics.Device.SamplerStates[0] = SamplerState.LinearClamp;

            // Reset buffers
            this.Graphics.Device.SetVertexBuffer(null);
            this.Graphics.Device.SetRenderTarget(null);
            this.Graphics.Device.Indices = null;
        }

        private void ResetEffect()
        {
            this.effectChanged = false;

            this.effectStack.Clear();
            this.effectCurrent = this.effectDefault;
        }

        private void ResetDefaultEffect()
        {
            this.effectDefault.Alpha = 1f;
            this.effectDefault.TextureEnabled = false;
            this.effectDefault.Texture = null;
            this.effectDefault.VertexColorEnabled = true;

            this.effectDefault.View = this.matrixView;
            this.effectDefault.World = this.matrixWorld;
            this.effectDefault.Projection = this.matrixProjection;
        }

        #endregion

        #region Draw

        internal void DrawPrimitives<T>(
            PrimitiveType primitiveType,
            T[] vertexData,
            int vertexOffset,
            int primitiveCount) where T : struct, IVertexType
        {
            if (primitiveCount <= 0)
            {
                return;
            }

            this.ApplyEffect();

            foreach (var pass in this.effectCurrent.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.Graphics.Device.DrawUserPrimitives(primitiveType, vertexData, vertexOffset, primitiveCount);
            }
        }

        internal void DrawIndexedPrimitives<T>(
            PrimitiveType primitiveType,
            T[] vertexData,
            int vertexOffset,
            int vertexCount,
            short[] indexData,
            int indexOffset,
            int primitiveCount)
            where T : struct, IVertexType
        {
            if (primitiveCount <= 0)
            {
                return;
            }

            this.ApplyEffect();

            foreach (var pass in this.effectCurrent.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.Graphics.Device.DrawUserIndexedPrimitives(
                    primitiveType,
                    vertexData,
                    vertexOffset,
                    vertexCount,
                    indexData,
                    indexOffset,
                    primitiveCount);
            }
        }

        public void DrawQuad(ref CCV3F_C4B_T2F_Quad quad)
        {
            CCV3F_C4B_T2F[] vertices = quadVertices;

            if (vertices == null)
            {
                vertices = quadVertices = new CCV3F_C4B_T2F[4];
                CheckQuadsIndexBuffer(1);
            }

            vertices[0] = quad.TopLeft;
            vertices[1] = quad.BottomLeft;
            vertices[2] = quad.TopRight;
            vertices[3] = quad.BottomRight;

            this.DrawIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, 4, quadsIndexBuffer.Data.Elements, 0, 2);
        }

        public void DrawQuads(CCRawList<CCV3F_C4B_T2F_Quad> quads, int start, int n)
        {
            if (n == 0)
            {
                return;
            }

            CheckQuadsIndexBuffer(start + n);
            CheckQuadsVertexBuffer(start + n);

            quadsBuffer.UpdateBuffer(quads, start, n);

            this.Graphics.Device.SetVertexBuffer(quadsBuffer.VertexBuffer);
            this.Graphics.Device.Indices = quadsIndexBuffer.IndexBuffer;

            this.ApplyEffect();

            foreach (var pass in this.effectCurrent.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.Graphics.Device.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    n * 4,
                    start * 6,
                    n * 2);
            }

            // Reset buffers
            this.Graphics.Device.SetVertexBuffer(null);
            this.Graphics.Device.Indices = null;
        }

        internal void DrawBuffer<T, T2>(CCVertexBuffer<T> vertexBuffer, CCIndexBuffer<T2> indexBuffer, int start, int count)
            where T : struct, IVertexType
            where T2 : struct
        {
            this.Graphics.Device.Indices = indexBuffer.IndexBuffer;
            this.Graphics.Device.SetVertexBuffer(vertexBuffer.VertexBuffer);

            this.ApplyEffect();

            foreach (var pass in this.effectCurrent.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.Graphics.Device.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    vertexBuffer.VertexBuffer.VertexCount,
                    start,
                    count);
            }

            // Reset buffers
            this.Graphics.Device.SetVertexBuffer(null);
            this.Graphics.Device.Indices = null;
        }

        internal void DrawRawBuffer<T>(
            T[] vertexBuffer,
            int vertexOffset,
            int vertexCount,
            short[] indexBuffer,
            int indexOffset,

            // TODO: Wrong parameter?
            int indexCount)
            where T : struct, IVertexType
        {
            this.ApplyEffect();

            foreach (var pass in this.effectCurrent.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.Graphics.Device.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertexBuffer,
                    vertexOffset,
                    vertexCount,
                    indexBuffer,
                    indexOffset,
                    indexCount);
            }
        }

        internal void DrawQuadsBuffer<T>(
            CCVertexBuffer<T> vertexBuffer,
            int start,
            int n) where T : struct, IVertexType
        {
            if (n == 0)
            {
                return;
            }

            CheckQuadsIndexBuffer(start + n);

            this.Graphics.Device.Indices = quadsIndexBuffer.IndexBuffer;
            this.Graphics.Device.SetVertexBuffer(vertexBuffer.VertexBuffer);

            this.ApplyEffect();

            EffectPassCollection passes = this.effectCurrent.CurrentTechnique.Passes;
            for (int i = 0; i < passes.Count; i++)
            {
                passes[i].Apply();
                this.Graphics.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexBuffer.VertexBuffer.VertexCount, start * 6, n * 2);
            }

            this.Graphics.Device.SetVertexBuffer(null);
            this.Graphics.Device.Indices = null;
        }

        #endregion
    }
}