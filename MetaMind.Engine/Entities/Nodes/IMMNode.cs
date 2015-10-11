namespace MetaMind.Engine.Entities.Nodes
{
    using System;
    using System.Collections.Generic;
    using Entities.Graphics;

    public interface IMMNode : IMMNodeOrganization, IMMInputEntity, ICCUpdatable, IMMFocusable, IComparer<MMNode>, IComparable<MMNode>
    {
        IMMRenderOpacity Opacity { get; }

        IMMNodeColor Color { get; }
    }
 
    public interface IMMNodeInternal : IMMNodeOrganizationInternal 
    {
        IMMNodeRenderer Renderer { get; }

        IMMNodeController Controller { get; }
    }
}