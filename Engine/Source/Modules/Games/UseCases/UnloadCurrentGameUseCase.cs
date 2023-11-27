using GEngine.Modules.Entities.UseCases;
using GEngine.Modules.Games.Core;
using GEngine.Modules.Games.Data;
using GEngine.Modules.Tickables.UseCases;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Games.UseCases;

public sealed class UnloadCurrentGameUseCase
{
    readonly GamesData _gamesData;
    readonly RemoveAllTickablesUseCase _removeAllTickablesUseCase;
    readonly DestroyAllActiveEntitiesUseCase _destroyAllActiveEntitiesUseCase;

    public UnloadCurrentGameUseCase(
        GamesData gamesData,
        RemoveAllTickablesUseCase removeAllTickablesUseCase,
        DestroyAllActiveEntitiesUseCase destroyAllActiveEntitiesUseCase
    )
    {
        _gamesData = gamesData;
        _removeAllTickablesUseCase = removeAllTickablesUseCase;
        _destroyAllActiveEntitiesUseCase = destroyAllActiveEntitiesUseCase;
    }

    public void Execute()
    {
        bool hasCurrentExample = _gamesData.CurrentGameRunner.TryGet(out GameRunner exampleRunner);

        if (!hasCurrentExample)
        {
            return;
        }
        
        _gamesData.CurrentGameRunner = Optional<GameRunner>.None;
        
        _removeAllTickablesUseCase.Execute();
        _destroyAllActiveEntitiesUseCase.Execute();
    }
}