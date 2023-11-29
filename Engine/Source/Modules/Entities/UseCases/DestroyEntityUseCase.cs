using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class DestroyEntityUseCase
{
    public void Execute(IEntity entity)
    {
        entity.SetActive(false);

        List<IEntity> entitiesToDestroy = entity.GetChildEntitiesOnHierarchy(false).ToList();

        foreach (IEntity entityToDestroy in entitiesToDestroy)
        {
            DestroyEntity(entityToDestroy);
        }

        DestroyEntity(entity);
    }
    
    void DestroyEntity(IEntity entityToDestroy)
    {
        entityToDestroy.Name = $"{entityToDestroy.Name} [Destroyed]";
        entityToDestroy.RemoveAllComponents();
        entityToDestroy.RemoveParent();
        entityToDestroy.RemoveAllChilds();
        entityToDestroy.RemoveFromScene();
    }
}