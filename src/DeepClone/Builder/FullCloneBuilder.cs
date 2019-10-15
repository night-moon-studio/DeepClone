using DeepClone.Template;
using Natasha;
using System;

namespace DeepClone.Builder
{

    public static class FullCloneBuilder<T>
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
            Register<FullCloneArrayTemplate>();
            Register<FullCloneDictTemplate>();
            Register<FullCloneListTemplate>();
            Register<FullCloneClassTemplate>();
        }

        public Delegate Create<T>(Type type)
        {
            NBuildInfo info = type;
            info.FatherType = typeof(T);
            return TypeRouter(info);
        }

    }

}
