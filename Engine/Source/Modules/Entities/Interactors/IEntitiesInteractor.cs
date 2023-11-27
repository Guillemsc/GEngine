using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.Interactors;

public interface IEntitiesInteractor
{
    Entity Create(string name, bool active = true);
    Entity Create(string name, Entity parent, bool active = true);
    void DestroyAllActive();
}