using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Games.Core;
using GEngine.Modules.Physics2d.Enums;
using Raylib_cs;

namespace GEngine.Editor.Examples.GameRunners;

public sealed class TransformScaleExampleGameRunner : GameRunner
{
    Entity? _box1Entity;
    Entity? _box2Entity;
    Entity? _box3Entity;
    
    public TransformScaleExampleGameRunner(IREngineInteractor engine) : base(engine)
    {
    }

    public override void Start()
    {
        _box1Entity = Entities.Create("Box1");
        _box1Entity.Transform.SetWorldPosition(new Vector3(-100, 0, 0));
        _box1Entity.Transform.SetLocalScale(new Vector3(1, 1, 1));
        BoxRenderer2dComponent box1 = _box1Entity.AddComponent<BoxRenderer2dComponent>();
        PhysicsBody2dComponent pb1 = _box1Entity.AddComponent<PhysicsBody2dComponent>();
        pb1.CreateBox(new Vector2(20, 20));
        pb1.SetType(PhysicsBody2dType.Kinematic);
        box1.SetSortingOrder(2);
        
        _box2Entity = Entities.Create("Box2");
        _box2Entity.SetParent(_box1Entity);
        _box2Entity.Transform.SetWorldPosition(new Vector3(0, 0, 0));
        _box2Entity.Transform.SetLocalScale(new Vector3(2, 2, 1));
        BoxRenderer2dComponent box2 = _box2Entity.AddComponent<BoxRenderer2dComponent>();
        box2.SetColor(Color.GOLD);
        PhysicsBody2dComponent pb2 = _box2Entity.AddComponent<PhysicsBody2dComponent>();
        pb2.CreateBox(new Vector2(20, 20));
        pb2.SetType(PhysicsBody2dType.Kinematic);
        
        _box3Entity = Entities.Create("Box3");
        _box3Entity.SetParent(_box2Entity);
        _box3Entity.Transform.SetWorldPosition(new Vector3(100, 0, 0));
        _box3Entity.Transform.SetLocalScale(new Vector3(3, 3, 1));
        BoxRenderer2dComponent box3 = _box3Entity.AddComponent<BoxRenderer2dComponent>();
        box3.SetColor(Color.BLUE);
        PhysicsBody2dComponent pb3 = _box3Entity.AddComponent<PhysicsBody2dComponent>();
        pb3.CreateBox(new Vector2(20, 20));
        pb3.SetType(PhysicsBody2dType.Kinematic);
    }

    public override void Tick()
    {
        float scaleChange = 0;
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_Q))
        {
            scaleChange -= 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_E))
        {
            scaleChange += 1;
        }

        scaleChange *= Framerate.GetFrameTimeSeconds() * 2f;
        
        _box1Entity!.Transform.AddLocalScale(new Vector3(scaleChange, scaleChange, 0));
    }

    public override void Dispose()
    {
        
    }
}