using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class GetWindowResizableEnabledUseCase
{
    public bool Execute()
    {
        return Raylib.IsWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
    }
}