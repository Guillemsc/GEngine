using GEngine.Utils.Enums.Utils;

namespace GEngine.Utils.Directions
{
    /// <summary>
    /// The four cardinal directions, or cardinal point.
    /// </summary>
    public enum CardinalDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public sealed class CardinalDirectionInfo : EnumInfo<CardinalDirection> { }
}
