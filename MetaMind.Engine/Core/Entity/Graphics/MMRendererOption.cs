using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Core.Entity.Graphics
{
    using Backend.Graphics;

    public class MMRendererOption
    {
        public bool CascadedEnabled { get; set; } = false;

        public CCClipMode ClipMode { get; set; } = CCClipMode.None;

    }
}
