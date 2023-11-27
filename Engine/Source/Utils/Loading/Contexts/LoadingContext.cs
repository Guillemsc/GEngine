using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Delegates.Animation;
using GEngine.Utils.Sequencing.Instructions;
using GEngine.Utils.Sequencing.Sequencer;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Loading.Contexts
{
    /// <inheritdoc />
    public sealed class LoadingContext : ILoadingContext
    {
        readonly ISequencer _sequencer;

        readonly IReadOnlyList<TaskAnimationEvent> _beforeLoad;
        readonly IReadOnlyList<TaskAnimationEvent> _afterLoad;

        readonly Queue<IInstruction> _enqueuedInstructions = new();
        readonly Queue<IInstruction> _afterLoadEnqueuedInstructions = new();

        bool _runBeforeLoadActionsInstantly;
        bool _dontRunAfterLoadActions;

        public bool IsLoading { get; private set; }

        public LoadingContext(
            ISequencer sequencer,
            IReadOnlyList<TaskAnimationEvent> beforeLoad,
            IReadOnlyList<TaskAnimationEvent> afterLoad
            )
        {
            _sequencer = sequencer;
            _beforeLoad = beforeLoad;
            _afterLoad = afterLoad;
        }

        public ILoadingContext Enqueue(Func<CancellationToken, Task> function)
        {
            _enqueuedInstructions.Enqueue(new TaskInstruction( function ));
            return this;
        }

        public ILoadingContext Enqueue(Action action)
        {
            _enqueuedInstructions.Enqueue(new ActionInstruction(action));
            return this;
        }

        public ILoadingContext EnqueueAfterLoad(params Action[] actions)
        {
            foreach (Action action in actions)
            {
                _afterLoadEnqueuedInstructions.Enqueue(new ActionInstruction(action));
            }

            return this;
        }

        public ILoadingContext ShowInstantly()
        {
            return RunBeforeLoadActionsInstantly();
        }

        public ILoadingContext DoNotHide()
        {
            return DontRunAfterLoadActions();
        }

        public ILoadingContext RunBeforeLoadActionsInstantly()
        {
            _runBeforeLoadActionsInstantly = true;
            return this;
        }

        public ILoadingContext DontRunAfterLoadActions()
        {
            _dontRunAfterLoadActions = true;
            return this;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            await _sequencer.WaitCompletition();

            IsLoading = true;

            foreach (TaskAnimationEvent before in _beforeLoad)
            {
                _sequencer.Play(ct => before.Invoke(_runBeforeLoadActionsInstantly, ct));
            }

            while (_enqueuedInstructions.Count > 0)
            {
                _sequencer.Play(_enqueuedInstructions.Dequeue());
            }

            if (!_dontRunAfterLoadActions)
            {
                foreach (TaskAnimationEvent after in _afterLoad)
                {
                    _sequencer.Play(ct => after.Invoke(false, ct));
                }
            }

            while (_afterLoadEnqueuedInstructions.Count > 0)
            {
                _sequencer.Play(_afterLoadEnqueuedInstructions.Dequeue());
            }

            await _sequencer.WaitCompletition();

            IsLoading = false;
        }

        public void Execute()
        {
            ExecuteAsync();
        }

        public void ExecuteAsync()
        {
            Execute(CancellationToken.None).RunAsync();
        }
    }
}
