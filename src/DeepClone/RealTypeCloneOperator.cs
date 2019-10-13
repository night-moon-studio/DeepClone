using System;
using System.Collections.Generic;
using System.Reflection;
using Natasha;
using Natasha.Operator;

namespace DeepClone
{
    public class RealTypeCloneOperator
    {
        public static T Clone<T>(T instance)
        {
            return RealTypeCloneOperator<T>.Clone(instance);
        }
        public static bool IsRealType<T>(T instance)
        {
            return RealTypeCloneOperator<T>.IsRealType(instance);
        }
    }
    public class RealTypeCloneOperator<T>
    {
        private static readonly Type ParentType = typeof(T);
        private static readonly MethodInfo CloneOperatorCloneMethodInfo = typeof(CloneOperator).GetMethod("Clone");
        private static readonly Func<Type, object, object> CloneOperatorClone = (type, i) => CloneOperatorCloneMethodInfo.MakeGenericMethod(type).Invoke(null, new[] { i });
        private static readonly Dictionary<Type, Func<T, T>> RealTypeCloneCloneCache;
        static RealTypeCloneOperator() => RealTypeCloneCloneCache = new Dictionary<Type, Func<T, T>>();

        internal static T Clone(T instance)
        {
            var instanceType = instance.GetType();
            if (RealTypeCloneCloneCache.TryGetValue(instanceType, out var func)) return func(instance);
            return (RealTypeCloneCloneCache[instanceType] = i => (T)CloneOperatorClone(instanceType, i))(instance);
        }

        internal static bool IsRealType(T instance)
        {
            return ParentType == instance.GetType();
        }
    }
}