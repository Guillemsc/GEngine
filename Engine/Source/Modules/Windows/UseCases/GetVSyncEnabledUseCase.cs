using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class GetVSyncEnabledUseCase
{
    public bool Execute()
    {
        return Raylib.IsWindowState(ConfigFlags.FLAG_VSYNC_HINT);
    }
}