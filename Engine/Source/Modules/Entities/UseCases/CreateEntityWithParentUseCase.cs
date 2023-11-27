using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class CreateEntityWithParentUseCase
{
    readonly CreateEntityUseCase _createEntityUseCase;

    public CreateEntityWithParentUseCase(
        CreateEntityUseCase createEntityUseCase
        )
    {
        _createEntityUseCase = createEntityUseCase;
    }

    public Entity Execute(string name, Entity parentEntity, bool active = true)
    {
        Entity _entity = _createEntityUseCase.Execute(name, active);
        _entity.SetParent(parentEntity);
        return _entity;
    }
}