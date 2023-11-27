using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class GetWindowFullscreenEnabledUseCase
{
    public bool Execute()
    {
        return Raylib.IsWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
    }
}