using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeepCloneUT.EqualityComparer
{
    public class ListArrayEqualityComparer : IEqualityComparer<List<int>>
    {
        public bool Equals(List<int> x, List<int> y)
        {
            if (x == null && y == null)
                return true;
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<int> obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class List1JaggedArrayEqualityComparer : IEqualityComparer<List<int>[]>
    {
        public bool Equals(List<int>[] x, List<int>[] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new ListArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(List<int>[] obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class List2JaggedArrayEqualityComparer : IEqualityComparer<List<int>[][]>
    {
        public bool Equals(List<int>[][] x, List<int>[][] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new List1JaggedArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(List<int>[][] obj)
        {
            throw new System.NotImplementedException();
        }
    }
}