using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Entities.UseCases;

namespace GEngine.Modules.Entities.Interactors;

public sealed class EntitiesInteractor : IEntitiesInteractor
{
    readonly CreateWorldEntityUseCase _createWorldEntityUseCase;
    readonly CreateWorldEntityWithParentUseCase _createWorldEntityWithParentUseCase;
    readonly CreateUiEntityUseCase _createUiEntityUseCase;
    readonly DestroyAllActiveEntitiesUseCase _destroyAllActiveEntitiesUseCase;

    public EntitiesInteractor(
        CreateWorldEntityUseCase createWorldEntityUseCase, 
        CreateWorldEntityWithParentUseCase createWorldEntityWithParentUseCase,
        CreateUiEntityUseCase createUiEntityUseCase,
        DestroyAllActiveEntitiesUseCase destroyAllActiveEntitiesUseCase
        )
    {
        _createWorldEntityUseCase = createWorldEntityUseCase;
        _destroyAllActiveEntitiesUseCase = destroyAllActiveEntitiesUseCase;
        _createUiEntityUseCase = createUiEntityUseCase;
        _createWorldEntityWithParentUseCase = createWorldEntityWithParentUseCase;
    }

    public WorldEntity CreateWorld(string name, bool active = true)
        => _createWorldEntityUseCase.Execute(name);

    public WorldEntity CreateWorld(string name, WorldEntity parent, bool active = true)
        => _createWorldEntityWithParentUseCase.Execute(name, parent, active);

    public UiEntity CreateUi(string name, bool active = true)
        => _createUiEntityUseCase.Execute(name, active);

    public void DestroyAllActive()
        => _destroyAllActiveEntitiesUseCase.Execute();
}