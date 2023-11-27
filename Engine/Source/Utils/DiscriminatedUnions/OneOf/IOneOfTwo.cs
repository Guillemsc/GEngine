namespace GEngine.Utils.DiscriminatedUnions
{
    public interface IOneOf<TFirst, TSecond>
    {
        bool TryGetFirst(out TFirst value);
        bool TryGetSecond(out TSecond value);
        TFirst UnsafeGetFirst();
        TSecond UnsafeGetSecond();
    }
}
