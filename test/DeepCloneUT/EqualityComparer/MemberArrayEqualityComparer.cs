using System.Collections.Generic;
using Xunit;

namespace DeepCloneUT.EqualityComparer
{
    public class MemberArrayEqualityComparer : IEqualityComparer<Member>
    {
        public bool Equals(Member x, Member y)
        {
            if (x == null && y == null)
                return true;
            return x.Equals(y);
        }

        public int GetHashCode(Member obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Member1JaggedArrayEqualityComparer : IEqualityComparer<Member[]>
    {
        public bool Equals(Member[] x, Member[] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new MemberArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(Member[] obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Member2JaggedArrayEqualityComparer : IEqualityComparer<Member[][]>
    {
        public bool Equals(Member[][] x, Member[][] y)
        {
            if (x == null && y == null)
                return true;

            if (x?.Length != y?.Length)
                return false;

            Assert.Equal(x, y, new Member1JaggedArrayEqualityComparer());
            return true;
        }

        public int GetHashCode(Member[][] obj)
        {
            throw new System.NotImplementedException();
        }
    }
}