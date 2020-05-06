using DeepClone.Template;
using Natasha;
using System;
using System.Collections;

namespace DeepClone.Builder
{

    public static class CloneBuilder<T>
    {
        public static Func<T, T> Create(Type realType)
        {

            return (Func<T, T>)(new FullCloneBuilder()).Create<T>(realType);

        }
    }


    public class FullCloneBuilder : CloneTemplate
    {
        public FullCloneBuilder() : base()
        {
            Register<CloneArrayTemplate>();
            Register<CloneDictTemplate>();
            Register<CloneListTemplate>();
            Register<CloneClassTemplate>();
        }

        public Delegate Create<T>(Type type)
        {
            NBuildInfo info = type;
            info.FatherType = typeof(T);
            return TypeRouter(info);
        }

    }

}
