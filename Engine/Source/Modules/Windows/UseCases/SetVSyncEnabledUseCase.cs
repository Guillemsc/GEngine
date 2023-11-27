using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class SetVSyncEnabledUseCase
{
    public void Execute(bool enabled)
    {
        if (enabled)
        {
            bool wasFullscreen = Raylib.IsWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
            Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT);
            if (wasFullscreen)
            {
                Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
            }
        }
        else
        {
            Raylib.ClearWindowState(ConfigFlags.FLAG_VSYNC_HINT);
        }
    }
}