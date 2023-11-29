using GEngine.Modules.Entities.Objects;

namespace GEngine.Modules.Entities.Interactors;

public interface IEntitiesInteractor
{
    WorldEntity CreateWorld(string name, bool active = true);
    WorldEntity CreateWorld(string name, WorldEntity parent, bool active = true);

    UiEntity CreateUi(string name, bool active = true);
    
    void DestroyAllActive();
}