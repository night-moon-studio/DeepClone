using DeepClone.Builder;
using Natasha;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DeepClone
{
    public static class FastObjectCloneOperator
    {

        public readonly static ConcurrentDictionary<Type, Func<object,object>> MethodCache;
        static FastObjectCloneOperator()
        {
            MethodCache = new ConcurrentDictionary<Type, Func<object,object>>();
        }

        public static object Clone(object instance)
        {

            if (instance==null)
            {

                return null;

            }
            else
            {

                var type = instance.GetType();
                if (type.IsSimpleType())
                {

                    return instance;

                }
                else if (type==typeof(object))
                {

                    return new object();

                }
                else if (MethodCache.ContainsKey(type))
                {

                    return MethodCache[type](instance);

                }
                else
                {

                    var func = FastObjectCloneBuilder.Create(type);
                    MethodCache[type] = func;
                    return func(instance);

                }

            }

        }

    }

}
