using Natasha;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepClone.Builder
{

    public static class FullObjectCloneBuilder
    {

        public static Func<object, object> Create(Type type)
        {
            return NFunc<object, object>.Delegate($"return CloneOperator.Clone(({type.GetDevelopName()})arg);", type, "DeepClone");
        }

    }

}
