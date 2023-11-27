using System;
using GEngine.Utils.Di.Builder;
using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Services;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Utils.Tick.Extensions
{
    public static class DiTickablesExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            TickType tickType = TickType.Update
            )
            where T : ITickable
        {
            actionBuilder.WhenInit((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(o, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.RemoveNow(o, tickType);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<T, Action> func,
            TickType tickType = TickType.Update
            )
        {
            CallbackTickable callbackTickable = null;

            actionBuilder.WhenInit((c, o) =>
            {
                Action action = func.Invoke(o);

                callbackTickable = new CallbackTickable(action);

                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(callbackTickable, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.RemoveNow(callbackTickable, tickType);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
