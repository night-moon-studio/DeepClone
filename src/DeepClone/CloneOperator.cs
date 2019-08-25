using DeepClone.Builder;
using System;

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
        public readonly static Func<T, T> Clone;
        static CloneOperator()
        {
            Clone = CloneBuilder<T>.Create();   //这里要对接CloneBuilder
        }

    }

}
