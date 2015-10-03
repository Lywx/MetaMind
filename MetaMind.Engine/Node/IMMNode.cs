namespace MetaMind.Engine.Node
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Gui.Controls;

    public interface IMMNode : IMMNodeOrganization, IMMInputable, ICCUpdatable, IMMFocusable, IComparer<MMNode>, IComparable<MMNode>, IMMEntity
    {
        IMMRenderOpacity Opacity { get; set; }

        IMMNodeColor Color { get; set;}
    }

    public interface IMMNodeOrganization
    {
        IMMNode Parent { get; } 

        MMNodeCollection Children { get; }
    }

    public interface IMMNodeInternal
    {
        IMMNodeVisual Visual { get; }

        IMMNodeControl Control { get; }
    }

    public interface IMMNodeControl : IMMControlComponent
    {
        
    }
}