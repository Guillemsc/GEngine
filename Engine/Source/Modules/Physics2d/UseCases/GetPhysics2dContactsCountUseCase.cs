using GEngine.Modules.Physics2d.Data;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class GetPhysics2dContactsCountUseCase
{
    readonly PhysicsContacts2dData _physics2dContactsData;

    public GetPhysics2dContactsCountUseCase(PhysicsContacts2dData physics2dContactsData)
    {
        _physics2dContactsData = physics2dContactsData;
    }

    public int Execute()
    {
        return _physics2dContactsData.StayContacts.Count;
    }
}