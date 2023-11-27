namespace GEngine.Utils.Cloning.Cloneables
{
    /// <summary>
    /// Same as <see cref="System.ICloneable"/> but with an enforced type.
    /// </summary>
    public interface ICloneable<T>
    {
        T Clone();
    }
}
