using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class CreateWorldEntityUseCase
{
    readonly Lazy<IREngineInteractor> _rEngineInteractor;
    readonly RefreshEntityOnSceneUseCase _refreshEntityOnSceneUseCase;
    readonly RefreshEntityOnActiveEntitiesListsUseCase _refreshEntityOnActiveEntitiesListsUseCase;

    public CreateWorldEntityUseCase(
        Lazy<IREngineInteractor> rEngineInteractor, 
        RefreshEntityOnSceneUseCase refreshEntityOnSceneUseCase,
        RefreshEntityOnActiveEntitiesListsUseCase refreshEntityOnActiveEntitiesListsUseCase
        )
    {
        _rEngineInteractor = rEngineInteractor;
        _refreshEntityOnSceneUseCase = refreshEntityOnSceneUseCase;
        _refreshEntityOnActiveEntitiesListsUseCase = refreshEntityOnActiveEntitiesListsUseCase;
    }

    public WorldEntity Execute(string name, bool active = true)
    {
        Guid uid = Guid.NewGuid();

        WorldEntity entity = new(
            _rEngineInteractor.Value,
            uid,
            _refreshEntityOnSceneUseCase.Execute,
            _refreshEntityOnActiveEntitiesListsUseCase.Execute
        )
        {
            Name = name
        };
        
        entity.AddToScene();

        if (active)
        {
            entity.SetActive(true);
        }

        return entity;
    }
}