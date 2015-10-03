namespace MetaMind.Engine.Node
{
    using Entities;

    public class MMNode : MMInputEntity, IMMNode, IMMNodeInternal
    {
        #region Constructors and Finalizer

        public MMNode()
        {
            // TODO: Wrong
            this.Visual = new MMNodeVisual(this); 
            this.Co
        }

        #endregion

        #region Update

        public void Update(float dt)
        {
        }

        #endregion

        public bool CanFocus { get; }

        public bool HasFocus { get; set; }

        #region Comparison

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

        public IMMRenderOpacity Opacity
        {
            get { return this.Visual.Opacity; }
            set { this.Visual.Opacity = value; }
        }

        public IMMNodeColor Color
        {
            get { return this.Visual.Color; }
            set { this.Visual.Color = value; }
        }

        public IMMNodeVisual Visual { get; }
    }
}