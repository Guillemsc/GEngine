﻿using GEngine.Modules.Entities.UseCases;
using GEngine.Modules.Framerate.UseCases;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Modules.Windows.UseCases;

namespace GEngine.Modules.Tickables.UseCases
{
    public sealed class TickUseCase
    {
        readonly TickFramerateUseCase _tickFramerateUseCase;
        readonly TickWindowsUseCase _tickWindowsUseCase;
        readonly TickEntitiesUseCase _tickEntitiesUseCase;
        readonly TickTickablesUseCase _tickTickablesUseCase;
        readonly TickPhysics2dUseCase _tickPhysics2dUseCase;
        readonly TickRenderingUseCase _tickRenderingUseCase;

        public TickUseCase(
            TickFramerateUseCase tickFramerateUseCase,
            TickWindowsUseCase tickWindowsUseCase,
            TickEntitiesUseCase tickEntitiesUseCase,
            TickTickablesUseCase tickTickablesUseCase,
            TickPhysics2dUseCase tickPhysics2dUseCase,
            TickRenderingUseCase tickRenderingUseCase
            )
        {
            _tickFramerateUseCase = tickFramerateUseCase;
            _tickWindowsUseCase = tickWindowsUseCase;
            _tickEntitiesUseCase = tickEntitiesUseCase;
            _tickTickablesUseCase = tickTickablesUseCase;
            _tickPhysics2dUseCase = tickPhysics2dUseCase;
            _tickRenderingUseCase = tickRenderingUseCase;
        }

        public void Execute()
        {
            _tickFramerateUseCase.Execute();
            _tickWindowsUseCase.Execute();
            _tickEntitiesUseCase.Execute();
            _tickTickablesUseCase.Execute();
            _tickPhysics2dUseCase.Execute();
            _tickRenderingUseCase.Execute();
        }
    }
}
