using GEngine.Utils.Profiling.Results;

namespace GEngine.Utils.Profiling.Builder
{
    public sealed class NopProfilingBuilder : IProfilingBuilder
    {
        public static readonly NopProfilingBuilder Instance = new();

        NopProfilingBuilder()
        {

        }

        public void Next(string name)
        {

        }

        public void Complete()
        {

        }

        public ProfilingResult Build()
        {
            return ProfilingResult.Empty;
        }
    }
}
