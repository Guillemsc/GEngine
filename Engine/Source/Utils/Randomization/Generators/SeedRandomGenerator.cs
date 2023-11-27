using Random = System.Random;

namespace GEngine.Utils.Randomization.Generators
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IRandomGenerator"/> based on the default C# <see cref="Random"/>,
    /// which uses an initial seed.
    /// </summary>
    public sealed class SeedRandomGenerator : IRandomGenerator
    {
        readonly Random _random;

        public SeedRandomGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public int NewInt()
        {
            return _random.Next();
        }

        public int NewInt(int minInclusive, int maxExclusive)
        {
            return _random.Next(minInclusive, maxExclusive);
        }

        public float NewFloat()
        {
            float range = float.MaxValue - float.MinValue;

            float sample = (float)_random.NextDouble();

            return (sample * range) + float.MinValue;
        }

        public float NewFloat(float minInclusive, float maxInclusive)
        {
            float range = maxInclusive - minInclusive;

            float sample = (float)_random.NextDouble();

            return (sample * range) + minInclusive;
        }
    }
}
