using System;
using System.Collections.Generic;
using System.Diagnostics;
using GEngine.Utils.Profiling.Results;
using GEngine.Utils.Profiling.Steps;

namespace GEngine.Utils.Profiling.Builder
{
    public sealed class ProfilingBuilder : IProfilingBuilder
    {
        readonly Stopwatch _stopwatch = new();
        readonly List<ProfilingStep> _steps = new();

        readonly string _name;
        TimeSpan _totalDuration;

        string _currentStepName;

        public ProfilingBuilder(string name)
        {
            _name = name;
        }

        public void Next(string name)
        {
            Complete();

            _currentStepName = name;

            _stopwatch.Restart();
        }

        public void Complete()
        {
            if (!_stopwatch.IsRunning)
            {
                return;
            }

            _stopwatch.Stop();

            _totalDuration = _totalDuration.Add(_stopwatch.Elapsed);

            _steps.Add(new ProfilingStep(_currentStepName, _stopwatch.Elapsed));
        }

        public ProfilingResult Build()
        {
            return new ProfilingResult(_name, _totalDuration, _steps);
        }
    }
}
