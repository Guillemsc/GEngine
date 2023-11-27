using GEngine.Modules.Cameras.Objects;

namespace GEngine.Modules.Cameras.Interactors;

public interface ICamerasInteractor
{
    void SetActiveCamera2d(Camera2d camera);
    bool HasActiveCamera2d();
    void SetActiveCamera2dIfThereIsNone(Camera2d camera);
    void RemoveActiveCamera2dIfMatches(Camera2d camera);
}