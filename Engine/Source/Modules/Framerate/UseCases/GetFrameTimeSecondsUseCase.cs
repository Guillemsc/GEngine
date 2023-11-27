using Raylib_cs;

namespace GEngine.Modules.Framerate.UseCases;

public sealed class GetFrameTimeSecondsUseCase
{
    public float Execute()
    {
        return Raylib.GetFrameTime();
    }
}