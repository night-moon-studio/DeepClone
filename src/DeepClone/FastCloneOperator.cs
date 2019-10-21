using DeepClone.Builder;
using Natasha;
using Natasha.Operator;
using System;

namespace DeepClone
{
    public static class FastCloneOperator
    {

        public static T Clone<T>(T instance)
        {
            if (instance == null)
            {
                return instance;
            }

            if (typeof(T) == typeof(object))
            {
                var func=FastObjectCloneBuilder.Create(instance.GetType());
                return (T)func(instance);
            }
            if (typeof(T).IsSimpleType()) 
            {
                return instance;
            }
            return FastCloneOperator<T>.Clone(instance);
        }
    }



    public static class FastCloneOperator<T>
    {
        public readonly static Func<T, T> Clone;
        static FastCloneOperator()
        {
            Clone = FastCloneBuilder<T>.Create();   //这里要对接CloneBuilder
        }

    }

}
