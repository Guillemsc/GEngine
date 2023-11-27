using System;

namespace GEngine.Utils.Sequencing.Instructions
{
    public sealed class ActionInstruction : InstantInstruction
    {
        readonly Action _action;

        public ActionInstruction(Action action)
        {
            _action = action;
        }

        protected override void OnInstantExecute()
        {
            _action?.Invoke();
        }
    }
}
