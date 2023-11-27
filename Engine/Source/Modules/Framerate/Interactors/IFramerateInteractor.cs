namespace GEngine.Modules.Framerate.Interactors;

public interface IFramerateInteractor
{
    int GetFps();
    int GetSecondAverageFps();
    float GetFrameTimeSeconds();
}