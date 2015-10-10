namespace MetaMind.Engine.Components.Graphics
{
    using System;
    using System.Collections.Generic;
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

                if (this.GraphicsDevice.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal)
                {
                    this.GraphicsDevice.SetRenderTarget(this.currentRenderTarget);
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
            get { return this.GraphicsDevice.ScissorRectangle; }
            set
            {
                this.GraphicsDevice.ScissorRectangle = new Rectangle(
                    value.X,
                    value.Y,
                    value.Width,
                    value.Height);
            }
        }

        public bool ScissorRectangleEnabled
        {
            get { return this.GraphicsDevice.RasterizerState.ScissorTestEnable; }
            set
            {
                var changed = this.GraphicsDevice.RasterizerState.ScissorTestEnable != value;
                if (changed)
                {
                    this.GraphicsDevice.RasterizerState = this.GetScissorRasterizerState(value);
                }
            }
        }

        private RasterizerState GetScissorRasterizerState(bool scissorEnabled)
        {
            var currentState = this.GraphicsDevice.RasterizerState;

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
    }
}