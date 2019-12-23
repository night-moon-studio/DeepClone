using System;

namespace DeepClone
{

    public class NeedCtorAttribute:Attribute
    {
        public string Name;
        public NeedCtorAttribute()
        {

        }
        public NeedCtorAttribute(string name)
        {
            Name = name;
        }
    }

}
