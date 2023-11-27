using System;
using System.Collections.Generic;
using System.Text;
using GEngine.Utils.Profiling.Steps;

namespace GEngine.Utils.Profiling.Results
{
    public sealed class ProfilingResult
    {
        public static readonly ProfilingResult Empty = new();

        public string Name { get; }
        public TimeSpan TotalDuration { get; }
        public IReadOnlyList<ProfilingStep> Steps { get; }

        public ProfilingResult(string name, TimeSpan totalDuration, IReadOnlyList<ProfilingStep> steps)
        {
            Name = name;
            TotalDuration = totalDuration;
            Steps = steps;
        }

        ProfilingResult()
        {
            Name = string.Empty;
            TotalDuration = TimeSpan.Zero;
            Steps = Array.Empty<ProfilingStep>();
        }

        public override string ToString()
        {
            if (Steps.Count == 0)
            {
                return String.Empty;
            }

            StringBuilder stringBuilder = new();

            stringBuilder.Append($"{Name} took {TotalDuration.TotalMilliseconds:0.0#}ms ");

            foreach (ProfilingStep step in Steps)
            {
                stringBuilder.Append($"({step.Name}: {step.Duration.TotalMilliseconds:0.0#}ms) ");
            }

            return stringBuilder.ToString();
        }
    }
}
