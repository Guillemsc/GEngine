using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.Data;

public sealed class PhysicsContacts2dData
{
    public List<PhysicsContact2d> BeginContacts { get; } = new();
    public HashSet<PhysicsContact2d> StayContacts { get; } = new();
    public List<PhysicsContact2d> EndContacts { get; } = new();
}