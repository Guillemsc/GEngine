using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Interfaces;
using GEngine.Modules.Entities.Objects;
using GEngine.Utils.Extensions;
using GEngine.Utils.Optionals;
using Raylib_cs;

namespace GEngine.Modules.Components.Objects;

public sealed class RectUiComponent : UiComponent
{
    bool _actsAsRoot;
    
    Vector2 HalfSizeDelta => SizeDelta * 0.5f;
    
    public Vector2 AnchorMax { get; private set; } = new(0.5f, 0.5f);
    public Vector2 AnchorMin { get; private set; } = new(0.5f, 0.5f);
    
    public Vector2 Pivot { get; private set; } = new(0.5f, 0.5f);
    public Vector2 AnchoredPosition { get; private set; }
    public Vector2 SizeDelta { get; private set; } = new(20, 20);
    
    public Rectangle Rectangle { get; private set; }
    public Vector2 RectangleMin { get; private set; }
    public Vector2 RectangleMax { get; private set; }
    
    
    public RectUiComponent(IREngineInteractor engine, Guid uid, BaseEntity<UiComponent> owner) : base(engine, uid, owner)
    {
        
    }

    public override void Init()
    {
        RefreshRectangle();
    }

    public override void Enable()
    {
        RefreshActsAsRootState();
    }
    
    public override void Disable()
    {
        RefreshActsAsRootState();
    }

    public override void ParentChanged()
    {
        RefreshActsAsRootState();
        RecalculateChildHierearchyRectangleValues();
    }
    
    public void SetAnchoredPosition(Vector2 anchoredPosition)
    {
        bool isSameAnchoredPosition = anchoredPosition == AnchoredPosition;

        if (isSameAnchoredPosition)
        {
            return;
        }
        
        AnchoredPosition = anchoredPosition;
        
        RecalculateChildHierearchyRectangleValues();
    }
    
    public void SetSizeDelta(Vector2 sizeDelta)
    {
        SizeDelta = sizeDelta;
        
        RecalculateChildHierearchyRectangleValues();
    }

    public void AddSizeDelta(Vector2 sizeDelta)
    {
        SetSizeDelta(SizeDelta + sizeDelta);
    }

    public void SetAnchorMinMax(Vector2 min, Vector2 max)
    {
        AnchorMin = min;
        AnchorMax = max;
        
        RefreshRectangle();
    }

    public void SetPivot(Vector2 pivot)
    {
        Pivot = pivot;
        
        RefreshRectangle();
    }

    public void SetSizeDeltaExpand()
    {
        SetSizeDelta(Vector2.Zero);
    }
    
    public void SetAnchorExpand()
    {
        SetAnchorMinMax(Vector2.Zero, Vector2.One);
    }

    public void SetAnchorBottomRight()
    {
        SetAnchorMinMax(Vector2.Zero, Vector2.Zero);
    }
    
    public void SetAnchorTopLeft()
    {
        SetAnchorMinMax(new Vector2(0, 1), new Vector2(0, 1));
    }

    public void SetAnchorExpandBottom()
    {
        SetAnchorMinMax(Vector2.Zero, new Vector2(1, 0));
    }

    public void SetPivotBottomRight()
    {
        SetPivot(Vector2.Zero);
    }
    
    public void SetPivotBottomCenter()
    {
        SetPivot(new Vector2(0.5f, 0f));
    }

    public Vector2 GetWorldPosition2d()
    {
        Rectangle rectangle = Rectangle;
        Vector2 worldPosition = Engine.UiRenderer.GetWorldPosition2dFromUiPosition(new Vector2(rectangle.X, rectangle.Y));
        return worldPosition;
    }

    public void SetWorldPosition2d(Vector2 position)
    {
        Vector2 uiPosition = Engine.UiRenderer.GetUiPositionFromWorldPosition2d(position);

        SetAnchoredPositionFromUiPosition(uiPosition);
    }
    
    void RecalculateChildHierearchyRectangleValues()
    {
        IEnumerable<IEntity> childEntities = Owner.GetChildEntitiesOnHierarchy(true);

        foreach (IEntity childEntity in childEntities)
        {
            UiEntity castedChildEntity = (UiEntity)childEntity;
            
            castedChildEntity.RectUi.RefreshRectangle();
        }
    }
    
    void RefreshRectangle()
    {
        float scale = Engine.UiRenderer.GetUiScale();
        
        Vector2 rectangleMin = GetRectangleMinUiPosition(scale);
        Vector2 rectangleMax = GetRectangleMaxUiPosition(scale);
        
        Vector2 rectangleSize = rectangleMax - rectangleMin;
        Vector2 rectanglePosition = rectangleMin + (rectangleSize * 0.5f);
        
        Vector2 shiftedPivot = new(-0.5f + Pivot.X, -0.5f + Pivot.Y);
        rectanglePosition += rectangleSize * -shiftedPivot;
        
        Rectangle uiRectangle = new(
            rectanglePosition.X,
            rectanglePosition.Y,
            rectangleSize.X,
            rectangleSize.Y
        );
        
        RectangleMin = new Vector2(
            rectanglePosition.X - rectangleSize.X * 0.5f,
            rectanglePosition.Y - rectangleSize.Y * 0.5f
        );
        
        RectangleMax = new Vector2(
            rectanglePosition.X + rectangleSize.X * 0.5f,
            rectanglePosition.Y + rectangleSize.Y * 0.5f
        );
        
        Rectangle = uiRectangle;

        NotifyComponentsRectUiChanged();
    }

    void SetAnchoredPositionFromUiPosition(Vector2 uiPosition)
    {
        float scale = Engine.UiRenderer.GetUiScale();
        
        Vector2 anchorMaxUiPosition = GetAnchorMaxUiPosition();
        Vector2 anchorMinUiPosition = GetAnchorMinUiPosition();
        Vector2 anchorUiSize = anchorMaxUiPosition - anchorMinUiPosition;
        Vector2 anchorUiPosition = anchorMinUiPosition * anchorUiSize * 0.5f;

        Vector2 distanceDifference = uiPosition - anchorUiPosition;
        
        float anchoredPositionX = MathExtensions.Divide(distanceDifference.X, scale);
        float anchoredPositionY = MathExtensions.Divide(distanceDifference.Y, scale);

        Vector2 newAnchoredPosition = new Vector2(anchoredPositionX, anchoredPositionY);
        
        SetAnchoredPosition(newAnchoredPosition);
    }

    Vector2 GetUiPositionFromAnchor(Vector2 anchor)
    {
        Vector2 screenSize = Engine.Windows.GetScreenSize();

        Rectangle parentUiRectangle = new Rectangle(
            0,
            0,
            screenSize.X,
            screenSize.Y
        );

        bool hasParent = Owner.Parent.TryGet(out BaseEntity<UiComponent> parent);

        if (hasParent)
        {
            Optional<RectUiComponent> optionalRectUiComponent = parent.GetComponent<RectUiComponent>();

            bool hasRectUiComponent = optionalRectUiComponent.TryGet(out RectUiComponent parentRectUiComponent);

            if (hasRectUiComponent)
            {
                parentUiRectangle = parentRectUiComponent.Rectangle;
            }
        }

        Vector2 size = new Vector2(parentUiRectangle.Width, parentUiRectangle.Height);
        Vector2 min = new Vector2(parentUiRectangle.X - (size.X * 0.5f), parentUiRectangle.Y - (size.Y * 0.5f));

        Vector2 uiPosition = min + (size * anchor);
        
        return uiPosition;
    }

    Vector2 GetAnchorMaxUiPosition()
    {
        return GetUiPositionFromAnchor(AnchorMax);
    }
    
    Vector2 GetAnchorMinUiPosition()
    {
        return GetUiPositionFromAnchor(AnchorMin);
    }
    
    Vector2 GetRectangleMaxUiPosition(float scale)
    {
        Vector2 anchorMaxUiPosition = GetAnchorMaxUiPosition();
        
        float rectangleRight = anchorMaxUiPosition.X + (HalfSizeDelta.X * scale) + (AnchoredPosition.X * scale);
        float rectangleTop = anchorMaxUiPosition.Y + (HalfSizeDelta.Y * scale) + (AnchoredPosition.Y * scale);

        Vector2 rectangleMax = new(rectangleRight, rectangleTop);
        return rectangleMax;
    }

    Vector2 GetRectangleMinUiPosition(float scale)
    {
        Vector2 anchorMinUiPosition = GetAnchorMinUiPosition();
        
        float rectangleLeft = anchorMinUiPosition.X - (HalfSizeDelta.X * scale) + (AnchoredPosition.X * scale);
        float rectangleBottom = anchorMinUiPosition.Y - (HalfSizeDelta.Y * scale) + (AnchoredPosition.Y * scale);
        
        Vector2 rectangleMin = new(rectangleLeft, rectangleBottom);
        return rectangleMin;
    }

    void RefreshActsAsRootState()
    {
        bool wantsToActAsRoot = !Owner.HasParent && Owner.IsActiveInHierachy && !_actsAsRoot;
        
        if (wantsToActAsRoot)
        {
            _actsAsRoot = true;
            Engine.Windows.WindowSizeChangedEvent.AddListener(WhenWindowSizeChanged);
        }
        
        bool wantsToStopActingAsRoot = (Owner.HasParent || !Owner.IsActiveInHierachy) && _actsAsRoot;
        
        if(wantsToStopActingAsRoot)
        {
            _actsAsRoot = false;
            Engine.Windows.WindowSizeChangedEvent.RemoveListener(WhenWindowSizeChanged);
        }
    }

    void WhenWindowSizeChanged(Vector2 size)
    {
        RecalculateChildHierearchyRectangleValues();
    }
    
    void NotifyComponentsRectUiChanged()
    {
        foreach (UiComponent component in Owner.Components)
        {
            if (component is INotifyRectUiChanged notifyRectUiChanged)
            {
                notifyRectUiChanged.OnRectUiChanged();
            }
        }
    }
}