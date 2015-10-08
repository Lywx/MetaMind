namespace MetaMind.Engine.Gui.Controls
{
    using System;
    using Entities;

    public interface IMMControlComponent : IMMControlComponentOrganization, IMMControlComponentOperations, IMMFocusable, IMMReactor, IMMEntity
    {
        IMMControlManager Manager { get; }

        event EventHandler FocusGained;

        event EventHandler FocusLost;
    }

    public interface IMMControlComponentInternal : IMMControlComponentOrganizationInternal, IMMControlComponentOperationsInternal, IMMFocusable, IMMEntity
    {
        IMMControlManagerInternal Manager { get; set; }
    }
}