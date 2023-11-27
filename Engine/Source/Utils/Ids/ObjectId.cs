using System;

namespace GEngine.Utils.Ids
{
    // TODO: Obsolete with new code generation?
    public abstract class ObjectId<T> : IEquatable<ObjectId<T>>
    {
        readonly Guid _guid;

        protected ObjectId(Guid guid)
        {
            _guid = guid;
        }

        public Guid ToGuid() => _guid;

        public bool Equals(ObjectId<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _guid.Equals(other._guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ObjectId<T>)obj);
        }

        public override int GetHashCode() => _guid.GetHashCode();
    }
}
