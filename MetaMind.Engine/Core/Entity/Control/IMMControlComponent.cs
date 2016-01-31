namespace MetaMind.Engine.Core.Entity.Control
{
    using System;
    using Entity.Common;

    public interface IMMControlComponentBase : IMMReactor, IMMInputtableEntity, IMMFocusable  
    {
        
    }

    public interface IMMControlComponentOrganization
    {
        MMControlCollection Children { get; }

        IMMControlComponent Parent { get; }

        IMMControlComponent Root { get; }

        bool IsChild { get; }

        bool IsParent { get; }

        bool IsRoot { get; }
    }

    public interface IMMControlComponentOrganizationInternal
    {
        MMControlCollection Children { get; set; }

        IMMControlComponentInternal Parent { get; set; }

        IMMControlComponentInternal Root { get; set;}
    }

    public interface IMMControlComponentOperations 
    {
        void Add(IMMControlComponent component);

        void Remove(IMMControlComponent component);

        bool Contains(IMMControlComponent component, bool recursive);
    }

    public interface IMMControlComponentOperationsInternal
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponentInternal component);

        bool Contains(IMMControlComponentInternal component, bool recursive);
    }

    public interface IMMControlComponent : IMMControlComponentBase, IMMControlComponentOrganization, IMMControlComponentOperations
    {
        IMMControlManager Manager { get; }

        event EventHandler FocusGained;

        event EventHandler FocusLost;
    }

    public interface IMMControlComponentInternal : IMMControlComponentOrganizationInternal, IMMControlComponentOperationsInternal, IMMControlComponentBase
    {
        IMMControlManagerInternal Manager { get; set; }
    }
}