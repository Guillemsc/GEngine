using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Time.Timers;

namespace GEngine.Utils.Extensions
{
    public static class TaskAlgorithmExtensions
    {
        /// <summary>
        /// Runs a list of asynchronous functions with a delay between each execution.
        /// </summary>
        /// <param name="taskFuncs">A list of asynchronous functions to execute.</param>
        /// <param name="timer">The timer instance to use for delays between executions.</param>
        /// <param name="delay">The delay between each execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that awaits until all operations are finished.</returns>
        public static async Task RunWithDelayBetweenExecutions(
            IEnumerable<Func<CancellationToken, Task>> taskFuncs,
            ITimer timer,
            TimeSpan delay,
            CancellationToken cancellationToken
            )
        {
            if (delay <= TimeSpan.Zero)
            {
                await Task.WhenAll(taskFuncs.Select(x => x.Invoke(cancellationToken)));
                return;
            }

            timer.Start();

            var tasks = new List<Task>();
            foreach (var taskFunc in taskFuncs)
            {
                if (cancellationToken.IsCancellationRequested) return;

                tasks.Add(taskFunc.Invoke(cancellationToken));

                await timer.AwaitSpan(delay, cancellationToken);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Runs a list of asynchronous functions with a delay between each execution.
        /// </summary>
        /// <param name="taskFuncs">A list of asynchronous functions to execute.</param>
        /// <param name="timer">The timer instance to use for delays between executions.</param>
        /// <param name="delay">The delay between each execution.</param>
        /// <param name="maxTotalDelay">The maximum total delay.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that awaits until all operations are finished.</returns>
        public static async Task RunWithDelayBetweenExecutions(
            IEnumerable<Func<CancellationToken, Task>> taskFuncs,
            ITimer timer,
            TimeSpan delay,
            TimeSpan maxTotalDelay,
            CancellationToken cancellationToken
        )
        {
            if (delay <= TimeSpan.Zero)
            {
                await Task.WhenAll(taskFuncs.Select(x => x.Invoke(cancellationToken)));
                return;
            }

            timer.Start();

            TimeSpan totalDelay = TimeSpan.Zero;

            var tasks = new List<Task>();
            foreach (var taskFunc in taskFuncs)
            {
                if (cancellationToken.IsCancellationRequested) return;

                bool overMaxDelay = totalDelay >= maxTotalDelay;

                tasks.Add(taskFunc.Invoke(cancellationToken));

                if (!overMaxDelay)
                {
                    await timer.AwaitSpan(delay, cancellationToken);
                }

                totalDelay += delay;
            }

            await Task.WhenAll(tasks);
        }
    }
}
