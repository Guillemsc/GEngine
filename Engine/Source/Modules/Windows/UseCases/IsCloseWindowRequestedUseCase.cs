using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class IsCloseWindowRequestedUseCase
{
    public bool Execute()
    {
        return Raylib.WindowShouldClose();
    }
}