using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Utils.Optionals;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Entities.Objects;

public abstract class BaseEntity<T> : IEntity where T : Component
{
    readonly IREngineInteractor _engine;
    readonly List<BaseEntity<T>> _children = new();
    readonly List<T> _components = new();

    readonly Action<BaseEntity<T>> _refreshEntityOnSceneAction;
    readonly Action<BaseEntity<T>> _refreshEntityOnActiveEntitiesListsAction;

    public Guid Uid { get; }
    public string Name { get; set; } = string.Empty;
    
    public bool IsOnScene { get; private set; }
    public bool IsActiveSelf { get; private set; }
    public bool IsActiveInHierachy { get; private set; }
    public bool HasParent => Parent.HasValue;

    public Optional<BaseEntity<T>> Parent { get; private set; }
    public IReadOnlyList<BaseEntity<T>> Children => _children;

    public IReadOnlyList<T> Components => _components;

    protected BaseEntity(
        IREngineInteractor engine,
        Guid uid, 
        Action<BaseEntity<T>> refreshEntityOnSceneAction,
        Action<BaseEntity<T>> refreshEntityOnActiveEntitiesListsAction
        )
    {
        _engine = engine;
        Uid = uid;
        _refreshEntityOnSceneAction = refreshEntityOnSceneAction;
        _refreshEntityOnActiveEntitiesListsAction = refreshEntityOnActiveEntitiesListsAction;
    }
    
    public void Tick()
    {
        _components.ForEach(component => component.Tick());
    }

    public void AddToScene()
    {
        if (IsOnScene)
        {
            return;
        }
        
        IEnumerable<IEntity> childEntities = GetChildEntitiesOnHierarchy(
            true
        );

        foreach (IEntity childEntity in childEntities)
        {
            BaseEntity<T> childCastedEntity = (BaseEntity<T>)childEntity;
            
            childCastedEntity.IsOnScene = true;
            
            childCastedEntity._components.ForEach(component => component.Init());

            if (childEntity.IsActiveInHierachy)
            {
                childCastedEntity._components.ForEach(component => component.Enable());
            }
        
            _refreshEntityOnSceneAction.Invoke(childCastedEntity);   
        }
    }

    public void RemoveFromScene()
    {
        if (!IsOnScene)
        {
            return;
        }
        
        IEnumerable<IEntity> childEntities = GetChildEntitiesOnHierarchy(
            true
        );
        
        foreach (IEntity childEntity in childEntities)
        {
            BaseEntity<T> childCastedEntity = (BaseEntity<T>)childEntity;
            
            childCastedEntity.IsOnScene = false;

            if (childEntity.IsActiveInHierachy)
            {
                childCastedEntity._components.ForEach(component => component.Disable());
            }
            
            childCastedEntity._components.ForEach(component => component.Dispose());
        
            _refreshEntityOnSceneAction.Invoke(childCastedEntity);   
        }
    }

    public void SetActive(bool active)
    {
        if (IsActiveSelf == active)
        {
            return;
        }
        
        IsActiveSelf = active;

        RefreshActiveInHierachyState();
    }

    public void SetParent(BaseEntity<T> parentEntity)
    {
        bool isSameEntity = this == parentEntity;

        if (isSameEntity)
        {
            return;
        }

        if (parentEntity.IsOnScene != IsOnScene)
        {
            return;
        }

        bool parentEntityIsOnEntityHierarchy = IsEntityOnChildHierarchy(parentEntity);

        if (parentEntityIsOnEntityHierarchy)
        {
            return;
        }
        
        bool hadParent = Parent.TryGet(out BaseEntity<T> currentParentEntity);

        if (hadParent)
        {
            currentParentEntity._children.Remove(this);
        }

        Parent = parentEntity;
        parentEntity._children.Add(this);

        bool activeStateShouldChange = parentEntity.IsActiveInHierachy != IsActiveInHierachy;

        if (activeStateShouldChange)
        {
            RefreshActiveInHierachyState();
        }
        else
        {
            _refreshEntityOnActiveEntitiesListsAction(this); 
        }
        
        parentEntity._components.ForEach(component => component.ChildsChanged());   
        _components.ForEach(component => component.ParentChanged());   
    }

    public bool RemoveParent()
    {
        bool hasParent = Parent.TryGet(out BaseEntity<T> parentEntity);

        if (!hasParent)
        {
            return false;
        }

        Parent = Optional<BaseEntity<T>>.None;
        parentEntity._children.Remove(this);

        RefreshActiveInHierachyState();
        
        parentEntity._components.ForEach(component => component.ChildsChanged());  
        _components.ForEach(component => component.ParentChanged());   

        return true;
    }

    public void AddChild(BaseEntity<T> entity)
    {
        entity.SetParent(this);
    }

    public void RemoveAllChilds()
    {
        for (int i = _children.Count - 1; i >= 0; --i)
        {
            BaseEntity<T> child = _children[i];

            child.RemoveParent();
        }
    }
    
    public IEnumerable<BaseEntity<T>> GetParentEntitiesOnHierarchy(
        bool includeCurrent
    )
    {
        Stack<BaseEntity<T>> entitiesToCheck = new();

        if (includeCurrent)
        {
            yield return this;
        }

        bool hasParent = Parent.TryGet(out BaseEntity<T> parent);

        if (hasParent)
        {
            entitiesToCheck.Push(parent);
        }

        while (entitiesToCheck.Count > 0)
        {
            BaseEntity<T> checkingEntityData = entitiesToCheck.Pop();
            
            yield return checkingEntityData;
            
            hasParent = Parent.TryGet(out parent);
            
            if (hasParent)
            {
                entitiesToCheck.Push(parent);
            }
        }
    }
    
    public IEnumerable<IEntity> GetChildEntitiesOnHierarchy(
        bool includeCurrent
    )
    {
        Stack<BaseEntity<T>> entitiesToCheck = new();

        if (includeCurrent)
        {
            yield return this;
        }
        
        entitiesToCheck.PushRange(Children);

        while (entitiesToCheck.Count > 0)
        {
            BaseEntity<T> checkingEntityData = entitiesToCheck.Pop();
            
            yield return checkingEntityData;
            
            entitiesToCheck.PushRange(checkingEntityData.Children);
        }
    }

    public bool IsEntityOnChildHierarchy(BaseEntity<T> entity)
    {
        Stack<BaseEntity<T>> toCheck = new();
        toCheck.Push(this);

        while (toCheck.Count > 0)
        {
            BaseEntity<T> checkingEntity = toCheck.Pop();

            bool isEntity = entity == checkingEntity;

            if (isEntity)
            {
                return true;
            }

            toCheck.PushRange(checkingEntity.Children);
        }

        return false;
    }
    
    public bool IsEntityOnParentHierarchy(BaseEntity<T> entity)
    {
        BaseEntity<T> checking = this;

        while (true)
        {
            bool isEntity = entity == checking;

            if (isEntity)
            {
                return true;
            }

            bool hasParent = checking.Parent.TryGet(out checking);

            if (!hasParent)
            {
                return false;
            }
        }
    }

    public TComponent AddComponent<TComponent>() where TComponent : T
    {
        Type type = typeof(TComponent);
        
        Guid uid = Guid.NewGuid();
        
        TComponent component = (TComponent)Activator.CreateInstance(type, _engine, uid, this)!;
        
        _components.Add(component);
        
        if (IsOnScene)
        {
            component.Init();
        }
        
        if (IsActiveInHierachy)
        {
            component.Enable();
        }

        return component;
    }

    public void RemoveComponent<TComponent>() where TComponent : T
    {
        Type type = typeof(TComponent);

        foreach (T component in _components)
        {
            bool isType = type.IsInstanceOfType(component);
            
            if (isType)
            {
                if (IsOnScene)
                {
                    if (IsActiveInHierachy)
                    {
                        component.Disable();
                    }
                    
                    component.Dispose();
                }
                
                _components.Remove(component);
                break;
            }
        }
    }
    
    public void RemoveAllComponents()
    {
        foreach (T component in _components)
        {
            if (IsOnScene)
            {
                if (IsActiveInHierachy)
                {
                    component.Disable();
                }
                
                component.Dispose();
            }
        }
        
        _components.Clear();
    }

    public Optional<TComponent> GetComponent<TComponent>() where TComponent : Component
    {
        Type type = typeof(TComponent);
        
        foreach (Component component in Components)
        {
            bool isType = type.IsInstanceOfType(component);

            if (isType)
            {
                return (TComponent)component;
            }
        }

        return Optional<TComponent>.None;
    }

    public TComponent GetComponentUnsafe<TComponent>() where TComponent : Component
    {
        Optional<TComponent> component = GetComponent<TComponent>();

        return component.UnsafeGet();
    }

    public Optional<TComponent> GetComponentInParentHierarchy<TComponent>() where TComponent : Component
    {
        IEnumerable<BaseEntity<T>> parentEntities = GetParentEntitiesOnHierarchy(true);

        foreach (BaseEntity<T> entity in parentEntities)
        {
            Optional<TComponent> component = entity.GetComponent<TComponent>();

            if (component.HasValue)
            {
                return component;
            }
        }
        
        return Optional<TComponent>.None;
    }
    
    public Optional<TComponent> GetComponentInChildHierarchy<TComponent>() where TComponent : Component
    {
        IEnumerable<IEntity> parentEntities = GetChildEntitiesOnHierarchy(true);

        foreach (IEntity entity in parentEntities)
        {
            BaseEntity<T> castedEntity = (BaseEntity<T>)entity;
            
            Optional<TComponent> component = castedEntity.GetComponent<TComponent>();

            if (component.HasValue)
            {
                return component;
            }
        }
        
        return Optional<TComponent>.None;
    }
    
    public List<TComponent> GetComponents<TComponent>() where TComponent : Component
    {
        List<TComponent> ret = new();
        
        Type type = typeof(TComponent);
        
        foreach (Component component in Components)
        {
            bool isType = type.IsInstanceOfType(component);

            if (isType)
            {
                ret.Add((TComponent)component);
            }
        }

        return ret;
    }
    
    public override int GetHashCode()
    {
        return Uid.GetHashCode();
    }

    void RefreshActiveInHierachyState()
    {
        IEnumerable<IEntity> childEntities = GetChildEntitiesOnHierarchy(
            true
        );

        foreach (IEntity childEntity in childEntities)
        {
            BaseEntity<T> childCastedEntity = (BaseEntity<T>)childEntity;
            
            bool wasActiveInHierarchy = childEntity.IsActiveInHierachy;

            bool hasParent = childCastedEntity.Parent.TryGet(out BaseEntity<T> parentEntity);

            if (hasParent)
            {
                childCastedEntity.IsActiveInHierachy = parentEntity.IsActiveInHierachy && childEntity.IsActiveSelf;
            }
            else
            {
                childCastedEntity.IsActiveInHierachy = childEntity.IsActiveSelf;
            }

            bool activeInHierarchyChanged = wasActiveInHierarchy != childEntity.IsActiveInHierachy;

            if (!activeInHierarchyChanged)
            {
                return;
            }

            if (IsOnScene)
            {
                if (childEntity.IsActiveInHierachy)
                {
                    _components.ForEach(component => component.Enable());   
                }
                else
                {
                    _components.ForEach(component => component.Disable());   
                }   
            }
            
            _refreshEntityOnActiveEntitiesListsAction(childCastedEntity);
        }
    }
}