using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Utils.Optionals;
using GEngine.Utils.Tick.Tickables;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.Entities.Objects;

public sealed class Entity : ITickable
{
    readonly IREngineInteractor _engine;
    readonly List<Entity> _children = new();
    readonly List<Component> _components = new();

    readonly Action<Entity> _refreshEntityOnSceneAction;
    readonly Action<Entity> _refreshEntityOnActiveEntitiesListsAction;

    public Guid Uid { get; }
    public string Name { get; set; } = string.Empty;
    
    public bool IsOnScene { get; private set; }
    public bool ActiveSelf { get; private set; }
    public bool ActiveInHierachy { get; private set; }
    
    public Optional<Entity> Parent { get; private set; }
    public IReadOnlyList<Entity> Children => _children;

    public IReadOnlyList<Component> Components => _components;
    public TransformComponent Transform { get; }
    
    public Entity(
        IREngineInteractor engine,
        Guid uid, 
        Action<Entity> refreshEntityOnSceneAction,
        Action<Entity> refreshEntityOnActiveEntitiesListsAction
        )
    {
        _engine = engine;
        Uid = uid;
        _refreshEntityOnSceneAction = refreshEntityOnSceneAction;
        _refreshEntityOnActiveEntitiesListsAction = refreshEntityOnActiveEntitiesListsAction;

        Transform = AddComponent<TransformComponent>();
    }

    public void AddToScene()
    {
        if (IsOnScene)
        {
            return;
        }
        
        IEnumerable<Entity> childEntities = GetChildEntitiesOnHierarchy(
            true
        );

        foreach (Entity childEntity in childEntities)
        {
            childEntity.IsOnScene = true;
            
            childEntity._components.ForEach(component => component.Init());

            if (childEntity.ActiveInHierachy)
            {
                childEntity._components.ForEach(component => component.Enable());
            }
        
            _refreshEntityOnSceneAction.Invoke(childEntity);   
        }
    }

    public void RemoveFromScene()
    {
        if (!IsOnScene)
        {
            return;
        }
        
        IEnumerable<Entity> childEntities = GetChildEntitiesOnHierarchy(
            true
        );
        
        foreach (Entity childEntity in childEntities)
        {
            childEntity.IsOnScene = false;

            if (childEntity.ActiveInHierachy)
            {
                childEntity._components.ForEach(component => component.Disable());
            }
            
            childEntity._components.ForEach(component => component.Dispose());
        
            _refreshEntityOnSceneAction.Invoke(childEntity);   
        }
    }

    public void SetActive(bool active)
    {
        if (ActiveSelf == active)
        {
            return;
        }
        
        ActiveSelf = active;

        RefreshActiveInHierachyState();
    }

    public void SetParent(Entity parentEntity)
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
        
        bool hadParent = Parent.TryGet(out Entity currentParentEntity);

        if (hadParent)
        {
            currentParentEntity._children.Remove(this);
        }

        Parent = parentEntity;
        parentEntity._children.Add(this);

        bool activeStateShouldChange = parentEntity.ActiveInHierachy != ActiveInHierachy;

        if (activeStateShouldChange)
        {
            RefreshActiveInHierachyState();
        }
        else
        {
            _refreshEntityOnActiveEntitiesListsAction(this); 
        }
    }

    public bool RemoveParent()
    {
        bool hasParent = Parent.TryGet(out Entity parentEntity);

        if (!hasParent)
        {
            return false;
        }

        Parent = Optional<Entity>.None;
        parentEntity._children.Remove(this);

        RefreshActiveInHierachyState();

        return true;
    }

    public void AddChild(Entity entity)
    {
        entity.SetParent(this);
    }

    public void RemoveAllChilds()
    {
        for (int i = _children.Count - 1; i >= 0; --i)
        {
            Entity child = _children[i];

            child.RemoveParent();
        }
    }
    
    public IEnumerable<Entity> GetParentEntitiesOnHierarchy(
        bool includeCurrent
    )
    {
        Stack<Entity> entitiesToCheck = new();

        if (includeCurrent)
        {
            yield return this;
        }

        bool hasParent = Parent.TryGet(out Entity parent);

        if (hasParent)
        {
            entitiesToCheck.Push(parent);
        }

        while (entitiesToCheck.Count > 0)
        {
            Entity checkingEntityData = entitiesToCheck.Pop();
            
            yield return checkingEntityData;
            
            hasParent = Parent.TryGet(out parent);
            
            if (hasParent)
            {
                entitiesToCheck.Push(parent);
            }
        }
    }
    
    public IEnumerable<Entity> GetChildEntitiesOnHierarchy(
        bool includeCurrent
    )
    {
        Stack<Entity> entitiesToCheck = new();

        if (includeCurrent)
        {
            yield return this;
        }
        
        entitiesToCheck.PushRange(Children);

        while (entitiesToCheck.Count > 0)
        {
            Entity checkingEntityData = entitiesToCheck.Pop();
            
            yield return checkingEntityData;
            
            entitiesToCheck.PushRange(checkingEntityData.Children);
        }
    }

    public bool IsEntityOnChildHierarchy(Entity entity)
    {
        Stack<Entity> toCheck = new();
        toCheck.Push(this);

        while (toCheck.Count > 0)
        {
            Entity checkingEntity = toCheck.Pop();

            bool isEntity = entity == checkingEntity;

            if (isEntity)
            {
                return true;
            }

            toCheck.PushRange(checkingEntity.Children);
        }

        return false;
    }
    
    public bool IsEntityOnParentHierarchy(Entity entity)
    {
        Entity checking = this;

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

    public T AddComponent<T>() where T : Component
    {
        Type type = typeof(T);
        
        Guid uid = Guid.NewGuid();
        
        T component = (T)Activator.CreateInstance(type, _engine, uid, this)!;
        
        _components.Add(component);
        
        if (IsOnScene)
        {
            component.Init();
        }
        
        if (ActiveInHierachy)
        {
            component.Enable();
        }

        return component;
    }

    public void RemoveComponent<T>()
    {
        Type type = typeof(T);

        foreach (Component component in _components)
        {
            bool isType = type.IsInstanceOfType(component);
            
            if (isType)
            {
                if (IsOnScene)
                {
                    if (ActiveInHierachy)
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
        foreach (Component component in _components)
        {
            if (IsOnScene)
            {
                if (ActiveInHierachy)
                {
                    component.Disable();
                }
                
                component.Dispose();
            }
        }
        
        _components.Clear();
    }

    public Optional<T> GetComponent<T>() where T : Component
    {
        Type type = typeof(T);
        
        foreach (Component component in Components)
        {
            bool isType = type.IsInstanceOfType(component);

            if (isType)
            {
                return (T)component;
            }
        }

        return Optional<T>.None;
    }

    public T GetComponentUnsafe<T>() where T : Component
    {
        Optional<T> component = GetComponent<T>();

        return component.UnsafeGet();
    }

    public Optional<T> GetComponentInParentHierarchy<T>() where T : Component
    {
        IEnumerable<Entity> parentEntities = GetParentEntitiesOnHierarchy(true);

        foreach (Entity entity in parentEntities)
        {
            Optional<T> component = entity.GetComponent<T>();

            if (component.HasValue)
            {
                return component;
            }
        }
        
        return Optional<T>.None;
    }
    
    public Optional<T> GetComponentInChildHierarchy<T>() where T : Component
    {
        IEnumerable<Entity> parentEntities = GetChildEntitiesOnHierarchy(true);

        foreach (Entity entity in parentEntities)
        {
            Optional<T> component = entity.GetComponent<T>();

            if (component.HasValue)
            {
                return component;
            }
        }
        
        return Optional<T>.None;
    }
    
    public List<T> GetComponents<T>() where T : Component
    {
        List<T> ret = new();
        
        Type type = typeof(T);
        
        foreach (Component component in Components)
        {
            bool isType = type.IsInstanceOfType(component);

            if (isType)
            {
                ret.Add((T)component);
            }
        }

        return ret;
    }

    public void Tick()
    {
        _components.ForEach(component => component.Tick());
    }
    
    public override int GetHashCode()
    {
        return Uid.GetHashCode();
    }

    void RefreshActiveInHierachyState()
    {
        IEnumerable<Entity> childEntities = GetChildEntitiesOnHierarchy(
            true
        );

        foreach (Entity childEntity in childEntities)
        {
            bool wasActiveInHierarchy = childEntity.ActiveInHierachy;

            bool hasParent = childEntity.Parent.TryGet(out Entity parentEntity);

            if (hasParent)
            {
                childEntity.ActiveInHierachy = parentEntity.ActiveInHierachy && childEntity.ActiveSelf;
            }
            else
            {
                childEntity.ActiveInHierachy = childEntity.ActiveSelf;
            }

            bool activeInHierarchyChanged = wasActiveInHierarchy != childEntity.ActiveInHierachy;

            if (!activeInHierarchyChanged)
            {
                return;
            }

            if (IsOnScene)
            {
                if (childEntity.ActiveInHierachy)
                {
                    _components.ForEach(component => component.Enable());   
                }
                else
                {
                    _components.ForEach(component => component.Disable());   
                }   
            }
            
            _refreshEntityOnActiveEntitiesListsAction(childEntity);
        }
    }
}