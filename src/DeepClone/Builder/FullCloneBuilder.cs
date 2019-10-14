using DeepClone.Template;
using System;

namespace DeepClone.Builder
{

    public static class FullCloneBuilder<T>
    {
        public static Func<T, T> Create(Type realType)
        {
            return (Func<T, T>)(new FullCloneBuilder()).Create(realType);
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

        public Delegate Create(Type type)
        {
            return TypeRouter(type);
        }

    }

}
