using GEngine.Utils.Locations.Enums;

namespace GEngine.Utils.Locations.Extensions
{
    public static class VerticalLocationExtensions
    {
        public static VerticalLocation Opposite(this VerticalLocation verticalLocation)
        {
            return verticalLocation == VerticalLocation.Bottom ? VerticalLocation.Top : VerticalLocation.Bottom;
        }
    }
}
