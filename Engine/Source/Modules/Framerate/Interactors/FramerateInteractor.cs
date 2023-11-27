using GEngine.Modules.Framerate.UseCases;

namespace GEngine.Modules.Framerate.Interactors;

public sealed class FramerateInteractor : IFramerateInteractor
{
    readonly GetFpsUseCase _getFpsUseCase;
    readonly GetSecondAverageFpsUseCase _getSecondAverageFpsUseCase;
    readonly GetFrameTimeSecondsUseCase _getFrameTimeSecondsUseCase;

    public FramerateInteractor(
        GetFpsUseCase getFpsUseCase,
        GetSecondAverageFpsUseCase getSecondAverageFpsUseCase,
        GetFrameTimeSecondsUseCase getFrameTimeSecondsUseCase
        )
    {
        _getFpsUseCase = getFpsUseCase;
        _getSecondAverageFpsUseCase = getSecondAverageFpsUseCase;
        _getFrameTimeSecondsUseCase = getFrameTimeSecondsUseCase;
    }

    public int GetFps()
    {
        return _getFpsUseCase.Execute();
    }

    public int GetSecondAverageFps()
    {
        return _getSecondAverageFpsUseCase.Execute();
    }

    public float GetFrameTimeSeconds()
    {
        return _getFrameTimeSecondsUseCase.Execute();
    }
}