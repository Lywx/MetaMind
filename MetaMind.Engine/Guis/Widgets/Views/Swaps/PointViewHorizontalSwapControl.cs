using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    public class PointViewHorizontalSwapControl : ViewSwapControl ,IPointViewHorizontalSwapControl
    {
        public PointViewHorizontalSwapControl(IView view)
            : base(view)
        {
        }
    }
}
