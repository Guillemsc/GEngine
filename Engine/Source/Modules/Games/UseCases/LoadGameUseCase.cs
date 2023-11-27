using GEngine.Modules.Games.Core;
using GEngine.Modules.Games.Data;
using GEngine.Modules.Tickables.UseCases;

namespace GEngine.Modules.Games.UseCases;

public sealed class LoadGameUseCase
{
    readonly GamesData _gamesData;
    readonly UnloadCurrentGameUseCase _unloadCurrentGameUseCase;
    readonly AddTickableUseCase _addTickableUseCase;

    public LoadGameUseCase(
        GamesData gamesData, 
        UnloadCurrentGameUseCase unloadCurrentGameUseCase,
        AddTickableUseCase addTickableUseCase
    )
    {
        _gamesData = gamesData;
        _unloadCurrentGameUseCase = unloadCurrentGameUseCase;
        _addTickableUseCase = addTickableUseCase;
    }

    public void Execute(GameRunner gameRunner)
    {
        _unloadCurrentGameUseCase.Execute();

        _gamesData.CurrentGameRunner = gameRunner;
        
        gameRunner.Start();
        
        _addTickableUseCase.Execute(gameRunner);
    }
}