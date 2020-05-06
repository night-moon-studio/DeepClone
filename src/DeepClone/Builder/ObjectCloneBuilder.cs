using Natasha;
using Natasha.CSharp;
using System;

namespace DeepClone.Builder
{

    public static class ObjectCloneBuilder
    {

        public static Func<object, object> Create(Type type)
        {
            return NDelegate.UseDomain(type.GetDomain()).Func<object, object>($"return CloneOperator.Clone(({type.GetDevelopName()})arg);", type, "DeepClone");
        }

    }

}
