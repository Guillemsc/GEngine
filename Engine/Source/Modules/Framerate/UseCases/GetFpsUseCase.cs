using Raylib_cs;

namespace GEngine.Modules.Framerate.UseCases;

public sealed class GetFpsUseCase
{
    public int Execute()
    {
        return Raylib.GetFPS();
    }
}