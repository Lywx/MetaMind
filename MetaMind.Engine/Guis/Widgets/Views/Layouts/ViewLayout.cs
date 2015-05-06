// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewLayout.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    public class PointView2DLayout : ViewComponent, IPointView2DLayout
    {
        private readonly PointView2DSettings viewSettings;

        public PointView2DLayout(IView view)
            : base(view)
        {
            this.viewSettings = this.ViewGetLayer<PointView2DLayer>().ViewSettings;
        }

        public int ColumnNum
        {
            get
            {
                return this.RowNum > 1 ? this.viewSettings.ColumnNumMax : this.View.Items.Count;
            }
        }

        public int RowNum
        {
            get
            {
                var lastId = this.View.Items.Count - 1;
                return this.RowFrom(lastId) + 1;
            }
        }

        public int ColumnFrom(int id)
        {
            return id % this.viewSettings.ColumnNumMax;
        }

        public int IdFrom(int i, int j)
        {
            return i * this.viewSettings.ColumnNumMax + j;
        }

        public int RowFrom(int id)
        {
            for (var row = 0; row < this.viewSettings.RowNumMax; row++)
            {
                if (id - row * this.viewSettings.ColumnNumMax >= 0)
                {
                    continue;
                }

                return row - 1;
            }

            return this.viewSettings.RowNumMax - 1;
        }

        public void Sort(Func<IViewItem, dynamic> key)
        {
            this.View.Items = this.View.Items.OrderBy(key).ToList();
            this.View.Items.ForEach(item => item.ItemLogic.ItemLayout.Id = this.View.Items.IndexOf(item));
        }
    }
}