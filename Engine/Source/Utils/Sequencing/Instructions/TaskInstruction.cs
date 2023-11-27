using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Sequencing.Instructions
{
    public sealed class TaskInstruction : Instruction
    {
        readonly Func<CancellationToken, Task> _function;

        public TaskInstruction(Func<CancellationToken, Task> function)
        {
            _function = function;
        }

        protected override Task OnExecute(CancellationToken cancellationToken)
        {
            if(_function == null)
            {
                return Task.CompletedTask;
            }

            return _function.Invoke(cancellationToken);
        }
    }
}
