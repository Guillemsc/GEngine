using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Games.Core;
using Raylib_cs;

namespace GEngine.Editor.Examples.GameRunners
{
    public class Camera2dExampleGameRunner : GameRunner
    {
        Entity? _entity1;
        Entity? _camera;

        
        public Camera2dExampleGameRunner(IREngineInteractor engine) : base(engine)
        {
            
        }

        public override void Start()
        {
            _entity1 = Entities.Create("Entity");
            _camera = Entities.Create("Camera");

            CircleRenderer2dComponent circle1 = _entity1.AddComponent<CircleRenderer2dComponent>();
            circle1.SetColor(Color.PINK);
            circle1.SetRadius(10);
            circle1.SetSortingOrder(3);
            
            _camera.AddComponent<Camera2dComponent>();
        }

        public override void Tick()
        {
            Vector3 position = Vector3.Zero;
            
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) position.Y += 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) position.Y -= 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) position.X -= 2;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) position.X += 2;
            
            position *= (Framerate.GetFrameTimeSeconds() * 100);
            
            _camera!.Transform.AddLocalPosition(position);

            Camera2dComponent camera = _camera.GetComponentUnsafe<Camera2dComponent>();
            
            float zoom = 0f;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_Q)) zoom -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_E)) zoom += 1;
            
            zoom *= (Framerate.GetFrameTimeSeconds() * 1);
            
            camera.AddSize(zoom);

            // if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
            // {
            //     Engine.EntitiesOld.Destroy(entity2!);
            // }
            //
            // if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            // {
            //     Transforms.SetWorldRotation(_boxEntityData, new Vector3(0, 0, -30).ToQuaternionFromEuler());
            // }
            //
            // if (Raylib.IsKeyPressed(KeyboardKey.KEY_T))
            // {
            //     Transforms.SetWorldRotation(_boxEntityData, new Vector3(0, 0, 0).ToQuaternionFromEuler());
            // }
            //
            // if (cameraEntity!.TransformComponent.HasValue)
            // {
            //     TransformComponentOld transformComponentOld = cameraEntity!.TransformComponent.UnsafeGet();
            //     Vector2 position = Vector2.Zero;
            //
            //     if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) position.Y += 2;
            //     if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) position.Y -= 2;
            //     if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) position.X -= 2;
            //     if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) position.X += 2;
            //
            //     position *= (Framerate.GetFrameTimeSeconds() * 100);
            //     
            //     Transforms.SetWorldPosition(
            //         cameraEntity!.TransformComponent.UnsafeGet(),
            //         new Vector3(
            //             transformComponentOld.WorldPosition.X + position.X,
            //             transformComponentOld.WorldPosition.Y + position.Y,
            //             transformComponentOld.WorldPosition.Z
            //         )
            //     );
            //
            //     Camera2dComponentOld camera = Components.Get<Camera2dComponentOld>(cameraEntity).UnsafeGet();
            //
            //     float zoom = 0f;
            //     if (Raylib.IsKeyDown(KeyboardKey.KEY_Q)) zoom -= 1;
            //     if (Raylib.IsKeyDown(KeyboardKey.KEY_E)) zoom += 1;
            //     
            //     zoom *= (Framerate.GetFrameTimeSeconds() * 1);
            //     
            //     Camera2d.SetZoom(camera, camera.Zoom + zoom);
            // }
        }

        public override void Dispose()
        {
            
        }
    }
}
