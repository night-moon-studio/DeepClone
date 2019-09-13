using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeepCloneUT.EqualityComparer
{
    public class DictArrayEqualityComparer : IEqualityComparer<Dictionary<int, int>>
    {
        public bool Equals(Dictionary<int, int> x, Dictionary<int, int> y)
        {
            if (x == null && y == null)
                return true;
            return x.Keys.SequenceEqual(y.Keys) && x.Values.SequenceEqual(y.Values);
        }

        public int GetHashCode(Dictionary<int, int> obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Dict1JaggedArrayEqualityComparer : IEqualityComparer<Dictionary<int, int>[]>
    {
        public bool Equals(Dictionary<int, int>[] x, Dictionary<int, int>[] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new DictArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(Dictionary<int, int>[] obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Dict2JaggedArrayEqualityComparer : IEqualityComparer<Dictionary<int, int>[][]>
    {
        public bool Equals(Dictionary<int, int>[][] x, Dictionary<int, int>[][] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new Dict1JaggedArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(Dictionary<int, int>[][] obj)
        {
            throw new System.NotImplementedException();
        }
    }
}