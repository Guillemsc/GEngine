using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class SetWindowResizableEnabledUseCase
{
    public void Execute(bool enabled)
    {
        if (enabled)
        {
            Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);   
        }
        else
        {
            Raylib.ClearWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        }
    }
}