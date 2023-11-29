using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Interfaces;
using GEngine.Modules.Entities.Objects;
using GEngine.Utils.Extensions;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Components.Objects;

public sealed class TransformComponent : WorldComponent
{
    public Matrix4x4 LocalMatrix { get; private set; } = Matrix4x4.Identity;
    public Matrix4x4 WorldMatrix { get; private set; } = Matrix4x4.Identity;
    
    public Vector3 LocalPosition { get; private set; }
    public Quaternion LocalRotation { get; private set; } = Quaternion.Identity;
    public Vector3 LocalRotationRadiants { get; private set; }
    public Vector3 LocalRotationDegrees { get; private set; }
    public Vector3 LocalScale { get; private set; } = Vector3.One;

    public Vector3 WorldPosition { get; private set; }
    public Quaternion WorldRotation { get; private set; } = Quaternion.Identity;
    public Vector3 WorldRotationRadiants { get; private set; }
    public Vector3 WorldRotationDegrees { get; private set; }
    public Vector3 WorldScale { get; private set; } = Vector3.One;
    
    public TransformComponent(IREngineInteractor engine, Guid uid, BaseEntity<WorldComponent> owner) : base(engine, uid, owner)
    {
        RefreshLocalMatrix();
    }

    public override void ParentChanged()
    {
        RecalculateChildHirearchyTransformValues(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        SetLocalPosition(position, false);
    }
    
    public void SetLocalPositionX(float position)
    {
        Vector3 localPosition = LocalPosition;
        localPosition.X = position;
        
        SetLocalPosition(localPosition);
    }
    
    public void SetLocalPositionY(float position)
    {
        Vector3 localPosition = LocalPosition;
        localPosition.Y = position;
        
        SetLocalPosition(localPosition);
    }
    
    public void SetLocalPositionZ(float position)
    {
        Vector3 localPosition = LocalPosition;
        localPosition.Z = position;
        
        SetLocalPosition(localPosition);
    }
    
    public void SetLocalPositionXY(Vector2 position)
    {
        Vector3 localPosition = LocalPosition;
        localPosition.X = position.X;
        localPosition.Y = position.Y;
        
        SetLocalPosition(localPosition);
    }

    public void AddLocalPosition(Vector3 position)
    {
        SetLocalPosition(LocalPosition + position);
    }
    
    public void AddLocalPositionX(float position)
    {
        SetLocalPositionX(LocalPosition.X + position);
    }
    
    public void AddLocalPositionY(float position)
    {
        SetLocalPositionX(LocalPosition.Y + position);
    }
    
    public void AddLocalPositionZ(float position)
    {
        SetLocalPositionX(LocalPosition.Z + position);
    }
    
    public void AddLocalPositionXY(Vector2 position)
    {
        SetLocalPositionXY(LocalPosition.ToVector2XY() + position);
    }
    
    public void SetWorldPosition(Vector3 position)
    {
        SetWorldPosition(position, false);
    }
    
    public void SetWorldPositionX(float position)
    {
        Vector3 worldPosition = WorldPosition;
        worldPosition.X = position;
        
        SetWorldPosition(worldPosition);
    }
    
    public void SetWorldPositionY(float position)
    {
        Vector3 worldPosition = WorldPosition;
        worldPosition.Y = position;
        
        SetWorldPosition(worldPosition);
    }
    
    public void SetWorldPositionZ(float position)
    {
        Vector3 worldPosition = WorldPosition;
        worldPosition.Z = position;
        
        SetWorldPosition(worldPosition);
    }
    
    public void SetWorldPositionXY(Vector2 position)
    {
        SetWorldPositionXY(position, false);
    }

    public void SetWorldPositionXYByPhysics(Vector2 position)
    {
        SetWorldPositionXY(position, true);
    }
    
    public void AddWorldPosition(Vector3 position)
    {
        SetWorldPosition(WorldPosition + position, false);
    }
    
    public void AddWorldPositionX(float position)
    {
        SetWorldPositionX(WorldPosition.X + position);
    }
    
    public void AddWorldPositionY(float position)
    {
        SetWorldPositionX(WorldPosition.Y + position);
    }
    
    public void AddWorldPositionZ(float position)
    {
        SetWorldPositionX(WorldPosition.Z + position);
    }
    
    public void AddWorldPositionXY(Vector2 position)
    {
        SetWorldPositionXY(WorldPosition.ToVector2XY() + position);
    }

    public void SetLocalRotation(Quaternion rotation)
    {
        SetLocalRotation(rotation, false);
    }

    public void SetLocalRotationRadiants(Vector3 rotation)
    {
        SetLocalRotationRadiants(rotation, false);
    }
    
    public void SetLocalRotationRadiantsZ(float rotation)
    {
        Vector3 localRotationRadiants = LocalRotationRadiants;
        localRotationRadiants.Z = rotation;
        SetLocalRotationRadiants(localRotationRadiants);
    }
    
    public void SetLocalRotationDegrees(Vector3 rotation)
    {
        SetLocalRotationRadiants(rotation * MathExtensions.Deg2Rad);
    }
    
    public void SetLocalRotationDegreesZ(float rotation)
    {
        Vector3 localRotationDegrees = LocalRotationDegrees;
        localRotationDegrees.Z = rotation;
        SetLocalRotationDegrees(localRotationDegrees);
    }
    
    public void SetWorldRotation(Quaternion rotation)
    {
        SetWorldRotation(rotation, false);
    }

    public void SetWorldRotationRadiants(Vector3 rotation)
    {
        SetWorldRotationRadiants(rotation, false);
    }

    public void SetWorldRotationRadiantsZ(float rotation)
    {
        SetWorldRotationRadiantsZ(rotation, false);
    }
    
    public void SetWorldRotationRadiantsZByPhysics(float rotation)
    {
        SetWorldRotationRadiantsZ(rotation, true);
    }

    public void SetWorldRotationDegrees(Vector3 rotation)
    {
        SetWorldRotationRadiants(rotation * MathExtensions.Rad2Deg);
    }
    
    public void SetWorldRotationDegreesZ(float rotation)
    {
        Vector3 worldRotationDegrees = WorldRotationDegrees;
        worldRotationDegrees.Z = rotation;
        SetWorldRotationDegrees(worldRotationDegrees);
    }

    public void SetLocalScale(Vector3 scale)
    {
        SetLocalScale(scale, false);
    }
    
    public void SetLocalScaleX(float scale)
    {
        Vector3 localScale = LocalScale;
        localScale.X = scale;
        SetLocalScale(localScale);
    }
    
    public void SetLocalScaleY(float scale)
    {
        Vector3 localScale = LocalScale;
        localScale.Y = scale;
        SetLocalScale(localScale);
    }
    
    public void SetLocalScaleZ(float scale)
    {
        Vector3 localScale = LocalScale;
        localScale.Z = scale;
        SetLocalScale(localScale);
    }
    
    public void SetLocalScaleXY(Vector2 scale)
    {
        Vector3 localScale = LocalScale;
        localScale.X = scale.X;
        localScale.Y = scale.Y;
        SetLocalScale(localScale);
    }

    public void AddLocalScale(Vector3 scale)
    {
        SetLocalScale(LocalScale + scale);
    }

    public Optional<TransformComponent> GetParentTransformComponent()
    {
        bool hasParent = Owner.Parent.TryGet(out BaseEntity<WorldComponent> parentEntity);

        if (!hasParent)
        {
            return Optional<TransformComponent>.None;
        }

        WorldEntity parentWorldEntity = (WorldEntity)parentEntity;

        return parentWorldEntity.Transform;
    }

    public Matrix4x4 TransformToLocalSpace(TransformComponent transformComponent)
    {
        bool couldInvert = Matrix4x4.Invert(transformComponent.WorldMatrix, out Matrix4x4 invertedMatrix);

        if (!couldInvert)
        {
            return transformComponent.WorldMatrix;
        }
        
        return WorldMatrix * invertedMatrix;
    }
    
    void SetLocalPosition(Vector3 position, bool byPhysics)
    {
        bool isSamePosition = position == LocalPosition;

        if (isSamePosition)
        {
            return;
        }
        
        LocalPosition = position;
        
        RefreshLocalMatrix();

        RecalculateChildHirearchyTransformValues(byPhysics);
    }
    
    void SetWorldPosition(Vector3 position, bool byPhysics)
    {
        Vector3 parentWorldPosition = Vector3.Zero;

        Optional<TransformComponent> optionalParentTransform = GetParentTransformComponent();
        
        bool hasParentTransform = optionalParentTransform.TryGet(out TransformComponent parentTransform);

        if (hasParentTransform)
        {
            parentWorldPosition = parentTransform.WorldPosition;
        }

        Vector3 newLocalPosition = position - parentWorldPosition;
        
        bool isSamePosition = newLocalPosition == LocalPosition;

        if (isSamePosition)
        {
            return;
        }
        
        SetLocalPosition(newLocalPosition, byPhysics);
    }
    
    void SetWorldPositionXY(Vector2 position, bool byPhysics)
    {
        Vector3 worldPosition = WorldPosition;
        worldPosition.X = position.X;
        worldPosition.Y = position.Y;

        SetWorldPosition(worldPosition, byPhysics);
    }
    
    void SetWorldRotation(Quaternion rotation, bool byPhysics)
    {
        bool isSameRotation = rotation == WorldRotation;

        if (isSameRotation)
        {
            return;
        }
      
        Quaternion parentWorldRotation = Quaternion.Identity;
        
        Optional<TransformComponent> optionalParentTransform = GetParentTransformComponent();
        
        bool hasParentTransform = optionalParentTransform.TryGet(out TransformComponent parentTransform);

        if (hasParentTransform)
        {
            parentWorldRotation = parentTransform.WorldRotation;
        }

        Quaternion newLocalRotation = rotation * parentWorldRotation.Inverse();
        
        SetLocalRotation(newLocalRotation, byPhysics);
    }
    
    void SetWorldRotationRadiants(Vector3 rotation, bool byPhysics)
    {
        Quaternion quaternion = rotation.ToQuaternionFromRadiants();
        
        SetWorldRotation(quaternion, byPhysics);
    }
    
    void SetWorldRotationRadiantsZ(float rotation, bool byPhysics)
    {
        Vector3 worldRotationRadiants = WorldRotationRadiants;
        worldRotationRadiants.Z = rotation;

        SetWorldRotationRadiants(worldRotationRadiants, byPhysics);
    }
    
    void SetLocalRotation(Quaternion rotation, bool byPhysics)
    {
        bool isSameRotation = rotation == LocalRotation;

        if (isSameRotation)
        {
            return;
        }
        
        LocalRotation = rotation;
        LocalRotationRadiants = LocalRotation.ToRadiantAngles();
        LocalRotationDegrees = LocalRotationRadiants * MathExtensions.Rad2Deg;
        
        RefreshLocalMatrix();
        RecalculateChildHirearchyTransformValues(byPhysics);
    }
    
    void SetLocalRotationRadiants(Vector3 radiants, bool byPhysics)
    {
        bool isSameRotation = radiants == LocalRotationRadiants;

        if (isSameRotation)
        {
            return;
        }
        
        LocalRotationRadiants = radiants;
        LocalRotation = radiants.ToQuaternionFromRadiants();
        LocalRotationDegrees = LocalRotationRadiants * MathExtensions.Rad2Deg;
        
        RefreshLocalMatrix();
        RecalculateChildHirearchyTransformValues(byPhysics);
    }
    
    void SetLocalScale(Vector3 scale, bool byPhysics)
    {
        bool isSameScale = scale == LocalScale;

        if (isSameScale)
        {
            return;
        }
        
        LocalScale = scale;
        
        RefreshLocalMatrix();
        RecalculateChildHirearchyTransformValues(byPhysics);
    }
    
    void RefreshLocalMatrix()
    {
        LocalMatrix = Matrix4X4Extensions.Compose(
            LocalPosition,
            LocalRotation,
            LocalScale
        );
    }

    void RecalculateChildHirearchyTransformValues(bool byPhysics)
    {
        IEnumerable<IEntity> childEntities = Owner.GetChildEntitiesOnHierarchy(true);

        foreach (IEntity childEntity in childEntities)
        {
            WorldEntity castedChildEntity = (WorldEntity)childEntity;
            
            castedChildEntity.Transform.RecalculateWorldValues(byPhysics);
        }
    }

    void RecalculateWorldValues(bool byPhysics)
    {
         bool transformHasChanged = false;
         
        Vector3 previousWorldPosition = WorldPosition;
        Quaternion previousWorldRotation = WorldRotation;
        Vector3 previousWorldScale = WorldScale;

        Optional<TransformComponent> optionalParentTransform = GetParentTransformComponent();

        Matrix4x4 parentMatrix = Matrix4x4.Identity;
        
        bool hasParentTransform = optionalParentTransform.TryGet(out TransformComponent parentTransform);

        if (hasParentTransform)
        {
            parentMatrix = parentTransform.WorldMatrix;
        }
        
        WorldMatrix = LocalMatrix * parentMatrix;
            
        WorldMatrix.Decompose(
            out Vector3 worldPosition,
            out Quaternion worldRotation,
            out Vector3 worldScale
        );
            
        WorldPosition = worldPosition;
        WorldRotation = worldRotation;
        WorldRotationRadiants = WorldRotation.ToRadiantAngles();
        WorldRotationDegrees = WorldRotationRadiants * MathExtensions.Rad2Deg;
        WorldScale = worldScale;
        
        if (previousWorldPosition != WorldPosition)
        {
            transformHasChanged = true;
        }
        
        if (previousWorldRotation != WorldRotation)
        {
            transformHasChanged = true;
        }
        
        if (previousWorldScale != WorldScale)
        {
            transformHasChanged = true;
        }

        if (transformHasChanged)
        {
            NotifyComponentsTransformChanged(byPhysics);
        }
    }

    void NotifyComponentsTransformChanged(bool byPhysics)
    {
        foreach (WorldComponent component in Owner.Components)
        {
            if (component is INotifyTransformChanged notifyTransformChanged)
            {
                notifyTransformChanged.OnTransformChanged(byPhysics);
            }
        }
    }
}