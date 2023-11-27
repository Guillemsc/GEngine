using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class SetWindowFullscreenEnabledUseCase
{
    public void Execute(bool enabled)
    {
        if (enabled)
        {
            Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);   
        }
        else
        {
            Raylib.ClearWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
        }
    }
}