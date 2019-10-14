using DeepClone.Template;
using System;

namespace DeepClone.Builder
{

    public static class FastCloneBuilder<T>
    {
        public static Func<T, T> Create()
        {
            return (Func<T, T>)(new FastCloneBuilder()).Create(typeof(T));
        }
    }


    public class FastCloneBuilder : CloneTemplate
    {
        public FastCloneBuilder() : base()
        {
            Register<FastCloneArrayTemplate>();
            Register<FastCloneDictTemplate>();
            Register<FastCloneListTemplate>();
            Register<FastCloneClassTemplate>();
        }

        public Delegate Create(Type type)
        {
            return TypeRouter(type);
        }

    }


}



