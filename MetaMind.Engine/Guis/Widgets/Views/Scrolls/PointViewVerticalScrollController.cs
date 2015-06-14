﻿namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Layers;
    using Settings;

    using Microsoft.Xna.Framework;

    public class PointViewVerticalScrollController : ViewComponent, IPointViewVerticalScrollController
    {
        private int rowOffset;

        private PointViewVerticalSettings viewSettings;

        public PointViewVerticalScrollController(IView view)
            : base(view)
        {
        }

        protected PointViewVerticalSettings ViewSettings
        {
            get { return this.viewSettings; }
        }

        #region Display 

        protected virtual bool CanMoveUp
        {
            get { return this.RowOffset > this.RowOffsetMin; }
        }

        protected virtual bool CanMoveDown
        {
            get
            {
                return this.RowOffset < this.RowOffsetMax;
            }
        }

        public bool CanDisplay(int id)
        {
            var row = id;
            return this.RowOffset <= row && row < this.ViewSettings.ViewRowDisplay + this.RowOffset;
        }

        public virtual bool IsUpToDisplay(int id)
        {
            var row = id;
            return row < this.RowOffset - 1;
        }

        public virtual bool IsDownToDisplay(int id)
        {
            var row = id;
            return row > this.ViewSettings.ViewRowDisplay + this.RowOffset;
        }

        public virtual Vector2 Position(int row)
        {
            return new Vector2(
                this.ViewSettings.ViewPosition.X,
                this.ViewSettings.ViewDirection == ViewDirection.Normal
                    ? this.ViewSettings.ViewPosition.Y - (this.RowOffset * this.ViewSettings.ItemMargin.Y) + row * this.ViewSettings.ItemMargin.Y
                    : this.ViewSettings.ViewPosition.Y + (this.RowOffset * this.ViewSettings.ItemMargin.Y) - row * this.ViewSettings.ItemMargin.Y);
        }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointViewVerticalLayer>();
            this.viewSettings = viewLayer.ViewSettings;
        }

        #endregion

        #region State

        public virtual int RowOffset
        {
            get { return this.rowOffset; }
            set
            {
                if (value < 0)
                {
                    this.rowOffset = 0;
                }
                else if (value >= this.RowOffsetMax)
                {
                    this.rowOffset = this.View.ItemsRead.Count - this.ViewSettings.ViewRowDisplay;
                }
                else
                {
                    this.rowOffset = value;
                }
            }
        }

        protected virtual int RowOffsetMax
        {
            get { return this.View.ItemsRead.Count - this.ViewSettings.ViewRowDisplay; }
        }

        protected virtual int RowOffsetMin
        {
            get { return 0; }
        }

        #endregion

        #region Operations

        public virtual void MoveUp()
        {
            if (this.CanMoveUp)
            {
                --this.RowOffset;
            }
        }

        public void MoveUpToTop()
        {
            this.RowOffset = 0;
        }

        public virtual void MoveDown()
        {
            if (this.CanMoveDown)
            {
                ++this.RowOffset;
            }
        }

        public virtual void Zoom(int id)
        {
            if (!this.CanDisplay(id))
            {
                while (this.IsUpToDisplay(id))
                {
                    this.MoveUp();
                }

                while (this.IsDownToDisplay(id))
                {
                    this.MoveDown();
                }
            }
        }

        #endregion
    }
}