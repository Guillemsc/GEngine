using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class GetMSAAEnabledUseCase
{
    public bool Execute()
    {
        return Raylib.IsWindowState(ConfigFlags.FLAG_MSAA_4X_HINT);
    }
}