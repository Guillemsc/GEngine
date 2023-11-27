using GEngine.Modules.Tickables.Data;

namespace GEngine.Modules.Tickables.UseCases
{
    public sealed class TickTickablesUseCase
    {
        readonly TickablesData _tickablesData;

        public TickTickablesUseCase(
            TickablesData tickablesData
            )
        {
            _tickablesData = tickablesData;
        }

        public void Execute()
        {
            _tickablesData.PreUpdateTickables.Tick();
            _tickablesData.UpdateTickables.Tick();
            _tickablesData.LateUpdateTickables.Tick();
        }
    }
}
