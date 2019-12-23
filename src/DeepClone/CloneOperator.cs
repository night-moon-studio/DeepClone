using DeepClone.Builder;
using System;
using System.Collections.Concurrent;

namespace DeepClone
{
    public static class CloneOperator
    {

        public static T Clone<T>(T instance) where T: class
        {

            return CloneOperator<T>.Clone(instance);

        }

    }



    public static class CloneOperator<T> where T: class
    {

        public static HashCache<Type, Func<T, T>> CloneMapping;
        public readonly static ConcurrentDictionary<Type, Func<T, T>> _mapping_cache;
        static CloneOperator() 
        {
           
            _mapping_cache = new ConcurrentDictionary<Type, Func<T, T>>();
            CloneMapping = _mapping_cache.HashTree();

        }




        public static T Clone(T instance)
        {

            if (instance == default)
            {
                return default;
            }


            Type type = instance.GetType();
            var func = CloneMapping.GetValue(type);
            if (func==default)
            {

                func = CloneBuilder<T>.Create(type);
                _mapping_cache[type] = func;
                CloneMapping = _mapping_cache.HashTree();

            }

            return func(instance);

        }

    }

}
