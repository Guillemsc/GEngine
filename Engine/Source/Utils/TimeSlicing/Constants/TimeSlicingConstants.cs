using System;

namespace GEngine.Utils.TimeSlicing.Awaiting
{
    public static class TimeSlicingConstants
    {
        // We assume rendering takes half our budget
        public static readonly TimeSpan TargetMsFor10Fps = TimeSpan.FromMilliseconds(50);
        public static readonly TimeSpan TargetMsFor15Fps = TimeSpan.FromMilliseconds(33);
        public static readonly TimeSpan TargetMsFor30Fps = TimeSpan.FromMilliseconds(16);
        public static readonly TimeSpan TargetMsFor60Fps = TimeSpan.FromMilliseconds(8);
        public static readonly TimeSpan TargetMsFor120Fps = TimeSpan.FromMilliseconds(4);
    }
}
