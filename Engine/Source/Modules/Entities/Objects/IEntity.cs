using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Entities.Objects;

public interface IEntity : ITickable
{
    Guid Uid { get; }
    string Name { get; set; }
    
    bool IsOnScene { get; }
    bool IsActiveSelf { get; }
    bool IsActiveInHierachy { get; }
    
    bool HasParent { get; }

    void SetActive(bool active);
    
    void RemoveAllComponents();
    bool RemoveParent();
    void RemoveAllChilds();
    void RemoveFromScene();

    public IEnumerable<IEntity> GetChildEntitiesOnHierarchy(bool includeCurrent);
}