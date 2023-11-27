using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;
using Raylib_cs;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Components.Objects;

public sealed class CircleRenderer2dComponent : Component
{
    public int SortingOrder { get; private set; }

    public Vector2 Offset { get; private set; }
    public float Radius { get; private set; } = 10f;
    public Color Color { get; private set; } = Color.PINK;
    
    public CircleRenderer2dComponent(IREngineInteractor engine, Guid uid, Entity owner) : base(engine, uid, owner)
    {
        
    }

    public override void Tick()
    {
        void Render()
        {
            TransformComponent transform = Owner.Transform;
            
            Vector2 finalPosition = new(
                transform.WorldPosition.X + Offset.X,
                -transform.WorldPosition.Y - Offset.Y
            );

            Vector2 finalRadius = new Vector2(Radius * transform.WorldScale.X, Radius * transform.WorldScale.Y);
                
            Raylib.DrawEllipse(
                (int)finalPosition.X, 
                (int)finalPosition.Y,
                finalRadius.X,
                finalRadius.Y,
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
    
    public void SetRadius(float radius)
    {
        Radius = radius;
    }
    
    public void SetColor(Color color)
    {
        Color = color;
    }
}