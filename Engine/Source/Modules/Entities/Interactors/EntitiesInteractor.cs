using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Entities.UseCases;

namespace GEngine.Modules.Entities.Interactors;

public sealed class EntitiesInteractor : IEntitiesInteractor
{
    readonly CreateEntityUseCase _createEntityUseCase;
    readonly CreateEntityWithParentUseCase _createEntityWithParentUseCase;
    readonly DestroyAllActiveEntitiesUseCase _destroyAllActiveEntitiesUseCase;

    public EntitiesInteractor(
        CreateEntityUseCase createEntityUseCase, 
        CreateEntityWithParentUseCase createEntityWithParentUseCase,
        DestroyAllActiveEntitiesUseCase destroyAllActiveEntitiesUseCase
        )
    {
        _createEntityUseCase = createEntityUseCase;
        _destroyAllActiveEntitiesUseCase = destroyAllActiveEntitiesUseCase;
        _createEntityWithParentUseCase = createEntityWithParentUseCase;
    }

    public Entity Create(string name, bool active = true)
        => _createEntityUseCase.Execute(name);

    public Entity Create(string name, Entity parent, bool active = true)
        => _createEntityWithParentUseCase.Execute(name, parent, active);

    public void DestroyAllActive()
        => _destroyAllActiveEntitiesUseCase.Execute();
}