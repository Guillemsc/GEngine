using System;

namespace GEngine.Utils.Enums.Utils
{
    /// <summary>
    /// Base class used for retreiving basic information from an Enum.
    /// To use it, just inherit from this class with the wanted Enum type. Then you
    /// just need to call the static methods from the inherited class.
    /// Values are generated once on the first method call, and then they get cached.
    /// </summary>
    public abstract class EnumInfo<T> where T : Enum
    {
        static readonly Random _defaultRandom = new();
        static int _length = -1;
        static T[] _values;

        [Obsolete("This method is obsolete. Use Values.Length instead", true)]
        public static int Length => _length == -1 ? _length = Values.Length : _length;
        public static T[] Values => _values ??= (T[])Enum.GetValues(typeof(T));

        [Obsolete("This method is obsolete. Use extension methods of an enum.", true)]
        public static T Random(Random random) => Values[random.Next(0, Length)];

        [Obsolete("This method is obsolete. Use extension methods of an enum.", true)]
        public static T Random() => Random(_defaultRandom);
    }
}
