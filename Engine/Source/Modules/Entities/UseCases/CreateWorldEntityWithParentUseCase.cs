using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class CreateWorldEntityWithParentUseCase
{
    readonly CreateWorldEntityUseCase _createWorldEntityUseCase;

    public CreateWorldEntityWithParentUseCase(
        CreateWorldEntityUseCase createWorldEntityUseCase
        )
    {
        _createWorldEntityUseCase = createWorldEntityUseCase;
    }

    public WorldEntity Execute(string name, WorldEntity parentEntity, bool active = true)
    {
        WorldEntity _entity = _createWorldEntityUseCase.Execute(name, active);
        _entity.SetParent(parentEntity);
        return _entity;
    }
}