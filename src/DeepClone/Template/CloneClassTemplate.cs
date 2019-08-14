using DeepClone.Model;
using System;

namespace DeepClone.Template
{
    public class CloneClassTemplate : ICloneTemplate
    {

        internal readonly static int HashCode;
        static CloneClassTemplate() => HashCode = typeof(CloneClassTemplate).GetHashCode();




        public override int GetHashCode() => HashCode;




        public bool MatchType(Type type)
        {
            return type.IsClass && type != typeof(object) && type != typeof(string) && !type.IsArray;
        }




        public Delegate TypeRouter(Type type)
        {
            return default;
        }

    }

}
