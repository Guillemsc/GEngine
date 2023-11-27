using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Games.Core;
using Raylib_cs;

namespace GEngine.Editor.Examples.GameRunners
{
    public class TransformPositionExampleGameRunner : GameRunner
    {
        Entity? _entity1;
        Entity? _entity2;
        Entity? _entity3;

        
        public TransformPositionExampleGameRunner(IREngineInteractor engine) : base(engine)
        {
            
        }

        public override void Start()
        {
            _entity1 = Entities.Create("1");
            _entity2 = Entities.Create("2");
            _entity3 = Entities.Create("3");

            CircleRenderer2dComponent circle1 = _entity1.AddComponent<CircleRenderer2dComponent>();
            circle1.SetColor(Color.PINK);
            circle1.SetRadius(10);
            circle1.SetSortingOrder(3);
            
            CircleRenderer2dComponent circle2 = _entity2.AddComponent<CircleRenderer2dComponent>();
            circle2.SetColor(Color.GOLD);
            circle2.SetRadius(20);
            circle2.SetSortingOrder(2);
            
            CircleRenderer2dComponent circle3 = _entity3.AddComponent<CircleRenderer2dComponent>();
            circle3.SetColor(Color.BLUE);
            circle3.SetRadius(30);
            circle3.SetSortingOrder(1);
            
            _entity1.AddChild(_entity2);
            _entity2.AddChild(_entity3);
        }

        public override void Tick()
        {
            Vector3 position = Vector3.Zero;
            
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) position.Y += 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) position.Y -= 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) position.X -= 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) position.X += 2;
            
            position *= (Framerate.GetFrameTimeSeconds() * 100);
            
            _entity1!.Transform.AddLocalPosition(position);
        }

        public override void Dispose()
        {
            
        }
    }
}
