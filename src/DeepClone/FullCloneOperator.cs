using DeepClone.Builder;
using System;
using System.Collections.Concurrent;

namespace DeepClone
{
    public static class CloneOperator
    {

        public static T Clone<T>(T instance)
        {

            return CloneOperator<T>.Clone(instance);

        }

    }



    public static class CloneOperator<T>
    {

        public readonly static ConcurrentDictionary<Type, Func<T, T>> CloneMapping;
        static CloneOperator() => CloneMapping = new ConcurrentDictionary<Type, Func<T, T>>();




        public static T Clone(T instance)
        {

            if (instance == default)
            {
                return default;
            }


            Type type = instance.GetType();
            if (!CloneMapping.ContainsKey(type))
            {
                CloneMapping[type] = FullCloneBuilder<T>.Create(type);
            }


            return CloneMapping[type](instance);

        }

    }

}
