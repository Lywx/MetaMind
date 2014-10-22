using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Items
{
    public enum ItemFrameType
    {
        //------------------------------------------------------------------
        RootFrame,
        NameFrame,

        //------------------------------------------------------------------
        TypeNum,
    }

    public static class ItemFrameTypeExtensions
    {
        public static void SetIn( this ItemFrameType state, Point[ ] sizes, Point size )
        {
            sizes[ ( int ) state ] = size;
        }

        public static Point GetFrom( this ItemFrameType state, Point[ ] sizes )
        {
            return sizes[ ( int ) state ];
        }
    }
}