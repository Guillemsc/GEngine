using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;
using Raylib_cs;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Components.Objects;

public sealed class BoxRenderer2dComponent : Component
{
    public int SortingOrder { get; private set; }

    public Vector2 Offset { get; private set; }
    public Vector2 Size { get; private set; } = new(20, 20);
    public Color Color { get; private set; } = Color.PINK;
    
    public BoxRenderer2dComponent(IREngineInteractor engine, Guid uid, Entity owner) : base(engine, uid, owner)
    {
    }
    
    public override void Tick()
    {
        void Render()
        {
            TransformComponent transform = Owner.Transform;
            
            Vector2 size = Size * transform.WorldScale.ToVector2XY();
            Vector2 halfSize = size * 0.5f;
            
            Rectangle rectangle = new(
                transform.WorldPosition.X + Offset.X,
                -transform.WorldPosition.Y - Offset.Y,
                size.X,
                size.Y
            );

            float zRotation = transform.WorldRotationRadiants.Z.ToDegrees();
                
            Raylib.DrawRectanglePro(
                rectangle,
                halfSize, 
                -zRotation,
                Color
            );
        }
        
        Engine.Rendering.AddToRendering2dQueue(SortingOrder, Render);
    }

    public void SetSortingOrder(int sortingOrder)
    {
        SortingOrder = sortingOrder;
    }
    
    public void SetOffset(Vector2 offset)
    {
        Offset = offset;
    }
    
    public void SetSize(Vector2 size)
    {
        Size = size;
    }
    
    public void SetColor(Color color)
    {
        Color = color;
    }
}