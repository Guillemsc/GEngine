namespace GEngine.Utils.Persistence.Migrations
{
    public sealed class NopMigration<T> : IMigration<T> where T : class
    {
        public static readonly NopMigration<T> Instance = new();

        NopMigration() { }

        public void Migrate(T data) { }
    }
}
