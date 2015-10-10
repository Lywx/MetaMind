namespace MetaMind.Engine.Nodes
{
    using System;
    using Entities;
    using Actions;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class MMNode : MMInputEntity, IMMNode, IMMNodeInternal
    {
        #region Constructors and Finalizer

        protected MMNode()
        {
        }

        #endregion

        #region Dependency 

        CCScheduler Scheduler
        {
            get { return Application != null ? Application.Scheduler : null; }
        }

        MMActionManager ActionManager
        {
            get { return Application != null ? Application.ActionManager : null; }
        }

        #endregion

        public IMMRenderOpacity Opacity
        {
            get { return this.Renderer.Opacity; }
        }

        public IMMNodeColor Color
        {
            get { return this.Renderer.Color; }
            set { this.Renderer.Color = value; }
        }

        public IMMNodeRenderer Renderer { get; protected set; }

        public IMMNodeController Controller { get; protected set;}

        public bool CanFocus => this.Controller.CanFocus;

        public bool HasFocus
        {
            get { return this.Controller.HasFocus; }
            set { this.Controller.HasFocus = value; }
        }

        #region Organization

        public IMMNode Parent { get; set; }

        public MMNodeCollection Children { get; set; } = new MMNodeCollection();

        #endregion

        #region Children Add and Remove

        public virtual void Add(MMNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node == this)
            {
                throw new InvalidOperationException("Can not add node to itself.");
            }

            if (node.Parent != null)
            {
                throw new InvalidOperationException("Node is already added. It can't be added again.");
            }

            node.Parent = this;

            this.Children  .Add(node);
            this.Controller.Add(node.Controller);
            this.Renderer  .Add(node.Renderer);
        }

        public virtual void Remove(MMNode node, bool disposing)
        {
            if (node == null)
            {
                return;
            }

            if (this.Children.Contains(node))
            {
                this.DetachChild(node, disposing);
            }
        }

        private void DetachChild(MMNode node, bool disposing)
        {
            if (IsRunning)
            {
                node.OnExitTransitionDidStart();
                node.OnExit();
            }

            // If you don't do cleanup, the child's actions will not get removed and the
            // its scheduledSelectors_ dict will not get released!
            if (disposing)
            {
                node.Dispose();
            }

            node.Parent = null;

            this.Children  .Remove(node);
            this.Controller.Remove(node.Controller);
            this.Renderer  .Remove(node.Renderer);
        }

        #endregion

        #region Children Sorting and Comparison

        public int Compare(MMNode x, MMNode y)
        {
            return x.CompareTo(y);
        }

        public int CompareTo(MMNode other)
        {
            int compare = ZOrder.CompareTo(other.ZOrder);

            // In the case where zOrders are equivalent, resort to ordering
            // based on when children were added to parent
            if (compare == 0)
            {
                compare = arrivalIndex.CompareTo(other.arrivalIndex);
            }

            return compare;
        }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            (graphics1, time1) => this.Renderer.Draw(time1)
        }

        public override void BeginDraw(GameTime time)
        {
            base.BeginDraw(time);
        }

        public override void EndDraw(GameTime time)
        {
            base.EndDraw(time);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        public void Update(float dt)
        {
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {

                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}