namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Components.Inputs;

    public interface IGameEngine
    {
        AudioManager AudioManager { get; }

        FolderManager Folder { get; }

        FontManager FontManager { get; }

        StringDrawer StringDrawer { get; set; }

        GraphicsManager GraphicsManager { get; }

        GraphicsSettings GraphicsSettings { get; set; }

        MessageDrawer MessageDrawer { get; }

        ScreenManager Screen { get; }

        InputEvent InputEvent { get; }

        InputState InputState { get; }

        EventManager Event { get; }

        ProcessManager Process { get; }

        GameManager Games { get; }
    }
}