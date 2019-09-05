using System.Collections.Generic;

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

}