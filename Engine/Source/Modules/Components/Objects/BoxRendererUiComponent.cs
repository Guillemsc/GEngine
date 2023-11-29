using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Objects;
using Raylib_cs;

namespace GEngine.Modules.Components.Objects;

public sealed class BoxRendererUiComponent : UiComponent
{
    public int SortingOrder { get; private set; }
    public Color Color { get; private set; } = Color.PINK;
    
    public BoxRendererUiComponent(IREngineInteractor engine, Guid uid, BaseEntity<UiComponent> owner) : base(engine, uid, owner)
    {
    }
    
    public override void Tick()
    {
        void Render(float scale)
        {
            Rectangle uiRectangle = Owner.RectUi.Rectangle;
            
            Rectangle rectangle = new(
                uiRectangle.X,
                -uiRectangle.Y,
                uiRectangle.Width,
                uiRectangle.Height
            );

            Vector2 size = new(uiRectangle.Width, uiRectangle.Height);

            Vector2 origin = size * 0.5f;
        
            Raylib.DrawRectanglePro(
                rectangle,
                origin, 
                -0,
                Color
            );
        }
        
        Engine.UiRenderer.AddToUiRenderingQueue(SortingOrder, Render);
    }

    public void SetSortingOrder(int sortingOrder)
    {
        SortingOrder = sortingOrder;
    }
    
    public void SetColor(Color color)
    {
        Color = color;
    }
}