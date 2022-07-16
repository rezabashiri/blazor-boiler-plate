using System.Diagnostics.CodeAnalysis;

namespace Core.Comparers
{
    internal class AdHocEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equals;
        private readonly Func<T, int> _getHashCode;

        private AdHocEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            _equals = equals;
            _getHashCode = getHashCode;
        }

        public static IEqualityComparer<T> CreateWithoutHashCode([NotNull] Func<T, T, bool> equals)
        {
            if (equals == null) throw new ArgumentNullException(nameof(@equals));

            return Create(equals, _ => 0);
        }

        public static IEqualityComparer<T> Create([NotNull] Func<T, T, bool> equals, [NotNull] Func<T, int> getHashCode)
        {
            if (equals == null) throw new ArgumentNullException(nameof(equals));
            if (getHashCode == null) throw new ArgumentNullException(nameof(getHashCode));

            return new AdHocEqualityComparer<T>(equals, getHashCode);
        }

        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(null, x)) return false;
            if (ReferenceEquals(null, y)) return false;
            if (ReferenceEquals(x, y)) return true;
            return _equals(x, y);
        }

        public int GetHashCode(T obj) => _getHashCode(obj);
    }
}