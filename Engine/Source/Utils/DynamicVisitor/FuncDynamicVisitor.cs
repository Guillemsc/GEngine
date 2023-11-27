using System;

namespace GEngine.Utils.DynamicVisitor
{
    public class FuncDynamicVisitor<TObj, TResult>
    {
        readonly ByAssignableTypeStore<Func<TObj, TResult>> _calls = new ();

        public void Add<TConcrete>(Func<TConcrete, TResult> func)
            where TConcrete : TObj
        {
            TResult InverseFunc(
                TObj x) =>
                func.Invoke((TConcrete)x);

            _calls[typeof(TConcrete)] = InverseFunc;
        }

        public TResult Execute(TObj param)
        {
            if (!TryExecute(param, out var result))
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public bool TryExecute(
            TObj obj,
            out TResult result
            )
        {
            if (obj is null)
            {
                result = default;
                return false;
            }

            if (!_calls.TryGetAction(obj.GetType(), out var func))
            {
                result = default;
                return false;
            }

            result = func.Invoke(obj);
            return true;
        }
    }

    public class FuncDynamicVisitor<TObj, TArg1, TResult>
    {
        readonly ByAssignableTypeStore<Func<TObj, TArg1, TResult>> _calls = new ();

        public void Add<TConcrete>(Func<TConcrete, TArg1, TResult> func)
            where TConcrete : TObj
        {
            TResult InverseFunc(
                TObj x,
                TArg1 arg1
                ) => func.Invoke((TConcrete)x, arg1);

            _calls[typeof(TConcrete)] = InverseFunc;
        }

        public TResult Execute(TObj param, TArg1 arg1)
        {
            if (!TryExecute(param, arg1, out var result))
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public bool TryExecute(
            TObj obj,
            TArg1 arg1,
            out TResult result)
        {
            if (obj is null)
            {
                result = default;
                return false;
            }

            if (!_calls.TryGetAction(obj.GetType(), out var func))
            {
                result = default;
                return false;
            }

            result = func.Invoke(obj, arg1);
            return true;
        }
    }
}
