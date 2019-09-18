using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Model
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
