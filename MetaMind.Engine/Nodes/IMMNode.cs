namespace MetaMind.Engine.Nodes
{
    using System;
    using System.Collections.Generic;
    using Entities;

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