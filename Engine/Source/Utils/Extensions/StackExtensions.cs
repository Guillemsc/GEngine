namespace GEngine.Utils.Extensions;

public static class StackExtensions
{
    public static void PushRange<T>(this Stack<T> stack, IReadOnlyList<T> range)
    {
        foreach (T item in range)
        {
            stack.Push(item);   
        }
    }
}