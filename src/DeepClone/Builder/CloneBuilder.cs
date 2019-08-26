using DeepClone.Template;
using System;

namespace DeepClone.Builder
{

    public static class CloneBuilder<T>
    {
        public static Func<T, T> Create()
        {
            return (Func<T, T>)(new CloneBuilder()).Create(typeof(T));
        }
    }


    public class CloneBuilder : CloneTemplate
    {
        public CloneBuilder() : base()
        {
            Register<CloneArrayTemplate>();
        }

        public Delegate Create(Type type)
        {
            return TypeRouter(type);
        }

    }


}



