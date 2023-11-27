using System;
using System.Collections.Generic;
using GEngine.Utils.Enums.Utils;

namespace GEngine.Utils.ActiveSource
{
    public sealed class IdActiveSource<T> : IIdActiveSource<T>
    {
        public event Action<T, bool> OnActiveChanged;

        readonly HashSet<T> _trackedIds = new();
        readonly Dictionary<T, bool> _trackedValues = new();
        readonly HashSet<object> _blockAll = new();
        readonly Dictionary<object, HashSet<(T id, bool blocked)>> _concreteChanges = new();

        public IdActiveSource()
        {

        }

        public IdActiveSource(IEnumerable<T> ids)
        {
            foreach (var id in ids)
            {
                Track(id);
            }
        }

        public bool IsActive(T id)
        {
            if (_blockAll.Count == 0)
            {
                foreach (var concreteChange in _concreteChanges)
                {
                    if (concreteChange.Value.Contains((id, true)))
                    {
                        return false;
                    }
                }

                return true;
            }

            foreach (var blockingObject in _blockAll)
            {
                if (!_concreteChanges.TryGetValue(blockingObject, out var changes))
                {
                    return false;
                }

                if (!changes.Contains((id, false)))
                {
                    return false;
                }
            }

            return true;
        }

        HashSet<(T id, bool blocked)> GetConcreteChanges(object owner)
        {
            if(!_concreteChanges.TryGetValue(owner, out var ids))
            {
                ids = new HashSet<(T, bool)>();
                _concreteChanges.Add(owner, ids);
            }

            return ids;
        }

        public void SetActive(object owner, T id, bool active)
        {
            if (active)
            {
                Unblock(owner, id);
            }
            else
            {
                Block(owner, id);
            }

            RaiseIfChanged(id);
        }

        public void Track(T id)
        {
            _trackedValues.Add(id, true);
            _trackedIds.Add(id);
        }

        public void Untrack(T id)
        {
            _trackedValues.Remove(id);
            _trackedIds.Remove(id);

            //TODO: we could remove _concreteChanges that reference the id here
        }

        void Unblock(object owner, T id)
        {
            var changes = GetConcreteChanges(owner);

            changes.Remove((id, true));

            if (!_blockAll.Contains(owner))
            {
                if (changes.Count == 0)
                {
                    _concreteChanges.Remove(owner);
                }

                return;
            }

            changes.Add((id, false));
        }

        void Block(object owner, T id)
        {
            if (_blockAll.Contains(owner))
            {
                return;
            }

            var changes = GetConcreteChanges(owner);
            changes.Remove((id, false));
            changes.Add((id, true));
        }

        public void SetActiveAll(
            object owner,
            bool active
            )
        {
            if (active)
            {
                ActivateAll(owner);
            }
            else
            {
                DeactivateAll(owner);
            }
        }

        void DeactivateAll(object owner)
        {
            _concreteChanges.Remove(owner);
            _blockAll.Add(owner);

            foreach (var trackedId in _trackedIds)
            {
                RaiseIfChanged(trackedId);
            }
        }

        void ActivateAll(object owner)
        {
            _concreteChanges.Remove(owner);
            _blockAll.Remove(owner);

            foreach (var trackedId in _trackedIds)
            {
                RaiseIfChanged(trackedId);
            }
        }

        void RaiseIfChanged(T id)
        {
            bool previous = _trackedValues[id];
            bool next = IsActive(id);

            if (previous == next)
            {
                return;
            }

            _trackedValues[id] = next;
            OnActiveChanged?.Invoke(id, next);
        }
    }
}
