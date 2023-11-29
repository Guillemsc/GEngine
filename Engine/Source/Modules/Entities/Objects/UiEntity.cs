using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;

namespace GEngine.Modules.Entities.Objects;

public sealed class UiEntity : BaseEntity<UiComponent>
{
    public RectUiComponent RectUi { get; }
    
    public UiEntity(
        IREngineInteractor engine, 
        Guid uid, 
        Action<BaseEntity<UiComponent>> refreshEntityOnSceneAction, 
        Action<BaseEntity<UiComponent>> refreshEntityOnActiveEntitiesListsAction) 
        : base(engine, uid, refreshEntityOnSceneAction, refreshEntityOnActiveEntitiesListsAction)
    {
        RectUi = AddComponent<RectUiComponent>();
    }
}