using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.UseCases;

public sealed class DestroyEntityUseCase
{
    public void Execute(Entity entity)
    {
        entity.SetActive(false);

        List<Entity> entitiesToDestroy = entity.GetChildEntitiesOnHierarchy(false).ToList();

        foreach (Entity entityToDestroy in entitiesToDestroy)
        {
            DestroyEntity(entityToDestroy);
        }

        DestroyEntity(entity);
    }
    
    void DestroyEntity(Entity entityToDestroy)
    {
        entityToDestroy.Name = $"{entityToDestroy.Name} [Destroyed]";
        entityToDestroy.RemoveAllComponents();
        entityToDestroy.RemoveParent();
        entityToDestroy.RemoveAllChilds();
        entityToDestroy.RemoveFromScene();
    }
}