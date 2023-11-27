using GEngine.Modules.Games.Core;

namespace GEngine.Modules.Games.Interactors;

public interface IGamesInteractor
{
    void Load(GameRunner gameRunner);
    void UnloadCurrent();
}