using GEngine.Utils.Profiling.Results;

namespace GEngine.Utils.Profiling.Builder
{
    public interface IProfilingBuilder
    {
        void Next(string name);
        void Complete();

        ProfilingResult Build();
    }
}
