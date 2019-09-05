using System.Collections.Generic;
using System.Linq;

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
}