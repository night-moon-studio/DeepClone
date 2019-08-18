using DeepClone.Template;
using System;

namespace DeepClone.Builder
{

    public class CloneBuidler<T> : CloneBuilder
    {
        public Func<T,T> Create()
        {
            return (Func<T, T>)(new CloneBuilder()).Create(typeof(T));
        }
    }


    public class CloneBuilder: CloneTemplate
    {

        public Delegate Create(Type type)
        {
            return TypeRouter(type);
        }

    }


}



