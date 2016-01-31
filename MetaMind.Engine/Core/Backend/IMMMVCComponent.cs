namespace MetaMind.Engine.Core.Backend
{
    public interface IMMMVCComponent<out TMVCSettings, out TMVCController, out TMVCRenderer> : IMMGeneralComponent
        where                            TMVCController                                      : IMMMVCComponentController<TMVCSettings> 
        where                            TMVCRenderer                                        : IMMMVCComponentRenderer<TMVCSettings>
    {
        TMVCSettings Settings { get; }

        TMVCController Controller { get; }

        TMVCRenderer Renderer { get; }
    }
}