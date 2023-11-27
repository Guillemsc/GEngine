using System.Threading.Tasks;

namespace GEngine.Utils.TimeSlicing.Awaiting
{
    public sealed class NopTimeSlicingAwaiter : ITimeSlicingAwaiter
    {
        public static readonly NopTimeSlicingAwaiter Instance = new();

        static readonly ValueTask CompletedTask = new(Task.CompletedTask);

        NopTimeSlicingAwaiter()
        {

        }

        public void Start() { }
        public ValueTask TryTimeSlice() => CompletedTask;
    }
}
