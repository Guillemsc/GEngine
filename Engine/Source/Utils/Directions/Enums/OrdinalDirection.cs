using GEngine.Utils.Enums.Utils;

namespace GEngine.Utils.Directions
{
    /// <summary>
    /// The four cardinal directions, plus four transitions between them.
    /// </summary>
    public enum OrdinalDirection
    {
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7
    }

    public sealed class OrdinalDirectionInfo : EnumInfo<CardinalDirection> { }
}
