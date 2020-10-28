using DeepClone.Builder;
using DynamicCache;
using Natasha;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DeepClone
{
    public static class ObjectCloneOperator
    {

        public static HashCache<Type, Func<object, object>> CloneMapping;
        public readonly static ConcurrentDictionary<Type, Func<object,object>> _methodCache;
        static ObjectCloneOperator()
        {
            _methodCache = new ConcurrentDictionary<Type, Func<object,object>>();
            CloneMapping = _methodCache.HashTree();
        }

        public unsafe static object Clone(object instance)
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
                else
                {

                    var func = CloneMapping.GetValue(type);
                    if (func==default)
                    {

                        func = ObjectCloneBuilder.Create(type);
                        _methodCache[type] = func;
                        int[] code = new int[_methodCache.Count];
                        int i = 0;
                        foreach (var item in _methodCache)
                        {
                            code[i] = item.Value.GetHashCode();
                            i++;
                        }
                        CloneMapping = _methodCache.HashTree(DyanamicCacheDirection.KeyToValue);

                    }
                    return func(instance);

                }


            }

        }

    }

}
