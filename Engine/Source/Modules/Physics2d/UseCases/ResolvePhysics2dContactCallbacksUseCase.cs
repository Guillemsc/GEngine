using GEngine.Modules.Physics2d.Data;
using GEngine.Modules.Physics2d.Objects;

namespace GEngine.Modules.Physics2d.UseCases;

public sealed class ResolvePhysics2dContactCallbacksUseCase
{
    readonly PhysicsContacts2dData _physicsContacts2dData;
    readonly PhysicsStep2dData _physicsStep2dData;

    public ResolvePhysics2dContactCallbacksUseCase(
        PhysicsContacts2dData physicsContacts2dData,
        PhysicsStep2dData physicsStep2dData
    )
    {
        _physicsContacts2dData = physicsContacts2dData;
        _physicsStep2dData = physicsStep2dData;
    }

    public void Execute()
    {
        if (!_physicsStep2dData.StepedThisFrame)
        {
            return;
        }
        
        foreach (PhysicsContact2d contact in _physicsContacts2dData.BeginContacts)
        {
            contact.PhysicsBody2dA.OnCollisionBegin?.Invoke(
                new PhysicsCollision2d(contact.PhysicsBody2dA, contact.PhysicsBody2dB)
            );
            contact.PhysicsBody2dB.OnCollisionBegin?.Invoke(
                new PhysicsCollision2d(contact.PhysicsBody2dB, contact.PhysicsBody2dA)
            );
            
            _physicsContacts2dData.StayContacts.Add(contact);
        }
        
        foreach (PhysicsContact2d contact in _physicsContacts2dData.StayContacts)
        {
            contact.PhysicsBody2dA.OnCollisionStay?.Invoke(
                new PhysicsCollision2d(contact.PhysicsBody2dA, contact.PhysicsBody2dB)
            );
            contact.PhysicsBody2dB.OnCollisionStay?.Invoke(
                new PhysicsCollision2d(contact.PhysicsBody2dB, contact.PhysicsBody2dA)
            );
        }
        
        foreach (PhysicsContact2d contact in _physicsContacts2dData.EndContacts)
        {
            _physicsContacts2dData.StayContacts.Remove(contact);
            
            contact.PhysicsBody2dA.OnCollisionEnd?.Invoke(
                new PhysicsCollision2d(contact.PhysicsBody2dA, contact.PhysicsBody2dB)
            );
            contact.PhysicsBody2dB.OnCollisionEnd?.Invoke(
                new PhysicsCollision2d(contact.PhysicsBody2dB, contact.PhysicsBody2dA)
            );
        }
        
        _physicsContacts2dData.BeginContacts.Clear();
        _physicsContacts2dData.EndContacts.Clear();
    }
}