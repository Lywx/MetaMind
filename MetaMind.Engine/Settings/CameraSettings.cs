using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMind.Engine.Settings
{
    public class CameraSettings
    {
        //---------------------------------------------------------------------
        public float PanVelocity        = 10f;
        public int   PanRegionWidth     = 50;
        public int   PanForbiddenHeight = 50;

        public static CameraSettings Default
        {
            get { return new CameraSettings(); }
        }
    }
}