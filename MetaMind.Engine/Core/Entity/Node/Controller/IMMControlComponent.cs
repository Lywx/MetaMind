namespace MetaMind.Engine.Core.Entity.Control
{
    using Entity.Common;
    using System;

    // TODO: Messy
    public interface __IMMControlComponentBase :
        __IMMControlComponentBaseOrganizational,
        IMMInputtableEntity,
        IMMReactor,
        IMMFocusable 
    {
        
    }

    public interface __IMMControlComponentBaseOrganizational
    {
        MMControlCollection Children { get; }

        IMMControlComponent Parent { get; }

        IMMControlComponent Root { get; }

        bool IsChild { get; }

        bool IsParent { get; }

        bool IsRoot { get; }
    }

    public interface __IMMControlComponentBaseOrganizationalInternal : __IMMControlComponentBaseOrganizational
    {
        new MMControlCollection Children { get; set; }

        new IMMControlComponentInternal Parent { get; set; }

        new IMMControlComponentInternal Root { get; set;}
    }

    public interface __IMMControlComponentOperations 
    {
        void Add(IMMControlComponent component);

        void Remove(IMMControlComponent component);

        bool Contains(IMMControlComponent component, bool recursive);
    }

    public interface __IMMControlComponentOperationsInternal : __IMMControlComponentOperations
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponentInternal component);

        bool Contains(IMMControlComponentInternal component, bool recursive);
    }

    public interface IMMControlComponent : __IMMControlComponentBase, __IMMControlComponentOperations
    {
        IMMControlManager Manager { get; }

        event EventHandler FocusGained;

        event EventHandler FocusLost;
    }

    public interface IMMControlComponentInternal : __IMMControlComponentBaseOrganizationalInternal, __IMMControlComponentOperationsInternal, __IMMControlComponentBase
    {
        IMMControlManagerInternal Manager { get; set; }
    }
}