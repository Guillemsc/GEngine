using System;

namespace GEngine.Utils.DynamicVisitor
{
    public class ActionDynamicVisitor<TObj>
    {
        readonly ByAssignableTypeStore<Action<TObj>> _calls = new();

        public void Add<TConcrete>(Action<TConcrete> action)
            where TConcrete : TObj
        {
            void InverseAction(
                TObj x) =>
                action.Invoke((TConcrete)x);

            _calls[typeof(TConcrete)] = InverseAction;
        }

        public void Execute(TObj param)
        {
            if (!TryExecute(param))
            {
                throw new InvalidOperationException();
            }
        }

        public bool TryExecute(TObj obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (!_calls.TryGetAction(obj.GetType(), out var action))
            {
                return false;
            }

            action.Invoke(obj);
            return true;
        }
    }

    public class ActionDynamicVisitor<TObj, TArg1>
    {
        readonly ByAssignableTypeStore<Action<TObj, TArg1>> _calls = new ();

        public void Add<TConcrete>(Action<TConcrete, TArg1> action)
            where TConcrete : TObj
        {
            void InverseAction(
                TObj x,
                TArg1 arg1
                ) => action.Invoke((TConcrete)x, arg1);

            _calls[typeof(TConcrete)] = InverseAction;
        }

        public void Execute(TObj obj, TArg1 arg1)
        {
            if (!TryExecute(obj, arg1))
            {
                throw new InvalidOperationException();
            }
        }

        public bool TryExecute(
            TObj obj,
            TArg1 arg1
            )
        {
            if (obj is null)
            {
                return false;
            }

            if (!_calls.TryGetAction(obj.GetType(), out var action))
            {
                return false;
            }

            action.Invoke(obj, arg1);
            return true;
        }
    }
}
