using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class CreateUiEntityUseCase
{
    readonly Lazy<IREngineInteractor> _rEngineInteractor;
    readonly RefreshEntityOnSceneUseCase _refreshEntityOnSceneUseCase;
    readonly RefreshEntityOnActiveEntitiesListsUseCase _refreshEntityOnActiveEntitiesListsUseCase;

    public CreateUiEntityUseCase(
        Lazy<IREngineInteractor> rEngineInteractor, 
        RefreshEntityOnSceneUseCase refreshEntityOnSceneUseCase,
        RefreshEntityOnActiveEntitiesListsUseCase refreshEntityOnActiveEntitiesListsUseCase
    )
    {
        _rEngineInteractor = rEngineInteractor;
        _refreshEntityOnSceneUseCase = refreshEntityOnSceneUseCase;
        _refreshEntityOnActiveEntitiesListsUseCase = refreshEntityOnActiveEntitiesListsUseCase;
    }

    public UiEntity Execute(string name, bool active = true)
    {
        Guid uid = Guid.NewGuid();

        UiEntity entity = new(
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