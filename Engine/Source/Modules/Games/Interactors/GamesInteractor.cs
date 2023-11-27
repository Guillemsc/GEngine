using GEngine.Modules.Games.Core;
using GEngine.Modules.Games.UseCases;

namespace GEngine.Modules.Games.Interactors;

public sealed class GamesInteractor : IGamesInteractor
{
    readonly LoadGameUseCase _loadGameUseCase;
    readonly UnloadCurrentGameUseCase _unloadCurrentGameUseCase;

    public GamesInteractor(
        LoadGameUseCase loadGameUseCase, 
        UnloadCurrentGameUseCase unloadCurrentGameUseCase
        )
    {
        _loadGameUseCase = loadGameUseCase;
        _unloadCurrentGameUseCase = unloadCurrentGameUseCase;
    }

    public void Load(GameRunner gameRunner)
    {
        _loadGameUseCase.Execute(gameRunner);
    }

    public void UnloadCurrent()
    {
        _unloadCurrentGameUseCase.Execute();
    }
}